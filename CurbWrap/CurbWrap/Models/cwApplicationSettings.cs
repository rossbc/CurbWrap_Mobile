using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Xamarin.Forms;
using CurbWrap.Models;
using Newtonsoft.Json;

namespace CurbWrap.Helpers
{
    public static class cwApplicationSettings
    {
        private static async void cwSet(string settingName, string value)
        {
            try
            {
                Application.Current.Properties[settingName] = value;
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static string cwGet(string settingName)
        {
            try
            {
                return Application.Current.Properties[settingName].ToString();
            }
            catch (Exception )
            {
                return "";
            }
        }

        //Configuration data
        public static CategoryData ConfigData
        {
            get {
                string data = cwGet("ConfigData");
                return JsonConvert.DeserializeObject<CategoryData>(data); ;
            }
            set {
                string configData = JsonConvert.SerializeObject(value);
                cwSet("ConfigData", configData);
            }
        }

        //Fundraiser Code
        public static string FundRaiserCode
        {
            get { return cwGet("FundRaiserCode"); }
            set { cwSet("FundRaiserCode", value); }
        }

        //Profile
        public static string FirstName
        {
            get { return cwGet("FirstName");}
            set { cwSet("FirstName", value);}
        }
        public static string LastName
        {
            get { return cwGet("LastName");}
            set { cwSet("LastName", value); }
        }
        public static string EmailAddress
        {
            get { return cwGet("EmailAddress");}
            set { cwSet("EmailAddress", value); }
        }
        //Shipping Address
        public static string ShippingAddress1
        {
            get { return cwGet("ShippingAddress1");}
            set { cwSet("ShippingAddress1", value); }
        }
        public static string ShippingAddress2
        {
            get { return cwGet("ShippingAddress2");}
            set { cwSet("ShippingAddress2", value); }
        }
        public static string ShippingCity
        {
            get { return cwGet("ShippingCity");}
            set { cwSet("ShippingCity", value); }
        }
        public static string ShippingState
        {
            get { return cwGet("ShippingState");}
            set { cwSet("ShippingState", value); }
        }
        public static string ShippingZip
        {
            get { return cwGet("ShippingZip");}
            set { cwSet("ShippingZip", value); }
        }
        //CreditCard Info
        public static string CardNumber
        {
            get { return cwGet("CardNumber");}
            set { cwSet("CardNumber", value); }
        }
        public static string ExpirationYear
        {
            get { return cwGet("ExpirationYear");}
            set { cwSet("ExpirationYear", value); }
        }
        public static string ExpirationMonth
        {
            get { return cwGet("ExpirationMonth");}
            set { cwSet("ExpirationMonth", value); }
        }
        public static string CardCSC
        {
            get { return cwGet("CardCSC");}
            set { cwSet("CardCSC", value); }
        }
        public static string BillingAddress1
        {
            get { return cwGet("BillingAddress1");}
            set { cwSet("BillingAddress1", value); }
        }
        public static string BillingAddress2
        {
            get { return cwGet("BillingAddress2");}
            set { cwSet("BillingAddress2", value); }
        }
        public static string BillingCity
        {
            get { return cwGet("BillingCity");}
            set { cwSet("BillingCity", value); }
        }
        public static string BillingState
        {
            get { return cwGet("BillingState");}
            set { cwSet("BillingState", value); }
        }
        public static string BillingZip
        {
            get { return cwGet("BillingZip");}
            set { cwSet("BillingZip", value); }
        }
    }
}
