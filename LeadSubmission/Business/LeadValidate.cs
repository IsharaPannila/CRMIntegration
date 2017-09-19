using System;
using System.Collections.Generic;
using LeadSubmission.Models;
using System.Configuration;
using LeadSubmission.WebReferences.CrmSdk;
using LeadSubmission.DAL;
using LeadSubmission.Constants;
using LeadSubmission.BusinessObjects;
using LeadSubmission.WebReferences.CrmSdk.MetaData;

namespace LeadSubmission.Business
{
    public class LeadProcess
    {
        private CrmService service;
        private MetadataService metaDataService;
        //Service connection set up
        internal void CreateServiceConnection()
        {
            string _userName = ConfigurationManager.AppSettings[LeadConstants.UserName];
            string _password = ConfigurationManager.AppSettings[LeadConstants.Password];
            string _domain = ConfigurationManager.AppSettings[LeadConstants.Domain];
            string _url = ConfigurationManager.AppSettings[LeadConstants.CrmSdkUrl];
            string _metaDataUrl = ConfigurationManager.AppSettings[LeadConstants.MetaDataURL];
            string _org = ConfigurationManager.AppSettings[LeadConstants.Organisation];

            try
            {
                service = CrmHelper.CreateConnection(_url, _org, _userName, _password, _domain);
                metaDataService = CrmHelper.CreateMetaDataServiceConnection(_metaDataUrl, _org, _userName, _password, _domain);

            }
            catch (Exception)
            {
                //throw;
            }
        }


        /// <summary>
        /// Initialvalidation for contact number of lead
        /// </summary>
        internal Boolean ValidateLeadContact(LeadInfo leadInfo, ref ReturnInfo mRetInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(leadInfo.EmployerCode) && string.IsNullOrEmpty(leadInfo.EmployerName))
                {
                    mRetInfo.ErrorMessage = LeadConstants.NoEmpCode; return false;
                }
                else if (leadInfo.MobilePreferredContact && (string.IsNullOrEmpty(leadInfo.MobilePhone)))
                {
                    mRetInfo.ErrorMessage = LeadConstants.NoMobileNumber; return false;
                }
                else if (!leadInfo.MobilePreferredContact && (string.IsNullOrEmpty(leadInfo.OtherPhone)))
                {
                    mRetInfo.ErrorMessage = LeadConstants.NoOtherPhoneNumber; return false;
                }
            }
            catch (Exception ex)
            {
                mRetInfo.ErrorMessage = ex.Message;
            }
            return true;
        }

        internal void ProcessLeadInfo(LeadInfo leadInfo, ref ReturnInfo mRetInfo)
        {
            string leadid = string.Empty;
            string leadStatus = string.Empty;
            string leadSource = string.Empty;
            DateTime createdOn = DateTime.Now ;
            DateTime modifiedOn = DateTime.Now;
            DateTime reactivateOn = DateTime.Now;  //Need to get from dynamic entity

            Dictionary<string,string> attributes = new Dictionary<string, string>();

            try
            {
                attributes.Add(LeadConstants.AttrEmail,leadInfo.Email);
                DynamicEntity de= LeadDAL.SelectLead(service, attributes);
                if (de == null)
                {
                    attributes.Clear();
                    attributes.Add(LeadConstants.AttrMobile,leadInfo.MobilePhone);
                    attributes.Add(LeadConstants.AttrFirstName,leadInfo.Firstname);
                    de = LeadDAL.SelectLead(service, attributes);
                    if (de == null)
                    {
                        //Create New Lead and exit
                        //to write
                        //RC 1
                        mRetInfo.ReturnCode = 1;
                        return;
                    }
                }

                //Get the existing lead status
                foreach (Property p in de.Properties)
                {
                    if (p.Name == LeadConstants.AttrLeadID)
                    {
                        leadid = CrmHelper.GetSourceValue(p.GetType().Name, p);
                    }
                    if (p.Name == LeadConstants.AttrLeadStatus)
                    {
                        leadStatus = CrmHelper.GetSourceValue(p.GetType().Name, p);
                    }
                    else if (p.Name == LeadConstants.AttrLeadSource)
                    {
                        leadSource=CrmHelper.GetSourceValue(p.GetType().Name, p);
                    }
                    else if (p.Name == LeadConstants.AttrCreatedOn)
                    {
                        createdOn =Convert.ToDateTime(CrmHelper.GetSourceValue(p.GetType().Name, p));
                    }
                    else if (p.Name == LeadConstants.AttrModifiedOn)
                    {
                        modifiedOn = Convert.ToDateTime(CrmHelper.GetSourceValue(p.GetType().Name, p));
                    }
                    //else if (p.Name == LeadConstants.AttrReactivateOn)
                    //{
                    //    reactivateOn = Convert.ToDateTime(CrmHelper.GetSourceValue(p.GetType().Name, p));
                    //}
                }

                Guid _leadid = new Guid(leadid);
                mRetInfo.LeadID = _leadid.ToString();

                //UpdateOnly = False
                if (!leadInfo.UpdateOnly)
                {
                    if (leadStatus.ToLower() == LeadConstants.StatusOpen)
                    {
                        if (leadSource == LeadConstants.AdminUpload)
                        {
                            //Send Email
                            //need info
                            //RC 3
                            mRetInfo.ReturnCode = 3;
                        }
                        else
                        {
                            //RC 2
                            mRetInfo.ReturnCode = 2;
                        }
                        //Create a Note <LeadDetails>
                        LeadDAL.AddNoteToLead(service, _leadid, leadInfo.LeadDetails);
                    }
                    else
                    {
                        //Reactivate Lead and update info.
                        LeadDAL.ReactivateLead(service, metaDataService, leadInfo, _leadid,de);
                        LeadDAL.AddNoteToLead(service, _leadid, leadInfo.LeadDetails);
                        //RC 4
                        mRetInfo.ReturnCode = 4;
                    }
                }
                else
                {
                    if (leadStatus.ToLower() == LeadConstants.StatusOpen)
                    {
                        if ((createdOn == leadInfo.CreateOn) || (reactivateOn==leadInfo.CreateOn))
                        {
                            //update refferal name, lead source, enquiry source, campaign source
                            //RC 5
                            mRetInfo.ReturnCode = 5;
                        }
                        else
                        {
                            //RC 7
                            mRetInfo.ReturnCode = 7;
                        }

                        //Create a Note <LeadDetails>
                        LeadDAL.AddNoteToLead(service, _leadid, leadInfo.LeadDetails);
                    }
                    else
                    {
                        if(modifiedOn == leadInfo.CreateOn)
                        {
                            //RC 6
                            mRetInfo.ReturnCode = 6;
                        }
                        else
                        {
                            //RC 8
                            mRetInfo.ReturnCode = 8;
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                mRetInfo.ErrorMessage = ex.Message;
            }
        }


    }
}