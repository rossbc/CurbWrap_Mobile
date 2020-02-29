using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CurbWrap.Helpers;
using CurbWrap.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cw = CurbWrap.Helpers.cwApplicationSettings;
using hlp = CurbWrap.Helpers.HelperFunctions;

namespace CurbWrap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {

        public ObservableCollection<NewsItem> Items { get; set; }
        public ActivityIndicator newsActivityIndicator;

        public NewsPage()
        {
            InitializeComponent();
            newsActivityIndicator = hlp.AddActivityIndicator(this.relNews);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(() => {
                //Load the config
                cwSettings.LoadcwSettings();


                //Add the news page items
                Items = new ObservableCollection<NewsItem>();

                foreach (var item in cwSettings.CategoryData.News)
                {
                    Items.Add(item);
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (hlp.IsRegistered() && cwSettings.IsRegistering)
                    {
                        var appMenu = Shell.Current as AppShell;
                        // pop the register page from the stack if register process just happened
                        //if (string.IsNullOrEmpty(cw.FundRaiserCode))
                        //{
                            Navigation.PopAsync();
                        //}
                        cwSettings.IsRegistering = false;
                        appMenu.Items.RemoveAt(3);
                        appMenu.Items.RemoveAt(2);
                        appMenu.Items.RemoveAt(1);

                        //Load up new menus
                        var newMenu = hlp.CreateMenuItems(true);
                        appMenu.BuildMenu(newMenu);
                    }


                    MyListView.ItemsSource = Items;
                    this.newsActivityIndicator.IsRunning = false;
                });
            });

        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
