<?php
try {
    $dbh = new PDO('mysql:host=localhost;dbname=bp', 'root','');
	
	$level = $_POST['level'];
	$win = $_POST['win'];
	$time = $_POST['time'];
	
	$stm = $dbh->prepare("insert into stat values (:level,:time,:win)");
	$stm->bindParam(':win',$win);
	$stm->bindParam(':level',$level);
	$stm->bindParam(':time',$time);
	$stm->execute();

    $dbh = null;
} catch (PDOException $e) {
    print "Error!: " . $e->getMessage() . "<br/>";
    die();
}
?>