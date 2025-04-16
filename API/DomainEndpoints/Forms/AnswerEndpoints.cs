using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.Forms;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.Forms
{
    public static class AnswerEndpoints
    {
        public static void MapAnswerEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Answers").WithTags(nameof(Answer));

            // Get all Answers
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Answers.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllAnswers")
            .WithOpenApi();

            // Get Addon by ID
            group.MapGet("/{id}", async Task<Results<Ok<Answer>, NotFound>> (int id, OdaDbContext db) =>
            {
                var answer = await db.Answers.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Answerid == id);
                return answer is not null
                    ? TypedResults.Ok(answer)
                    : TypedResults.NotFound();
            })
            .WithName("GetAnswerById")
            .WithOpenApi();

            // Create an Answer
            group.MapPost("/", async Task<IResult> (Answer answer, OdaDbContext db) =>
            {
                db.Answers.Add(answer);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Answers/{answer.Answerid}", answer);
            })
            .WithName("CreateAnswer")
            .WithOpenApi();

            // Update an Answer
            group.MapPut("/{id:int}", async Task<IResult> (int id, Answer updatedAnswer, OdaDbContext db) =>
            {
                var answer = await db.Answers.FindAsync(id);
                if (answer == null) return TypedResults.NotFound();

                answer.Answertext = updatedAnswer.Answertext;
                answer.Answercode = updatedAnswer.Answercode;
                answer.Createdat = updatedAnswer.Createdat;
                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            })
            .WithName("UpdateAnswer")
            .WithOpenApi();

            // Delete an Answer
            group.MapDelete("/{id:int}", async Task<IResult> (int id, OdaDbContext db) =>
            {
                var answer = await db.Answers.FindAsync(id);
                if (answer == null) return TypedResults.NotFound();

                db.Answers.Remove(answer);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            })
            .WithName("DeleteAnswer")
            .WithOpenApi();
        }
    }
}