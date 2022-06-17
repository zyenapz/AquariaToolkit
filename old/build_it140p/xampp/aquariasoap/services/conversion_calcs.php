<?php
// conversion_calcs.php
// Calculators for unit or measurement conversions
// Deprecated as of now

// -------------------------
// Volume to mass converter
// -------------------------
function get_mass($volume)
{
    require_once "data/constants.php";

    $mass = $WATER_DENSITY * $volume;

    return $mass;
}

// ---------------------------------
// Temperature conversion functions
// ---------------------------------
function celsius_to_kelvin($celsius)
{
    require_once "data/constants.php";

    $kelvin = $celsius + $KELVIN_CELSIUS_RATIO;

    return $kelvin;
}
function kelvin_to_celsius($kelvin)
{
    require_once "data/constants.php";

    $celsius = $kelvin - $KELVIN_CELSIUS_RATIO;

    return $celsius;
}
function fahrenheit_to_celsius($fahrenheit)
{
    $celsius = ($fahrenheit - 32) * (5 / 9);

    return $celsius;
}
function celsius_to_fahrenheit($celsius)
{
    $fahrenheit = ($celsius * (9 / 5)) + 32;

    return $fahrenheit;
}
function fahrenheit_to_kelvin($fahrenheit)
{
    require_once "data/constants.php";

    $kelvin = ($fahrenheit - 32) * (5 / 9) + $KELVIN_CELSIUS_RATIO;

    return $kelvin;
}
function kelvin_to_fahrenheit($kelvin)
{
    require_once "data/constants.php";

    $fahrenheit = ($kelvin - $KELVIN_CELSIUS_RATIO) * (9 / 5) + 32;

    return $fahrenheit;
}

// ----------------------------
// Volume conversion functions
// ----------------------------
function cm3_to_liter($cm3)
{
    $liter = $cm3 / 1000;

    return $liter;
}
function liter_to_cm3($liter)
{
    $cm3 = $liter * 1000;

    return $cm3;
}
function liter_to_gallonUS($liter)
{
    $gallonUS = $liter / 3.785;

    return $gallonUS;
}
function gallonUS_to_liter($gallonUS)
{
    $liter = $gallonUS * 3.785;

    return $liter;
}

// ----------------------------
// Length conversion functions
// ----------------------------
function meter_to_centimeter($meter)
{
    $cm = $meter * 100;

    return $cm;
}
function centimeter_to_meter($cm)
{
    $meter = $cm / 100;

    return $meter;
}
function foot_to_inch($foot)
{
    $inch = $foot * 12;

    return $inch;
}
function inch_to_foot($inch)
{
    $foot = $inch / 12;

    return $foot;
}

// -- Meter to Foot and vice versa

function meter_to_foot($meter)
{
    require_once "data/constants.php";

    $foot = $meter * $METER_FOOT_RATIO;

    return $foot;
}
function foot_to_meter($foot)
{
    require_once "data/constants.php";

    $meter = $foot / $METER_FOOT_RATIO;

    return $meter;
}
