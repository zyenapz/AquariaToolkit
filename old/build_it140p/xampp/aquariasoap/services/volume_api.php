<?php
// volume_api.php
// Public-facing API for retrieving the name for the 'portion_s' parameter of the get_cylinder_volume() function

function get_halfcylinder_name()
{
    require_once "data/constants.php";

    return $HALF_CYLINDER_NAME;
}

function get_quartercylinder_name()
{
    require_once "data/constants.php";

    return $QUARTER_CYLINDER_NAME;
}
