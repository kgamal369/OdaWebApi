using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Invoice
{
    public int Invoiceid { get; set; }

    public int? Bookingid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public decimal? Invoiceamount { get; set; }

    public string? Invoicestatus { get; set; }

    public DateTime? Invoiceduedate { get; set; }

    public virtual Booking? Booking { get; set; }
}


public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Invoice").WithTags(nameof(Invoice));

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Invoices.ToListAsync();
        })
        .WithName("GetAllInvoices")
        .WithOpenApi();

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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int invoiceid, Invoice invoice, OdaDbContext db) =>
        {
            var affected = await db.Invoices
                .Where(model => model.Invoiceid == invoiceid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Invoiceid, invoice.Invoiceid)
                  .SetProperty(m => m.Bookingid, invoice.Bookingid)
                  .SetProperty(m => m.Createdatetime, invoice.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, invoice.Lastmodifieddatetime)
                  .SetProperty(m => m.Invoiceamount, invoice.Invoiceamount)
                  .SetProperty(m => m.Invoicestatus, invoice.Invoicestatus)
                  .SetProperty(m => m.Invoiceduedate, invoice.Invoiceduedate)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateInvoice")
        .WithOpenApi();

        group.MapPost("/", async (Invoice invoice, OdaDbContext db) =>
        {
            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Invoice/{invoice.Invoiceid}", invoice);
        })
        .WithName("CreateInvoice")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int invoiceid, OdaDbContext db) =>
        {
            var affected = await db.Invoices
                .Where(model => model.Invoiceid == invoiceid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteInvoice")
        .WithOpenApi();
    }
}