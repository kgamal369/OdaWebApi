using Microsoft.EntityFrameworkCore;
using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;

namespace OdaWepApi.DataFlows
{
    public class BookingDataOutServices
    {
        public static async Task<BookingDataOut?> GetBookingDataOut(OdaDbContext db, int bookingID)
        {
            var booking = await db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Plan)
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Automation)
                .Include(b => b.Paymentplan)
                    .ThenInclude(p => p.Installmentbreakdowns)
                .Include(b => b.Customeranswers)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Bookingid == bookingID);

            if (booking == null) return null;

            var apartment = booking.Apartment;
            if (apartment == null) return null;

            var plan = apartment.Plan;
            var paymentPlan = booking.Paymentplan ?? throw new System.Exception("Payment plan not found.");

            var totalPlanPrice = (plan?.Pricepermeter ?? 0) * (apartment.Apartmentspace ?? 0);

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
                Price = (decimal)((aa.Addon.Unitormeter == UnitOrMeterType.Unit)
                        ? aa.Addon.Price * aa.Quantity
                        : aa.Addon.Price * (apartment.Apartmentspace ?? 0))
            }).ToList();

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

            // ✅ Get the selected CustomerAnswerDTO from the database
            var customerAnswers = await db.Customeranswers
                .Where(ca => ca.Bookingid == bookingID)
                .Include(ca => ca.Question)
                .Include(ca => ca.Answer)
                .Select(ca => new CustomerAnswersOutDTO
                {
                    Customeranswerid = ca.Customeranswerid,
                    Questionid = ca.Questionid,
                    Questiontext = ca.Question != null ? ca.Question.Questiontext : string.Empty,
                    Answerid = ca.Answerid,
                    Answertext = ca.Answer != null ? ca.Answer.Answertext : string.Empty,
                    Answercode = ca.Answer != null ? ca.Answer.Answercode : ' '
                })
                .ToListAsync();

            var unitTypeName = await db.Unittypes
                .Where(k => k.Unittypeid == apartment.Unittypeid)
                .Select(k => k.UnittypeName)
                .FirstOrDefaultAsync() ?? "N/A";

            // var groupedRooms = await db.Faceliftrooms
            //     .Where(r => r.Apartmentid == apartment.Apartmentid)
            //     .GroupBy(r => r.Roomtype)
            //     .Select(g => new ApartmentRoomsDTO
            //     {
            //         RoomType = g.Key,
            //         Quantity = g.Count()
            //     })
            //     .ToListAsync();

            var totalAddonPrice = addonDetails.Sum(a => a.Price);

            var totalAirconditionerPrice = addonDetails
                .Where(a => a.Addongroup == "AirConditioning")
                .Sum(a => a.Price);
            var totalPrice_Addons_Plan = totalPlanPrice + totalAddonPrice;

            bool isEqualPayment = paymentPlan.Paymentplanid != 1 && paymentPlan.Paymentplanid != 2;
            decimal interestRateValuePerYear = 0;
            decimal totalInterestRateValue = 0;
            decimal totalAmount = totalPrice_Addons_Plan;
            List<decimal> installmentValues = new List<decimal>();

            // ✅ Ensure default values for null fields
            decimal downPaymentPercentage = paymentPlan.Downpaymentpercentage ?? 0;
            decimal adminFeesPercentage = paymentPlan.Adminfeespercentage ?? 0;
            decimal interestRatePerYear = paymentPlan.Interestrateperyearpercentage ?? 0;
            int numberOfInstallments = paymentPlan.Numberofinstallmentmonths > 0 ? paymentPlan.Numberofinstallmentmonths : 1;

            // ✅ Safe Calculations
            decimal DPValue = (downPaymentPercentage / 100) * totalPrice_Addons_Plan;
            decimal AdminFeesValue = (adminFeesPercentage / 100) * totalPrice_Addons_Plan;

            if (isEqualPayment)
            {
                if (paymentPlan.Interestrate)
                {
                    interestRateValuePerYear = (interestRatePerYear / 100) * totalPrice_Addons_Plan;
                    totalInterestRateValue = interestRateValuePerYear * (numberOfInstallments / 12);
                    totalAmount += totalInterestRateValue;
                }

                // ✅ Prevent division by zero
                decimal equalInstallment = totalAmount / numberOfInstallments;
                installmentValues = Enumerable.Repeat(equalInstallment, numberOfInstallments).ToList();
            }
            else
            {
                List<decimal> installmentPercentage = paymentPlan.Installmentbreakdowns
                    .Select(id => id.Installmentpercentage)
                    .ToList();

                foreach (var percentage in installmentPercentage)
                {
                    decimal value = (percentage / 100m) * totalAmount; // ✅ Safe calculation
                    installmentValues.Add(value);
                }
            }

            // ✅ Correctly assign each month a SINGLE installment value
            var paymentDTO = new PaymentDTO
            {
                Paymentplanid = paymentPlan.Paymentplanid,
                Paymentplanname = paymentPlan.Paymentplanname,
                Numberofinstallmentmonths = numberOfInstallments,
                Downpayment = paymentPlan.Downpayment,
                Downpaymentpercentage = downPaymentPercentage,
                DPValue = DPValue,
                Adminfees = paymentPlan.Adminfees,
                Adminfeespercentage = adminFeesPercentage,
                AdminfeesValue = AdminFeesValue,
                Interestrate = paymentPlan.Interestrate,
                Interestrateperyearpercentage = interestRatePerYear,
                EqualPayment = isEqualPayment,
                InterestrateValuePerYear = interestRateValuePerYear,
                TotalInterestrateValue = totalInterestRateValue,

                InstallmentDTO = paymentPlan.Installmentbreakdowns
                    .Select((id, index) => new InstallmentDTO
                    {
                        Installmentmonth = id.Installmentmonth,
                        Installmentpercentage = id.Installmentpercentage,
                        Installmentvalue = installmentValues.ElementAtOrDefault(index) // ✅ Assign correct value
                    }).ToList()
            };
            var apartmentDTO = new ApartmentDTO
            {
                ApartmentId = apartment?.Apartmentid ?? 0,
                ApartmentType = (int?)(apartment?.Apartmenttype),
                ApartmentAddress = apartment.Apartmentaddress,
                ApartmentSpace = apartment.Apartmentspace ?? 0,
                Unittypeid = apartment.Unittypeid ?? 0,
                UnittypeName = unitTypeName,
                //    ApartmentRooms = groupedRooms,
            };
            return new BookingDataOut
            {
                BookingID = booking.Bookingid,
                DeveloperID = apartment.Developerid ?? 0,
                ProjectID = apartment.Projectid ?? 0,
                ApartmentDTO = apartmentDTO,
                PlanID = plan?.Planid ?? 0,
                PlanName = plan?.Planname ?? "N/A",
                TotalPlanPrice = totalPlanPrice,
                Addons = addonDetails,
                SumOfTotalAddonPrices = totalAddonPrice,
                TotalAirconditionerPrice = totalAirconditionerPrice,
                AutomationID = apartment.Automationid,
                AddonPerRequests = addonPerRequestDetails,
                CustomerInfo = booking.Customer ?? new Customer(),
                CustomerAnswers = customerAnswers,
                TotalAmount = totalAmount,
                TotalAmount_Addons_plan = totalPrice_Addons_Plan,
                paymentDTO = paymentDTO
            };
        }
    }
}