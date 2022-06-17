using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Android.Material.Card;
using Android.Graphics;
using Google.Android.Material.Snackbar;

namespace AquariaToolkit
{
    [Activity(Label = "CalculateVolumeActivity")]
    public class VolumeCalculatorActivity : Activity
    {
        AquariumShape selectedShape;

        MaterialCardView
            cardRectangle, cardBowfront,
            cardTriangle, cardPentagon,
            cardCylinder, cardHalfCylinder,
            cardQuarterCylinder;

        EditText editText1, editText2, editText3, editText4;
        List<EditText> editTexts;

        ImageButton btnBack;
        Button btnCalculate;
        TextView textViewResults;
        ImageView guideImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_volumecalc);

            // Find views
            cardRectangle = FindViewById<MaterialCardView>(Resource.Id.materialCardViewRectangle);
            cardBowfront = FindViewById<MaterialCardView>(Resource.Id.materialCardViewBowfront);
            cardTriangle = FindViewById<MaterialCardView>(Resource.Id.materialCardViewTriangle);
            cardPentagon = FindViewById<MaterialCardView>(Resource.Id.materialCardViewCornerPentagon);
            cardCylinder = FindViewById<MaterialCardView>(Resource.Id.materialCardViewCylinder);
            cardHalfCylinder = FindViewById<MaterialCardView>(Resource.Id.materialCardViewHalfCylinder);
            cardQuarterCylinder = FindViewById<MaterialCardView>(Resource.Id.materialCardViewQuarterCylinder);

            editText1 = FindViewById<EditText>(Resource.Id.editText1);
            editText2 = FindViewById<EditText>(Resource.Id.editText2);
            editText3 = FindViewById<EditText>(Resource.Id.editText3);
            editText4 = FindViewById<EditText>(Resource.Id.editText4);
            editTexts = new List<EditText>();
            editTexts.Add(editText1);
            editTexts.Add(editText2);
            editTexts.Add(editText3);
            editTexts.Add(editText4);

            btnBack = FindViewById<ImageButton>(Resource.Id.imageButtonBack);
            btnCalculate = FindViewById<Button>(Resource.Id.buttonCalculate);
            textViewResults = FindViewById<TextView>(Resource.Id.results);
            textViewResults.Text = "";

            guideImage = FindViewById<ImageView>(Resource.Id.guideImage);

            // Add click handlers
            btnBack.Click += OnButtonBackClick;
            btnCalculate.Click += OnButtonCalculateClick;
            cardRectangle.Click += OnCardRectangleClick;
            cardBowfront.Click += OnCardBowfrontClick;
            cardTriangle.Click += OnCardTriangleClick;
            cardPentagon.Click += OnCardPentagonClick;
            cardCylinder.Click += OnCardCylinderClick;
            cardHalfCylinder.Click += OnCardHalfCylinderClick;
            cardQuarterCylinder.Click += OnCardQuarterCylinderClick;

