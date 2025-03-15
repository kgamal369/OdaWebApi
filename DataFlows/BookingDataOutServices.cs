using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.DataFlows
{
    public class BookingDataOutServices
    {
        public static async Task<BookingDataOut> GetBookingDataOut(OdaDbContext db, int bookingID)
        {
            var booking = await db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                // .ThenInclude(a => a.Project)
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Plan)
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Automation)
                .Include(b => b.Paymentplan)
                    .ThenInclude(p => p.Installmentbreakdowns)
                .Include(b => b.Customer)
                .Include(b => b.Questions)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null) return null;

            var apartment = booking.Apartment;
            if (apartment == null) return null; // Additional null check

            var plan = apartment?.Plan;
            var paymentPlan = booking.Paymentplan;
            if (paymentPlan == null) return null;

            var customer = booking.Customer;

            var totalPlanPrice = (plan?.Pricepermeter ?? 0) * (apartment?.Apartmentspace ?? 0);

            var addons = await db.ApartmentAddons
                .Where(aa => aa.Apartmentid == apartment.Apartmentid)
                .Include(aa => aa.Addon)
                .AsNoTracking()
                .ToListAsync();


            var addonDetails = addons.Select(aa => new AddonDetail
            {
                AddonID = aa.Addonid,
                AddonName = aa.Addon.Addonname,
                Addongroup = aa.Addon.Addongroup,
                Unitormeter = aa.Addon.Unitormeter,
                Description = aa.Addon.Description,
                Quantity = aa.Quantity,
                Price = (decimal)(aa.Addon.Unitormeter == UnitOrMeterType.Unit ? aa.Addon.Price * aa.Quantity : aa.Addon.Price * apartment.Apartmentspace)
            }).ToList();


            var totalAddonPrice = addonDetails.Sum(a => a.Price);

            var addonPerRequests = await db.ApartmentAddonperrequests
                .Where(aapr => aapr.Apartmentid == apartment.Apartmentid)
                .Include(aapr => aapr.Addperrequest)
                .AsNoTracking()
                .ToListAsync();

            var addonPerRequestDetails = addonPerRequests.Select(aapr => new AddonPerRequestDetail
            {
                AddonPerRequestID = aapr.Addperrequestid,
                AddonPerRequestName = aapr.Addperrequest.Addperrequestname,
                AddonPerRequestDescription = aapr.Addperrequest.Description
            }).ToList();

            var selectedquestions = await db.Questions.AsNoTracking()
                    .Where(q => q.Bookingid == bookingID)
                    .ToListAsync();

            var UnittypeNames =
                        apartment?.Unittypeid != null
                        ? await db.Unittypes.AsNoTracking()
                    .Where(k => k.Unittypeid == apartment.Unittypeid).
                    Select(k => k.UnittypeName).FirstOrDefaultAsync() : null;

            // Calculate total price first
            var totalprice_Addons_plan = totalPlanPrice + totalAddonPrice;

            // Calculate InterestrateValue first before using it
            var interestrateValue = paymentPlan.Interestrateperyearpercentage / 100 * totalprice_Addons_plan;

            var paymentDTO = new PaymentDTO
            {
                Paymentplanid = paymentPlan.Paymentplanid,
                Paymentplanname = paymentPlan.Paymentplanname,
                Numberofinstallmentmonths = paymentPlan.Numberofinstallmentmonths,
                Downpayment = paymentPlan.Downpayment,
                Downpaymentpercentage = paymentPlan.Downpaymentpercentage,
                DPValue = paymentPlan.Downpaymentpercentage / 100 * totalprice_Addons_plan,

                Adminfees = paymentPlan.Adminfees,
                Adminfeespercentage = paymentPlan.Adminfeespercentage,
                AdminfeesValue = paymentPlan.Adminfeespercentage / 100 * totalprice_Addons_plan,

                Interestrate = paymentPlan.Interestrate,
                Interestrateperyearpercentage = paymentPlan.Interestrateperyearpercentage,
                InterestrateValuePerYear = interestrateValue, // Assign the precomputed value
                TotalInterestrateValue = interestrateValue * (paymentPlan.Numberofinstallmentmonths / 12), // Assign the precomputed value

                EqualPayment = paymentPlan.Paymentplanid != 1 && paymentPlan.Paymentplanid != 2,
                InstallmentDTO = paymentPlan.Installmentbreakdowns.Select(id => new InstallmentDTO
                {
                    Installmentmonth = id.Installmentmonth,
                    Installmentpercentage = id.Installmentpercentage,
                    Installmentvalue = (decimal)((totalprice_Addons_plan + interestrateValue) * id.Installmentpercentage / 100)
                }).ToList()
            };

            return new BookingDataOut
            {
                BookingID = booking.Bookingid,
                DeveloperID = apartment?.Developerid ?? 0,
                ProjectID = apartment?.Projectid ?? 0,
                NewApartmentID = apartment?.Apartmentid ?? 0,
                ApartmentType = (ApartmentType)(apartment?.Apartmenttype),
                ApartmentAddress = apartment?.Apartmentaddress,
                ApartmentSpace = apartment?.Apartmentspace ?? 0,
                Unittypeid = apartment?.Unittypeid ?? 0,
                UnittypeName = UnittypeNames?.ToString() ?? "N/A",
                PlanID = plan?.Planid ?? 0,
                PlanName = plan?.Planname ?? "N/A",
                TotalPlanPrice = totalPlanPrice,
                Addons = addonDetails,
                SumOfTotalAddonPrices = totalAddonPrice,
                AutomationID = apartment.Automationid,
                AddonPerRequests = addonPerRequestDetails,
                CustomerInfo = customer ?? new Customer(), // Ensure non-null assignment
                questions = selectedquestions,
                TotalAmount = (decimal)(totalprice_Addons_plan + paymentDTO.TotalInterestrateValue),
                TotalAmount_Addons_plan = totalprice_Addons_plan,
                paymentDTO = paymentDTO
            };
        }
    }
}
