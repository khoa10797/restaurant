using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace restaurant.Areas.Admin.Models
{
    public class ProductUpload
    {
        public string id { get; set; }

        public string categoryID { get; set; }

        public string productName { get; set; }

        public HttpPostedFileBase imageFile { get; set; }

        public double? price { get; set; }

        public string note { get; set; }

        public int? buyCount { get; set; }
    }
}