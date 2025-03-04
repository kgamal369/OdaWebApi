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
                var plans = await db.Plans
                .AsNoTracking()
                .Select(a => new
                {
                    a.Planid,
                    a.Planname,
                    a.Pricepermeter,
                    a.Description,
                    a.Projecttype,
                    a.Createdatetime,
                    a.Lastmodifieddatetime,
                    // ✅ Convert bytea to Base64 string
                    PlanBase64 = a.Planphoto != null ? Convert.ToBase64String(a.Planphoto) : null
                })
                .ToListAsync();
                return Results.Ok(plans);
            })
            .WithName("GetAllPlansList")
            .WithOpenApi();

            // 2. Get Plan by ID
            group.MapGet("/{id:int}", async (int id, OdaDbContext db) =>
            {
                var plan = await db.Plans
                .AsNoTracking()
                .Where(a => a.Planid == id)
                .Select(a => new
                {
                    a.Planid,
                    a.Planname,
                    a.Pricepermeter,
                    a.Description,
                    a.Createdatetime,
                    a.Lastmodifieddatetime,

                    // ✅ Convert bytea to Base64 string
                    PlanBase64 = a.Planphoto != null ? Convert.ToBase64String(a.Planphoto) : null
                })
                .FirstOrDefaultAsync();

                return plan is not null
                    ? Results.Ok(plan)
                    : Results.NotFound();
            })
            .WithName("GetPlanById")
            .WithOpenApi();
            //3. Get All Plans Per Project Type Locate your home 
            group.MapGet("/locate-your-home", async (OdaDbContext db) =>
          {
              var plans = await db.Plans
              .AsNoTracking()
               .Where(a => a.Projecttype == true)
              .Select(a => new
              {
                  a.Planid,
                  a.Planname,
                  a.Pricepermeter,
                  a.Description,
                  a.Projecttype,
                  a.Createdatetime,
                  a.Lastmodifieddatetime,
                  // ✅ Convert bytea to Base64 string
                  PlanBase64 = a.Planphoto != null ? Convert.ToBase64String(a.Planphoto) : null
              })
              .ToListAsync();
              return Results.Ok(plans);
          })
          .WithName("GetAllPlansLocateYourHome")
          .WithOpenApi();

            //4. Get All Plans Per Project Type Locate your home 
            group.MapGet("/build-your-home", async (OdaDbContext db) =>
          {
              var plans = await db.Plans
              .AsNoTracking()
               .Where(a => a.Projecttype == false)
              .Select(a => new
              {
                  a.Planid,
                  a.Planname,
                  a.Pricepermeter,
                  a.Description,
                  a.Projecttype,
                  a.Createdatetime,
                  a.Lastmodifieddatetime,
                  // ✅ Convert bytea to Base64 string
                  PlanBase64 = a.Planphoto != null ? Convert.ToBase64String(a.Planphoto) : null
              })
              .ToListAsync();
              return Results.Ok(plans);
          })
          .WithName("GetAllPlansBuildYourHome")
          .WithOpenApi();

            // 5. Create a Plan
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

            // 6. Delete a Plan
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

            // 7. Get All PlanDetails for a PlanId
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

            // 8. Get PlanDetails for a PlanId where PlanDetailsType = Foundation
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

            // 9. Get PlanDetails for a PlanId where PlanDetailsType = Decoration
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
