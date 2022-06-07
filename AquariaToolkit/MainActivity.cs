using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Google.Android.Material.Card;
using Google.Android.Material.Snackbar;
using Android.Content;
using Android.Views;

namespace AquariaToolkit
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        MaterialCardView cardVolume, cardExpenses, cardFishCompat, cardUnitConversion, cardAbout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Find views
            cardVolume = FindViewById<MaterialCardView>(Resource.Id.materialCardViewVolume);
            cardExpenses = FindViewById<MaterialCardView>(Resource.Id.materialCardViewExpenses);
            cardFishCompat = FindViewById<MaterialCardView>(Resource.Id.materialCardViewFishCompat);
            cardUnitConversion = FindViewById<MaterialCardView>(Resource.Id.materialCardViewUnitConversion);
            cardAbout = FindViewById<MaterialCardView>(Resource.Id.materialCardViewAbout);

            // Add event handlers
            cardVolume.Click += OnCardVolumeClick;
            cardExpenses.Click += OnCardExpensesClick;
            cardFishCompat.Click += OnCardFishCompatClick;
            cardUnitConversion.Click += OnCardUnitConversionClick;
            cardAbout.Click += OnCardAboutClick;

        }

        private void OnCardVolumeClick(object sender, EventArgs eventArgs)
        {
            Intent i = new Intent(this, typeof(VolumeCalculatorActivity));
            StartActivity(i);
        }

        private void OnCardExpensesClick(object sender, EventArgs eventArgs)
        {
            Snackbar.Make((View)sender, "TODO!", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        private void OnCardFishCompatClick(object sender, EventArgs eventArgs)
        {
            Intent i = new Intent(this, typeof(FishCompatActivity));
            StartActivity(i);
        }

        private void OnCardUnitConversionClick(object sender, EventArgs eventArgs)
        {
            Intent i = new Intent(this, typeof(UnitConversionActivity));
            StartActivity(i);
        }

        private void OnCardAboutClick(object sender, EventArgs eventArgs)
        {
            Intent i = new Intent(this, typeof(AboutActivity));
            StartActivity(i);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}

}
