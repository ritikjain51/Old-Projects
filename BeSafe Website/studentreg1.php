
<?php

include_once('connection.php');

$name = $_POST['name'];
$reg_no = $_POST['regno'];
$pword = $_POST['pword'];
$phn = $_POST['phn'];
$email  = $_POST['email'];
$par_email = $_POST['par_email'];
$curr_add = $_POST['curr_add'];
$check = getimagesize($_FILES["pic"]["tmp_name"]);

if($check !== false){
        $image = $_FILES['pic']['tmp_name'];
        $imgContent = addslashes(file_get_contents($image));
$imgContent = addslashes(file_get_contents($image));
}

$query = "INSERT INTO besafe.student(name, reg_no, p_word, phn, email, par_email, curr_add, profile) VALUES('$reg_no', '$name', '$pword', $phn, '$email', '$par_email', '$curr_add', '$imgContent')";

$result = $db->query($query);
echo "<script> alert($db->error);</script>";
header("login.php");
?>	