using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadSubmission.DAL
{
    public class DALConstants
    {
        //Brands
        public const string BrandM = "maxxia";
        public const string BrandR = "remserv";

        //Entity
        public const string EntityProduct = "product";

        //Attributes
        public const string AttrProductTypeDisplayValue = "ms_producttypedisplayvalue";

        //Others
        public const string ProductTypeFMNL = "Fully Maintained Novated Lease";
        

        public enum Brand
        { Maxxia=0,
            Remserv =1}

        public enum OwnTheLead
        {
            No = 0,
            Yes = 1
        }
    }


}
