using OdaWepApi.Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Infrastructure;
using OdaWepApi.DataFlows;

using OdaWepApi.Domain.DTOs.FaceLiftDTO;


namespace OdaWepApi.API.DTOEndpoints
{
    public static class BookingDataOutEndpoints
    {
        public static void MapBookingDataOutEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/BookingDataOut").WithTags(nameof(BookingDataOut));

            group.MapGet("/{bookingID}", async Task<Results<Ok<BookingDataOut>, NotFound>> (int bookingID, OdaDbContext db) =>
            {
                // Call the GetBookingDataOut method
                var bookingDataOut = await BookingDataOutServices.GetBookingDataOut(db, bookingID);

                // Return the result
                return bookingDataOut != null
                ? TypedResults.Ok(bookingDataOut)
                : TypedResults.NotFound();
            });
        }
    }
}

