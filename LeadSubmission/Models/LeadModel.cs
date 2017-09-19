using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeadSubmission.BusinessObjects;

namespace LeadSubmission.Models
{

    public class LeadModel
    {
        public LeadInfo LeadInfo;
        
    }

    public class ReturnInfo
    {
        public int ReturnCode { get; set; }
        public string LeadID { get; set; }
        public string ErrorMessage { get; set; }
    }
}