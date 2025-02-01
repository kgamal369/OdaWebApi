using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class ApartmentEndpoints
    {
        public static void MapApartmentEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Apartment").WithTags(nameof(Apartment));

            // Get all Apartments
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Apartments
                    .Include(a => a.Project)
                    .Include(a => a.Plan)
                    .Include(a => a.Automation)
                    .AsNoTracking()
                    .ToListAsync();
            })
            .WithName("GetAllApartments")
            .WithOpenApi();

            // Get Apartment by ID
            group.MapGet("/{id}", async Task<Results<Ok<Apartment>, NotFound>> (int id, OdaDbContext db) =>
            {
                var apartment = await db.Apartments
                    .Include(a => a.Project)
                    .Include(a => a.Plan)
                    .Include(a => a.Automation)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Apartmentid == id);

                return apartment is not null
                    ? TypedResults.Ok(apartment)
                    : TypedResults.NotFound();
            })
            .WithName("GetApartmentById")
            .WithOpenApi();


            // Create Apartment
            group.MapPost("/", async Task<Results<Created<Apartment>, BadRequest<string>>> (Apartment apartment, OdaDbContext db) =>
            {
                // Validate ApartmentType
                if (!Enum.IsDefined(typeof(ApartmentType), apartment.Apartmenttype))
                {
                    return TypedResults.BadRequest<string>("Invalid ApartmentType.");
                }

                // Validate ApartmentStatus
                if (!Enum.IsDefined(typeof(Apartmentstatus), apartment.Apartmentstatus))
                {
                    return TypedResults.BadRequest<string>("Invalid ApartmentStatus.");
                }

                // Validate ProjectId
                if (apartment.Projectid.HasValue && !await db.Projects.AnyAsync(p => p.Projectid == apartment.Projectid))
                {
                    return TypedResults.BadRequest<string>($"Invalid ProjectId: {apartment.Projectid}");
                }

                // Validate PlanId
                if (apartment.Planid.HasValue && !await db.Plans.AnyAsync(p => p.Planid == apartment.Planid))
                {
                    return TypedResults.BadRequest<string>($"Invalid PlanId: {apartment.Planid}");
                }

                // Validate AutomationId
                if (apartment.Automationid.HasValue && !await db.Automations.AnyAsync(a => a.Automationid == apartment.Automationid))
                {
                    return TypedResults.BadRequest<string>($"Invalid AutomationId: {apartment.Automationid}");
                }

                // Apply validation rules
                if (apartment.Apartmentstatus == Apartmentstatus.InProgress || apartment.Apartmentstatus == Apartmentstatus.InReview)
                {
                    if (!apartment.Planid.HasValue)
                    {
                        return TypedResults.BadRequest<string>("PlanId is required when ApartmentStatus is InProgress or InReview.");
                    }
                }
                else if (apartment.Apartmentstatus == Apartmentstatus.ForSale)
                {
                    if (apartment.Planid.HasValue || apartment.Automationid.HasValue)
                    {
                        return TypedResults.BadRequest<string>("PlanId and AutomationId must be null when ApartmentStatus is ForSale.");
                    }
                }
                apartment.Createddatetime = DateTime.UtcNow;
                apartment.Lastmodifieddatetime = apartment.Createddatetime;

                db.Apartments.Add(apartment);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/Apartment/{apartment.Apartmentid}", apartment);
            })
            .WithName("CreateApartment")
            .WithOpenApi();

            // Assign a valid PlanId to an ApartmentId
            group.MapPut("/{id}/AssignPlan", async Task<Results<Ok, NotFound, BadRequest<string>>> (int id, int planId, OdaDbContext db) =>
            {
                var apartment = await db.Apartments.FindAsync(id);
                if (apartment == null)
                    return TypedResults.NotFound();

                if (!await db.Plans.AnyAsync(p => p.Planid == planId))
                    return TypedResults.BadRequest<string>($"Invalid PlanId: {planId}");

                // Ensure PlanId can only be assigned when status is InProgress or InReview
                if (apartment.Apartmentstatus == Apartmentstatus.ForSale)
                {
                    return TypedResults.BadRequest<string>("Cannot assign PlanId when ApartmentStatus is ForSale.");
                }

                apartment.Planid = planId;
                apartment.Lastmodifieddatetime = DateTime.UtcNow;
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("AssignPlan")
            .WithOpenApi();

            // Assign a valid AutomationId to an ApartmentId
            group.MapPut("/{id}/AssignAutomation", async Task<Results<Ok, NotFound, BadRequest<string>>> (int id, int automationId, OdaDbContext db) =>
            {
                var apartment = await db.Apartments.FindAsync(id);
                if (apartment == null)
                    return TypedResults.NotFound();

                if (!await db.Automations.AnyAsync(a => a.Automationid == automationId))
                    return TypedResults.BadRequest<string>($"Invalid AutomationId: {automationId}");

                // Ensure AutomationId can only be assigned when status is InProgress or InReview
                if (apartment.Apartmentstatus == Apartmentstatus.ForSale)
                {
                    return TypedResults.BadRequest<string>("Cannot assign AutomationId when ApartmentStatus is ForSale.");
                }

                apartment.Automationid = automationId;
                apartment.Lastmodifieddatetime = DateTime.UtcNow;
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("AssignAutomation")
            .WithOpenApi();


            // Update Apartment
            group.MapPut("/{id}", async Task<Results<Ok, NotFound, BadRequest<string>>> (int id, Apartment updatedApartment, OdaDbContext db) =>
            {
                var existingApartment = await db.Apartments.FirstOrDefaultAsync(a => a.Apartmentid == id);
                if (existingApartment is null)
                {
                    return TypedResults.NotFound();
                }

                // Validate ApartmentType
                if (!Enum.IsDefined(typeof(ApartmentType), updatedApartment.Apartmenttype))
                {
                    return TypedResults.BadRequest<string>("Invalid ApartmentType.");
                }

                // Validate ApartmentStatus
                if (!Enum.IsDefined(typeof(Apartmentstatus), updatedApartment.Apartmentstatus))
                {
                    return TypedResults.BadRequest<string>("Invalid ApartmentStatus.");
                }

                // Validate ProjectId
                if (updatedApartment.Projectid.HasValue && !await db.Projects.AnyAsync(p => p.Projectid == updatedApartment.Projectid))
                {
                    return TypedResults.BadRequest<string>($"Invalid ProjectId: {updatedApartment.Projectid}");
                }

                // Validate PlanId
                if (updatedApartment.Planid.HasValue && !await db.Plans.AnyAsync(p => p.Planid == updatedApartment.Planid))
                {
                    return TypedResults.BadRequest<string>($"Invalid PlanId: {updatedApartment.Planid}");
                }

                // Validate AutomationId
                if (updatedApartment.Automationid.HasValue && !await db.Automations.AnyAsync(a => a.Automationid == updatedApartment.Automationid))
                {
                    return TypedResults.BadRequest<string>($"Invalid AutomationId: {updatedApartment.Automationid}");
                }

                // Apply validation rules
                if (updatedApartment.Apartmentstatus == Apartmentstatus.InProgress || updatedApartment.Apartmentstatus == Apartmentstatus.InReview)
                {
                    if (!updatedApartment.Planid.HasValue)
                    {
                        return TypedResults.BadRequest<string>("PlanId is required when ApartmentStatus is InProgress or InReview.");
                    }
                }
                else if (updatedApartment.Apartmentstatus == Apartmentstatus.ForSale)
                {
                    if (updatedApartment.Planid.HasValue || updatedApartment.Automationid.HasValue)
                    {
                        return TypedResults.BadRequest<string>("PlanId and AutomationId must be null when ApartmentStatus is ForSale.");
                    }
                }

                // Update the existing apartment
                existingApartment.Apartmentname = updatedApartment.Apartmentname;
                existingApartment.Apartmenttype = updatedApartment.Apartmenttype;
                existingApartment.Apartmentstatus = updatedApartment.Apartmentstatus;
                existingApartment.Apartmentspace = updatedApartment.Apartmentspace;
                existingApartment.Description = updatedApartment.Description;
                existingApartment.Apartmentphotos = updatedApartment.Apartmentphotos;
                existingApartment.Projectid = updatedApartment.Projectid;
                existingApartment.Floornumber = updatedApartment.Floornumber;
                existingApartment.Availabilitydate = updatedApartment.Availabilitydate;
                existingApartment.Planid = updatedApartment.Planid;
                existingApartment.Automationid = updatedApartment.Automationid;
                existingApartment.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateApartment")
            .WithOpenApi();



            // Delete Apartment
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var apartment = await db.Apartments.FirstOrDefaultAsync(a => a.Apartmentid == id);
                if (apartment is null)
                {
                    return TypedResults.NotFound();
                }

                db.Apartments.Remove(apartment);
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("DeleteApartment")
            .WithOpenApi();



            // **Automation Endpoints**
            // 1. Get AutomationId of an Apartment
            group.MapGet("/{id}/AutomationId", async Task<Results<Ok<int?>, NotFound>> (int id, OdaDbContext db) =>
            {
                var automationId = await db.Apartments
                    .Where(a => a.Apartmentid == id)
                    .Select(a => a.Automationid)
                    .FirstOrDefaultAsync();

                return automationId != null ? TypedResults.Ok(automationId) : TypedResults.NotFound();
            });

            // 2. Get AutomationName of the Assigned AutomationId
            group.MapGet("/{id}/AutomationName", async Task<Results<Ok<string>, NotFound>> (int id, OdaDbContext db) =>
            {
                var automationName = await db.Apartments
                    .Where(a => a.Apartmentid == id && a.Automationid != null)
                    .Select(a => a.Automation.Automationname)
                    .FirstOrDefaultAsync();

                return automationName != null ? TypedResults.Ok(automationName) : TypedResults.NotFound();
            });

            // 3. Assign AutomationId to an Apartment
            group.MapPost("/{id}/AssignAutomation", async Task<Results<Ok, NotFound>> (int id, int automationId, OdaDbContext db) =>
            {
                var apartment = await db.Apartments.FindAsync(id);
                if (apartment == null)
                    return TypedResults.NotFound();

                apartment.Automationid = automationId;
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            });

            // 4. Update and Modify the Assigned AutomationId
            group.MapPut("/{id}/UpdateAutomation", async Task<Results<Ok, NotFound>> (int id, int automationId, OdaDbContext db) =>
            {
                var apartment = await db.Apartments.FindAsync(id);
                if (apartment == null)
                    return TypedResults.NotFound();

                apartment.Automationid = automationId;
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            });

            // **Plan Endpoints**
            // 5. Get PlanId of an Apartment
            group.MapGet("/{id}/PlanId", async Task<Results<Ok<int?>, NotFound>> (int id, OdaDbContext db) =>
            {
                var planId = await db.Apartments
                    .Where(a => a.Apartmentid == id)
                    .Select(a => a.Planid)
                    .FirstOrDefaultAsync();

                return planId != null ? TypedResults.Ok(planId) : TypedResults.NotFound();
            });

            // 6. Get PlanName of the Assigned PlanId
            group.MapGet("/{id}/PlanName", async Task<Results<Ok<string>, NotFound>> (int id, OdaDbContext db) =>
            {
                var planName = await db.Apartments
                    .Where(a => a.Apartmentid == id && a.Planid != null)
                    .Select(a => a.Plan.Planname)
                    .FirstOrDefaultAsync();

                return planName != null ? TypedResults.Ok(planName) : TypedResults.NotFound();
            });

            // // 7. Assign PlanId to an Apartment
            // group.MapPost("/{id}/AssignPlan", async Task<Results<Ok, NotFound>> (int id, int planId, OdaDbContext db) =>
            // {
            //     var apartment = await db.Apartments.FindAsync(id);
            //     if (apartment == null)
            //         return TypedResults.NotFound();

            //     apartment.Planid = planId;
            //     await db.SaveChangesAsync();
            //     return TypedResults.Ok();
            // });

            // 8. Update and Modify the Assigned PlanId
            group.MapPut("/{id}/UpdatePlan", async Task<Results<Ok, NotFound>> (int id, int planId, OdaDbContext db) =>
            {
                var apartment = await db.Apartments.FindAsync(id);
                if (apartment == null)
                    return TypedResults.NotFound();

                apartment.Planid = planId;
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            });

            // **Addon Endpoints**
            // 9. Get All AddonId Assigned to an Apartment
            group.MapGet("/{id}/AddonIds", async Task<Results<Ok<List<int>>, NotFound>> (int id, OdaDbContext db) =>
            {
                var addonIds = await db.ApartmentAddons
                    .Where(aa => aa.Apartmentid == id)
                    .Select(aa => aa.Addonid)
                    .ToListAsync();

                return addonIds.Any() ? TypedResults.Ok(addonIds) : TypedResults.NotFound();
            });

            // 10. Get All AddonName Assigned to an Apartment
            group.MapGet("/{id}/AddonNames", async Task<Results<Ok<List<string>>, NotFound>> (int id, OdaDbContext db) =>
            {
                var addonNames = await db.ApartmentAddons
                    .Where(aa => aa.Apartmentid == id)
                    .Join(
                        db.Addons,                         // Target table
                        aa => aa.Addonid,                  // Foreign key in ApartmentAddon
                        addon => addon.Addonid,            // Primary key in Addon
                        (aa, addon) => addon.Addonname     // Select the AddonName
                    )
                    .ToListAsync();

                return addonNames.Any() ? TypedResults.Ok(addonNames) : TypedResults.NotFound();
            });


            // 11. Assign a New AddonId to an Apartment
            group.MapPost("/{id}/AssignAddon", async Task<Results<Ok, NotFound, BadRequest>> (int id, int addonId, int quantity, OdaDbContext db) =>
            {
                // Validate the apartment exists
                var apartmentExists = await db.Apartments.AnyAsync(a => a.Apartmentid == id);
                if (!apartmentExists)
                    return TypedResults.NotFound();

                // Validate the addon exists
                var addon = await db.Addons.FindAsync(addonId);
                if (addon == null)
                    return TypedResults.NotFound();

                // Ensure quantity is valid
                if (quantity <= 0)
                    return TypedResults.BadRequest();

                // Check if the addon is already assigned to the apartment
                var existingAssignment = await db.ApartmentAddons
                    .FirstOrDefaultAsync(aa => aa.Apartmentid == id && aa.Addonid == addonId);

                if (existingAssignment != null)
                    return TypedResults.BadRequest();

                // Determine the quantity logic based on UnitOrMeter
                if (addon.Unitormeter == UnitOrMeterType.Meter)
                {
                    quantity = 1; // For Meter, quantity is always 1
                }

                // Assign the addon with the quantity
                db.ApartmentAddons.Add(new ApartmentAddon
                {
                    Apartmentid = id,
                    Addonid = addonId,
                    Quantity = quantity
                });

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            });

            // Update an existing AddonId from an Apartment 
            group.MapPut("/{id}/UpdateAddonQuantity", async Task<Results<Ok, NotFound, BadRequest>> (int id, int addonId, int newQuantity, OdaDbContext db) =>
            {
                // Validate the apartment and addon assignment exists
                var existingAssignment = await db.ApartmentAddons
                    .Include(aa => aa.Addon) // Include Addon to access UnitOrMeter
                    .FirstOrDefaultAsync(aa => aa.Apartmentid == id && aa.Addonid == addonId);

                if (existingAssignment == null)
                    return TypedResults.NotFound();

                // Ensure the new quantity is positive
                if (newQuantity <= 0)
                    return TypedResults.BadRequest();

                // Validate the quantity based on UnitOrMeter
                if (existingAssignment.Addon.Unitormeter == UnitOrMeterType.Meter)
                {
                    // For Meter, quantity must remain 1
                    newQuantity = 1;
                }

                // Update the quantity
                existingAssignment.Quantity = newQuantity;
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            });



            // 12. Delete an Existing AddonId from an Apartment
            group.MapDelete("/{id}/DeleteAddon/{addonId}", async Task<Results<Ok, NotFound>> (int id, int addonId, OdaDbContext db) =>
            {
                var existing = await db.ApartmentAddons
                    .FirstOrDefaultAsync(aa => aa.Apartmentid == id && aa.Addonid == addonId);

                if (existing == null)
                    return TypedResults.NotFound();

                db.ApartmentAddons.Remove(existing);
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            });




            // **AddPerRequest Endpoints**
            // 13. Get All AddPerRequestId Assigned to an Apartment
            group.MapGet("/{id}/AddPerRequestIds", async Task<Results<Ok<List<int>>, NotFound>> (int id, OdaDbContext db) =>
            {
                var addPerRequestIds = await db.ApartmentAddonperrequests
                    .Where(ap => ap.Apartmentid == id)
                    .Select(ap => ap.Addperrequestid)
                    .ToListAsync();

                return addPerRequestIds.Any() ? TypedResults.Ok(addPerRequestIds) : TypedResults.NotFound();
            });

            // 14. Get All AddPerRequestName Assigned to an Apartment
            group.MapGet("/{id}/AddPerRequestNames", async Task<Results<Ok<List<string>>, NotFound>> (int id, OdaDbContext db) =>
            {
                var addPerRequestNames = await db.ApartmentAddonperrequests
                    .Where(ap => ap.Apartmentid == id)
                    .Join(
                        db.Addperrequests,
                        ap => ap.Addperrequestid,
                        ar => ar.Addperrequestid,
                        (ap, ar) => ar.Addperrequestname
                    )
                    .ToListAsync();

                return addPerRequestNames.Any() ? TypedResults.Ok(addPerRequestNames) : TypedResults.NotFound();
            });


            //// 15. Assign a New AddPerRequestId to an Apartment
            //group.MapGet("/{id}/AddPerRequestNames", async Task<Results<Ok<List<string>>, NotFound>> (int id, OdaDbContext db) =>
            //{
            //    var addPerRequestNames = await db.ApartmentAddonperrequests
            //        .Where(ap => ap.Apartmentid == id)
            //        .Join(
            //            db.Addperrequests,                       // The target table
            //            ap => ap.Addperrequestid,               // The key from ApartmentAddonPerRequest
            //            ar => ar.Addperrequestid,               // The key from Addperrequests
            //            (ap, ar) => ar.Addperrequestname        // Select Addperrequestname
            //        )
            //        .ToListAsync();

            //    return addPerRequestNames.Any() ? TypedResults.Ok(addPerRequestNames) : TypedResults.NotFound();
            //});


            // 16. Delete an Existing AddPerRequestId from an Apartment
            group.MapDelete("/{id}/DeleteAddPerRequest/{addPerRequestId}", async Task<Results<Ok, NotFound>> (int id, int addPerRequestId, OdaDbContext db) =>
            {
                // Find the existing relationship
                var existingLink = await db.ApartmentAddonperrequests
                    .FirstOrDefaultAsync(ap => ap.Apartmentid == id && ap.Addperrequestid == addPerRequestId);

                if (existingLink == null)
                    return TypedResults.NotFound();

                // Remove the relationship
                db.ApartmentAddonperrequests.Remove(existingLink);
                await db.SaveChangesAsync();

                return TypedResults.Ok();

            });
        }
    }
}
