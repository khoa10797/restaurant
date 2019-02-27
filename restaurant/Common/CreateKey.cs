using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace restaurant.Common
{
    public static class CreateKey
    {
        public static string InvoiceDetails(string tableID)
        {
            string[] day = DateTime.Now.ToShortDateString().Split('/');
            string[] time = DateTime.Now.ToString("HH:mm:ss").Split(':');
            return tableID + "_" + day[0] + day[1] + day[2] + "_" + time[0] + time[1] + time[2];
        }

        public static string Invoice()
        {
            string[] day = DateTime.Now.ToShortDateString().Split('/');
            string[] time = DateTime.Now.ToString("HH:mm:ss").Split(':');
            return "HD" + day[0] + day[1] + day[2] + time[0] + time[1] + time[2];
        }
    }
}