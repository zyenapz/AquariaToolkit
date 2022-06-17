<?php
require 'nusoap/lib/nusoap.php';
$client = new nusoap_client("http://localhost/aquariasoap/service.php?wsdl"); // Create a instance for nusoap client
?>

<!DOCTYPE html>
<html lang="en">

<head>
  <title>Aquaria SOAP Web Service</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">client.php
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>

<body>

  <div class="container">
    <h2>Aquaria SOAP Web Service Client</h2>
    <form class="form-inline" action="" method="POST">
      <label>Calculate volume for a bowfront aquarium</label>
      <br>
      <div class="form-group">
        <input type="text" name="length" class="form-control" placeholder="Enter length (cm)" required />
        <input type="text" name="width" class="form-control" placeholder="Enter width (cm)" required />
        <input type="text" name="full_width" class="form-control" placeholder="Enter full_width (cm)" required />
        <input type="text" name="height" class="form-control" placeholder="Enter height (cm)" required />
      </div>
      <input type="submit" name="calc_bowfront" class="btn btn-default"></input>
    </form>

    <br>

    <form class="form-inline" action="" method="POST">
      <label>Check fish compatibility</label>
      <br>
      <div class="form-group">
        <label for="fish1">Select fish 1: </label>
        <select id="fish1" name="fish1">
          <option value="0">Angelfish</option>
          <option value="1">Betta fish</option>
          <option value="2">Common goldfish</option>
          <option value="3">Fancy goldfish</option>
          <option value="4">Danio</option>
          <option value="5">Gourami</option>
          <option value="6">Guppy</option>
          <option value="7">Molly</option>
        </select>

        <label for="fish2">Select fish 2: </label>
        <select id="fish2" name="fish2">
          <option value="0">Angelfish</option>
          <option value="1">Betta fish</option>
          <option value="2">Common goldfish</option>
          <option value="3">Fancy goldfish</option>
          <option value="4">Danio</option>
          <option value="5">Gourami</option>
          <option value="6">Guppy</option>
          <option value="7">Molly</option>
        </select>
      </div>
      <input type="submit" name="check_compat" class="btn btn-default"></input>
    </form>

    <br>

    <form class="form-inline" action="" method="POST">
      <label>Convert between cm3 to liters</label>
      <br>
      <div class="form-group">
        <input type="text" name="cm3" class="form-control" placeholder="Enter cm3" required />
      </div>
      <input type="submit" name="cm3_to_liters" class="btn btn-default"></input>
    </form>

    <br>

    <form class="form-inline" action="" method="POST">
      <label>Calculate electricity cost</label>
      <br>
      <div class="form-group">
        <input type="text" name="wattsPerHour" class="form-control" placeholder="Enter your water filter (watts per hour)" required />
      </div>
      <input type="submit" name="calculate_electricity_cost" class="btn btn-default"></input>
    </form>

    <br><br><br>

    <!-- Results -->
    <h3>

      <?php
      if (isset($_POST['calc_bowfront'])) {
        $length =  $_POST['length'];
        $width =  $_POST['width'];
        $full_width =  $_POST['full_width'];
        $height =  $_POST['height'];

        $response = $client->call("get_bowfront_volume", array("length" => $length, "width" => $width, "full_width" => $full_width, "height" => $height));

        if (empty($response))
          echo "No data to extract from the SOAP Response";
        else
          echo "The volume is: " . $response;
      } else if (isset($_POST['check_compat'])) {
        $fish1_id = $_POST["fish1"];
        $fish2_id = $_POST["fish2"];

        $response = $client->call("compare_fish_compatibility", array("fish1_id" => $fish1_id, "fish2_id" => $fish2_id));


        if ($response == 2) {
          echo "The fish are are compatible. They can live together in a single aquarium";
        } else if ($response == 1) {
          echo "The fish are are usually compatible. Keep a look out for the fish's sizes and their behavior towards each other.";
        } else {
          echo "The fish are are not compatible. They can't live together and must be separated.";
        }
      } else if (isset($_POST['cm3_to_liters'])) {
        $cm3 = $_POST["cm3"];

        $response = $client->call("cm3_to_liter", array("cm3" => $cm3));

        echo $cm3 . " cm^3 = " . $response . " liters";
      } else if (isset($_POST['calculate_electricity_cost'])) {
        $wattsPerHour = $_POST["wattsPerHour"];

        $responseCost = $client->call("calculate_electricity_cost", array("watts" => $wattsPerHour));
        $responseRate = $client->call("get_meralco_rate", array());
        $responseMonthly = $client->call("calculate_monthly", array("cost" => $responseCost));

        echo "Meralco's rate is ₱" . $responseRate . " per kw/H";
        echo "<br><br>";
        echo "The cost of your electricity per hour is ₱" . $responseCost;
        echo "<br><br>";
        echo "The monthly cost of your electricity is ₱" . $responseMonthly;
      }

      echo "<br><br><br>";

      echo "<hr>";

      // Show request and response envelopes
      echo "<h2>Request</h2>";
      echo "<pre>" . htmlspecialchars($client->request, ENT_QUOTES) . "</pre>";
      echo "<h2>Response</h2>";
      echo "<pre>" . htmlspecialchars($client->response, ENT_QUOTES) . "</pre>";

      echo "<br><br><br>";
      ?>

    </h3>
  </div>
</body>

</html>