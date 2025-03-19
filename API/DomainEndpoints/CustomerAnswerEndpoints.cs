using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class CustomerAnswerEndpoints
    {
        public static void MapCustomerAnswerEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/CustomerAnswers").WithTags(nameof(Customeranswer));

            // Get all Customer Answers
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Customeranswers.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllCustomerAnswers")
            .WithOpenApi();

            // Create a Customer Answer
            group.MapPost("/", async Task<IResult> (Customeranswer customerAnswer, OdaDbContext db) =>
            {
                db.Customeranswers.Add(customerAnswer);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/CustomerAnswers/{customerAnswer.Customeranswerid}", customerAnswer);
            })
            .WithName("CreateCustomerAnswer")
            .WithOpenApi();

            // Update a Customer Answer
            group.MapPut("/{id:int}", async Task<IResult> (int id, Customeranswer updatedCustomerAnswer, OdaDbContext db) =>
            {
                var customerAnswer = await db.Customeranswers.FindAsync(id);
                if (customerAnswer == null) return TypedResults.NotFound();

                customerAnswer.Bookingid = updatedCustomerAnswer.Bookingid;
                customerAnswer.Questionid = updatedCustomerAnswer.Questionid;
                customerAnswer.Answerid = updatedCustomerAnswer.Answerid;
                customerAnswer.Createdat = updatedCustomerAnswer.Createdat;
                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            })
            .WithName("UpdateCustomerAnswer")
            .WithOpenApi();

            // Delete a Customer Answer
            group.MapDelete("/{id:int}", async Task<IResult> (int id, OdaDbContext db) =>
            {
                var customerAnswer = await db.Customeranswers.FindAsync(id);
                if (customerAnswer == null) return TypedResults.NotFound();

                db.Customeranswers.Remove(customerAnswer);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            })
            .WithName("DeleteCustomerAnswer")
            .WithOpenApi();
        }
    }
}