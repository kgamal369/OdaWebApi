using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Endpoints
{
    public static class InvoiceEndpoints
    {
        public static void MapInvoiceEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Invoice").WithTags(nameof(Invoice));

            // Get all Invoices
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Invoices.ToListAsync()
            ).WithName("GetAllInvoices").WithOpenApi();

            // Get Invoice by Id
            group.MapGet("/{id}", async Task<Results<Ok<Invoice>, NotFound>> (int invoiceid, OdaDbContext db) =>
                await db.Invoices.AsNoTracking()
                    .FirstOrDefaultAsync(i => i.Invoiceid == invoiceid)
                    is Invoice invoice
                        ? TypedResults.Ok(invoice)
                        : TypedResults.NotFound()
            ).WithName("GetInvoiceById").WithOpenApi();

            // Update Invoice
            group.MapPut("/{id}", async Task<IResult> (int invoiceid, Invoice invoice, OdaDbContext db) =>
            {
                if (!await db.Bookings.AnyAsync(b => b.Bookingid == invoice.Bookingid))
                    return TypedResults.BadRequest("Invalid Booking ID.");

                invoice.Lastmodifieddatetime = DateTime.UtcNow;

                if (invoice.Invoicestatus == "Pending" || invoice.Invoicestatus == "Partially Paid")
                {
                    if (invoice.Invoiceduedate.HasValue && DateTime.UtcNow > invoice.Invoiceduedate.Value)
                    {
                        invoice.Invoicestatus = "Overdue";
                    }
                }

                var affected = await db.Invoices
                    .Where(i => i.Invoiceid == invoiceid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(i => i.Bookingid, invoice.Bookingid)
                        .SetProperty(i => i.Invoiceamount, invoice.Invoiceamount)
                        .SetProperty(i => i.Invoicestatus, invoice.Invoicestatus)
                        .SetProperty(i => i.Invoiceduedate, invoice.Invoiceduedate)
                        .SetProperty(i => i.Lastmodifieddatetime, invoice.Lastmodifieddatetime));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdateInvoice").WithOpenApi();

            // Create Invoice
            group.MapPost("/", async (Invoice invoice, OdaDbContext db) =>
            {
                if (!await db.Bookings.AnyAsync(b => b.Bookingid == invoice.Bookingid))
                    return Results.BadRequest("Invalid Booking ID.");

                var maxInvoiceId = await db.Invoices.MaxAsync(i => (int?)i.Invoiceid) ?? 0;
                invoice.Invoiceid = maxInvoiceId + 1;
                invoice.Createdatetime = DateTime.UtcNow;
                invoice.Lastmodifieddatetime = invoice.Createdatetime;

                if (invoice.Invoicestatus == "Pending" || invoice.Invoicestatus == "Partially Paid")
                {
                    if (invoice.Invoiceduedate.HasValue && DateTime.UtcNow > invoice.Invoiceduedate.Value)
                    {
                        invoice.Invoicestatus = "Overdue";
                    }
                }

                db.Invoices.Add(invoice);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Invoice/{invoice.Invoiceid}", invoice);
            }).WithName("CreateInvoice").WithOpenApi();

            // Delete Invoice
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int invoiceid, OdaDbContext db) =>
            {
                var affected = await db.Invoices
                    .Where(i => i.Invoiceid == invoiceid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeleteInvoice").WithOpenApi();

            // Get Bookings Related to an Invoice
            group.MapGet("/{id}/booking", async Task<Results<Ok<Booking>, NotFound>> (int invoiceid, OdaDbContext db) =>
            {
                var invoice = await db.Invoices.Include(i => i.Booking).FirstOrDefaultAsync(i => i.Invoiceid == invoiceid);
                return invoice?.Booking != null
                    ? TypedResults.Ok(invoice.Booking)
                    : TypedResults.NotFound();
            }).WithName("GetInvoiceBooking").WithOpenApi();

            // Update Invoice Status
            group.MapPut("/{id}/status", async Task<IResult> (int invoiceid, string newStatus, OdaDbContext db) =>
            {
                if (string.IsNullOrWhiteSpace(newStatus))
                    return TypedResults.BadRequest("Invalid status.");

                var invoice = await db.Invoices.FirstOrDefaultAsync(i => i.Invoiceid == invoiceid);
                if (invoice == null) return TypedResults.NotFound();

                invoice.Invoicestatus = newStatus;
                invoice.Lastmodifieddatetime = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return TypedResults.Ok();
            }).WithName("UpdateInvoiceStatus").WithOpenApi();
        }
    }
}