using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;
using System.Text.Json;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class PaymentPlanEndpoints
    {
        public static void MapPaymentPlanEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/PaymentPlan").WithTags(nameof(Paymentplan));

            // 1. Get All Payment Plans
            group.MapGet("/", async (OdaDbContext db) =>
            {
                var paymentPlans = await db.Paymentplans
                .AsNoTracking()
                .Select(a => new
                {
                    a.Paymentplanid,
                    a.Paymentplanname,
                    a.Numberofinstallmentmonths,
                    a.Downpayment,
                    a.Downpaymentpercentage,
                    a.Adminfees,
                    a.Adminfeespercentage,
                    a.Interestrate,
                    a.Interestrateperyearpercentage,
                    // ✅ Convert bytea to Base64 string
                    IconBase64 = a.Paymentplanicon != null ? Convert.ToBase64String(a.Paymentplanicon) : null
                })
                .ToListAsync();
                return Results.Ok(paymentPlans);
            })
            .WithName("GetAllPaymentPlans")
            .WithOpenApi();


            // 2. Get Payment Plan by ID
            group.MapGet("/{id:int}", async (int id, OdaDbContext db) =>
            {
                var paymentPlan = await db.Paymentplans
                .AsNoTracking()
                .Where(a => a.Paymentplanid == id)
                .Select(a => new
                {
                    a.Paymentplanid,
                    a.Paymentplanname,
                    a.Numberofinstallmentmonths,
                    a.Downpayment,
                    a.Downpaymentpercentage,
                    a.Adminfees,
                    a.Adminfeespercentage,
                    a.Interestrate,
                    a.Interestrateperyearpercentage,
                    // ✅ Convert bytea to Base64 string
                    IconBase64 = a.Paymentplanicon != null ? Convert.ToBase64String(a.Paymentplanicon) : null
                })
                .FirstOrDefaultAsync();

                return paymentPlan is not null
                    ? Results.Ok(paymentPlan)
                    : Results.NotFound();
            })
            .WithName("GetPaymentPlanById")
            .WithOpenApi();

            // 3. Create a New Payment Plan
            // group.MapPost("/", async Task<Results<Created<Paymentplan>, BadRequest>> (Paymentplan paymentPlan, HttpContext context, OdaDbContext db) =>
            // {
            //    var request = context.Request;

            //    if (paymentPlan is null)
            //        return TypedResults.BadRequest();

            //    // Handle file upload (icon)
            //    if (request.Form.Files.Count > 0)
            //    {
            //        var icon = request.Form.Files[0];
            //        if (icon.Length > 0)
            //        {
            //            using var memoryStream = new MemoryStream();
            //            await icon.CopyToAsync(memoryStream);
            //            paymentPlan.Paymentplanicon = new List<byte[]> { memoryStream.ToArray() };
            //        }
            //    }

            //    db.Paymentplans.Add(paymentPlan);
            //    await db.SaveChangesAsync();

            //    return TypedResults.Created($"/api/PaymentPlan/{paymentPlan.Paymentplanid}", paymentPlan);
            // })
            //  .WithName("CreatePaymentPlan")
            //  .WithOpenApi();

            // 4. Update an Existing Payment Plan
            group.MapPut("/{id}", async Task<Results<Ok, NotFound, BadRequest>> (int id, HttpRequest request, OdaDbContext db) =>
            {
                var existingPaymentPlan = await db.Paymentplans.FindAsync(id);
                if (existingPaymentPlan is null)
                    return TypedResults.NotFound();

                var paymentPlanJson = request.Form["paymentPlan"];
                var updatedPaymentPlan = JsonSerializer.Deserialize<Paymentplan>(paymentPlanJson);

                if (updatedPaymentPlan is null)
                    return TypedResults.BadRequest();

                // Update properties
                existingPaymentPlan.Paymentplanname = updatedPaymentPlan.Paymentplanname;
                existingPaymentPlan.Numberofinstallmentmonths = updatedPaymentPlan.Numberofinstallmentmonths;
                existingPaymentPlan.Downpayment = updatedPaymentPlan.Downpayment;
                existingPaymentPlan.Downpaymentpercentage = updatedPaymentPlan.Downpaymentpercentage;
                existingPaymentPlan.Adminfees = updatedPaymentPlan.Adminfees;
                existingPaymentPlan.Adminfeespercentage = updatedPaymentPlan.Adminfeespercentage;
                existingPaymentPlan.Interestrate = updatedPaymentPlan.Interestrate;
                existingPaymentPlan.Interestrateperyearpercentage = updatedPaymentPlan.Interestrateperyearpercentage;

                // // Handle file upload (icon update)
                // if (request.Form.Files.Count > 0)
                // {
                //     var icon = request.Form.Files[0];
                //     if (icon.Length > 0)
                //     {
                //         using var memoryStream = new MemoryStream();
                //         await icon.CopyToAsync(memoryStream);
                //        existingPaymentPlan.Paymentplanicon = new byte[] { memoryStream.ToArray() };
                //     }
                // }

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            })
            .WithName("UpdatePaymentPlan")
            .WithOpenApi();

            // 5. Delete a Payment Plan
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OdaDbContext db) =>
            {
                var paymentPlan = await db.Paymentplans.FindAsync(id);
                if (paymentPlan is null)
                    return TypedResults.NotFound();

                db.Paymentplans.Remove(paymentPlan);
                await db.SaveChangesAsync();

                return TypedResults.Ok();
            })
            .WithName("DeletePaymentPlan")
            .WithOpenApi();

            // 6. Get All Installment Breakdowns for a Payment Plan
            group.MapGet("/{id}/InstallmentBreakdowns", async Task<Results<Ok<List<Installmentbreakdown>>, NotFound>> (int id, OdaDbContext db) =>
            {
                var breakdowns = await db.Installmentbreakdowns
                    .Where(b => b.Paymentplanid == id)
                    .ToListAsync();

                return breakdowns.Any()
                    ? TypedResults.Ok(breakdowns)
                    : TypedResults.NotFound();
            })
            .WithName("GetInstallmentBreakdownsByPaymentPlanRef")
            .WithOpenApi();
        }
    }
}
