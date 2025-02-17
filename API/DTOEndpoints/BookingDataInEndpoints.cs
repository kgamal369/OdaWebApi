using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;
using OdaWepApi.DataFlows;

namespace OdaWepApi.API.DTOEndpoints
{   
    public static class BookingDataInEndpoints
    {
        public static void MapBookingDataInEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/BookingDataIn").WithTags(nameof(BookingDataIn));

            group.MapPost("/", async (BookingDataIn bookingDataIn, OdaDbContext db) =>
            {
                try
                {
                    // Call the service to create booking data
                    int bookingId = await BookingDataInServices.CreateBookingDataIn(db, bookingDataIn);

                    // Return the booking ID
                    return Results.Ok(new { BookingID = bookingId });
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return a bad request with the error message
                    return Results.BadRequest(new { Error = ex.Message });
                }
            })
            .WithName("CreateBookingDataIn")
            .WithOpenApi();


            group.MapPut("/{bookingID}", async (int bookingID, BookingDataIn bookingDataIn, OdaDbContext db) =>
            {
                try
                {
                    int updatedBookingId = await BookingDataInServices.UpdateBookingDataIn(db, bookingID, bookingDataIn);
                    return Results.Ok(new { BookingID = updatedBookingId });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Error = ex.Message });
                }
            }).WithName("UpdateBookingDataIn").WithOpenApi();
        }

    }
}
