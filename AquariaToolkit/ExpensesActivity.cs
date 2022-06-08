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
using Google.Android.Material.Snackbar;
using Android.Graphics;

namespace AquariaToolkit
{
    [Activity(Label = "ExpensesActivity")]
    public class ExpensesActivity : Activity
    {

        EditText etWaterFiltersKWH, etLightingKWH, etHeatersKWH, etFilterFoams, etFood;
        TextView tvElectricityRate, tvEstimatedMonthly, tvEstimatedAnnually;
        List<EditText> editTexts;

        ImageButton btnBack;
        Button btnCompute;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_expenses);

            // Find views
            etWaterFiltersKWH = FindViewById<EditText>(Resource.Id.editTextWaterFiltersKWH);
            etLightingKWH = FindViewById<EditText>(Resource.Id.editTextLightingKWH);
            etHeatersKWH = FindViewById<EditText>(Resource.Id.editTextHeatersKWH);
            etFilterFoams = FindViewById<EditText>(Resource.Id.editTextFilterFoams);
            etFood = FindViewById<EditText>(Resource.Id.editTextFishFoodPKG);

            editTexts = new List<EditText>();
            editTexts.Add(etWaterFiltersKWH);
            editTexts.Add(etLightingKWH);
            editTexts.Add(etHeatersKWH);
            editTexts.Add(etFilterFoams);
            editTexts.Add(etFood);

            tvElectricityRate = FindViewById<TextView>(Resource.Id.textViewElectricityRate);
            tvEstimatedMonthly = FindViewById<TextView>(Resource.Id.textViewExpensesMonthly);
            tvEstimatedAnnually = FindViewById<TextView>(Resource.Id.textViewExpensesAnnual);

            btnBack = FindViewById<ImageButton>(Resource.Id.imageButtonBack);
            btnCompute = FindViewById<Button>(Resource.Id.buttonComputeExpenses);

            // Add event handlers
            btnBack.Click += OnButtonBackClick;
            btnCompute.Click += OnButtonComputeClick;
        }

        #region Event handlers
        protected void OnButtonComputeClick(object sender, EventArgs eventArgs)
        {
            try
            {
                // Declare data
                AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();
                double waterFiltersKWH, lightingKWH, heatersKWH, filterFoamsPesos, foodPKG;
                double totalKilowattsPerHour, otherCosts, pesosPerKWH;
                double monthlyExpense, annualExpense;
                double electricityRate;

                // If textfields are blank, fill it with 0
                foreach (EditText editText in editTexts)
                {
                    if (string.IsNullOrEmpty(editText.Text))
                    {
                        editText.Text = "0";
                    }
                }

                // Get values
                waterFiltersKWH = double.Parse(etWaterFiltersKWH.Text);
                lightingKWH = double.Parse(etLightingKWH.Text);
                heatersKWH = double.Parse(etHeatersKWH.Text);
                filterFoamsPesos = double.Parse(etFilterFoams.Text);
                foodPKG = double.Parse(etFood.Text);

                // Compute
                totalKilowattsPerHour = waterFiltersKWH + lightingKWH + heatersKWH;
                otherCosts = filterFoamsPesos + foodPKG;

                pesosPerKWH = webService.calculate_electricity_cost(totalKilowattsPerHour);
                monthlyExpense = webService.calculate_monthly(otherCosts) + pesosPerKWH;
                annualExpense = webService.calculate_annual(monthlyExpense);

                electricityRate = webService.get_meralco_rate();

                // Display data
                string displayRate, displayMonthly, displayAnnually;
                displayRate = electricityRate.ToString();
                displayMonthly = Math.Round(monthlyExpense, 2).ToString("N0");
                displayAnnually = Math.Round(annualExpense, 2).ToString("N0");

                tvElectricityRate.Text = string.Format("₱{0} kw/H", displayRate);
                tvEstimatedMonthly.Text = string.Format("₱{0}", displayMonthly);
                tvEstimatedAnnually.Text = string.Format("₱{0}", displayAnnually);

                tvElectricityRate.SetTextColor(Color.Green);
                tvEstimatedMonthly.SetTextColor(Color.Orange);
                tvEstimatedAnnually.SetTextColor(Color.Orange);
        }
            catch
            {

                Snackbar.Make((View) sender, "Something went wrong! Please check your internet connection!", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
    }
}
        protected void OnButtonBackClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }
        #endregion
    }
}