using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using hlp = CurbWrap.Helpers.HelperFunctions;

namespace CurbWrap.Catalog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GalleryPage : ContentPage
	{

        public ActivityIndicator galleryActivityIndicator;

        public GalleryPage()
		{
            InitializeComponent ();
            galleryActivityIndicator = hlp.AddActivityIndicator(this.relGallery);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(() => {

                //Populate the grid
                var catGrid = new Grid();
                catGrid.Padding = new Thickness(15, 0, 15, 0);
                catGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                //Read the config file from the website
                var categoryList = hlp.GetCategoryData();
                var category = cwSettings.SelectedCategory;

                var categoryImages = category.cwImages.Where(i => i.Tags.Contains(cwApplicationSettings.FundRaiserCode) || i.Tags == "").ToList();

                var numRows = Math.Ceiling(categoryImages.Count / 3d);

                var gridRow = -1;
                var gridCol = 0;

                foreach (var img in categoryImages)
                {
                    catGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(150) });
                    gridCol = 0;
                    gridRow++;

                    //Add one row
                    var cell = new GalleryCell(new Uri(img.ImageUrl), img.Name, category.Name);
                    cell.HorizontalOptions = LayoutOptions.Center;
                    catGrid.Children.Add(cell, gridCol, gridRow);

                }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        galleryView.Content = catGrid;
                        this.galleryActivityIndicator.IsRunning = false;
                    });
            });


        }

    }
}