using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;


namespace OdaWepApi.Models;

public partial class Apartment
{
    [Key]
    public int Apartmentid { get; set; }

    [Required(ErrorMessage = "Apartment Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Apartment Name must be between 2 and 100 characters.")]
    public string? Apartmentname { get; set; }

    [Required(ErrorMessage = "Apartment Type is required.")]
    [EnumDataType(typeof(Enum.ApartmentType), ErrorMessage = "Invalid Apartment Type.")]
    public string? Apartmenttype { get; set; }

    [Required(ErrorMessage = "Apartment status is required.")]
    [EnumDataType(typeof(Enum.ApartmentStatus), ErrorMessage = "Invalid Apartment Status.")]
    public string? Apartmentstatus { get; set; }

    [Required(ErrorMessage = "Apartment space is required.")]
    [Range(10, 1000, ErrorMessage = "Space must be between 10 and 1,000 square meters.")]
    public decimal? Apartmentspace { get; set; }

    public string? Description { get; set; }

    public List<byte[]>? Apartmentphotos { get; set; }

    public int? Projectid { get; set; }

    public int? Floornumber { get; set; }

    public DateTime? Availabilitydate { get; set; }

    public DateTime? Createddatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Apartmentaddon> Apartmentaddons { get; set; } = new List<Apartmentaddon>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual Project? Project { get; set; }

    // Virtual method to calculate total price
    public virtual decimal CalculateApartmentTotalPrice()
    {
        // Sum of assigned package price
        var totalPackagePrice = Packages?.Where(p => p.Assignedpackage == true)
                                         .Sum(p => p.Price) ?? 0;

        // Sum of installed addons price
        var totalAddonPrice = Apartmentaddons?
            .Where(aa => aa.Assignedaddons == true)
            .Sum(aa => (aa.Installedamount ?? 0) * (aa.Addon?.Priceperunit ?? 0)) ?? 0;

        // Total Price
        return totalPackagePrice + totalAddonPrice;
    }
}


public static class ApartmentEndpoints
{
    public static void MapApartmentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Apartment").WithTags(nameof(Apartment));

        // Get all Apartments
        group.MapGet("/", async (OdaDbContext db) =>
        {

            return await db.Apartments.ToListAsync();
        })
        .WithName("GetAllApartments")
        .WithOpenApi();

