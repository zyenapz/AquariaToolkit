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

namespace AquariaToolkit
{
    public enum MeasurementType
    {
        Volume,
        Temperature,
        Length
    }

    public enum VolumeUnit
    {
        Cm3,
        Liter,
        Gallon
    }

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }

    public enum LengthUnit
    {
        Meter,
        Centimeter,
        Foot,
        Inch
    }
}