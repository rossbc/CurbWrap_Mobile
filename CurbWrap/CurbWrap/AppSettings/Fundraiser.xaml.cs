using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cw = CurbWrap.Helpers.cwApplicationSettings;
using cws = CurbWrap.Helpers.cwSettings;

using CurbWrap.Helpers;
using CurbWrap.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurbWrap.AppSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FundraiserPage : ContentPage
	{
		public FundraiserPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            eFirstName.Text = cw.FirstName;
            eLastName.Text = cw.LastName;
            eFundraiserCode.Text = cw.FundRaiserCode;
            if (eFundraiserCode.Text != "")
            {
                LoadFundraiserInfo();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            cw.FirstName = eFirstName.Text;
            cw.LastName = eLastName.Text;
            cw.FundRaiserCode = eFundraiserCode.Text;
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            cw.FirstName = eFirstName.Text;
            cw.LastName = eLastName.Text;
            cw.FundRaiserCode = eFundraiserCode.Text;
            LoadFundraiserInfo();

            if (lFundraiserCode.Text != "INVALID FUNDRAISER CODE!")
            {
                cws.IsRegistering = true;
                Navigation.PushAsync(new NewsPage());
            }

        }

        private void LoadFundraiserInfo()
        {
            //Find the code
            Fundraiser fnd = cwSettings.CategoryData.Fundraisers.FirstOrDefault(f => f.FundraiserCode == cw.FundRaiserCode);
            if (fnd != null)
            {
                lFundraiserCode.Text = fnd.FundraiserCode;
                lExpiresOn.Text = fnd.ExpiresOn;
            }
            else
            {
                lFundraiserCode.Text = "INVALID FUNDRAISER CODE!";
                eFundraiserCode.Text = "";
            }

        }
    }
}