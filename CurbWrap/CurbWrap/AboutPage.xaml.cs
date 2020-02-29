using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurbWrap
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();

            var p = cwSettings.CategoryData;
            Header.Text = $"CurbWrap \u2122 Version {p.Version}";
            string abtText = $"This mobile application is owned by Curb N Sign and its use is subscription based. Monthly fees of ${p.SubscriptionPrice} will be charged to the ";
            abtText += $"card you have on file. \n\nEach order will be charged ${p.Price}, payment will be taken from your card BEFORE the order is processed. Shipping is included in the price. You can charge ";
            abtText += $"your customer whatever you want. \n\nCustom orders will be charged ${p.CustomPrice}, shipping is also included. \n\nUse this to streamline the ordering process for your business and show your ";
            abtText += "customers the approximate look of their final product. \n\n";
            abtText += "SUPERCHARGE your business so you have more time to play!";
            AboutText.Text = abtText;
        }
	}
}