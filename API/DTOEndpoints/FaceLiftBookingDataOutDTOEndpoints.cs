using OdaWepApi.Domain.DTOs;
using OdaWepApi.Infrastructure;
using OdaWepApi.DataFlows;
using OdaWepApi.Domain.DTOs.FaceLiftDTO;

namespace OdaWepApi.API.DTOEndpoints
{
    public static class FaceLiftBookingDataOutDTOEndpoints
    {
        public static void MapFaceLiftBookingDataOutDTOEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/FaceLiftBookingDataOutDTO").WithTags(nameof(FaceLiftBookingDataOutDTO));

            group.MapGet("/{bookingId}", async (int bookingId, OdaDbContext db) =>
            {
                // Call the GetFaceLiftBookingDataOut method
                var bookingDataOut = await FaceLiftBookingDataOutServices.GetFaceLiftBookingDataOutDTO(db, bookingId);

                // Return the result
                return bookingDataOut != null ? Results.Ok(bookingDataOut) : Results.NotFound();
            });
        }
    }
}