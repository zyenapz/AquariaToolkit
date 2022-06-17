<?php
// fcm_provider.php
// Provides functionalities for checking the fish compatibility matrix

function compare_fish_compatibility($fish1_id, $fish2_id)
{
    require_once "data/fish_compatmatrix.php";

    $compatibility_level = $FISH_COMPATMATRIX[$fish1_id][$fish2_id];

    return $compatibility_level;
}
