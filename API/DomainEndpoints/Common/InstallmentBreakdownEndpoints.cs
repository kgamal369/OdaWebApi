using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.Common;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.Common
{
    public static class InstallmentBreakdownEndpoints
    {
        public static void MapInstallmentBreakdownEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/InstallmentBreakdown").WithTags(nameof(Installmentbreakdown));

            // 1. Get All Installment Breakdowns
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Installmentbreakdowns.AsNoTracking()
                    .ToListAsync();
            })
            .WithName("GetAllInstallmentBreakdowns")
            .WithOpenApi();

            // 2. Get Installment Breakdown by ID
            group.MapGet("/{id}", async Task<Results<Ok<Installmentbreakdown>, NotFound>> (int id, OdaDbContext db) =>
            {
                var breakdown = await db.Installmentbreakdowns.AsNoTracking()
                    .FirstOrDefaultAsync(i => i.Breakdownid == id);

                return breakdown is not null
                    ? TypedResults.Ok(breakdown)
                    : TypedResults.NotFound();
            })
            .WithName("GetInstallmentBreakdownById")
            .WithOpenApi();

            // 3. Create a New Installment Breakdown
            group.MapPost("/", async Task<Results<Created<Installmentbreakdown>, BadRequest<string>>> (Installmentbreakdown breakdown, OdaDbContext db) =>
            {
                // Validate Payment Plan ID
                if (breakdown.Paymentplanid.HasValue && !await db.Paymentplans.AnyAsync(p => p.Paymentplanid == breakdown.Paymentplanid))
                {
                    return TypedResults.BadRequest($"Invalid PaymentPlanId: {breakdown.Paymentplanid}");
                }

                // Ensure installment percentage is within valid range
                if (breakdown.Installmentpercentage <= 0 || breakdown.Installmentpercentage > 100)
                {
                    return TypedResults.BadRequest("Installment percentage must be between 0 and 100.");
                }

                breakdown.Createddatetime = DateTime.UtcNow;
                breakdown.Lastmodifieddatetime = breakdown.Createddatetime;

                db.Installmentbreakdowns.Add(breakdown);
                await db.SaveChangesAsync();

                return TypedResults.Created($"/api/InstallmentBreakdown/{breakdown.Breakdownid}", breakdown);
            })
            .WithName("CreateInstallmentBreakdown")
            .WithOpenApi();

            // 4. Update an Existing Installment Breakdown
            group.MapPut("/{id}", async Task<Results<Ok, NotFound, BadRequest<string>>> (int id, Installmentbreakdown updatedBreakdown, OdaDbContext db) =>
            {
                var existingBreakdown = await db.Installmentbreakdowns.FindAsync(id);
                if (existingBreakdown is null)
                    return TypedResults.NotFound();

                // Validate Payment Plan ID
                if (updatedBreakdown.Paymentplanid.HasValue && !await db.Paymentplans.AnyAsync(p => p.Paymentplanid == updatedBreakdown.Paymentplanid))
                {
                    return TypedResults.BadRequest($"Invalid PaymentPlanId: {updatedBreakdown.Paymentplanid}");
                }

                // Ensure installment percentage is within valid range
                if (updatedBreakdown.Installmentpercentage <= 0 || updatedBreakdown.Installmentpercentage > 100)
                {
                    return TypedResults.BadRequest("Installment percentage must be between 0 and 100.");
                }

                // Update properties
                existingBreakdown.Paymentplanid = updatedBreakdown.Paymentplanid;
                existingBreakdown.Installmentmonth = updatedBreakdown.Installmentmonth;
                existingBreakdown.Installmentpercentage = updatedBreakdown.Installmentpercentage;
                existingBreakdown.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdateInstallmentBreakdown")
            .WithOpenApi();

            // 5. Delete an Installment Breakdown
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var breakdown = await db.Installmentbreakdowns.FindAsync(id);
                if (breakdown is null)
                    return TypedResults.NotFound();

                db.Installmentbreakdowns.Remove(breakdown);
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("DeleteInstallmentBreakdown")
            .WithOpenApi();

            // 6. Get All Installment Breakdowns for a Specific Payment Plan
            group.MapGet("/by-paymentplan/{paymentPlanId}", async Task<Results<Ok<List<Installmentbreakdown>>, NotFound>> (int paymentPlanId, OdaDbContext db) =>
            {
                var breakdowns = await db.Installmentbreakdowns
                    .Where(b => b.Paymentplanid == paymentPlanId)
                    .ToListAsync();

                return breakdowns.Any()
                    ? TypedResults.Ok(breakdowns)
                    : TypedResults.NotFound();
            })
            .WithName("GetInstallmentBreakdownsByPaymentPlanId")
            .WithOpenApi();
        }
    }
}
