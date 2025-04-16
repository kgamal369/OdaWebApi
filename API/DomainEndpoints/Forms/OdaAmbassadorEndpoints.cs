using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.DataFlows;
using OdaWepApi.Domain.Models.Forms;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints.Forms
{
    public static class OdaambassadorEndpoints
    {
        public static void MapOdaAmbassadorEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Odaambassador").WithTags(nameof(Odaambassador));

            // Get all Roles
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Odaambassadors.ToListAsync()
            ).WithName("GetAllOdaAmbassadors").WithOpenApi();

            // Create Role
            group.MapPost("/", async (Odaambassador oda, OdaDbContext db) =>
            {
                db.Odaambassadors.Add(oda);
                await db.SaveChangesAsync();
                // Get Data for email
                string emailBody = EmailService.GenerateEmailOdaAmbassadorBody(oda);
                await EmailService.SendEmailToAllRecipients("Oda Ambassador Email!", emailBody);

                return TypedResults.Created($"/api/Odaambassador/{oda.Odaambassadorid}", oda);


            }).WithName("CreateOdaAmbassador").WithOpenApi();
        }
    }
}