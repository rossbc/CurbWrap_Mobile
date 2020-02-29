using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CurbWrap.Models;
using hlp = CurbWrap.Helpers.HelperFunctions;

namespace CurbWrap.Helpers
{
    public static class cwSettings
    {
        public static void LoadcwSettings()
        {
            //Read the config file from the website
            try
            {
                var categoryList = hlp.GetCategoryData();
                CategoryData = categoryList;


                cwSettings.CategoryData = categoryList;
            }
            catch (Exception)
            {
                return;
            }
        }

        private static CategoryData LoadFromCache()
        {
            return cwSettings.CategoryData;

        }

        public static bool IsFundRaiserExpired(string code)
        {
            string expDate = CategoryData.Fundraisers.Where(f => f.FundraiserCode == code).Select(f => f.ExpiresOn).FirstOrDefault();

            var dateFields = expDate.Split('/');
            var expireDate = new DateTime(int.Parse( dateFields[2]), int.Parse(dateFields[0]), int.Parse(dateFields[1]), 23, 59, 59);
            var rightNow = DateTime.Now;

            if (rightNow > expireDate)
            {
                return true;
            }
            return false;
        }

        public static CategoryData CategoryData { get; set; }

        public static Category SelectedCategory { get; set; }

        public static cwImage SelectedImage { get; set; }

        public static bool IsRegistering { get; set; }
    }
}
