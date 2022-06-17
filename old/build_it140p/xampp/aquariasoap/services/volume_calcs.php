<?php
// volume_calculators.php
// Calculators for getting the volume of aquarium tanks

function get_rectangle_volume($length, $width, $height)
{
    $volume = $length * $width * $height;
    return $volume;
}
function get_bowfront_volume($length, $width, $full_width, $height)
{
    require_once "data/constants.php";

    // Get areas of the underlying shapes
    $square_area = $length * $width;
    $elliptical_area = $PI * ($length / 2) * (($full_width - $width) / 2);
    $volume = ($square_area + $elliptical_area) * $height;

    return $volume;
}
function get_triangleprism_volume($base, $length, $height)
{
    $volume = ($base * $length * $height) / 2;

    return $volume;
}
function get_cornerpentagon_volume($long_side, $short_side, $height)
{
    // Get areas of the underlying shapes
    $box_area = $short_side * $short_side;
    $rect_area = ($long_side - $short_side) * $short_side;
    $tri_leg_length = ($long_side - $short_side);
    $triangle_area = ($tri_leg_length * $tri_leg_length) / 2;

    // Calculate total area
    $total_area = $box_area + ($rect_area * 2) + $triangle_area;

    // Calculate volume
    $volume = $total_area * $height;

    return $volume;
}
function get_cylinder_volume($diameter, $height, $portion_s)
{
    require_once "data/constants.php";

    // Convert portion string to numeric value
    $portion = 1;
    if ($portion_s == $HALF_CYLINDER_NAME) {
        $portion = 2;
    } else if ($portion_s == $QUARTER_CYLINDER_NAME) {
        $portion = 4;
    } else {
        $portion = 1;
    }

    $volume = ($PI * (($diameter / 2) ** 2) * $height) / $portion;

    return $volume;
}
