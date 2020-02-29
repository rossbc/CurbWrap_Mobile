using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CurbWrap.Helpers;

namespace CurbWrap.Catalog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewCell : ContentView
	{
        private readonly Category _category;

        public ViewCell (Category category)
		{
            InitializeComponent ();
            _category = category;
            this.lblCategory.Text = category.Name;

            //ImageSource 
            string imageUrl = string.Concat(cwSettings.CategoryData.BaseUrl, category.Uri);
            this.btnCategory.Source = ImageSource.FromUri(new Uri(imageUrl));
        }

        public void Category_Clicked(object sender, EventArgs e)
        {
            //Selected a category.
            cwSettings.SelectedCategory = _category;
            if (_category.cwImages.Count == 1)
            {
                var image = _category.cwImages[0];
                Navigation.PushAsync(new DesignWrapPage(_category.Name, ImageSource.FromUri(new Uri(image.ImageUrl))));
            }
            else
            {
                //var gallery = new GalleryPage(this.lblCategory.Text);
                var gallery = new GalleryPage();
                Navigation.PushAsync(gallery);
            }
        }
    }
}