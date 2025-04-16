using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models.Forms;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.Forms
{
    public static class QuestionEndpoints
    {
        public static void MapQuestionEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Questions").WithTags(nameof(Question));

            // Get all Questions
            group.MapGet("/", async (OdaDbContext db) =>
            {
                return await db.Questions.AsNoTracking().ToListAsync();
            })
            .WithName("GetAllQuestions")
            .WithOpenApi();

            // ✅ Get all Questions with their Answers
            group.MapGet("/with-answers", async (OdaDbContext db) =>
            {
                var questionsWithAnswers = await db.Questions
                    .Include(q => q.Answers)
                    .Select(q => new
                    {
                        q.Questionid,
                        q.Questiontext,
                        Answers = q.Answers.Select(a => new
                        {
                            a.Answerid,
                            a.Answercode,
                            a.Answertext,
                            AnswerPhotoBase64 = a.Answerphoto != null ? Convert.ToBase64String(a.Answerphoto) : null
                        }).ToList()
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return TypedResults.Ok(questionsWithAnswers);
            })
            .WithName("GetAllQuestionAnswers")
            .WithOpenApi();

            // Get Question by Id
            group.MapGet("/{id}", async Task<Results<Ok<Question>, NotFound>> (int id, OdaDbContext db) =>
            {
                var question = await db.Questions.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Questionid == id);
                return question is not null
                    ? TypedResults.Ok(question)
                    : TypedResults.NotFound();
            })
            .WithName("GetQuestionById")
            .WithOpenApi();

            // Create a new Question
            group.MapPost("/", async Task<IResult> (Question question, OdaDbContext db) =>
            {
                db.Questions.Add(question);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Questions/{question.Questionid}", question);
            })
            .WithName("CreateQuestion")
            .WithOpenApi();

            // Update a Question
            group.MapPut("/{id:int}", async Task<IResult> (int id, Question updatedQuestion, OdaDbContext db) =>
            {
                var question = await db.Questions.FindAsync(id);
                if (question == null) return TypedResults.NotFound();

                question.Questiontext = updatedQuestion.Questiontext;
                question.Createdat = updatedQuestion.Createdat;
                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            })
            .WithName("UpdateQuestion")
            .WithOpenApi();

            // Delete a Question
            group.MapDelete("/{id:int}", async Task<IResult> (int id, OdaDbContext db) =>
            {
                var question = await db.Questions.FindAsync(id);
                if (question == null) return TypedResults.NotFound();

                db.Questions.Remove(question);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            })
            .WithName("DeleteQuestion")
            .WithOpenApi();
        }
    }
}
