using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class PlanEndpoints
    {
        public static void MapPlanEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Plan").WithTags(nameof(Plan));

            // 1. Get All Plans
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Plans.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllPlans")
            .WithOpenApi();

            // 2. Get Plan by ID
            group.MapGet("/{id}", async Task<Results<Ok<Plan>, NotFound>> (int id, OdaDbContext db) =>
            {
                var plan = await db.Plans.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Planid == id);

                return plan is not null
                    ? TypedResults.Ok(plan)
                    : TypedResults.NotFound();
            })
            .WithName("GetPlanById")
            .WithOpenApi();

            // 3. Create a Plan
            group.MapPost("/", async Task<Results<Created<Plan>, BadRequest>> (Plan plan, OdaDbContext db) =>
            {
                plan.Createdatetime = DateTime.UtcNow;
                plan.Lastmodifieddatetime = plan.Createdatetime;

                db.Plans.Add(plan);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/Plan/{plan.Planid}", plan);
            })
            .WithName("CreatePlan")
            .WithOpenApi();

            // 4. Update a Plan
            group.MapPut("/{id}", async Task<Results<Ok, NotFound, BadRequest>> (int id, Plan updatedPlan, OdaDbContext db) =>
            {
                var existingPlan = await db.Plans.FindAsync(id);
                if (existingPlan is null)
                    return TypedResults.NotFound();

                // Update properties
                existingPlan.Planname = updatedPlan.Planname;
                existingPlan.Pricepermeter = updatedPlan.Pricepermeter;
                existingPlan.Description = updatedPlan.Description;
                existingPlan.Planphoto = updatedPlan.Planphoto;
                existingPlan.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdatePlan")
            .WithOpenApi();

            // 5. Delete a Plan
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var plan = await db.Plans.FindAsync(id);
                if (plan is null)
                    return TypedResults.NotFound();

                db.Plans.Remove(plan);
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("DeletePlan")
            .WithOpenApi();

            // 6. Get All PlanDetails for a PlanId
            group.MapGet("/{planid}/PlanDetails", async Task<Results<Ok<List<Plandetail>>, NotFound>> (int planid, OdaDbContext db) =>
            {
                var planDetails = await db.Plandetails
                    .Where(pd => pd.Planid == planid)
                    .ToListAsync();

                return planDetails.Any()
                    ? TypedResults.Ok(planDetails)
                    : TypedResults.NotFound();
            })
            .WithName("GetAllPlanDetailsForPlan")
            .WithOpenApi();

            // 7. Get PlanDetails for a PlanId where PlanDetailsType = Foundation
            group.MapGet("/{planid}/PlanDetails/Foundation", async Task<Results<Ok<List<Plandetail>>, NotFound>> (int planid, OdaDbContext db) =>
            {
                var foundationDetails = await db.Plandetails
                    .Where(pd => pd.Planid == planid && pd.Plandetailstype == PlanDetailsType.Foundation)
                    .ToListAsync();

                return foundationDetails.Any()
                    ? TypedResults.Ok(foundationDetails)
                    : TypedResults.NotFound();
            })
            .WithName("GetFoundationPlanDetails")
            .WithOpenApi();

            // 8. Get PlanDetails for a PlanId where PlanDetailsType = Decoration
            group.MapGet("/{planid}/PlanDetails/Decoration", async Task<Results<Ok<List<Plandetail>>, NotFound>> (int planid, OdaDbContext db) =>
            {
                var decorationDetails = await db.Plandetails
                    .Where(pd => pd.Planid == planid && pd.Plandetailstype == PlanDetailsType.Decoration)
                    .ToListAsync();

                return decorationDetails.Any()
                    ? TypedResults.Ok(decorationDetails)
                    : TypedResults.NotFound();
            })
            .WithName("GetDecorationPlanDetails")
            .WithOpenApi();
        }
    }
}
