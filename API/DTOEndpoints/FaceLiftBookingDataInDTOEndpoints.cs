using OdaWepApi.Domain.DTOs;
using OdaWepApi.Infrastructure;
using OdaWepApi.DataFlows;
using OdaWepApi.Domain.DTOs.FaceLiftDTO;

namespace OdaWepApi.API.DTOEndpoints
{
    public static class FaceLiftBookingDataInDTOEndpoints
    {
        public static void MapFaceLiftBookingDataInDTOEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/FaceLiftBookingDataIn").WithTags(nameof(FaceLiftBookingDataInDTO));

            group.MapPost("/", async (FaceLiftBookingDataInDTO faceLiftBookingDataInDTO, OdaDbContext db) =>
            {
                try
                {
                    // Call the service to create booking data
                    int bookingId = await FaceLiftBookingDataInServices.CreateFaceLiftBookingDataIn(db, faceLiftBookingDataInDTO);

                    // Return the booking ID
                    return Results.Ok(new { BookingID = bookingId });
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return a bad request with the error message
                    return Results.BadRequest(new { Error = ex.Message });
                }
            }).WithName("CreateFaceLiftBookingDataIn")
            .WithOpenApi();

            group.MapPut("/{bookingID}", async (int bookingID, FaceLiftBookingDataInDTO faceLiftBookingDataInDTO, OdaDbContext db) =>
            {
                try
                {

                    int updatedBookingId = await FaceLiftBookingDataInServices.UpdateFaceLiftBookingDataIn(db, bookingID, faceLiftBookingDataInDTO);
                    var bookingDataOut = await FaceLiftBookingDataOutServices.GetFaceLiftBookingDataOutDTO(db, updatedBookingId);
                    return Results.Ok(bookingDataOut);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Error = ex.Message });
                }
            }).WithName("UpdateFaceLiftBookingDataIn").WithOpenApi();
        }
    }
}