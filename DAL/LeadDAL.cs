using System.Collections.Generic;
using LeadSubmission.WebReferences.CrmSdk;
using System;
using LeadSubmission.BusinessObjects;
using LeadSubmission.WebReferences.CrmSdk.MetaData;

namespace LeadSubmission.DAL
{
    public class LeadDAL
    {
        public static DynamicEntity SelectLead(CrmService service, Dictionary<string, string> attributeDetails)
        {
            DynamicEntity de=null;
            try
            {
                var conditionExpressions = new List<ConditionExpression>();
                foreach (KeyValuePair<string,string>  attr in attributeDetails)
                {
                    var queryCondition = new ConditionExpression
                    {
                        AttributeName = attr.Key,
                        Operator = ConditionOperator.Equal,
                        Values = new object[] { attr.Value }
                    };
                    conditionExpressions.Add(queryCondition);
                }                

                var filterExpression = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions = conditionExpressions.ToArray()
                };

                var queryExpression = new QueryExpression
                {
                    EntityName = EntityName.lead.ToString(),
                    ColumnSet = new AllColumns(),
                    Criteria = filterExpression
                };

                var request = new RetrieveMultipleRequest { Query = queryExpression, ReturnDynamicEntities = true };

                var response = (RetrieveMultipleResponse)service.Execute(request);

                if (response.BusinessEntityCollection.BusinessEntities.Length > 0)
                {
                    de = (DynamicEntity)response.BusinessEntityCollection.BusinessEntities[0];
                }              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return de;
        }

        //public static void CreateLead(CrmService service, LeadInfo leadDetails, Guid leadID, DynamicEntity crmLeadInfo)
        //{
        //    string firstName = string.Empty;
        //    string lastName = string.Empty;

        //    try
        //    {
        //        var lead = new lead();

        //        foreach (Property p in crmLeadInfo.Properties)
        //        {
        //            if (p.Name == "firstname")
        //            {
        //                firstName = CrmHelper.GetSourceValue(p.GetType().Name, p);
        //            }
        //            if (p.Name == "lastname")
        //            {
        //                lastName = CrmHelper.GetSourceValue(p.GetType().Name, p);
        //            }
        //        }

        //        if (leadDetails.LeadOriginatorName == string.Concat(firstName, " ", lastName))
        //        {
        //            //lead.ms_bdm= matched CRM user
        //            lead.ms_staffreferrer = null;
        //            lead.ms_referralname = null;
        //        }
        //        else
        //        {
        //            lead.ms_bdm = null;
        //            lead.ms_staffreferrer = null;
        //            lead.ms_referralname = leadDetails.LeadOriginatorName;
        //        }


        //        var crmBrand = new Picklist();
        //        if (leadDetails.Brand.ToLower() == "maxxia")
        //        {
        //            crmBrand.Value = 0;
        //        }
        //        else { crmBrand.Value = 1; }
        //        lead.ms_brand = crmBrand;

        //        //var crmProductType = new Picklist();
        //        //crmProductType.Value = Convert.ToInt32("Fully Maintained Novated Lease");
        //        //lead.ms_producttype = crmProductType;

        //        //var crmOwnLead = new Picklist();
        //        //crmOwnLead.Value = Convert.ToInt32("No");
        //        //lead.ms_willyouownthelead = crmOwnLead;

        //        //var crmRating = new Picklist();
        //        //if (string.IsNullOrEmpty(leadDetails.LeadRating))
        //        //{
        //        //    crmRating.Value = Convert.ToInt32("Warm");
        //        //}
        //        //else
        //        //{
        //        //    crmRating.Value = Convert.ToInt32(leadDetails.LeadRating);
        //        //}
        //        //lead.leadqualitycode = crmRating;

        //        //var crmLeadSource = new Picklist();
        //        //crmLeadSource.Value = Convert.ToInt32(leadDetails.LeadSource);
        //        //lead.leadsourcecode = crmLeadSource;

        //        //var crmEnquirySource = new Picklist();
        //        //crmEnquirySource.Value = Convert.ToInt32(leadDetails.ActivityType);
        //        //lead.ms_enquirysource = crmEnquirySource;

