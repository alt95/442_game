<?php
try {
    $dbh = new PDO('mysql:host=localhost;dbname=bp', 'root','');
	
	$abi = $_POST['ability'];
	$level = $_POST['level'];

	$stm = $dbh->prepare("insert into levelatt values (:level,:abi)");
	$stm->bindParam(':abi',$abi);
	$stm->bindParam(':level',$level);
	$stm->execute();

    $dbh = null;
} catch (PDOException $e) {
    print "Error!: " . $e->getMessage() . "<br/>";
    die();
}
?>