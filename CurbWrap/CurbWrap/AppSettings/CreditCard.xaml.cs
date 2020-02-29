using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurbWrap.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;
using cw = CurbWrap.Helpers.cwApplicationSettings;

namespace CurbWrap.AppSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreditCard : ContentPage
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

        public CreditCard()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            eCardNumber.Text = cw.CardNumber;
            eExpMonth.Text = cw.ExpirationMonth;
            eExpYear.Text = cw.ExpirationYear;
            eCSC.Text = cw.CardCSC;

            eCardStreet1.Text = cw.BillingAddress1;
            eCardStreet2.Text = cw.BillingAddress2;
            eCardCity.Text = cw.BillingCity;
            eCardState.Text = cw.BillingState;
            eCardZip.Text = cw.BillingZip;

            //var page = new TargetPage();
            //MessagingCenter.Subscribe<TargetPage, double>(this, "SliderValueChanged", OnSliderValueChanged);
            //Navigation.PushAsync(page);

            ////To unsubscribe
            //MessagingCenter.Unsubscribe<MainPage>(this, "SliderValueChanged");

            if (_isRegistering)
            {
                btnCreditCardRegister.IsVisible = true;
            }else
            {
                btnCreditCardRegister.IsVisible = false;
            }

        }

        private async void BtnCreditCardRegister_OnClicked(object sender, EventArgs e)
        {
            cwSettings.IsRegistering = true;
            var doll = cwSettings.CategoryData.SubscriptionPrice;
            string msg = $"This system is a subscription based service. Your card will be charged ${doll}/month. ";
            msg +="Payment for product will be taken from your card when the product is ordered. Shipping is included. Items will not be shipped until they are paid for. ";
            msg += "Unless specified, product will be shipped directly to the customer.";
            var ret = await DisplayAlert("Curb Wrap", msg, "Accept", "Cancel");

            if (ret == true)
            {
                await Navigation.PushAsync(new NewsPage());
            }
        }

        private void BillingSwitchChanged(object sender, ToggledEventArgs e)
        {
            this.BillingAddress.IsVisible = !e.Value;
            if (BillingAddress.IsVisible)
            {
                lBillMessage.Text = "Card Address is";
                eCardStreet1.Focus();
            }
            else
            {
                lBillMessage.Text = "Card Address is the same as my shipping address";
                eCSC.Focus();
            }
        }

        private void onPickerFocus(object sender, FocusEventArgs e)
        {
            var chooseStatePage = new PickerPage(HelperFunctions.PickerType.State);
            chooseStatePage.pickerControl.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem.ToString();
                _stateCode = chooseStatePage.GetStateValue(item);
                Navigation.PopAsync();
                eCardState.Text = _stateCode;
            };
            Navigation.PushAsync(chooseStatePage);
        }


        private void EExpYear_OnFocused(object sender, FocusEventArgs e)
        {
            var chooseYearPage = new PickerPage(HelperFunctions.PickerType.Year);
            chooseYearPage.pickerControl.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem.ToString();
                Navigation.PopAsync();
                eExpYear.Text = item;
            };
            Navigation.PushAsync(chooseYearPage);
        }

        private void EExpMonth_OnFocused(object sender, FocusEventArgs e)
        {
            var chooseMonthPage = new PickerPage(HelperFunctions.PickerType.Month);
            chooseMonthPage.pickerControl.ItemSelected += (source, args) =>
            {
                var item = args.SelectedItem.ToString();
                var month = chooseMonthPage.GetMonthValue(item);
                Navigation.PopAsync();
                eExpMonth.Text = month;
            };
            Navigation.PushAsync(chooseMonthPage);
        }
    }

}