        //        //var crmCampaignSource = new Picklist();
        //        //if (string.IsNullOrEmpty(leadDetails.CampaignSource ))
        //        //{
        //        //    crmCampaignSource.Value = Convert.ToInt32("None");
        //        //}
        //        //else
        //        //{
        //        //    crmCampaignSource.Value = Convert.ToInt32(leadDetails.CampaignSource);
        //        //}
        //        //lead.ms_campaignsource = crmCampaignSource;

        //        //if (!string.IsNullOrEmpty(leadDetails.VehicleType))
        //        //{
        //        //    var crmVehicleType = new Picklist();
        //        //    if (leadDetails.VehicleType == "New Car")
        //        //    {
        //        //        crmVehicleType.Value = Convert.ToInt32("New");
        //        //    }
        //        //    else if(leadDetails.VehicleType == "Used Car")
        //        //    {
        //        //        crmVehicleType.Value = Convert.ToInt32("Used(Dealer)");
        //        //    }
        //        //    else if(leadDetails.VehicleType == "Refinance Existing")
        //        //    {
        //        //        crmVehicleType.Value = Convert.ToInt32("Sales & Leaseback");
        //        //    }
        //        //    lead.ms_vehicletype = crmVehicleType;
        //        //}

        //        //if (!string.IsNullOrEmpty(leadDetails.EmployerCode))
        //        //{
        //        //var emp = new account { accountnumber = leadDetails.EmployerCode };
        //        //    var crmEmployer = new Lookup();
        //        //    if (emp.accountid != null)
        //        //    {
        //        //        //crmEmployer.Value = emp;
        //        //    }
        //        //    else
        //        //    {
        //        //        emp = new account { name = leadDetails.EmployerName };
        //        //        if (emp.accountid != null)
        //        //        {
        //        //            // crmEmployer.Value = emp;
        //        //        }
        //        //        else
        //        //        {
        //        //            // crmEmployer.Value = TestEmployer;
        //        //            //lead.TemPMessage;
        //        //        }
        //        //    }
        //        //    lead.ms_employer = crmEmployer;

        //        //    lead.ms_salarypackager = emp.ms_salarypackager1;
        //        //}

        //        //var crmState = new Picklist();
        //        //crmState.Value = Convert.ToInt32(leadDetails.State);
        //        //lead.ms_homeaddressstate = crmState;

        //        lead.firstname = leadDetails.Firstname;
        //        lead.lastname = leadDetails.Surname;
        //        lead.emailaddress2 = leadDetails.Email;
        //        lead.mobilephone = leadDetails.MobilePhone;

        //        //var crmPrefferedPhone = new Picklist();
        //        //if (leadDetails.MobilePreferredContact)
        //        //{
        //        //    crmPrefferedPhone.Value = Convert.ToInt32("Mobile");
        //        //}
        //        //else
        //        //{
        //        //    crmPrefferedPhone.Value = Convert.ToInt32("Work");
        //        //}
        //        //lead.ms_preferredphone = crmPrefferedPhone;

        //        if (!string.IsNullOrEmpty(leadDetails.OtherPhone)) lead.telephone1 = leadDetails.OtherPhone;

        //        lead.ms_preferredcontacttime = leadDetails.PreferredCallBackTime;

        //        var crmAnnualGS = new CrmDecimal();
        //        if (leadDetails.AnnualGrossSalary.ToString() != "")
        //        {
        //            crmAnnualGS.Value = leadDetails.AnnualGrossSalary;

        //            var crmRefferalSplit = new CrmDecimal();
        //            if (leadDetails.AnnualGrossSalary < 110000)
        //            {
        //                crmRefferalSplit.Value = (decimal)999.00;
        //            }
        //            else
        //            {
        //                //crmRefferalSplit.Value = null;
        //            }
        //            lead.ms_referralsplit = crmRefferalSplit;
        //        }
        //        else
        //        {
        //            crmAnnualGS.Value = 0;
        //        }
        //        lead.ms_annualgrosssalary = crmAnnualGS;


        //        //lead.siteid


