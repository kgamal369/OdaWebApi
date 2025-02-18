using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class QuestionEndpoints
    {
        public static void MapQuestionEndpoints(IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Questions").WithTags(nameof(Question));

            // Get all Questions
            group.MapGet("/", async (OdaDbContext db) =>
            {
                var questions = await db.Questions.ToListAsync();
                return TypedResults.Ok(questions);
            })
            .WithName("GetAllQuestions")
            .WithOpenApi();

            // Get Question by Id
            group.MapGet("/{id:int}", async Task<IResult> (int id, OdaDbContext db) =>
            {
                var question = await db.Questions.AsNoTracking()
                    .FirstOrDefaultAsync(q => q.Questionsid == id);

                return question != null ? TypedResults.Ok(question) : TypedResults.NotFound();
            })
            .WithName("GetQuestionById")
            .WithOpenApi();

            // Get all Questions of a specific Booking Id
            group.MapGet("/booking/{bookingId:int}", async Task<IResult> (int bookingId, OdaDbContext db) =>
            {
                var questions = await db.Questions.AsNoTracking()
                    .Where(q => q.Bookingid == bookingId)
                    .ToListAsync();

                return questions.Any() ? TypedResults.Ok(questions) : TypedResults.NotFound();
            })
            .WithName("GetQuestionsOfBookingId")
            .WithOpenApi();
        }
    }
}