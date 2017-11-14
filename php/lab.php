<?php
try {
    $dbh = new PDO('mysql:host=localhost;dbname=bp', 'root','');
	
	$abi = $_POST['ability'];

	$stm = $dbh->prepare("update lab set clicks = clicks+1 where type = :abi");
	$stm->bindParam(':abi',$abi);
	$stm->execute();

    $dbh = null;
} catch (PDOException $e) {
    print "Error!: " . $e->getMessage() . "<br/>";
    die();
}
?>