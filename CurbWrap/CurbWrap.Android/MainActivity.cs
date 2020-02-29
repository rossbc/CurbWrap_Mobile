using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace CurbWrap.Android
{
    [Activity(Label = "CurbWrap", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //global::PanCardView.Droid.CardsViewRenderer.Preserve();
            LoadApplication(new App());
        }

        //This works for buttons, volume up/down, back etc
        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode == Keycode.VolumeUp) //VolumeUP on the tablet
            {
                var action = e.Action;
                if (action == KeyEventActions.Up) // When the key is released
                {//So we don't trigger on both up and down
                    //Do something magical
                    //Probably best to raise a MessageCenter event.  Then your shared project hears that with having a tightly coupled situation
                }
            }
            return base.OnKeyUp(keyCode, e);
        }
    }
}