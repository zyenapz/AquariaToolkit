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

    [Activity(Label = "UnitConversionActivity")]
    public class UnitConversionActivity : Activity
    {
        ImageButton btnBack;
        Spinner spinnerMeasureType, spinnerConvFrom, spinnerConvTo;
        EditText editTextValue;
        TextView textViewResults;
        Button btnConvert;

        List<string> measurementTypes;
        List<string> volumeUnits;
        List<string> temperatureUnits;
        List<string> lengthUnits;

        ArrayAdapter<string> volumeUnitsAdapter;
        ArrayAdapter<string> tempUnitsAdapter;
        ArrayAdapter<string> lengthUnitsAdapter;

        MeasurementType selected_MType;
        string selected_ConvertFrom;
        string selected_ConvertTo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_conversion);

            // Find views
            btnBack = FindViewById<ImageButton>(Resource.Id.imageButtonBack);
            spinnerMeasureType = FindViewById<Spinner>(Resource.Id.spinnerMeasurementType);
            spinnerConvFrom = FindViewById<Spinner>(Resource.Id.spinnerConvertFrom);
            spinnerConvTo = FindViewById<Spinner>(Resource.Id.spinnerConvertTo);
            editTextValue = FindViewById<EditText>(Resource.Id.editTextValue);
            textViewResults = FindViewById<TextView>(Resource.Id.textViewResults);
            textViewResults.Text = "";

            btnConvert = FindViewById<Button>(Resource.Id.buttonConvert);

            // Populate spinners
            measurementTypes = new List<string>()
            {
                nameof(MeasurementType.Volume),
                nameof(MeasurementType.Temperature),
                nameof(MeasurementType.Length)
            };

            volumeUnits = new List<string>()
            {
                nameof(VolumeUnit.Cm3),
                nameof(VolumeUnit.Liter),
                nameof(VolumeUnit.Gallon)
            };

            temperatureUnits = new List<string>()
            {
                nameof(TemperatureUnit.Celsius),
                nameof(TemperatureUnit.Fahrenheit),
                nameof(TemperatureUnit.Kelvin)
            };

            lengthUnits = new List<string>()
            {
                nameof(LengthUnit.Meter),
                nameof(LengthUnit.Centimeter),
                nameof(LengthUnit.Foot),
                nameof(LengthUnit.Inch)
            };

            var measureTypesAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, measurementTypes);
            volumeUnitsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, volumeUnits);
            tempUnitsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, temperatureUnits);
            lengthUnitsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lengthUnits);

            spinnerMeasureType.Adapter = measureTypesAdapter;
            spinnerConvFrom.Adapter = volumeUnitsAdapter;
            spinnerConvTo.Adapter = volumeUnitsAdapter;

            // Add event handlers
            btnBack.Click += OnButtonBackClick;
            btnConvert.Click += OnButtonConvertClick;
            spinnerMeasureType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnItemSelected_SpinnerMeasurementTypes);
            spinnerConvFrom.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnItemSelected_SpinnerConvFrom);
            spinnerConvTo.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(OnItemSelected_SpinnerConvTo);
        }

        #region Event handlers

        protected void OnItemSelected_SpinnerMeasurementTypes(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            Enum.TryParse(measurementTypes[e.Position], out MeasurementType measureType);

            selected_MType = measureType;

            switch (measureType)
            {
                case MeasurementType.Volume:
                    spinnerConvFrom.Adapter = volumeUnitsAdapter;
                    spinnerConvTo.Adapter = volumeUnitsAdapter;
                    break;
                case MeasurementType.Temperature:
                    spinnerConvFrom.Adapter = tempUnitsAdapter;
                    spinnerConvTo.Adapter = tempUnitsAdapter;
                    break;
                case MeasurementType.Length:
                    spinnerConvFrom.Adapter = lengthUnitsAdapter;
                    spinnerConvTo.Adapter = lengthUnitsAdapter;
                    break;
                default:
                    spinnerConvFrom.Adapter = volumeUnitsAdapter;
                    spinnerConvTo.Adapter = volumeUnitsAdapter;
                    break;
            }

        }
        protected void OnItemSelected_SpinnerConvFrom(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (selected_MType)
            {
                case MeasurementType.Volume:
                    selected_ConvertFrom = volumeUnits[e.Position];
                    break;

                case MeasurementType.Temperature:
                    selected_ConvertFrom = temperatureUnits[e.Position];
                    break;

                case MeasurementType.Length:
                    selected_ConvertFrom = lengthUnits[e.Position];
                    break;

                default:
                    throw new Exception("Reached default case on SpinnerConvFrom event handler");
            }

            //Snackbar.Make((View)sender, selected_ConvertFrom, Snackbar.LengthLong)
            //    .SetAction("Action", (View.IOnClickListener)null).Show();

        }
        protected void OnItemSelected_SpinnerConvTo(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            switch (selected_MType)
            {
                case MeasurementType.Volume:
                    selected_ConvertTo = volumeUnits[e.Position];
                    break;

                case MeasurementType.Temperature:
                    selected_ConvertTo = temperatureUnits[e.Position];
                    break;

                case MeasurementType.Length:
                    selected_ConvertTo = lengthUnits[e.Position];
                    break;

                default:
                    throw new Exception("Reached default case on SpinnerConvTo event handler");
            }

            //Snackbar.Make((View)sender, selected_ConvertTo, Snackbar.LengthLong)
            //    .SetAction("Action", (View.IOnClickListener)null).Show();

        }
        protected void OnButtonConvertClick(object sender, EventArgs eventArgs)
        {
            if(!string.IsNullOrEmpty(editTextValue.Text))
            {
                try
                {
                    switch (selected_MType)
                    {
                        case MeasurementType.Volume:
                            ConvertVolume();
                            break;
                        case MeasurementType.Temperature:
                            ConvertTemperature();
                            break;
                        case MeasurementType.Length:
                            ConvertLength();
                            break;
                        default:
                            throw new Exception("Reached default case");
                    }
                }
                catch
                {
                    Snackbar.Make((View)sender, "Something went wrong! Please check your internet connection!", Snackbar.LengthLong)
                    .SetAction("Action", (View.IOnClickListener)null).Show();
                }
            }
            else
            {
                DisplayError_EmptyInput();
            }
            
        }

        protected void OnButtonBackClick(object sender, EventArgs eventArgs)
        {
            Finish();
        }

        #endregion

        #region Conversion methods

        private void ConvertVolume()
        {
            Enum.TryParse(selected_ConvertFrom, out VolumeUnit volumeFrom);
            Enum.TryParse(selected_ConvertTo, out VolumeUnit volumeTo);

            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            Double value = Double.Parse(editTextValue.Text);
            Double results = 0;
            bool doDisplayResults = true;

            #region Volume conversion
            // -- From cm3
            if (volumeFrom == VolumeUnit.Cm3)
            {
                switch (volumeTo)
                {
                    case VolumeUnit.Cm3:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    case VolumeUnit.Liter:
                        results = webService.cm3_to_liter(value);
                        break;

                    case VolumeUnit.Gallon:
                        double liter = webService.cm3_to_liter(value);
                        results = webService.liter_to_gallonUS(liter);
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            // -- From liter
            else if (volumeFrom == VolumeUnit.Liter)
            {
                switch (volumeTo)
                {
                    case VolumeUnit.Cm3:
                        results = webService.liter_to_cm3(value);
                        break;

                    case VolumeUnit.Liter:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    case VolumeUnit.Gallon:
                        results = webService.liter_to_gallonUS(value);
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            // -- From US Gallon
            else if (volumeFrom == VolumeUnit.Gallon)
            {
                switch (volumeTo)
                {
                    case VolumeUnit.Cm3:
                        double liters = webService.gallonUS_to_liter(value);
                        results = webService.liter_to_cm3(value);
                        break;

                    case VolumeUnit.Liter:
                        results = webService.gallonUS_to_liter(value);
                        break;

                    case VolumeUnit.Gallon:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }
            #endregion

            // Display results
            if (doDisplayResults)
            {
                String unitDisplay;
                switch (volumeTo)
                {
                    case VolumeUnit.Cm3:
                        unitDisplay = "cm3";
                        break;

                    case VolumeUnit.Liter:
                        unitDisplay = "liters";
                        break;

                    case VolumeUnit.Gallon:
                        unitDisplay = "gallons";
                        break;

                    default:
                        unitDisplay = "UNKNOWN";
                        break;
                }

                DisplayResults(results, unitDisplay);
            }

        }

        private void ConvertTemperature()
        {
            Enum.TryParse(selected_ConvertFrom, out TemperatureUnit tempFrom);
            Enum.TryParse(selected_ConvertTo, out TemperatureUnit tempTo);

            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            Double value = Double.Parse(editTextValue.Text);
            Double results = 0;
            bool doDisplayResults = true;

            #region Temperature conversion

            // -- From celsius
            if(tempFrom == TemperatureUnit.Celsius)
            {
                switch (tempTo)
                {
                    case TemperatureUnit.Celsius:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    case TemperatureUnit.Fahrenheit:
                        results = webService.celsius_to_fahrenheit(value);
                        break;

                    case TemperatureUnit.Kelvin:
                        results = webService.celsius_to_kelvin(value);
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            // -- From fahrenheit
            else if(tempFrom == TemperatureUnit.Fahrenheit)
            {
                switch (tempTo)
                {
                    case TemperatureUnit.Celsius:
                        results = webService.fahrenheit_to_celsius(value);
                        break;

                    case TemperatureUnit.Fahrenheit:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    case TemperatureUnit.Kelvin:
                        results = webService.fahrenheit_to_kelvin(value);
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            // -- Kelvin
            else if(tempFrom == TemperatureUnit.Kelvin)
            {
                switch (tempTo)
                {
                    case TemperatureUnit.Celsius:
                        results = webService.kelvin_to_celsius(value);
                        break;

                    case TemperatureUnit.Fahrenheit:
                        results = webService.kelvin_to_fahrenheit(value);
                        break;

                    case TemperatureUnit.Kelvin:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            #endregion

            // Display results
            if (doDisplayResults)
            {
                String unitDisplay;
                switch (tempTo)
                {
                    case TemperatureUnit.Celsius:
                        unitDisplay = "C\x00B0";
                        break;
                    case TemperatureUnit.Fahrenheit:
                        unitDisplay = "F\x00B0";
                        break;
                    case TemperatureUnit.Kelvin:
                        unitDisplay = "K";
                        break;
                    default:
                        unitDisplay = "UNKNOWN";
                        break;
                }

                DisplayResults(results, unitDisplay);
            }
        }

        private void ConvertLength()
        {
            Enum.TryParse(selected_ConvertFrom, out LengthUnit lengthFrom);
            Enum.TryParse(selected_ConvertTo, out LengthUnit lengthTo);

            AquariaWebReference.AquariaSOAPService webService = new AquariaWebReference.AquariaSOAPService();

            Double value = Double.Parse(editTextValue.Text);
            Double results = 0;
            bool doDisplayResults = true;

            #region Length conversion

            // -- From meter
            if(lengthFrom == LengthUnit.Meter)
            {
                double foot;

                switch (lengthTo)
                {
                    case LengthUnit.Meter:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    case LengthUnit.Centimeter:
                        results = webService.meter_to_centimeter(value);
                        break;

                    case LengthUnit.Foot:
                        results = webService.meter_to_foot(value);
                        break;

                    case LengthUnit.Inch:
                        foot = webService.meter_to_foot(value);
                        results = webService.foot_to_inch(foot);
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            // -- From centimeter
            else if(lengthFrom == LengthUnit.Centimeter)
            {
                double meter;
                double foot;

                switch (lengthTo)
                {
                    case LengthUnit.Meter:
                        results = webService.centimeter_to_meter(value);
                        break;

                    case LengthUnit.Centimeter:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    case LengthUnit.Foot:
                        meter = webService.centimeter_to_meter(value);
                        results = webService.meter_to_foot(meter);
                        break;

                    case LengthUnit.Inch:
                        meter = webService.centimeter_to_meter(value);
                        foot = webService.meter_to_foot(meter);
                        results = webService.foot_to_inch(foot);
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            // -- From foot
            else if(lengthFrom == LengthUnit.Foot)
            {
                double meter;

                switch (lengthTo)
                {
                    case LengthUnit.Meter:
                        results = webService.foot_to_meter(value);
                        break;

                    case LengthUnit.Centimeter:
                        meter = webService.foot_to_meter(value);
                        results = webService.meter_to_centimeter(meter);
                        break;

                    case LengthUnit.Foot:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    case LengthUnit.Inch:
                        results = webService.foot_to_inch(value);
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            // -- From inch
            else if(lengthFrom == LengthUnit.Inch)
            {
                double foot;
                double meter;

                switch (lengthTo)
                {
                    case LengthUnit.Meter:
                        foot = webService.inch_to_foot(value);
                        results = webService.foot_to_meter(foot);
                        break;

                    case LengthUnit.Centimeter:
                        foot = webService.inch_to_foot(value);
                        meter = webService.foot_to_meter(foot);
                        results = webService.meter_to_centimeter(meter);
                        break;

                    case LengthUnit.Foot:
                        results = webService.inch_to_foot(value);
                        break;

                    case LengthUnit.Inch:
                        DisplayError_SameOriginAndDestination();
                        doDisplayResults = false;
                        break;

                    default:
                        throw new Exception("Default case reached");
                }
            }

            #endregion

            // Display results
            if (doDisplayResults)
            {
                String unitDisplay;
                switch (lengthTo)
                {
                    case LengthUnit.Meter:
                        unitDisplay = "m";
                        break;
                    case LengthUnit.Centimeter:
                        unitDisplay = "cm";
                        break;
                    case LengthUnit.Foot:
                        unitDisplay = "ft";
                        break;
                    case LengthUnit.Inch:
                        unitDisplay = "in";
                        break;
                    default:
                        unitDisplay = "UNKNOWN";
                        break;
                }

                DisplayResults(results, unitDisplay);
            }
        }

        #endregion

        #region Helper methods

        private void DisplayResults(Double results, String units)
        {
            textViewResults.Text = String.Format("{0} {1}", Math.Round(results, 2), units);
            textViewResults.SetTextColor(Color.LightGreen);
        }

        private void DisplayError_SameOriginAndDestination()
        {
            textViewResults.Text = "Conversion origin and destinations are the same!";
            textViewResults.SetTextColor(Color.Red);
        }

        private void DisplayError_EmptyInput()
        {
            textViewResults.Text = "Please enter a value!";
            textViewResults.SetTextColor(Color.Red);
        }

        #endregion
    }
}