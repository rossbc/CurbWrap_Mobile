using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CurbWrap.AppSettings;
using CurbWrap.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Threading;
using cw = CurbWrap.Helpers.cwApplicationSettings;
using CurbWrap.Catalog;

namespace CurbWrap.Helpers
{
    public static class HelperFunctions
    {
        private const string _url = @"http://www.rosswinds.com/CurbWrap/Categories.json";


        public static ActivityIndicator AddActivityIndicator(RelativeLayout view)
        {
            var act = new ActivityIndicator() {
                HeightRequest = 150, WidthRequest = 150,
                Color = (Color)Application.Current.Resources["Purple"],
                IsRunning=true
                
            };

            view.Children.Add(act,
                Constraint.RelativeToParent(parent => parent.Width / 2 - gethWidth(parent) / 2),
                Constraint.RelativeToParent(parent => parent.Height / 2 - getHeight(parent) / 2));

            double gethWidth(RelativeLayout parent) => act.Measure(parent.Width, parent.Height).Request.Width;
            double getHeight(RelativeLayout parent) => act.Measure(parent.Width, parent.Height).Request.Height;

            return act;
        }

        public static bool IsRegistered()
        {
            if (string.IsNullOrEmpty(cw.FirstName.Trim())) return false;
            if (string.IsNullOrEmpty(cw.LastName.Trim())) return false;

            return true;
        }

        public static CategoryData GetCategoryData()
        {
            StreamReader rdr;
            try
            {
                //Get current config data  from web
                WebRequest req = WebRequest.Create(_url);
                WebResponse rsp = (HttpWebResponse)req.GetResponse();
                rdr = new StreamReader(rsp.GetResponseStream());

            }
            catch (Exception)
            {
                // From the cache
                Thread.Sleep(2000);                
                return cwSettings.CategoryData;
            }

            string result = rdr.ReadToEnd();
            var catData = JsonConvert.DeserializeObject<CategoryData>(result);

            //Make sure URLs are fully qualified
            foreach (Category cat in catData.Categories)
            {
                foreach (cwImage img in cat.cwImages)
                {
                    img.ImageUrl = string.Concat(catData.BaseUrl, img.ImageUrl);

                }
            }

            //Store in cache
            cwSettings.CategoryData = catData;
            return catData;

        }

        public static int mod(int a, int n)
        {
            int res = ((a % n) + n) % n;
            return res;
        }

        public enum PickerType
        {
            Color,
            State,
            Month,
            Year
        }

        public static List<NavigationItem> CreateMenuItems(bool isSecured=false)
        {
            List<NavigationItem> MenuItems = new List<NavigationItem>();

            //News - this menu item is manually added otherwise the app crashes
            //MenuItems.Add(new NavigationItem()
            //{
            //    Title ="News", Image = "News.png", IsSecured = false, SortOrder = 0,
            //    Template = new DataTemplate(typeof(NewsPage))
            //});

            // ********** Unsecured menu items  *********************************************
            //Register
            MenuItems.Add(new NavigationItem()
            {
                Title = "Register", Image = "Profile.png", IsSecured = false, SortOrder = 1,
                Route = "register",
                Tabs = new List<ShellContent>
                    {
                        new ShellContent{
                            StyleId="register",
                            Title = "Personal",
                            Icon = "Profile.png",
                            Route = "profile",
                            ContentTemplate = new DataTemplate(typeof(Profile))},
                        new ShellContent{
                            StyleId="register",
                            Title = "Shipping",
                            Icon = "Address.png",
                            Route = "shippingAddress",
                            ContentTemplate = new DataTemplate(typeof(ShippingAddress))},
                        new ShellContent{
                            StyleId="register",
                            Title = "Card Info",
                            Icon = "CreditCard.png",
                            Route = "creditCard",
                            ContentTemplate = new DataTemplate(typeof(regCreditCard))}
                    }
            });

            //Fundraiser
            MenuItems.Add(new NavigationItem()
            {
                Title = "Fundraiser", Image = "Fundraiser.png", IsSecured = false, SortOrder = 2,
                Route = "fundraiser", Template = new DataTemplate(typeof(FundraiserPage))
            });

            //About
            MenuItems.Add(new NavigationItem()
            {
                Title = "About", Image = "Info.png", IsSecured = false, SortOrder = 3,
                Route = "about", Template = new DataTemplate(typeof(AboutPage))
            });



            //********************************  Secured menu items  ******************************************
            //Catalog
            MenuItems.Add(new NavigationItem()
            {
                Title = "Catalog", Image = "Home.png", IsSecured = true, SortOrder = 1,
                Route = "home", Template = new DataTemplate(typeof(HomePage))
            });

            if (!string.IsNullOrEmpty(cw.FundRaiserCode))
            {
                //Fundraiser
                MenuItems.Add(new NavigationItem()
                {
                    Title = "Fundraiser", Image = "Fundraiser.png", IsSecured = true, SortOrder = 2,
                    Route = "fundraiser", Template = new DataTemplate(typeof(FundraiserPage))
                });
            }


            if (string.IsNullOrEmpty(cw.FundRaiserCode ))
            {
                //Profile
                MenuItems.Add(new NavigationItem()
                {
                    Title = "Profile", Image = "Profile.png", IsSecured = true, SortOrder = 2,
                    Route = "profile", Template = new DataTemplate(typeof(Profile))
                });
            }

            if (string.IsNullOrEmpty(cw.FundRaiserCode))
            {
                //Shipping Address
                MenuItems.Add(new NavigationItem()
                {
                    Title = "Shipping Info", Image = "Address.png", IsSecured = true, SortOrder = 3,
                    Route = "shippingAddress", Template = new DataTemplate(typeof(ShippingAddress))
                });
            }

            if (string.IsNullOrEmpty(cw.FundRaiserCode))
            {
                //Credit Card
                MenuItems.Add(new NavigationItem()
                {
                    Title = "Billing Info", Image = "CreditCard.png", IsSecured = true, SortOrder = 4,
                    Route = "creditcard", Template = new DataTemplate(typeof(CreditCard))
                });
            }


            //About
            MenuItems.Add(new NavigationItem()
            {
                Title = "About", Image = "Info.png", IsSecured = true, SortOrder = 5,
                Route = "about", Template = new DataTemplate(typeof(AboutPage))
            });
 

            var appMenu = MenuItems.Where(m => m.IsSecured == isSecured).ToList();

            return appMenu;
        }

        //public static void OpenPicker(PickerType pickerType)
        //{
        //    switch (pickerType)
        //    {
        //        case PickerType.Color:
        //            var chooseColorPage = new PickerPage(HelperFunctions.PickerType.Color);
        //            chooseColorPage.pickerControl.ItemSelected += (source, args) =>
        //            {
        //                var item = args.SelectedItem.ToString();
        //                _stateCode = PickerPage.GetColor(item);
        //                Navigation.PopAsync();
        //                imageSizeChanged(null, null);
        //            };
        //            Navigation.PushAsync(chooseColorPage);
        //            break;
        //    }

        //}
    }
}
