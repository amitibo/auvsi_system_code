<?php
error_reporting(E_ALL);
$host = "localhost"; 
$user = "openCV"; 
$pass = "rK8sFzmWVFJ4CMm5"; 
$dbname = "auvsi";
$crop_dir = "/var/www/crops/*";
$img_dir = "/var/www/images/*";
$bk_dir = "/var/www/backups/";

$files = $bk_dir."INS_".(1+count(glob($bk_dir."*"))).".csv";

$conn = mysqli_connect($host, $user, $pass, $dbname);
if(!$conn)
{
    echo "<error>Could not connect to server</error>";
    trigger_error(mysqli_error(), E_USER_ERROR);
}


$result = mysqli_query($conn, "SELECT * FROM `INS`;");
$row_cnt = mysqli_num_rows($result);
if ($row_cnt > 0){
    $fh = fopen($files, 'w');
    $finfo = mysqli_fetch_fields($result);
    $last = end($finfo);
    foreach ($finfo as $val){
        fwrite($fh, $val->name);
        if ($val != $last){
            fwrite($fh, ",\t");
            }
    }
    fwrite($fh, "\n");
    while ($row = mysqli_fetch_array($result)) {
        $last = end($row);
        foreach ($row as $item) {
            fwrite($fh, $item);
            if ($item != $last)
                fwrite($fh, ",\t");
        }
        fwrite($fh, "\n");
    }
    fclose($fh);
    echo "Backed up INS data.<br>";
}

$sqlA = "TRUNCATE TABLE `images`";
$sqlB = "TRUNCATE TABLE `crops`";
$sqlC = "TRUNCATE TABLE `INS`";


//$db = mysql_select_db($dbname);
$rs = mysqli_query($conn, $sqlA);
$rs = mysqli_query($conn, $sqlB);
$rs = mysqli_query($conn, $sqlC);
//mysqli_query($sql);
mysqli_close($conn);


$files = glob($crop_dir); // get all file names
foreach($files as $file){ // iterate files
  if(is_file($file))
    unlink($file); // delete file
}

$files = glob($img_dir); // get all file names
foreach($files as $file){ // iterate files
  if(is_file($file))
    unlink($file); // delete file
}


echo "Deleted all data.\n";

?>
