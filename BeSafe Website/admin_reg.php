
<?php

include_once('connection.php');

$name = $_POST['name']; //Name
$p_word = $_POST['p_word']; //password
$phn = $_POST['phn']; //phn
$email  = $_POST['email']; // email
$check = getimagesize($_FILES["pic"]["tmp_name"]);
if($check !== false){
        $image = $_FILES['pic']['tmp_name'];
        $imgContent = addslashes(file_get_contents($image));
$imgContent = addslashes(file_get_contents($image));
}

$query = "INSERT INTO besafe.admin(name, phn, email, p_word, profile) VALUES('$name', $phn, '$email', '$p_word', '$imgContent')";

$result = $db->query($query);
echo $db->error;
if ($result)
	echo "<script> alert('Inserted');</script>";
else
	echo "<script> alert('Not Inserted');</script>";
?>	