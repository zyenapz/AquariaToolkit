using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Google.Android.Material.Card;
using Android.Content;

namespace AquariaToolkit
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Initialize card views
            MaterialCardView cardVolume = FindViewById<MaterialCardView>(Resource.Id.materialCardViewVolume);
            cardVolume.Click += OnCardVolumeClick;

            MaterialCardView cardFishCompat = FindViewById<MaterialCardView>(Resource.Id.materialCardViewFishCompat);
            cardFishCompat.Click += OnCardFishCompatClick;

            MaterialCardView cardAbout = FindViewById<MaterialCardView>(Resource.Id.materialCardViewAbout);
            cardAbout.Click += OnCardAboutClick;

        }

        private void OnCardVolumeClick(object sender, EventArgs eventArgs)
        {
            Intent i = new Intent(this, typeof(VolumeCalculatorActivity));
            StartActivity(i);
        }

        private void OnCardFishCompatClick(object sender, EventArgs eventArgs)
        {
            Intent i = new Intent(this, typeof(FishCompatActivity));
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
