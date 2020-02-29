using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurbWrap.Catalog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GalleryCell : ContentView
	{
        private readonly string _categoryName;

        public GalleryCell (Uri imageUri, string imageName, string categoryName)
		{
            _categoryName = categoryName;
            InitializeComponent ();
            this.btnCategory.Source = ImageSource.FromUri(imageUri);
            this.lblName.Text = imageName;
        }

        private void CategoryClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DesignWrapPage(_categoryName, btnCategory.Source));
        }
    }
}