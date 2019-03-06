using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace restaurant.Models
{
    public class Cart
    {
        public string productID { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }

        public Cart(string productID, string productName, int quantity)
        {
            this.productID = productID;
            this.productName = productName;
            this.quantity = quantity;
        }
    }
}