        // Get Apartment by Id
        group.MapGet("/{id}", async Task<Results<Ok<Apartment>, NotFound>> (int apartmentid, OdaDbContext db) =>
        {
            return await db.Apartments.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Apartmentid == apartmentid)
                is Apartment model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetApartmentById")
        .WithOpenApi();

        // Update Apartment
        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int apartmentid, Apartment apartment, OdaDbContext db) =>
        {
            // Set the Lastmodifieddatetime to the current time for updates
            apartment.Lastmodifieddatetime = DateTime.UtcNow;

            var affected = await db.Apartments
                .Where(model => model.Apartmentid == apartmentid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Apartmentid, apartment.Apartmentid)
                  .SetProperty(m => m.Apartmentname, apartment.Apartmentname)
                  .SetProperty(m => m.Apartmenttype, apartment.Apartmenttype)
                  .SetProperty(m => m.Apartmentstatus, apartment.Apartmentstatus)
                  .SetProperty(m => m.Apartmentspace, apartment.Apartmentspace)
                  .SetProperty(m => m.Description, apartment.Description)
                  .SetProperty(m => m.Apartmentphotos, apartment.Apartmentphotos)
                  .SetProperty(m => m.Projectid, apartment.Projectid)
                  .SetProperty(m => m.Floornumber, apartment.Floornumber)
                  .SetProperty(m => m.Availabilitydate, apartment.Availabilitydate)
                //  .SetProperty(m => m.Createddatetime, apartment.Createddatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, apartment.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateApartment")
        .WithOpenApi();

        // Create a new Apartment
        group.MapPost("/", async (Apartment apartment, OdaDbContext db) =>
        {
            // Set ApartmentId to MaxApartmentId + 1
            var maxApartmentId = await db.Apartments.MaxAsync(a => (int?)a.Apartmentid) ?? 0;
            apartment.Apartmentid = maxApartmentId + 1;

            // Set the Createddatetime for the first insertion
            apartment.Createddatetime = DateTime.UtcNow;
            apartment.Lastmodifieddatetime = apartment.Createddatetime;


            db.Apartments.Add(apartment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Apartment/{apartment.Apartmentid}", apartment);
        })
        .WithName("CreateApartment")
        .WithOpenApi();

        // Delete Apartment
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int apartmentid, OdaDbContext db) =>
        {
            var affected = await db.Apartments
                .Where(model => model.Apartmentid == apartmentid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteApartment")
        .WithOpenApi();

        // Endpoint to calculate Apartment total price
        group.MapGet("/{id}/totalPrice", async Task<IResult> (int apartmentid, OdaDbContext db) =>
        {
            try
            {
                var apartment = await db.Apartments
                    .Include(a => a.Packages)
                    .Include(a => a.Apartmentaddons)
                        .ThenInclude(aa => aa.Addon)
                    .FirstOrDefaultAsync(a => a.Apartmentid == apartmentid);

                if (apartment == null)
                    return TypedResults.NotFound();

                // Calculate total price
                var totalPrice = apartment.CalculateApartmentTotalPrice();
                return TypedResults.Ok(totalPrice);
            }
            catch
            {
                // Log exception details
                return TypedResults.BadRequest("An error occurred while calculating the total price.");

            }
        })


        .WithName("GetApartmentTotalPrice")
        .WithOpenApi();

        //AddPackageToApartment
        group.MapPost("/{id}/addPackage/{packageId}", async Task<IResult> (int id, int packageId, OdaDbContext db) =>
        {
            var apartment = await db.Apartments.FindAsync(id);
            var package = await db.Packages.FindAsync(packageId);

            if (apartment == null) return TypedResults.NotFound();
            if (package == null) return TypedResults.BadRequest("Package not found.");
            if (package.Apartmentid != null) return TypedResults.BadRequest("Package is already assigned to another apartment.");


            package.Apartmentid = id;
            package.Lastmodifieddatetime = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return TypedResults.Ok();
        })
        .WithName("AddPackageToApartment")
        .WithOpenApi();


        // AssignPackageToApartment
        group.MapPost("/{id}/assignPackage/{packageId}", async Task<IResult> (int id, int packageId, OdaDbContext db) =>
        {
            var apartment = await db.Apartments.Include(a => a.Packages).FirstOrDefaultAsync(a => a.Apartmentid == id);
            var package = await db.Packages.FirstOrDefaultAsync(p => p.Packageid == packageId && p.Apartmentid == id);

            if (apartment == null) return TypedResults.NotFound();
            if (package == null) return TypedResults.BadRequest("Package is not linked to this apartment.");
            if (apartment.Packages.Any(p => p.Assignedpackage == true)) return TypedResults.BadRequest("Apartment already has an assigned package.");

            // Explicitly update all packages linked to this apartment to set Assignedpackage to false
            var linkedPackages = await db.Packages.Where(p => p.Apartmentid == id && p.Assignedpackage == true).ToListAsync();
            foreach (var pkg in linkedPackages)
            {
                pkg.Assignedpackage = false;
                pkg.Lastmodifieddatetime = DateTime.UtcNow;
            }

            // Set the specified package as the assigned package
            package.Assignedpackage = true;
            package.Lastmodifieddatetime = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return TypedResults.Ok();
        })
        .WithName("AssignPackageToApartment")
        .WithOpenApi();


        //AddAddonsToApartment
        group.MapPost("/{id}/addAddon/{addonId}/{maxAvailable}", async Task<IResult> (int id, int addonId, int maxAvailable, OdaDbContext db) =>
        {
            if (maxAvailable <= 0) return TypedResults.BadRequest("MaxAvailable must be greater than 0.");

            var apartment = await db.Apartments.FindAsync(id);
            var addon = await db.Addons.FindAsync(addonId);

            if (apartment == null) return TypedResults.NotFound();
            if (addon == null) return TypedResults.BadRequest("Addon not found.");
            if (db.Apartmentaddons.Any(aa => aa.Apartmentid == id && aa.Addonid == addonId))
                return TypedResults.BadRequest("Addon is already linked to this apartment.");

            // Find the maximum Apartmentaddonsid and increment it
            var maxApartmentAddonsId = await db.Apartmentaddons.MaxAsync(aa => (int?)aa.Apartmentaddonsid) ?? 0;
            var newApartmentAddonsId = maxApartmentAddonsId + 1;


            var apartmentAddon = new Apartmentaddon
            {
                Apartmentaddonsid = newApartmentAddonsId,  // Set new Apartmentaddonsid
                Apartmentid = id,
                Addonid = addonId,
                Availableaddons = true,
                Assignedaddons = false,
                Maxavailable = maxAvailable,
                Installedamount = 0,
                Createdatetime = DateTime.UtcNow,
                Lastmodifieddatetime = DateTime.UtcNow
            };

            db.Apartmentaddons.Add(apartmentAddon);
            await db.SaveChangesAsync();

            return TypedResults.Ok();
        })
        .WithName("AddAddonToApartment")
        .WithOpenApi();


        //AssignAddonToApartment
        group.MapPost("/{id}/assignAddon/{addonId}/{installedAmount}", async Task<IResult> (int id, int addonId, int installedAmount, OdaDbContext db) =>
        {
            var apartmentAddon = await db.Apartmentaddons.FirstOrDefaultAsync(aa => aa.Apartmentid == id && aa.Addonid == addonId);

            if (apartmentAddon == null) return TypedResults.NotFound();
            if (installedAmount > apartmentAddon.Maxavailable) return TypedResults.BadRequest("Installed amount exceeds maximum available.");


            apartmentAddon.Assignedaddons = true;
            apartmentAddon.Installedamount = installedAmount;
            apartmentAddon.Lastmodifieddatetime = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return TypedResults.Ok();
        })
        .WithName("AssignAddonToApartment")
        .WithOpenApi();


        //RemoveAddonFromApartment
        group.MapDelete("/{id}/removeAddon/{addonId}", async Task<IResult> (int id, int addonId, OdaDbContext db) =>
        {
            var apartmentAddon = await db.Apartmentaddons.FirstOrDefaultAsync(aa => aa.Apartmentid == id && aa.Addonid == addonId);

            if (apartmentAddon == null) return TypedResults.NotFound();
            // Check if Assignedaddons is true (meaning the add-on is currently assigned)
            if (apartmentAddon.Assignedaddons == true) return TypedResults.BadRequest("Cannot remove an addon that is currently assigned.");


            db.Apartmentaddons.Remove(apartmentAddon);
            await db.SaveChangesAsync();

            return TypedResults.Ok();
        })
        .WithName("RemoveAddonFromApartment")
        .WithOpenApi();



        // 1. Get All Available Packages
        group.MapGet("/{id}/availablePackages", async Task<Results<Ok<IEnumerable<Package>>, NotFound>> (int id, OdaDbContext db) =>
        {
            var availablePackages = await db.Packages
                .Where(p => p.Apartmentid == id)
                .ToListAsync();

            if (availablePackages == null || !availablePackages.Any())
                return TypedResults.NotFound();

            // Return the list of available packages
            return TypedResults.Ok<IEnumerable<Package>>(availablePackages);

        })
        .WithName("GetAvailablePackages")
        .WithOpenApi();

        // 2. Get All Available AddOns
        group.MapGet("/{id}/availableAddOns", async Task<Results<Ok<IEnumerable<Addon>>, NotFound>> (int id, OdaDbContext db) =>
                {
                    var availableAddons = await db.Apartmentaddons
                        .Where(aa => aa.Apartmentid == id && aa.Availableaddons == true)
                        .Include(aa => aa.Addon)
                        .Select(aa => aa.Addon)
                        .ToListAsync();

                    if (availableAddons == null || !availableAddons.Any())
                        return TypedResults.NotFound();

                    // Return the list of available addons
                    return TypedResults.Ok<IEnumerable<Addon>>(availableAddons);

                })
                .WithName("GetAvailableAddOns")
                .WithOpenApi();

        // 3. Get Assigned Packages
        group.MapGet("/{id}/assignedPackages", async Task<Results<Ok<IEnumerable<Package>>, NotFound>> (int id, OdaDbContext db) =>
        {
            var assignedPackages = await db.Packages
                .Where(p => p.Apartmentid == id && p.Assignedpackage == true)
                .ToListAsync();

            if (assignedPackages == null || !assignedPackages.Any())
                return TypedResults.NotFound();


            // Return the list of assigned packages
            return TypedResults.Ok<IEnumerable<Package>>(assignedPackages);
        })
        .WithName("GetAssignedPackages")
        .WithOpenApi();

        // 4. Get Assigned AddOns
        group.MapGet("/{id}/assignedAddOns", async Task<Results<Ok<IEnumerable<Addon>>, NotFound>> (int id, OdaDbContext db) =>
        {
            var assignedAddons = await db.Apartmentaddons
                .Where(aa => aa.Apartmentid == id && aa.Availableaddons == true && aa.Assignedaddons == true)
                .Include(aa => aa.Addon)
                .Select(aa => aa.Addon)
                .ToListAsync();

            if (assignedAddons == null || !assignedAddons.Any())
                return TypedResults.NotFound();

            // Return the list of assigned packages
            return TypedResults.Ok<IEnumerable<Addon>>(assignedAddons);

        })
        .WithName("GetAssignedAddOns")
        .WithOpenApi();

    }
}