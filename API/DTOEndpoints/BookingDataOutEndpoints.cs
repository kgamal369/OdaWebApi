using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using OdaWepApi.Domain.Models;
using OdaWepApi.Infrastructure;
using OdaWepApi.DataFlows;

namespace OdaWepApi.API.DTOEndpoints
{
    public static class BookingDataOutEndpoints
    {
        public static void MapBookingDataOutEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/BookingDataOut").WithTags(nameof(BookingDataOut));

            group.MapGet("/{bookingID}", async (int bookingID, OdaDbContext db) =>
            {
                // Call the GetBookingDataOut method
                var bookingDataOut = await BookingDataOutServices.GetBookingDataOut(db, bookingID);

                // Return the result
                return bookingDataOut != null ? Results.Ok(bookingDataOut) : Results.NotFound();
            });
        }
    }
}

