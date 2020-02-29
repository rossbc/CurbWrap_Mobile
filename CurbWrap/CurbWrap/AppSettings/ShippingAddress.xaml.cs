using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cw = CurbWrap.Helpers.cwApplicationSettings;

namespace CurbWrap.AppSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShippingAddress : ContentPage
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

        protected string _stateCode { get; set; }

        public ShippingAddress()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            EShipOption_Toggled(null, null);

        }

        private void onPickerFocus(object sender, FocusEventArgs e)
        {
            var chooseStatePage = new PickerPage(HelperFunctions.PickerType.State);
            chooseStatePage.pickerControl.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem.ToString();
                _stateCode = chooseStatePage.GetStateValue(item);
                Navigation.PopAsync();
                eState.Text = _stateCode;
            };
            Navigation.PushAsync(chooseStatePage);
        }

        private void EShipOption_Toggled(object sender, ToggledEventArgs e)
        {
            this.repShipToAddress.IsVisible = eShipOption.IsToggled;
        }
    }
}