using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.Common;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.Common
{
    public static class AutomationDetailsEndpoints
    {
        public static void MapAutomationDetailsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/AutomationDetails").WithTags(nameof(Automationdetail));

            // 1️⃣ **Get All Automation Details - with Base64 Encoding for Image**
            group.MapGet("/", async (OdaDbContext db) =>
            {
                var automationDetails = await db.Automationdetails
                    .AsNoTracking()
                    .Select(a => new
                    {
                        a.Automationdetailsid,
                        a.Automationdetailsname,
                        a.Automationid,
                        a.Description,
                        a.Createdatetime,
                        a.Lastmodifieddatetime,

                        // ✅ Convert bytea to Base64 string
                        IconBase64 = a.Icon != null ? Convert.ToBase64String(a.Icon) : null
                    })
                    .ToListAsync();

                return Results.Ok(automationDetails);
            })
            .WithName("GetAllAutomationDetails")
            .WithOpenApi();

            // 2️⃣ **Get a Single Automation Detail by ID**
            group.MapGet("/{id:int}", async (int id, OdaDbContext db) =>
            {
                var automationDetail = await db.Automationdetails
                    .AsNoTracking()
                    .Where(a => a.Automationdetailsid == id)
                    .Select(a => new
                    {
                        a.Automationdetailsid,
                        a.Automationdetailsname,
                        a.Automationid,
                        a.Description,
                        a.Createdatetime,
                        a.Lastmodifieddatetime,

                        // ✅ Convert bytea to Base64 for JSON response
                        IconBase64 = a.Icon != null ? Convert.ToBase64String(a.Icon) : null
                    })
                    .FirstOrDefaultAsync();

                return automationDetail != null ? Results.Ok(automationDetail) : Results.NotFound();
            })
            .WithName("GetAutomationDetailById")
            .WithOpenApi();

            // 3. Create an Automation Detail
            group.MapPost("/", async Task<Results<Created<Automationdetail>, BadRequest>> (Automationdetail automationDetail, OdaDbContext db) =>
            {
                automationDetail.Createdatetime = DateTime.UtcNow;
                automationDetail.Lastmodifieddatetime = automationDetail.Createdatetime;

                db.Automationdetails.Add(automationDetail);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/AutomationDetails/{automationDetail.Automationdetailsid}", automationDetail);
            })
            .WithName("CreateAutomationDetail")
            .WithOpenApi();

            // 4. Update an Automation Detail
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Automationdetail updatedAutomationDetail, OdaDbContext db) =>
            {
                var automationDetail = await db.Automationdetails.FindAsync(id);
                if (automationDetail == null)
                    return TypedResults.NotFound();

                // Validate that the AutomationId exists if updating it
                if (updatedAutomationDetail.Automationid != null)
                {
                    var automationExists = await db.Automations.AnyAsync(a => a.Automationid == updatedAutomationDetail.Automationid);
                    if (!automationExists)
                        return TypedResults.NotFound();
                }

                automationDetail.Automationdetailsname = updatedAutomationDetail.Automationdetailsname;
                automationDetail.Description = updatedAutomationDetail.Description;
                automationDetail.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateAutomationDetail")
            .WithOpenApi();

            // 5. Delete an Automation Detail
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var automationDetail = await db.Automationdetails.FindAsync(id);
                if (automationDetail == null)
                    return TypedResults.NotFound();

                db.Automationdetails.Remove(automationDetail);
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("DeleteAutomationDetail")
            .WithOpenApi();

            // 6. Get All AutomationDetails by AutomationId
            group.MapGet("/automation/{automationId}", async Task<Results<Ok<List<Automationdetail>>, NotFound>> (int automationId, OdaDbContext db) =>
            {
                var automationDetails = await db.Automationdetails
                    .Where(ad => ad.Automationid == automationId)
                    .AsNoTracking()
                    .ToListAsync();

                return automationDetails.Any() ? TypedResults.Ok(automationDetails) : TypedResults.NotFound();
            })
            .WithName("GetAutomationDetailsByAutomationId")
            .WithOpenApi();
        }
    }
}
