using Microsoft.AspNetCore.Http.HttpResults;
using OdaWepApi.DataFlows;
using OdaWepApi.Domain.DTOs.FaceLiftDTO;
using OdaWepApi.Infrastructure;

namespace OdaWepApi.API.DTOEndpoints
{
    public static class FaceLiftBookingDataOutDTOEndpoints
    {
        public static void MapFaceLiftBookingDataOutDTOEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/FaceLiftBookingDataOutDTO").WithTags(nameof(FaceLiftBookingDataOutDTO));

            group.MapGet("/{bookingId}", async Task<Results<Ok<FaceLiftBookingDataOutDTO>, NotFound>> (int bookingId, OdaDbContext db) =>
            {
                var bookingDataOut = await FaceLiftBookingDataOutServices.GetFaceLiftBookingDataOutDTO(db, bookingId);

                return bookingDataOut != null
                    ? TypedResults.Ok(bookingDataOut)
                    : TypedResults.NotFound();
            });
        }
    }
}
