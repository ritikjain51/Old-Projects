
<?php

include_once('connection.php');

$name = $_POST['name']; //Name
$reg_no = $_POST['reg_no']; //Reg_no
$p_word = $_POST['p_word']; //password
$phn = $_POST['phn']; //phn
$email  = $_POST['email']; // email
$relation = $_POST['relation']; //relation
$address = $_POST['address']; //address
$check = getimagesize($_FILES["pic"]["tmp_name"]);
echo "$reg_no";
if($check !== false){
        $image = $_FILES['pic']['tmp_name'];
        $imgContent = addslashes(file_get_contents($image));
$imgContent = addslashes(file_get_contents($image));
}

$query = "INSERT INTO besafe.parent(name, email, phn, address, relation, profile, p_word, reg_no) VALUES('$name', '$email', $phn, '$address', '$relation', '$imgContent', '$p_word', '$reg_no')";

$result = $db->query($query);
echo $db->error;
if ($result)
	echo "<script> alert('Inserted');</script>";
else
	echo "<script> alert('Not Inserted');</script>";
?>	