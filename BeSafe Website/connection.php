
<?php
    $db = new mysqli("localhost", "root", "", "besafe");
    if ($db->connect_error)
    	die('Connection error: '.$db->connect_error);
?>