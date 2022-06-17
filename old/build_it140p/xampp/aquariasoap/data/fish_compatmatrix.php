<?php
// fish_compatmatrix.php
// Data for the Fish Compatibility Matrix (FCM)

// Index numbers, represented by the fish
$ANGEL_I = 0;
$BETTA_I = 1;
$DANIO_I = 2;
$CGOLD_I = 3;
$FGOLD_I = 4;
$GOURA_I = 5;
$GUPPY_I = 6;
$MOLLY_I = 7;

// Compatibility matrix and its levels
$FCM_LEVEL_NOTCOM = 0;      // NOT COMPATIBLE
$FCM_LEVEL_USACOM = 1;      // USUALLY COMPATIBLE
$FCM_LEVEL_VERCOM = 2;      // VERY COMPATIBLE

$FISH_COMPATMATRIX = array(
    array(2, 1, 2, 0, 0, 2, 0, 2),
    array(1, 0, 2, 0, 0, 1, 2, 2),
    array(2, 2, 2, 1, 1, 2, 2, 2),
    array(0, 0, 1, 2, 2, 0, 0, 0),
    array(0, 0, 1, 2, 2, 0, 0, 0),
    array(2, 1, 2, 0, 0, 2, 2, 2),
    array(0, 2, 2, 0, 0, 2, 2, 2),
    array(2, 2, 2, 0, 0, 2, 2, 2)
);
