using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Models;

namespace OdaWepApi.Models;

public partial class Customer
{
    public int Customerid { get; set; }

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

        group.MapGet("/", async (OdaDbContext db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int customerid, Customer customer, OdaDbContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.Customerid == customerid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Customerid, customer.Customerid)
                  .SetProperty(m => m.Firstname, customer.Firstname)
                  .SetProperty(m => m.Lastname, customer.Lastname)
                  .SetProperty(m => m.Email, customer.Email)
                  .SetProperty(m => m.Phonenumber, customer.Phonenumber)
                  .SetProperty(m => m.Address, customer.Address)
                  .SetProperty(m => m.Createdatetime, customer.Createdatetime)
                  .SetProperty(m => m.Lastmodifieddatetime, customer.Lastmodifieddatetime)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        group.MapPost("/", async (Customer customer, OdaDbContext db) =>
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Customer/{customer.Customerid}", customer);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int customerid, OdaDbContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.Customerid == customerid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}