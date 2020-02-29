using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.Catalog;
using CurbWrap.AppSettings;
using CurbWrap.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cw = CurbWrap.Helpers.HelperFunctions;

namespace CurbWrap
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : Shell
	{
        Dictionary<string, Type> routes = new Dictionary<string, Type>();

        public AppShell ()
		{
			InitializeComponent ();
            
            //Now set the menu!
            var menuItems = cw.CreateMenuItems(cw.IsRegistered());
            this.BuildMenu(menuItems);

            RegisterRoutes();
            BackgroundColor = (Color)App.Current.Resources["White"];
            //SetForegroundColor ( (Color)App.Current.Resources["White"]);
            BindingContext = this;

        }

        public void BuildMenu(List<NavigationItem> newMenu)
        {
            foreach (var item in newMenu)
            {
                var fly = new FlyoutItem();
                fly.Title = item.Title;
                fly.Icon = item.Image;

                var shell = new ShellContent();
                shell.Route = item.Route;

                //STYLE sets the color for nav bar
                shell.Style = (Style)this.Resources["BaseStyle"];
                if (item.Template != null)
                {
                    shell.ContentTemplate = item.Template;
                }

                if (item.Tabs != null)
                {
                    foreach (var tab in item.Tabs)
                    {
                        var tabItem = new ShellContent { Title = tab.Title, Icon=tab.Icon,
                            Style = (Style)this.Resources["BaseStyle"],
                            Route = tab.Route, ContentTemplate = tab.ContentTemplate, StyleId=tab.StyleId };
                        fly.Items.Add(tabItem);
                    }
                }

                fly.Items.Add(shell);

                //this.Items.Insert(item.SortOrder, fly);
                this.Items.Add( fly);

            }
        }

        void RegisterRoutes()
        {
            routes.Add("news", typeof(NewsPage));
            routes.Add("home", typeof(HomePage));
            routes.Add("fundraiser", typeof(FundraiserPage));
            routes.Add("profile", typeof(Profile));
            routes.Add("shippingAddress", typeof(ShippingAddress));
            routes.Add("creditCard", typeof(CreditCard));
            routes.Add("about", typeof(AboutPage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        void OnNavigating(object sender, ShellNavigatingEventArgs e)
        {
            // Cancel any back navigation
            //if (e.Source == ShellNavigationSource.Pop)
            //{
            //    e.Cancel();
            //}
        }

        void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
        }


    }
}