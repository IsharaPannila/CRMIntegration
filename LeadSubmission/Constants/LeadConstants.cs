using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeadSubmission.Constants
{
    public class LeadConstants
    {
        //Connection Parameters
        public const string UserName  = "username";
        public const string Password  = "password";
        public const string Domain  = "domain";
        public const string CrmSdkUrl  = "crmSdkURL";
        public const string MetaDataURL = "metaDataURL";
        public const string Organisation  = "organisation";

        //Lead Attributes
        public const string AttrEmail = "emailaddress2";
        public const string AttrLeadStatus = "statecode";
        public const string AttrMobile= "mobilephone";
        public const string AttrFirstName = "firstname";
        public const string AttrLeadSource = "ms_leadsourcedisplayvalue";
        public const string AttrCreatedOn = "createdon";
        public const string AttrModifiedOn = "modifiedon";
        public const string AttrReactivateOn = "ms_reactivatedate";  
        public const string AttrLeadID = "leadid";

        //Error Messages
        public const string NoEmpCode = "EmployerCode/Name not supplied.";
        public const string NoMobileNumber = "Mobile Number to be provided as preferred contact.";
        public const string NoOtherPhoneNumber = "Other Phone Number to be provided as preferred contact.";

        //Others
        public const string StatusOpen = "open";
        public const string AdminUpload = "Administration Upload";


    }

}