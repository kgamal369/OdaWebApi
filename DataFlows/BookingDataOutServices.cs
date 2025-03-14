﻿using Microsoft.EntityFrameworkCore;
using OdaWepApi.Infrastructure;
using OdaWepApi.Domain.Models;
using OdaWepApi.Domain.DTOs;
using OdaWepApi.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(b => b.Questions)
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

            var selectedQuestions = await db.Questions
                .Where(q => q.Bookingid == bookingID)
                .AsNoTracking()
                .ToListAsync();

            var unitTypeName = await db.Unittypes
                .Where(k => k.Unittypeid == apartment.Unittypeid)
                .Select(k => k.UnittypeName)
                .FirstOrDefaultAsync() ?? "N/A";

            var totalAddonPrice = addonDetails.Sum(a => a.Price);
            var totalPrice_Addons_Plan = totalPlanPrice + totalAddonPrice;

            bool isEqualPayment = paymentPlan.Paymentplanid != 1 && paymentPlan.Paymentplanid != 2;
            decimal interestRateValuePerYear = 0;
            decimal totalInterestRateValue = 0;
            decimal totalAmount = totalPrice_Addons_Plan;

            if (isEqualPayment)
            {
                interestRateValuePerYear = (decimal)(paymentPlan.Interestrateperyearpercentage / 100 * totalPrice_Addons_Plan);
                totalInterestRateValue = interestRateValuePerYear * (paymentPlan.Numberofinstallmentmonths / 12);
                totalAmount += totalInterestRateValue;
            }

            var paymentDTO = new PaymentDTO
            {
                Paymentplanid = paymentPlan.Paymentplanid,
                Paymentplanname = paymentPlan.Paymentplanname,
                Numberofinstallmentmonths = paymentPlan.Numberofinstallmentmonths,
                Downpayment = paymentPlan.Downpayment,
                Downpaymentpercentage = paymentPlan.Downpaymentpercentage,
                DPValue = paymentPlan.Downpaymentpercentage / 100 * totalPrice_Addons_Plan,
                Adminfees = paymentPlan.Adminfees,
                Adminfeespercentage = paymentPlan.Adminfeespercentage,
                AdminfeesValue = paymentPlan.Adminfeespercentage / 100 * totalPrice_Addons_Plan,
                Interestrate = paymentPlan.Interestrate,
                Interestrateperyearpercentage = paymentPlan.Interestrateperyearpercentage,
                EqualPayment = isEqualPayment,
                InterestrateValuePerYear = interestRateValuePerYear,
                TotalInterestrateValue = totalInterestRateValue,
                InstallmentDTO = paymentPlan.Installmentbreakdowns.Select(id => new InstallmentDTO
                {
                    Installmentmonth = id.Installmentmonth,
                    Installmentpercentage = id.Installmentpercentage,
                    Installmentvalue = totalAmount / paymentPlan.Numberofinstallmentmonths
                }).ToList()
            };

            return new BookingDataOut
            {
                BookingID = booking.Bookingid,
                DeveloperID = apartment.Developerid ?? 0,
                ProjectID = apartment.Projectid ?? 0,
                NewApartmentID = apartment?.Apartmentid ?? 0,
                ApartmentType = (ApartmentType)(apartment?.Apartmenttype),
                ApartmentAddress = apartment.Apartmentaddress,
                ApartmentSpace = apartment.Apartmentspace ?? 0,
                Unittypeid = apartment.Unittypeid ?? 0,
                UnittypeName = unitTypeName,
                PlanID = plan?.Planid ?? 0,
                PlanName = plan?.Planname ?? "N/A",
                TotalPlanPrice = totalPlanPrice,
                Addons = addonDetails,
                SumOfTotalAddonPrices = totalAddonPrice,
                AutomationID = apartment.Automationid,
                CustomerInfo = booking.Customer ?? new Customer(),
                questions = selectedQuestions,
                TotalAmount = totalAmount,
                TotalAmount_Addons_plan = totalPrice_Addons_Plan,
                paymentDTO = paymentDTO
            };
        }
    }
}