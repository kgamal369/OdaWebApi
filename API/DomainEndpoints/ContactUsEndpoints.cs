using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.DataFlows;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DomainEndpoints
{
    public static class contactusEndpoints
    {
        public static void MapContactUsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/contactus").WithTags(nameof(Contactus));

            // Get all Roles
            group.MapGet("/", async (OdaDbContext db) =>
                await db.Contactus.ToListAsync()
            ).WithName("GetAllContactUs").WithOpenApi();

            // Create Role
            group.MapPost("/", async (Contactus contact, OdaDbContext db) =>
            {
                db.Contactus.Add(contact);
                await db.SaveChangesAsync();
                string emailBody = EmailService.GenerateEmailContactUsBody(contact);
                await EmailService.SendEmailToAllRecipients("ContactUs!", emailBody);

                return TypedResults.Created($"/api/contactus/{contact.Contactusid}", contact);
            }).WithName("CreateContactUs").WithOpenApi();
        }
    }
}