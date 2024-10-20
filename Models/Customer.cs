using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Customer
{
    [Key]
    public int Customerid { get; set; }

    [Required(ErrorMessage = "Customer First Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer First Name must be between 2 and 100 characters.")]
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public string? Address { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}


public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Customer").WithTags(nameof(Customer));

        // Get all Customers
        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

        // Get Customer by Id
        group.MapGet("/{id}", async Task<Results<Ok<Customer>, NotFound>> (int customerid, OdaDbContext db) =>
        {
            return await db.Customers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Customerid == customerid)
                is Customer model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        // Update Customer
        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int customerid, Customer customer, OdaDbContext db) =>
        {
            // Update LastModifiedDateTime
            customer.Lastmodifieddatetime = DateTime.UtcNow;

            var affected = await db.Customers
                .Where(model => model.Customerid == customerid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Customerid, customer.Customerid)
                  .SetProperty(m => m.Firstname, customer.Firstname)
                  .SetProperty(m => m.Lastname, customer.Lastname)
                  .SetProperty(m => m.Email, customer.Email)
                  .SetProperty(m => m.Phonenumber, customer.Phonenumber)
                  .SetProperty(m => m.Address, customer.Address)
                //  .SetProperty(m => m.Createdatetime, customer.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, customer.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        // Create Customer
        group.MapPost("/", async (Customer customer, OdaDbContext db) =>
        {
            // Set Customer ID to MaxCustomerId + 1
            var maxCustomerId = await db.Customers.MaxAsync(c => (int?)c.Customerid) ?? 0;
            customer.Customerid = maxCustomerId + 1;

            // Set CreateDateTime and LastModifiedDateTime
            customer.Createdatetime = DateTime.UtcNow;
            customer.Lastmodifieddatetime = customer.Createdatetime;

            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Customer/{customer.Customerid}", customer);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();


        // Delete Customer
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int customerid, OdaDbContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.Customerid == customerid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();

        // Get All Bookings Related to Customer
        group.MapGet("/{id}/bookings", async Task<IResult> (int customerid, OdaDbContext db) =>
        {
            var bookings = await db.Bookings.Where(b => b.Customerid == customerid).ToListAsync();

            if (bookings == null || !bookings.Any())
                return TypedResults.NotFound();

            return TypedResults.Ok(bookings);
        })
        .WithName("GetCustomerBookings")
        .WithOpenApi();

        // Get All Invoices Related to Customer
        group.MapGet("/{id}/invoices", async Task<IResult> (int customerid, OdaDbContext db) =>
        {
            var invoices = await db.Invoices
                .Where(i => db.Bookings.Any(b => b.Bookingid == i.Bookingid && b.Customerid == customerid))
                .ToListAsync();

            if (invoices == null || !invoices.Any())
                return TypedResults.NotFound();

            return TypedResults.Ok(invoices);
        })
        .WithName("GetCustomerInvoices")
        .WithOpenApi();

    }
}