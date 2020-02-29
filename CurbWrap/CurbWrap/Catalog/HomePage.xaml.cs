using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
//using ViewCell = CurbWrap.Catalog.ViewCell;
using hlp = CurbWrap.Helpers.HelperFunctions;

namespace CurbWrap.Catalog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        public ActivityIndicator homeActivityIndicator;
        public HomePage ()
		{
			InitializeComponent ();
            //No need to go back
            NavigationPage.SetHasBackButton(this, false);
            homeActivityIndicator = hlp.AddActivityIndicator(this.relHome);

        }

        private void NoFundraiserAccess ()
        {
            var msg = $"The fundraising period for the code {cwApplicationSettings.FundRaiserCode} has ended.";
            categoryView.Content = new Label { Text = msg };
        }

        protected override void OnAppearing()
        {
            var catGrid = new Grid();

            //Need to check if fundraiser period has expired. If so, display a message and DON'T show the categories.
            if (!string.IsNullOrEmpty(cwApplicationSettings.FundRaiserCode))
            {
                if (cwSettings.IsFundRaiserExpired(cwApplicationSettings.FundRaiserCode))
                {
                    homeActivityIndicator.IsRunning = false;
                    NoFundraiserAccess();
                    return;
                }

            }

            base.OnAppearing();

            Task.Run(() => {

                //Populate the grid
                catGrid.Padding = new Thickness(10);
                catGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                catGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                catGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                var gridRow = -1;
                var gridCol = 0;

                var categories = cwSettings.CategoryData.Categories.ToList();

                if (string.IsNullOrEmpty( cwApplicationSettings.FundRaiserCode ))
                {
                    categories = cwSettings.CategoryData.Categories.Where(c => c.Name != "Fundraisers").ToList();
                }

                foreach (var category in categories)
                {
                    if (category.cwImages.Count != 0)
                    {
                        if (hlp.mod(gridCol, 3) == 0)
                        {
                            catGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(100) });
                            gridCol = 0;
                            gridRow++;
                        }
                        //Add one row
                        var cell = new ViewCell(category);
                        catGrid.Children.Add(cell, gridCol, gridRow);

                        gridCol++;
                    }
                }

          
                Device.BeginInvokeOnMainThread(() =>
                {
                    categoryView.Content = catGrid;
                    this.homeActivityIndicator.IsRunning = false;
                });
            });

        }

    }
}