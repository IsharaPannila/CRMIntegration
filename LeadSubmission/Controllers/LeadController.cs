using LeadSubmission.Models;
using System.Web.Http;
using LeadSubmission.Business;
using LeadSubmission.BusinessObjects;
using System;

namespace LeadSubmission.Controllers
{
    public class LeadController : ApiController
    {
        LeadProcess leadFunction = new LeadProcess();

        //LeadInfo mLeadInfo = new BusinessObjects.LeadInfo()
        //{
        //    ActivityType = "ActivityType",
        //    AnnualGrossSalary = 500000,
        //    Brand = "Brand",
        //    CampaignSource = "CampaignSource",
        //    CreateOn = DateTime.Today,
        //    Email = "nisha.rajan@softvision.com",
        //    EmployerCode = "REMYL",
        //    EmployerName = "EmployerName",
        //    Firstname = "Test",
        //    KMPA = 001,
        //    LeadDetails = "Adding test note",
        //    LeadOriginatorName = "LeadOriginatorName",
        //    LeadRating = "LeadRating",
        //    LeadSource = "Administration Upload",
        //    LeaseTerm = 5,
        //    MobilePhone = "+919538998011",
        //    MobilePreferredContact = true,
        //    Model = "Model",
        //    OtherInformation = "OtherInformation",
        //    OtherPhone = "123456",
        //    PreferredCallBackTime = "4.00PM",
        //    SiteID = "SiteID",
        //    State = "State",
        //    Surname = "Nisha",
        //    UpdateOnly = false,
        //    VehicleMake = "VehicleMake",
        //    VehicleType = "VehicleType",
        //    VehicleYear = 2017,
        //    VIP = false
        //};



        [HttpPost]
        //[HttpGet]
        //public ReturnInfo SubmitLeadInfo()
       public ReturnInfo SubmitLeadInfo(LeadInfo mLeadInfo)
        {
            ReturnInfo mRetInfo = new ReturnInfo() { };

            leadFunction.CreateServiceConnection();

            if (leadFunction.ValidateLeadContact(mLeadInfo, ref mRetInfo))
            {
                leadFunction.ProcessLeadInfo(mLeadInfo,ref mRetInfo);
            }

            return mRetInfo;
        }

        
    }
}
