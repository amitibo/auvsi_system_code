<?php
error_reporting(E_ALL);
header('Content-type: text/xml');

$host = "localhost"; 
$user = "openCV"; 
$pass = "pass"; 
$dbname = "auvsi";
$crop_dir = "crops/";
$img_dir = "images/";
$img_prefix = "";

$query = "SELECT *  FROM `images` WHERE `sent` = 'false' ORDER BY `images`.`id` ASC limit 1 ;";
if(isset($_GET['action']))
{
	if ($_GET['action'] == "latest")
	{
		$query = "SELECT *  FROM `images` WHERE `sent` = 'false' ORDER BY `images`.`id` DESC limit 1 ;";
	}
	if (intval($_GET['action']) > 0)
	{
		$query = "SELECT *  FROM `images` WHERE `id` = ".intval($_GET['action']).";";
	}
}

$conn = mysqli_connect($host, $user, $pass, $dbname);

if(!$conn)
{
    echo "<error>Could not connect to server</error>";
    trigger_error(mysqli_error(), E_USER_ERROR);
}
//$db = mysql_select_db($dbname);
$rs = mysqli_query($conn, $query);
if (!$rs)
{
	echo "<error>Could not execute query: $query</error>";
	trigger_error(mysqli_error(), E_USER_ERROR); 
}
else
{
	$nrows = mysqli_num_rows($rs);
}
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
echo "<AUVSI>";

while ($row = mysqli_fetch_assoc($rs))
{
	$id = $row['id'];
	$cropsql = "SELECT *  FROM `crops` WHERE `fatherid` = ".$id;
	$croprs = mysqli_query($conn, $cropsql);
	$ncrops = mysqli_num_rows($croprs);
?>

	<image id="<? echo $row['id'];?>" time="<? echo $row['time'];?>.<? printf('%06d', $row['milisec']);?>">
		<location altitude="<? echo $row['alt'];?>" lon="<? echo $row['lon'];?>" lat="<? echo $row['lat'];?>"/>
		<gimbal roll="<? echo $row['groll'];?>" pitch="<? echo $row['gpitch'];?>" yaw="<? echo $row['gyaw'];?>"/>
		<large_img src="<? echo $img_dir.$img_prefix.$row['id'].".jpg";?>"/>
		<crops>
		<? while ($croprow = mysqli_fetch_assoc($croprs)){?>
	<img id="<? echo $croprow['id'];?>" src="<? echo $crop_dir."crop_".$row['id']."_".$croprow['id'].".jpg";?>" C1="<? echo $croprow['c1x'];?>,<? echo $croprow['c1y'];?>" C2="<? echo $croprow['c2x'];?>,<? echo $croprow['c2y'];?>" C3="<? echo $croprow['c3x'];?>,<? echo $croprow['c3y'];?>" C4="<? echo $croprow['c4x'];?>,<? echo $croprow['c4y'];?>"/>
		<?}?></crops>
	</image>
<?php
}
?>
</AUVSI><?php
if(isset($id))
{
    $sql = "UPDATE `auvsi`.`images` SET `sent` = 'dddsent' WHERE `images`.`id` =".$id;
    mysqli_query($conn, $sql);
}
mysqli_close($conn);


?>