            // Activate rectangle inputs as initial view
            ActivateRectangleInputs();

        }

        #region Input activators

        private void ActivateRectangleInputs()
        {
            // Clear texts
            ClearEditTexts();

            // Set visibilities
            editText1.Visibility = ViewStates.Visible;
            editText2.Visibility = ViewStates.Visible;
            editText3.Visibility = ViewStates.Visible;
            editText4.Visibility = ViewStates.Gone;

            // Set hints
            editText1.Hint = "Length (cm)";
            editText2.Hint = "Height (cm)";
            editText3.Hint = "Width (cm)";

            // Set selected shape
            selectedShape = AquariumShape.Rectangle;

            // Set guide image
            guideImage.SetImageResource(Resource.Drawable.img_guide_rectangle);

        }
        private void ActivateBowfrontInputs()
        {
            // Clear texts
            ClearEditTexts();

            // Set visibilities
            editText1.Visibility = ViewStates.Visible;
            editText2.Visibility = ViewStates.Visible;
            editText3.Visibility = ViewStates.Visible;
            editText4.Visibility = ViewStates.Visible;

            // Set hints
            editText1.Hint = "Length (cm)";
            editText2.Hint = "Width 1 (cm)";
            editText3.Hint = "Width 2 (cm)";
            editText4.Hint = "Height (cm)";

            // Set selected shape
            selectedShape = AquariumShape.Bowfront;

            // Set guide image
            guideImage.SetImageResource(Resource.Drawable.img_guide_bowfront);
        }
        private void ActivateTriangleInputs()
        {
            // Clear texts
            ClearEditTexts();

            // Set visibilities
            editText1.Visibility = ViewStates.Visible;
            editText2.Visibility = ViewStates.Visible;
            editText3.Visibility = ViewStates.Visible;
            editText4.Visibility = ViewStates.Gone;

            // Set hints
            editText1.Hint = "Base (cm)";
            editText2.Hint = "Length (cm)";
            editText3.Hint = "Height (cm)";

            // Set selected shape
            selectedShape = AquariumShape.Triangle;

            // Set guide image
            guideImage.SetImageResource(Resource.Drawable.img_guide_triangle);
        }
        private void ActivatePentagonInputs()
        {
            // Clear texts
            ClearEditTexts();

            // Set visibilities
            editText1.Visibility = ViewStates.Visible;
            editText2.Visibility = ViewStates.Visible;
            editText3.Visibility = ViewStates.Visible;
            editText4.Visibility = ViewStates.Gone;

            // Set hints
            editText1.Hint = "Long side (cm)";
            editText2.Hint = "Short side (cm)";
            editText3.Hint = "Height (cm)";

            // Set selected shape
            selectedShape = AquariumShape.Pentagon;

            // Set guide image
            guideImage.SetImageResource(Resource.Drawable.img_guide_cornerpentagon);
        }
        private void ActivateCylinderInputs(AquariumShape cylinderPortion)
        {
            // Clear texts
            ClearEditTexts();

            // Set visibilities
            editText1.Visibility = ViewStates.Visible;
            editText2.Visibility = ViewStates.Visible;
            editText3.Visibility = ViewStates.Gone;
            editText4.Visibility = ViewStates.Gone;

            // Set selected shape
            selectedShape = cylinderPortion;

            // Set hints
            if(selectedShape == AquariumShape.Cylinder)
            {
                editText1.Hint = "Diameter (cm)";
            }
            else if(selectedShape == AquariumShape.HalfCylinder || selectedShape == AquariumShape.QuarterCylinder)
            {
                editText1.Hint = "Radius (cm)";
            }
            editText2.Hint = "Height (cm)";

            // Set guide image
            switch (selectedShape)
            {
                case AquariumShape.Cylinder:
                    guideImage.SetImageResource(Resource.Drawable.img_guide_cylinder);
                    break;

                case AquariumShape.HalfCylinder:
                    guideImage.SetImageResource(Resource.Drawable.img_guide_halfcylinder);
                    break;

                case AquariumShape.QuarterCylinder:
                    guideImage.SetImageResource(Resource.Drawable.img_guide_quartercylinder);
                    break;

                default:
                    guideImage.SetImageResource(Resource.Drawable.abc_ic_star_black_48dp);
                    break;
            }

        }

        #endregion

        #region Shape card click handlers
        private void OnCardRectangleClick(object sender, EventArgs eventArgs)
        {
            ActivateRectangleInputs();
        }

        private void OnCardBowfrontClick(object sender, EventArgs eventArgs)
        {
            ActivateBowfrontInputs();
        }
        private void OnCardTriangleClick(object sender, EventArgs eventArgs)
        {
            ActivateTriangleInputs();
        }
        private void OnCardPentagonClick(object sender, EventArgs eventArgs)
        {
            ActivatePentagonInputs();
        }
        private void OnCardCylinderClick(object sender, EventArgs eventArgs)
        {
            ActivateCylinderInputs(AquariumShape.Cylinder);
        }
        private void OnCardHalfCylinderClick(object sender, EventArgs eventArgs)
        {
            ActivateCylinderInputs(AquariumShape.HalfCylinder);
        }
        private void OnCardQuarterCylinderClick(object sender, EventArgs eventArgs)
        {
            ActivateCylinderInputs(AquariumShape.QuarterCylinder);
        }
        #endregion

        #region Web service consumers

        private void CalculateRectangleVolume()
        {
            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();
            
            if (!String.IsNullOrEmpty(editText1.Text) &&
                          !String.IsNullOrEmpty(editText2.Text) &&
                          !String.IsNullOrEmpty(editText3.Text))
            {
                double length = Double.Parse(editText1.Text);
                double height = Double.Parse(editText2.Text);
                double width = Double.Parse(editText3.Text);

                double results = webService.get_rectangle_volume(length, width, height); // returns cm^3
                double resultsLiter = webService.cm3_to_liter(results);

                DisplayVolumeResultMessage(Math.Round(resultsLiter, 2).ToString());
            }
            else
            {
                DisplayEmptyInputsMessage();
            }
        }
        private void CalculateBowfrontVolume()
        {
            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            if (!String.IsNullOrEmpty(editText1.Text) &&
                          !String.IsNullOrEmpty(editText2.Text) &&
                          !String.IsNullOrEmpty(editText3.Text) &&
                          !String.IsNullOrEmpty(editText4.Text))
            {
                double length = Double.Parse(editText1.Text);
                double width1 = Double.Parse(editText2.Text);
                double width2 = Double.Parse(editText3.Text);
                double height = Double.Parse(editText4.Text);

                double results = webService.get_bowfront_volume(length, width1, width2, height); // returns cm^3
                double resultsLiter = webService.cm3_to_liter(results);

                DisplayVolumeResultMessage(Math.Round(resultsLiter, 2).ToString());
            }
            else
            {
                DisplayEmptyInputsMessage();
            }
        }
        private void CalculateTriangleVolume()
        {
            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            if (!String.IsNullOrEmpty(editText1.Text) &&
                          !String.IsNullOrEmpty(editText2.Text) &&
                          !String.IsNullOrEmpty(editText3.Text))
            {
                double @base = Double.Parse(editText1.Text);
                double length = Double.Parse(editText2.Text);
                double height = Double.Parse(editText3.Text);

                double results = webService.get_triangleprism_volume(@base, length, height); // returns cm^3
                double resultsLiter = webService.cm3_to_liter(results);

                DisplayVolumeResultMessage(Math.Round(resultsLiter, 2).ToString());
            }
            else
            {
                DisplayEmptyInputsMessage();
            }
        }
        private void CalculatePentagonVolume()
        {
            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            if (!String.IsNullOrEmpty(editText1.Text) &&
                          !String.IsNullOrEmpty(editText2.Text) &&
                          !String.IsNullOrEmpty(editText3.Text))
            {
                double longSide = Double.Parse(editText1.Text);
                double shortSide = Double.Parse(editText2.Text);
                double height = Double.Parse(editText3.Text);

                double results = webService.get_cornerpentagon_volume(longSide, shortSide, height); // returns cm^3
                double resultsLiter = webService.cm3_to_liter(results);

                DisplayVolumeResultMessage(Math.Round(resultsLiter, 2).ToString());
            }
            else
            {
                DisplayEmptyInputsMessage();
            }
        }
        private void CalculateCylinderVolume(AquariumShape cylinderShape)
        {
            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            if (!String.IsNullOrEmpty(editText1.Text) &&
                          !String.IsNullOrEmpty(editText2.Text))
            {
                double diameter = Double.Parse(editText1.Text); // or radius, if working with half cylinder and quarter cylinder
                double height = Double.Parse(editText2.Text);

                String portion_string;

                switch(cylinderShape)
                {
                    case AquariumShape.HalfCylinder:
                        portion_string = webService.get_halfcylinder_name();
                        break;
                    case AquariumShape.QuarterCylinder:
                        portion_string = webService.get_quartercylinder_name();
                        break;
                    default:
                        portion_string = "DEFAULT";
                        break;
                } 

                double results = webService.get_cylinder_volume(diameter, height, portion_string); // returns cm^3
                double resultsLiter = webService.cm3_to_liter(results);

                DisplayVolumeResultMessage(Math.Round(resultsLiter, 2).ToString());
            }
            else
            {
                DisplayEmptyInputsMessage();
            }
        }

        #endregion

        #region Click handlers
        private void OnButtonCalculateClick(object sender, EventArgs eventArgs)
        {
            try
            {
                switch (selectedShape)
                {
                    case AquariumShape.Rectangle:
                        CalculateRectangleVolume();
                        break;

                    case AquariumShape.Bowfront:
                        CalculateBowfrontVolume();
                        break;

                    case AquariumShape.Triangle:
                        CalculateTriangleVolume();
                        break;

                    case AquariumShape.Pentagon:
                        CalculatePentagonVolume();
                        break;

                    case AquariumShape.Cylinder:
                        CalculateCylinderVolume(AquariumShape.Cylinder);
                        break;

                    case AquariumShape.HalfCylinder:
                        CalculateCylinderVolume(AquariumShape.HalfCylinder);
                        break;

                    case AquariumShape.QuarterCylinder:
                        CalculateCylinderVolume(AquariumShape.QuarterCylinder);
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                Snackbar.Make((View) sender, "Something went wrong! Please check your internet connection!", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
            }
        }

        private void OnButtonBackClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }
        #endregion

        #region View helper methods

        private void DisplayVolumeResultMessage(String results)
        {
            textViewResults.Text = "Tank's volume is " + results + " liters";
            textViewResults.SetTextColor(Color.LightGreen);
        }
        private void DisplayEmptyInputsMessage()
        {
            textViewResults.Text = "Please fill every input!";
            textViewResults.SetTextColor(Color.Red);
        }
        private void ClearEditTexts()
        {
            foreach(EditText et in editTexts)
            {
                et.Text = "";
            }
        }

        #endregion
    }
}