        //        lead.ms_salesmessages = "Year: " + leadDetails.VehicleYear + "/ Make: " + leadDetails.VehicleMake + "/ Model: " + leadDetails.VehicleType + "\r\n" + "Lease Term: " + leadDetails.LeaseTerm + "\r\n * **\r\n" + leadDetails.OtherInformation;

        //        //var emp = new account { ownerid = new Owner { Value = "" }; };
        //        //lead.ownerid=




        //        var oppKey = new WebReferences.CrmSdk.Key { Value = leadID };
        //        lead.leadid = oppKey;

        //        service.Update(lead);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static void ReactivateLead(CrmService service,MetadataService metaDataService, LeadInfo leadDetails,Guid leadID,DynamicEntity crmLeadInfo)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            try
            {
                var lead = new lead();
                
                foreach (Property p in crmLeadInfo.Properties)
                {
                    if (p.Name == "firstname")
                    {
                        firstName = CrmHelper.GetSourceValue(p.GetType().Name, p);
                    }
                    if (p.Name == "lastname")
                    {
                        lastName = CrmHelper.GetSourceValue(p.GetType().Name, p);
                    }
                }

                if (leadDetails.LeadOriginatorName == string.Concat(firstName, " ", lastName))
                {
                    //lead.ms_bdm= matched CRM user
                    lead.ms_staffreferrer = null;
                    lead.ms_referralname = null;
                }
                else
                {
                    lead.ms_bdm = null;
                    lead.ms_staffreferrer = null;
                    lead.ms_referralname = leadDetails.LeadOriginatorName;
                }


                //var crmBrand = new Picklist();
                //if (leadDetails.Brand.ToLower() == DALConstants.BrandM)
                //{
                //    crmBrand.Value = Convert.ToInt32(DALConstants.Brand.Maxxia) ;
                //}
                //else { crmBrand.Value = Convert.ToInt32(DALConstants.Brand.Remserv); }
                //lead.ms_brand = crmBrand;

                //var crmProductType = new Picklist
                //{
                //    name = "ms_producttype",
                //    Value = CrmHelper.GetPickListCode(metaDataService, "ms_producttype", "ms_producttype", DALConstants.ProductTypeFMNL)
                //};
                //lead.ms_producttype = crmProductType;
                

                //var crmOwnLead = new Picklist();
                //crmOwnLead.Value = Convert.ToInt32(DALConstants.OwnTheLead.No);
                //lead.ms_willyouownthelead = crmOwnLead;


                //var crmRating = new Picklist();
                //if (string.IsNullOrEmpty(leadDetails.LeadRating))
                //{
                //    crmRating.Value = Convert.ToInt32("Warm");
                //}
                //else
                //{
                //    crmRating.Value = Convert.ToInt32(leadDetails.LeadRating);
                //}
                //lead.leadqualitycode = crmRating;


                //var crmLeadSource = new Picklist
                //{
                //    name = "ms_leadsourcename",
                //    Value = CrmHelper.GetPickListCode(metaDataService, "ms_leadsource", "ms_leadsourcename", leadDetails.LeadSource)
                //};
                //lead.leadsourcecode = crmLeadSource;

                //leadsourceid required
                //var crmEnquirySource = new Picklist
                //{
                //    name = "ms_enquirysourcename",
                //    Value = CrmHelper.GetPickListCode(metaDataService, "ms_enquirysource", "ms_enquirysourcename", leadDetails.ActivityType )
                //};
                //lead.ms_enquirysource = crmEnquirySource;


                //string csName = string.Empty;
                //if (string.IsNullOrEmpty(leadDetails.CampaignSource))
                //{
                //    csName = "None";
                //}
                //else
                //{
                //    csName = leadDetails.CampaignSource;
                //}

                //var crmCampaignSource = new Picklist
                //{
                //    name = "ms_campaignsourcename",
                //    Value = CrmHelper.GetPickListCode(metaDataService, "ms_campaignsource", "ms_campaignsourcename", csName)
                //};
                //lead.ms_campaignsource = crmCampaignSource;


                //string vehicleType = string.Empty;
                //if (!string.IsNullOrEmpty(leadDetails.VehicleType))
                //{
                //    if (leadDetails.VehicleType == "New Car")
                //    {
                //        vehicleType="New";
                //    }
                //    else if (leadDetails.VehicleType == "Used Car")
                //    {
                //        vehicleType="Used(Dealer)";
                //    }
                //    else if (leadDetails.VehicleType == "Refinance Existing")
                //    {
                //        vehicleType="Sales & Leaseback";
                //    }

                //    var crmVehicleType = new Picklist()
                //    {
                //        name = "ms_vehicletype1",
                //        Value = CrmHelper.GetPickListCode(metaDataService, "ms_vehicletype", "ms_vehicletype1", vehicleType)
                //    };
                //    lead.ms_vehicletype = crmVehicleType;
                //}


                //if (!string.IsNullOrEmpty(leadDetails.EmployerCode))
                //{
                //var emp = new account { accountnumber = leadDetails.EmployerCode };
                //    var crmEmployer = new Lookup();
                //    if (emp.accountid != null)
                //    {
                //        //crmEmployer.Value = emp;
                //    }
                //    else
                //    {
                //        emp = new account { name = leadDetails.EmployerName };
                //        if (emp.accountid != null)
                //        {
                //            // crmEmployer.Value = emp;
                //        }
                //        else
                //        {
                //            // crmEmployer.Value = TestEmployer;
                //            //lead.TemPMessage;
                //        }
                //    }
                //    lead.ms_employer = crmEmployer;

                //    lead.ms_salarypackager = emp.ms_salarypackager1;
                //}

                //var crmState = new Picklist();
                //crmState.Value = Convert.ToInt32(leadDetails.State);
                //lead.ms_homeaddressstate = crmState;

                lead.firstname = leadDetails.Firstname;
                lead.lastname = leadDetails.Surname;
                lead.emailaddress2 = leadDetails.Email;
                lead.mobilephone = leadDetails.MobilePhone;

                //var crmPrefferedPhone = new Picklist();
                //if (leadDetails.MobilePreferredContact)
                //{
                //    crmPrefferedPhone.Value = Convert.ToInt32("Mobile");
                //}
                //else
                //{
                //    crmPrefferedPhone.Value = Convert.ToInt32("Work");
                //}
                //lead.ms_preferredphone = crmPrefferedPhone;

                if (!string.IsNullOrEmpty(leadDetails.OtherPhone)) lead.telephone1 = leadDetails.OtherPhone;

                lead.ms_preferredcontacttime = leadDetails.PreferredCallBackTime;

                var crmAnnualGS = new CrmDecimal();
                if (leadDetails.AnnualGrossSalary.ToString() != "")
                {
                    crmAnnualGS.Value = leadDetails.AnnualGrossSalary;

                    var crmRefferalSplit = new CrmDecimal();
                    if (leadDetails.AnnualGrossSalary < 110000)
                    {
                        crmRefferalSplit.Value = (decimal)999.00;
                    }
                    else
                    {
                        //crmRefferalSplit.Value = null;
                    }
                    lead.ms_referralsplit = crmRefferalSplit;
                }
                else
                {
                    crmAnnualGS.Value = 0;
                }
                lead.ms_annualgrosssalary = crmAnnualGS;


                //lead.siteid


                lead.ms_salesmessages = "Year: " + leadDetails.VehicleYear + "/ Make: " + leadDetails.VehicleMake + "/ Model: " + leadDetails.VehicleType + "\r\n" + "Lease Term: " + leadDetails.LeaseTerm + "\r\n * **\r\n" + leadDetails.OtherInformation;

                //var emp = new account { ownerid = new Owner { Value = "" }; };
                //lead.ownerid=

                


                var oppKey = new WebReferences.CrmSdk.Key { Value = leadID };
                lead.leadid = oppKey;
                
                service.Update(lead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AddNoteToLead(CrmService service, Guid leadID,string noteText)
        {
            try
            {
                var annotation = new annotation();
                
                annotation.notetext = noteText;
                annotation.objectid = new Lookup
                {
                    Value = leadID,
                    type = EntityName.lead.ToString()
                };

                var annotationId=service.Create(annotation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
