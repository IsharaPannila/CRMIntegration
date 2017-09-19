using System.Collections.Generic;
using LeadSubmission.WebReferences.CrmSdk;
using LeadSubmission.WebReferences.CrmSdk.MetaData;
using System;
using CrmNumber= LeadSubmission.WebReferences.CrmSdk.CrmNumber;
using LeadSubmission.BusinessObjects;

namespace LeadSubmission.DAL
{
    public class CrmHelper
    {
        public static CrmService CreateConnection(string url, string organisationName, string username, string password, string domain)
        {
            var token = new LeadSubmission.WebReferences.CrmSdk.CrmAuthenticationToken
            {
                AuthenticationType = 0,
                OrganizationName = organisationName
            };

            var crm = new CrmService
            {
                UnsafeAuthenticatedConnectionSharing = true,
                Timeout = 30000,
                Url = url,
                CrmAuthenticationTokenValue = token,
                Credentials = new System.Net.NetworkCredential(username, password, domain)
            };

            return crm;
        }

        public static MetadataService CreateMetaDataServiceConnection(string url, string organisationName, string username, string password, string domain)
        {
            var token = new LeadSubmission.WebReferences.CrmSdk.MetaData.CrmAuthenticationToken
            {
                AuthenticationType = 0,
                OrganizationName = organisationName
            };

            var crm = new MetadataService
            {
                UnsafeAuthenticatedConnectionSharing = true,
                Timeout = 30000,
                Url = url,
                CrmAuthenticationTokenValue = token,
                Credentials = new System.Net.NetworkCredential(username, password, domain)
            };

            return crm;
        }


        public static string GetSourceValue(string propType, Property p)
        {
            var returnValue = "";
            switch (propType)
            {
                case "KeyProperty":
                    returnValue = ((KeyProperty)p).Value.Value.ToString();
                    break;

                case "StateProperty":
                    returnValue = ((StateProperty)p).Value;
                    break;

                case "StatusProperty":
                    returnValue = ((StatusProperty)p).Value.Value.ToString();
                    break;

                case "LookupProperty":
                    returnValue = ((LookupProperty)p).Value.Value.ToString();
                    break;

                case "PicklistProperty":
                    returnValue = ((PicklistProperty)p).Value.name;
                    break;

                case "StringProperty":
                    returnValue = ((StringProperty)p).Value;
                    break;

                case "CrmBooleanProperty":
                    returnValue = ((CrmBooleanProperty)p).Value.Value.ToString();
                    break;

                case "CrmDateTimeProperty":
                    returnValue = ((CrmDateTimeProperty)p).Value.Value;
                    break;

                case "CrmDecimalProperty":
                    returnValue = ((CrmDecimalProperty)p).Value.Value.ToString();
                    break;

                case "CrmNumberProperty":
                    returnValue = ((CrmNumberProperty)p).Value.Value.ToString();
                    break;

                case "CrmMoneyProperty":
                    returnValue = ((CrmMoneyProperty)p).Value.Value.ToString();
                    break;
            }

            return returnValue;
        }

       
    }
}
