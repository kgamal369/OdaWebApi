using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class AutomationEndpoints
    {
        public static void MapAutomationEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Automation").WithTags(nameof(Automation));

            // 1. Get All Automations
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Automations.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllAutomations")
            .WithOpenApi();

            // 2. Get Automation by ID
            group.MapGet("/{id}", async Task<Results<Ok<Automation>, NotFound>> (int id, OdaDbContext db) =>
            {
                var automation = await db.Automations.AsNoTracking().FirstOrDefaultAsync(a => a.Automationid == id);
                return automation is not null ? TypedResults.Ok(automation) : TypedResults.NotFound();
            })
            .WithName("GetAutomationById")
            .WithOpenApi();

            // 3. Create an Automation
            group.MapPost("/", async (Automation automation, OdaDbContext db) =>
            {
                automation.Createdatetime = DateTime.UtcNow;
                automation.Lastmodifieddatetime = automation.Createdatetime;

                db.Automations.Add(automation);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/Automation/{automation.Automationid}", automation);
            })
            .WithName("CreateAutomation")
            .WithOpenApi();

            // 4. Update an Automation
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Automation updatedAutomation, OdaDbContext db) =>
            {
                var automation = await db.Automations.FindAsync(id);
                if (automation == null)
                    return TypedResults.NotFound();

                automation.Automationname = updatedAutomation.Automationname;
                automation.Description = updatedAutomation.Description;
                automation.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateAutomation")
            .WithOpenApi();

            // 5. Delete an Automation
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var automation = await db.Automations.FindAsync(id);
                if (automation == null)
                    return TypedResults.NotFound();

                db.Automations.Remove(automation);
                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("DeleteAutomation")
            .WithOpenApi();
        }
    }
}
