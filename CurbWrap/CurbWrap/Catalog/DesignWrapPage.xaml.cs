using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurbWrap.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CurbWrap.Models;

namespace CurbWrap.Catalog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DesignWrapPage : ContentPage
	{

        public List<cwImage> wrapImages;

        public cwImage selectedWrap;

        public enum ButtonMode
        {
            Both,
            None
        }

        public void SetButtonMode(ButtonMode mode)
        {
            switch (mode)
            {
                case ButtonMode.Both:
                    //Both buttons
                    //do nothing
                    break;
                case ButtonMode.None:
                    this.btnPrev.IsVisible = false;
                    this.btnNext.IsVisible = false;
                    break;
            }
        }

        public Color _textColor = Color.Black;
        public int pageNumber = 0;

        public Image WrapImage
        {
            get { return imgWrap; }
            set { imgWrap = value; }
        }

        public DesignWrapPage ()
		{
			InitializeComponent ();
            SetButtonMode(ButtonMode.Both);
        }

        public DesignWrapPage(string category, ImageSource imageUrl)
        {
            InitializeComponent();
            imgWrap.Source = imageUrl;

            var selectedCategory = cwSettings.SelectedCategory;
            if (selectedCategory.cwImages.Count == 1)
            {
                SetButtonMode(ButtonMode.None);
            }
            else
            {
                SetButtonMode(ButtonMode.Both);
            }

            wrapImages = new List<cwImage>();
            int CurrentIndex = 0;
            foreach (cwImage img in selectedCategory.cwImages)
            {
                wrapImages.Add(img);

                UriImageSource fileUri = (UriImageSource)imageUrl;

                if (img.ImageUrl.ToLower() == fileUri.Uri.ToString().ToLower() || selectedCategory.cwImages.Count == 1)
                {
                    ColorTypeConverter converter = new ColorTypeConverter();

                    selectedWrap = img;
                    pageNumber = CurrentIndex;
                    _textColor = (Color)(converter.ConvertFromInvariantString(selectedWrap.DefaultColor));
                    wrapName.Text = selectedWrap.Name;
                }
                CurrentIndex++;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ImageSizeChanged(null,null);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var chooseColorPage = new PickerPage(HelperFunctions.PickerType.Color);
            chooseColorPage.pickerControl.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem.ToString();
                _textColor = chooseColorPage.GetColor(item);
                Navigation.PopAsync();
                ImageSizeChanged(null,null);
            };
            Navigation.PushAsync(chooseColorPage);
        }

        private void ImageSizeChanged(object sender, EventArgs e)
        {
            imgWrap.HeightRequest = imgWrap.Width / 3.6;
            this.ForceLayout();

            addrNumbers.FontSize = imgWrap.Height * 0.8;
            addrNumbers.TextColor = _textColor;
            this.ForceLayout();
        }

        private void NextPrevious_Clicked(object sender, EventArgs e)
        {
            selectedWrap = wrapImages[pageNumber];

            var btn = (ImageButton)sender;
            if (btn.TabIndex == 10)
            {
                // Previous
                pageNumber--;
                pageNumber = pageNumber < 0 ? wrapImages.Count - 1 : pageNumber;
            }
            else
            {
                // Next
                pageNumber++;
                pageNumber = pageNumber == wrapImages.Count ? 0 : pageNumber;
            }


            ColorTypeConverter converter = new ColorTypeConverter();
            Color oldDfltColor = (Color)(converter.ConvertFromInvariantString(selectedWrap.DefaultColor));

            selectedWrap = wrapImages[pageNumber];

            bool canChangeColor = true;

            if (addrNumbers.TextColor != oldDfltColor)
            {
                canChangeColor = false;
            }
            else
            {
                addrNumbers.TextColor = (Color)(converter.ConvertFromInvariantString(selectedWrap.DefaultColor));
                _textColor = addrNumbers.TextColor;
            }
                       
            imgWrap.Source =  selectedWrap.ImageUrl;
            wrapName.Text = selectedWrap.Name;
        }

        private void Btn_Reset_Color(object sender, EventArgs e)
        {
            ColorTypeConverter converter = new ColorTypeConverter();

            addrNumbers.TextColor = (Color)(converter.ConvertFromInvariantString(selectedWrap.DefaultColor));
        }
    }
}