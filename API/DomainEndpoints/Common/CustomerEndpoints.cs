using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.Common;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.Common
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Customer").WithTags(nameof(Customer));

            // Get all Customers
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Customers.ToListAsync()
            ).WithName("GetAllCustomers").WithOpenApi();

            // Get Customer by Id
            group.MapGet("/{id}", async Task<Results<Ok<Customer>, NotFound>> (int customerid, OdaDbContext db) =>
                await db.Customers.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Customerid == customerid)
                    is Customer customer
                        ? TypedResults.Ok(customer)
                        : TypedResults.NotFound()
            ).WithName("GetCustomerById").WithOpenApi();

            // Update Customer
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int customerid, Customer customer, OdaDbContext db) =>
            {
                customer.Lastmodifieddatetime = DateTime.UtcNow;

                var affected = await db.Customers
                    .Where(c => c.Customerid == customerid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(c => c.Firstname, customer.Firstname)
                        .SetProperty(c => c.Lastname, customer.Lastname)
                        .SetProperty(c => c.Email, customer.Email)
                        .SetProperty(c => c.Phonenumber, customer.Phonenumber)
                        .SetProperty(c => c.Address, customer.Address)
                        .SetProperty(c => c.Lastmodifieddatetime, customer.Lastmodifieddatetime));

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("UpdateCustomer").WithOpenApi();

            // Create Customer
            group.MapPost("/", async (Customer customer, OdaDbContext db) =>
            {
                var maxCustomerId = await db.Customers.MaxAsync(c => (int?)c.Customerid) ?? 0;
                customer.Customerid = maxCustomerId + 1;
                customer.Createdatetime = DateTime.UtcNow;
                customer.Lastmodifieddatetime = customer.Createdatetime;

                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Customer/{customer.Customerid}", customer);
            }).WithName("CreateCustomer").WithOpenApi();

            // Delete Customer
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int customerid, OdaDbContext db) =>
            {
                var affected = await db.Customers
                    .Where(c => c.Customerid == customerid)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            }).WithName("DeleteCustomer").WithOpenApi();

            // Get All Bookings for a Customer
            group.MapGet("/{id}/bookings", async Task<IResult> (int customerid, OdaDbContext db) =>
            {
                var bookings = await db.Bookings.Where(b => b.Customerid == customerid).ToListAsync();
                return bookings.Any() ? TypedResults.Ok(bookings) : TypedResults.NotFound();
            }).WithName("GetCustomerBookings").WithOpenApi();

            // Get All Invoices for a Customer
            group.MapGet("/{id}/invoices", async Task<IResult> (int customerid, OdaDbContext db) =>
            {
                var invoices = await db.Invoices
                    .Where(i => db.Bookings.Any(b => b.Bookingid == i.Bookingid && b.Customerid == customerid))
                    .ToListAsync();

                return invoices.Any() ? TypedResults.Ok(invoices) : TypedResults.NotFound();
            }).WithName("GetCustomerInvoices").WithOpenApi();
        }
    }
}
