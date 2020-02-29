using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cw = CurbWrap.Helpers.cwApplicationSettings;

namespace CurbWrap.AppSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Profile : ContentPage
	{
        private bool _isRegistering = false;
        public virtual bool isRegistering
        {
            get { return _isRegistering; }
            set
            {
                Console.WriteLine("I'm in base");
                _isRegistering = value;
            }
        }

        public Profile()
        {
			InitializeComponent ();
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    Padding = new Thickness(0, 40, 0, 0);
            //}
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            eFirstName.Text = cw.FirstName;
            eLastName.Text = cw.LastName;
            eEmail.Text = cw.EmailAddress;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            cw.FirstName = eFirstName.Text;
            cw.LastName = eLastName.Text;
            cw.EmailAddress = eEmail.Text;
        }

    }
}