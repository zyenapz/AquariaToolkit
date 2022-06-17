<?php
// cost_calcs.php
// Calculators functions for electricity, monthly, and annual expenses
// Also some getters

function calculate_electricity_cost($watts)
{
    require_once "data/constants.php";

    $cost = ($watts / 1000) * $MERALCO_RATE;

    return $cost;
}

function calculate_monthly($cost)
{
    $monthly_cost = $cost * 30;

    return $monthly_cost;
}

function calculate_annual($monthly_cost)
{
    $annual_cost = $monthly_cost * 12;

    return $annual_cost;
}

function get_meralco_rate()
{
    require_once "data/constants.php";

    return $MERALCO_RATE;
}
