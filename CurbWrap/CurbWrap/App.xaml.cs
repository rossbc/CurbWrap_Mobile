using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CurbWrap
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //MainPage = new NavigationPage(new AppShell())
            //{
            //    BarBackgroundColor = (Color)Application.Current.Resources["Purple"],
            //    BarTextColor = (Color)Application.Current.Resources["ButtonTextColor"]
            //};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
