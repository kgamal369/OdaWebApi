using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class PlanDetailsEndpoints
    {
        public static void MapPlanDetailsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/PlanDetails").WithTags(nameof(Plandetail));

            // 1. Get All PlanDetails
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Plandetails.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllPlanDetails")
            .WithOpenApi();

            // 2. Get PlanDetail by ID
            group.MapGet("/{id}", async Task<Results<Ok<Plandetail>, NotFound>> (int id, OdaDbContext db) =>
            {
                var planDetail = await db.Plandetails.AsNoTracking()
                    .FirstOrDefaultAsync(pd => pd.Plandetailsid == id);

                return planDetail is not null
                    ? TypedResults.Ok(planDetail)
                    : TypedResults.NotFound();
            })
            .WithName("GetPlanDetailById")
            .WithOpenApi();

            // 3. Create a PlanDetail
            group.MapPost("/", async Task<Results<Created<Plandetail>, BadRequest>> (Plandetail planDetail, OdaDbContext db) =>
            {
                if (!Enum.IsDefined(typeof(PlanDetailsType), planDetail.Plandetailstype))
                    return TypedResults.BadRequest();

                planDetail.Createdatetime = DateTime.UtcNow;
                planDetail.Lastmodifieddatetime = planDetail.Createdatetime;

                db.Plandetails.Add(planDetail);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/PlanDetails/{planDetail.Plandetailsid}", planDetail);
            })
            .WithName("CreatePlanDetail")
            .WithOpenApi();

            // 4. Update a PlanDetail
            group.MapPut("/{id}", async Task<Results<Ok, NotFound, BadRequest>> (int id, Plandetail updatedPlanDetail, OdaDbContext db) =>
            {
                var existingDetail = await db.Plandetails.FindAsync(id);
                if (existingDetail is null)
                    return TypedResults.NotFound();

                if (!Enum.IsDefined(typeof(PlanDetailsType), updatedPlanDetail.Plandetailstype))
                    return TypedResults.BadRequest();

                existingDetail.Plandetailsname = updatedPlanDetail.Plandetailsname;
                existingDetail.Description = updatedPlanDetail.Description;
                existingDetail.Plandetailstype = updatedPlanDetail.Plandetailstype;
                existingDetail.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdatePlanDetail")
            .WithOpenApi();

            // 5. Delete a PlanDetail
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var planDetail = await db.Plandetails.FindAsync(id);
                if (planDetail is null)
                    return TypedResults.NotFound();

                db.Plandetails.Remove(planDetail);
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("DeletePlanDetail")
            .WithOpenApi();
        }
    }

}
