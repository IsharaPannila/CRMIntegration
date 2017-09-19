using System;

namespace LeadSubmission.BusinessObjects
{
        public class LeadInfo
    {
        public string LeadOriginatorName { get; set; }
        public string Brand { get; set; }
        public string EmployerCode { get; set; }
        public string EmployerName { get; set; }
        public string SiteID { get; set; }
        public string LeadSource { get; set; }
        public string ActivityType { get; set; }
        public string CampaignSource { get; set; }
        public string State { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public Boolean MobilePreferredContact { get; set; }
        public string OtherPhone { get; set; }
        public string PreferredCallBackTime { get; set; }
        public Int64 AnnualGrossSalary { get; set; }
        public Boolean VIP { get; set; }
        public string LeadRating { get; set; }
        public string VehicleType { get; set; }
        public int VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public string Model { get; set; }
        public int LeaseTerm { get; set; }
        public int KMPA { get; set; }
        public string OtherInformation { get; set; }
        public string LeadDetails { get; set; }
        public Boolean UpdateOnly { get; set; }
        public DateTime CreateOn { get; set; }
    }
}