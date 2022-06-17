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

        EditText etWaterFiltersWH, etLightingWH, etHeatersWH, etFilterFoams, etFood;
        TextView tvElectricityRate, tvEstimatedMonthly, tvEstimatedAnnually, tvElectricityTotal, tvOthersTotal;
        List<EditText> editTexts;

        ImageButton btnBack;
        Button btnCompute;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_expenses);

            // Find views
            etWaterFiltersWH = FindViewById<EditText>(Resource.Id.editTextWaterFiltersWH);
            etLightingWH = FindViewById<EditText>(Resource.Id.editTextLightingWH);
            etHeatersWH = FindViewById<EditText>(Resource.Id.editTextHeatersWH);
            etFilterFoams = FindViewById<EditText>(Resource.Id.editTextFilterFoams);
            etFood = FindViewById<EditText>(Resource.Id.editTextFishFoodPKG);

            editTexts = new List<EditText>();
            editTexts.Add(etWaterFiltersWH);
            editTexts.Add(etLightingWH);
            editTexts.Add(etHeatersWH);
            editTexts.Add(etFilterFoams);
            editTexts.Add(etFood);

            tvElectricityRate = FindViewById<TextView>(Resource.Id.textViewElectricityRate);
            tvEstimatedMonthly = FindViewById<TextView>(Resource.Id.textViewExpensesMonthly);
            tvEstimatedAnnually = FindViewById<TextView>(Resource.Id.textViewExpensesAnnual);
            tvElectricityTotal = FindViewById<TextView>(Resource.Id.textViewElectricityCostTotal);
            tvOthersTotal = FindViewById<TextView>(Resource.Id.textViewOtherCostTotal);

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
                double waterFiltersWH, lightingWH, heatersWH, filterFoamsPesos, foodPKG;
                double totalWattsPerHour, otherCosts, pesosPerWattsHourMonthly;
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
                waterFiltersWH = double.Parse(etWaterFiltersWH.Text);
                lightingWH = double.Parse(etLightingWH.Text);
                heatersWH = double.Parse(etHeatersWH.Text);
                filterFoamsPesos = double.Parse(etFilterFoams.Text);
                foodPKG = double.Parse(etFood.Text);

                // Compute
                totalWattsPerHour = waterFiltersWH + lightingWH + heatersWH;
                otherCosts = filterFoamsPesos + foodPKG;

                pesosPerWattsHourMonthly = webService.calculate_monthly(webService.calculate_electricity_cost(totalWattsPerHour) * 24);
                monthlyExpense = otherCosts + pesosPerWattsHourMonthly;
                annualExpense = webService.calculate_annual(monthlyExpense);

                electricityRate = webService.get_meralco_rate();

                // Display data
                string displayRate, displayMonthly, displayAnnually, displayOthers, displayElectricity;
                displayRate = electricityRate.ToString();
                displayMonthly = Math.Round(monthlyExpense, 2).ToString("N0");
                displayAnnually = Math.Round(annualExpense, 2).ToString("N0");
                displayOthers = Math.Round(otherCosts, 2).ToString("N0");
                displayElectricity = Math.Round(pesosPerWattsHourMonthly, 2).ToString("N0");

                tvElectricityRate.Text = string.Format("₱{0} kw/H", displayRate);
                tvEstimatedMonthly.Text = string.Format("₱{0}", displayMonthly);
                tvEstimatedAnnually.Text = string.Format("₱{0}", displayAnnually);
                tvOthersTotal.Text = string.Format("₱{0}", displayOthers);
                tvElectricityTotal.Text = string.Format("₱{0}", displayElectricity);

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