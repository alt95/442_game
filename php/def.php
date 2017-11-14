<?php
try {
    $dbh = new PDO('mysql:host=localhost;dbname=bp', 'root','');
	
	$level = $_POST['level'];
	$corr = $_POST['corr'];
	$time = $_POST['time'];
	
	if($corr == 1) {
	$stm = $dbh->prepare("update leveldef set correct = correct + 1  where level = :level");	
	$stm->bindParam(':level',$level);
	$stm->execute();
	} else {
	$stm = $dbh->prepare("update leveldef set wrong = wrong + 1  where level = :level");	
	$stm->bindParam(':level',$level);
	$stm->execute();
	}
	

    $dbh = null;
} catch (PDOException $e) {
    print "Error!: " . $e->getMessage() . "<br/>";
    die();
}
?>