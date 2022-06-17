<?php
// service.php
// This is the main SOAP web service that will be consumed by clients

require_once "nusoap/lib/nusoap.php";
require_once "services/fcm_api.php";
require_once "services/volume_calcs.php";
require_once "services/conversion_calcs.php";
require_once "services/volume_api.php";
require_once "services/fcm_provider.php";
require_once "services/cost_calcs.php";

// Create a instance for soap server
$server = new nusoap_server();

// Configure Web Services Description Language file
$namespace = "urn:aquariasoap";
$server->configureWSDL("Aquaria SOAP Service", $namespace);

// $server->wsdl->schemeTargetNamespace = $namespace;

// ----------------------------
// Register public-facing APIs
// ----------------------------
$server->register(
	"get_angelfish_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_bettafish_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_danio_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_commongoldfish_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_fancygoldfish_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_gourami_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_guppy_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_molly_id",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_fcm_notcompatible_number",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_fcm_usuallycompatible_number",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_fcm_verycompatible_number",
	array(),
	array("return" => "xsd:int")
);
$server->register(
	"get_halfcylinder_name",
	array(),
	array("return" => "xsd:string")
);
$server->register(
	"get_quartercylinder_name",
	array(),
	array("return" => "xsd:string")
);

// ------------------------------------------------------
// Register functions for the volume calculation service
// ------------------------------------------------------
$server->register(
	"get_mass",
	array("volume" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"get_rectangle_volume",
	array("length" => "xsd:double", "width" => "xsd:double", "height" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"get_bowfront_volume",
	array("length" => "xsd:double", "width" => "xsd:double", "full_width" => "xsd:double", "height" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"get_triangleprism_volume",
	array("base" => "xsd:double", "length" => "xsd:double", "height" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"get_cornerpentagon_volume",
	array("long_side" => "xsd:double", "short_side" => "xsd:double", "height" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"get_cylinder_volume",
	array("diameter" => "xsd:double", "height" => "xsd:double", "portion_s" => "xsd:string"),
	array("return" => "xsd:double")
);

// --------------------------------------------
// Register functions for the cost calculations
// --------------------------------------------
$server->register(
	"calculate_electricity_cost",
	array("watts" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"calculate_monthly",
	array("cost" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"calculate_annual",
	array("monthly_cost" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"get_meralco_rate",
	array(),
	array("return" => "xsd:double")
);

// -----------------------------------------------------------
// Register functions for the fish compatibility matrix checker
// -----------------------------------------------------------
$server->register(
	"compare_fish_compatibility",
	array("fish1_id" => "xsd:int", "fish2_id" => "xsd:int"),
	array("return" => "xsd:int")
);

// ----------------------------------------
// Register functions for unit conversions
// ----------------------------------------

// --- Temperature conversion
$server->register(
	"celsius_to_kelvin",
	array("celsius" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"kelvin_to_celsius",
	array("kelvin" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"fahrenheit_to_celsius",
	array("fahrenheit" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"celsius_to_fahrenheit",
	array("celsius" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"fahrenheit_to_kelvin",
	array("fahrenheit" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"kelvin_to_fahrenheit",
	array("kelvin" => "xsd:double"),
	array("return" => "xsd:double")
);

// --- Volume conversion
$server->register(
	"cm3_to_liter",
	array("cm3" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"liter_to_cm3",
	array("liter" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"liter_to_gallonUS",
	array("liter" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"gallonUS_to_liter",
	array("gallonUS" => "xsd:double"),
	array("return" => "xsd:double")
);

// --- Length conversion
$server->register(
	"meter_to_centimeter",
	array("meter" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"centimeter_to_meter",
	array("cm" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"foot_to_inch",
	array("foot" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"inch_to_foot",
	array("inch" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"meter_to_foot",
	array("meter" => "xsd:double"),
	array("return" => "xsd:double")
);
$server->register(
	"foot_to_meter",
	array("foot" => "xsd:double"),
	array("return" => "xsd:double")
);

// ---------------------------------------------------------------

// Pass data to the SOAP service
$server->service(file_get_contents("php://input"));

// Exit
exit();
