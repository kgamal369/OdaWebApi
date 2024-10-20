using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Invoice
{
    [Key]
    public int Invoiceid { get; set; }

    [Required]
    public int? Bookingid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    [Required(ErrorMessage = "Invoice Amount is required.")]
    [Range(10, 10000000, ErrorMessage = "Invoice amount must be between 10 and 10M EGP.")]
    public decimal? Invoiceamount { get; set; }

    [Required(ErrorMessage = "Invoice Status is required.")]
    [EnumDataType(typeof(Enum.InvoiceStatus), ErrorMessage = "Invalid Invoice Status.")]
    public string? Invoicestatus { get; set; }

    public DateTime? Invoiceduedate { get; set; }

    public virtual Booking? Booking { get; set; }
}


public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Invoice").WithTags(nameof(Invoice));

        // Get all Invoices
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Invoices.ToListAsync();
        })
        .WithName("GetAllInvoices")
        .WithOpenApi();

        // Get Invoice by Id
        group.MapGet("/{id}", async Task<Results<Ok<Invoice>, NotFound>> (int invoiceid, OdaDbContext db) =>
        {
            return await db.Invoices.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Invoiceid == invoiceid)
                is Invoice model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetInvoiceById")
        .WithOpenApi();

        // Update Invoice
        group.MapPut("/{id}", async Task<IResult> (int invoiceid, Invoice invoice, OdaDbContext db) =>
        {
            // Validate Booking ID
            if (!await db.Bookings.AnyAsync(b => b.Bookingid == invoice.Bookingid))
                return TypedResults.BadRequest("Invalid Booking ID.");

            // Update LastModifiedDateTime
            invoice.Lastmodifieddatetime = DateTime.UtcNow;

            // Auto-update status to "Overdue" if necessary
            if (invoice.Invoicestatus == "Pending" || invoice.Invoicestatus == "Partially Paid")
            {
                if (invoice.Invoiceduedate.HasValue && DateTime.UtcNow > invoice.Invoiceduedate.Value)
                {
                    invoice.Invoicestatus = "Overdue";
                }
            }

            var affected = await db.Invoices
                .Where(model => model.Invoiceid == invoiceid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Invoiceid, invoice.Invoiceid)
                  .SetProperty(m => m.Bookingid, invoice.Bookingid)
                //  .SetProperty(m => m.Createdatetime, invoice.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, invoice.Lastmodifieddatetime)
                  .SetProperty(m => m.Invoiceamount, invoice.Invoiceamount)
                  .SetProperty(m => m.Invoicestatus, invoice.Invoicestatus)
                  .SetProperty(m => m.Invoiceduedate, invoice.Invoiceduedate)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateInvoice")
        .WithOpenApi();

        // Create Invoice
        group.MapPost("/", async (Invoice invoice, OdaDbContext db) =>
        {
            // Validate Booking ID
            if (!await db.Bookings.AnyAsync(b => b.Bookingid == invoice.Bookingid))
                return Results.BadRequest("Invalid Booking ID.");

            // Set Invoice ID to MaxInvoiceId + 1
            var maxInvoiceId = await db.Invoices.MaxAsync(i => (int?)i.Invoiceid) ?? 0;
            invoice.Invoiceid = maxInvoiceId + 1;

            // Set CreateDateTime and LastModifiedDateTime
            invoice.Createdatetime = DateTime.UtcNow;
            invoice.Lastmodifieddatetime = invoice.Createdatetime;

            // Auto-update status to "Overdue" if necessary
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
        })
        .WithName("CreateInvoice")
        .WithOpenApi();

        // Delete Invoice
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int invoiceid, OdaDbContext db) =>
        {
            var affected = await db.Invoices
                .Where(model => model.Invoiceid == invoiceid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteInvoice")
        .WithOpenApi();

        // Get Bookings Related to an Invoice
        group.MapGet("/{id}/booking", async Task<Results<Ok<Booking>, NotFound>> (int invoiceid, OdaDbContext db) =>
        {
            var invoice = await db.Invoices.Include(i => i.Booking).FirstOrDefaultAsync(i => i.Invoiceid == invoiceid);
            if (invoice == null || invoice.Booking == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(invoice.Booking);
        })
        .WithName("GetInvoiceBooking")
        .WithOpenApi();

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
        })
        .WithName("UpdateInvoiceStatus")
        .WithOpenApi();
    }
}