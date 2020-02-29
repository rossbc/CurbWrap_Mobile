using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.AppSettings;
using CurbWrap.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using hlp = CurbWrap.Helpers.HelperFunctions;

namespace CurbWrap
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //No nav bar on the register home page
            NavigationPage.SetHasNavigationBar(this, false);
            cwSettings.LoadcwSettings();
        }

        //private void Register_Clicked(object sender, EventArgs e)
        //{
        //    //Go to the register page
        //    Navigation.PushAsync(new Register());
        //}
    }
}
