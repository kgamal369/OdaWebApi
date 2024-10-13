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
}


public static class ApartmentEndpoints
{
	public static void MapApartmentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Apartment").WithTags(nameof(Apartment));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Apartments.ToListAsync();
        })
        .WithName("GetAllApartments")
        .WithOpenApi();

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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int apartmentid, Apartment apartment, OdaDbContext db) =>
        {
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
                  .SetProperty(m => m.Createddatetime, apartment.Createddatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, apartment.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateApartment")
        .WithOpenApi();

        group.MapPost("/", async (Apartment apartment, OdaDbContext db) =>
        {
            db.Apartments.Add(apartment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Apartment/{apartment.Apartmentid}",apartment);
        })
        .WithName("CreateApartment")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int apartmentid, OdaDbContext db) =>
        {
            var affected = await db.Apartments
                .Where(model => model.Apartmentid == apartmentid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteApartment")
        .WithOpenApi();
    }
}