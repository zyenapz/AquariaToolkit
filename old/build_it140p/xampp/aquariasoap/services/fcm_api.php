<?php
// fcm_api.php
// Public-facing API for the Fish Compatibility Matrix (FCM)

// -------------------------
// Getters for the fish IDs
// -------------------------
function get_angelfish_id()
{
    require_once "data/fish_compatmatrix.php";
    return $ANGEL_I;
}
function get_bettafish_id()
{
    require_once "data/fish_compatmatrix.php";
    return $BETTA_I;
}
function get_danio_id()
{
    require_once "data/fish_compatmatrix.php";
    return $DANIO_I;
}
function get_commongoldfish_id()
{
    require_once "data/fish_compatmatrix.php";
    return $CGOLD_I;
}
function get_fancygoldfish_id()
{
    require_once "data/fish_compatmatrix.php";
    return $FGOLD_I;
}
function get_gourami_id()
{
    require_once "data/fish_compatmatrix.php";
    return $GOURA_I;
}
function get_guppy_id()
{
    require_once "data/fish_compatmatrix.php";
    return $GUPPY_I;
}
function get_molly_id()
{
    require_once "data/fish_compatmatrix.php";
    return $MOLLY_I;
}

// -------------------------------------------
// Getters for the FCM's compatibility levels
// -------------------------------------------
function get_fcm_notcompatible_number()
{
    require_once "data/fish_compatmatrix.php";
    return $FCM_LEVEL_NOTCOM;
}
function get_fcm_usuallycompatible_number()
{
    require_once "data/fish_compatmatrix.php";
    return $FCM_LEVEL_USACOM;
}
function get_fcm_verycompatible_number()
{
    require_once "data/fish_compatmatrix.php";
    return $FCM_LEVEL_VERCOM;
}
