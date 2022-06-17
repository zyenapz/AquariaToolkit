using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquariaToolkit
{
    public class FishData
    {
        public FishData(string fishName, FishType fishType, int fishImgResource)
        {
            this.fishName = fishName;
            this.fishType = fishType;
            this.fishImgResource = fishImgResource;
        }

        public String fishName { get; set; }
        public FishType fishType { get; set; }
        public int fishImgResource { get; set; }
    }


    [Activity(Label = "FishCompatActivity")]
    public class FishCompatActivity : Activity
    {
        ImageButton btnBack;
        ImageView imageFish1, imageFish2;
        Spinner spinnerFish1, spinnerFish2;
        TextView textResults;
        Button btnSubmit;

        List<FishData> fishDataList;

        FishType spinner1_selectedFish;
        FishType spinner2_selectedFish;

        string selectedFish1_name;
        string selectedFish2_name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_fishcompat);

            // Find views
            btnBack = FindViewById<ImageButton>(Resource.Id.imageButtonBack);
            imageFish1 = FindViewById<ImageView>(Resource.Id.imageViewFish1);
            imageFish2 = FindViewById<ImageView>(Resource.Id.imageViewFish2);
            btnSubmit = FindViewById<Button>(Resource.Id.buttonSubmit);
            spinnerFish1 = FindViewById<Spinner>(Resource.Id.spinnerFish1);
            spinnerFish2 = FindViewById<Spinner>(Resource.Id.spinnerFish2);
            textResults = FindViewById<TextView>(Resource.Id.textViewResults);
            textResults.Text = ""; // Clear the text results initially

            // Populate spinners
            fishDataList = new List<FishData>()
            {
                new FishData("Angelfish", FishType.Angelfish, Resource.Drawable.ic_fish_angelfish),
                new FishData("Betta fish", FishType.Betta, Resource.Drawable.ic_fish_betta),
                new FishData("Common Goldies", FishType.CommonGoldie, Resource.Drawable.ic_fish_commongoldie),
                new FishData("Fancy Goldies", FishType.FancyGoldie, Resource.Drawable.ic_fish_fancygoldie),
                new FishData("Danios", FishType.Danio, Resource.Drawable.ic_fish_danio),
                new FishData("Gouramis", FishType.Gourami, Resource.Drawable.ic_fish_gourami),
                new FishData("Guppies", FishType.Guppy, Resource.Drawable.ic_fish_guppy),
                new FishData("Mollies", FishType.Molly, Resource.Drawable.ic_fish_molly)
            };

            List<string> fishNames = new List<string>();
            foreach(FishData fishData in fishDataList)
            {
                fishNames.Add(fishData.fishName);
            }

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, fishNames);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            spinnerFish1.Adapter = adapter;
            spinnerFish2.Adapter = adapter; 

            // Add event handlers
            btnBack.Click += OnButtonBackClick;
            btnSubmit.Click += OnButtonSubmitClick;
            spinnerFish1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnItemSelected_SpinnerFish1);
            spinnerFish2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnItemSelected_SpinnerFish2);
        }

        #region Event handlers

        protected void OnItemSelected_SpinnerFish1(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            spinner1_selectedFish = fishDataList[e.Position].fishType;
            selectedFish1_name = fishDataList[e.Position].fishName;
            imageFish1.SetImageResource(fishDataList[e.Position].fishImgResource);
        }
        protected void OnItemSelected_SpinnerFish2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            spinner2_selectedFish = fishDataList[e.Position].fishType;
            selectedFish2_name = fishDataList[e.Position].fishName;
            imageFish2.SetImageResource(fishDataList[e.Position].fishImgResource);
        }

        protected void OnButtonSubmitClick(object sender, EventArgs eventArgs)
        {
            try
            {   
                AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

                // Get the id numbers for the compatiblity levels
                int COMPLEVEL_HGH = webService.get_fcm_verycompatible_number();
                int COMPLEVEL_MED = webService.get_fcm_usuallycompatible_number();
                int COMPLEVEL_LOW = webService.get_fcm_notcompatible_number();

                int fish1_id = GetEquivalentWebServiceFishID(spinner1_selectedFish);
                int fish2_id = GetEquivalentWebServiceFishID(spinner2_selectedFish);

                // Compare fish compatibility
                int compatibility = webService.compare_fish_compatibility(fish1_id, fish2_id);
                FishCompatibilityLevel compatibilityLevel = GetEquivalentFishCompatibilityLevel(compatibility);

                // Display results
                DisplayResultMessage(compatibilityLevel);
            }
            catch
            {
                Snackbar.Make((View)sender, "Something went wrong! Please check your internet connection!", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
            }
        }

        protected void OnButtonBackClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }

        #endregion

        #region Helper methods

        private void DisplayResultMessage(FishCompatibilityLevel compatibilityLevel)
        {
            switch (compatibilityLevel)
            {
                case FishCompatibilityLevel.NotCompatible:

                    textResults.Text = String.Format("" +
                        "{0} are not compatible with {1}{2}.\nIt is not advised to put them together in a tank at all.", 
                        selectedFish1_name, DoAddOtherClause(), selectedFish2_name);
                    textResults.SetTextColor(Color.Red);

                    break;
                case FishCompatibilityLevel.UsuallyCompatible:
                    textResults.Text = String.Format(
                        "{0} are usally compatible with {1}{2}.\nBe wary of the fish' sizes and behavior towards each other.", 
                        selectedFish1_name, DoAddOtherClause(), selectedFish2_name);
                    textResults.SetTextColor(Color.Orange);

                    break;
                case FishCompatibilityLevel.Compatible:
                    textResults.Text = String.Format(
                        "{0} are compatible with {1}{2}.\nThese fish can live together with no problems.", 
                        selectedFish1_name, DoAddOtherClause(), selectedFish2_name);
                    textResults.SetTextColor(Color.LightGreen);

                    break;
                default:
                    textResults.Text = "Something went wrong. You shouldn't be seeing this message";
                    textResults.SetTextColor(Color.Red);

                    break;
            }
        }

        private String DoAddOtherClause()
        {
            string otherClause = "";

            if (selectedFish1_name == selectedFish2_name)
            {
                otherClause = "other ";
            }

            return otherClause;
        }

        private FishCompatibilityLevel GetEquivalentFishCompatibilityLevel(int level)
        {
            switch(level)
            {
                case 0:
                    return FishCompatibilityLevel.NotCompatible;
                case 1:
                    return FishCompatibilityLevel.UsuallyCompatible;
                case 2:
                    return FishCompatibilityLevel.Compatible;
                default:
                    return FishCompatibilityLevel.NotCompatible;
            }
        }

        private int GetEquivalentWebServiceFishID(FishType spinner_selectedType)
        {
            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            // Get the fish's id numbers from the web service
            int ID_ANGELFISH = webService.get_angelfish_id();
            int ID_BETTAFISH = webService.get_bettafish_id();
            int ID_COMGOLDIE = webService.get_commongoldfish_id();
            int ID_FANGOLDIE = webService.get_fancygoldfish_id();
            int ID_DANIOFISH = webService.get_danio_id();
            int ID_GOURAFISH = webService.get_gourami_id();
            int ID_GUPPYFISH = webService.get_guppy_id();
            int ID_MOLLYFISH = webService.get_molly_id();

            switch (spinner_selectedType)
            {
                case FishType.Angelfish:
                    return ID_ANGELFISH;
                case FishType.Betta:
                    return ID_BETTAFISH;
                case FishType.CommonGoldie:
                    return ID_COMGOLDIE;
                case FishType.FancyGoldie:
                    return ID_FANGOLDIE;
                case FishType.Danio:
                    return ID_DANIOFISH;
                case FishType.Gourami:
                    return ID_GOURAFISH;
                case FishType.Guppy:
                    return ID_GUPPYFISH;
                case FishType.Molly:
                    return ID_MOLLYFISH;
                default:
                    return ID_ANGELFISH; // DEFAULT
            }
        }


        #endregion
    }
}