<?php

require_once ("PHPStream.php" );


$myData=mysqli_connect( "mysql.student.sussex.ac.uk" ,"wcwc20" ,"vgf604" );
if ( mysqli_connect_errno())
{
	echo mysqli_connect_error();
	return;
}

mysqli_query($myData,"set names utf8") ;
mysqli_select_db( $myData ,"wcwc20" );


$sql = "SELECT name, score FROM hiscores ORDER by score DESC LIMIT 20 ";

$result = mysqli_query($myData,$sql)or die("<br>SQL error!<br/>");
$num_results = mysqli_num_rows($result);

$webstream=new PHPStream();
$webstream->BeginWrite(PKEY);

$webstream->WriteInt($num_results);

for($i = 0; $i < $num_results; $i++)
{
	$row = mysqli_fetch_array($result ,MYSQLI_ASSOC);

	$data[$i][0]=$row['name'];
	$data[$i][1]=$row['score'];

	$webstream->WriteString($data[$i][0]);
	$webstream->WriteString($data[$i][1]);
	
	//echo $data[$i][0];
	//echo $data[$i][1];
}

$webstream->EndWrite();

mysqli_free_result($result);

mysqli_close($myData); 

echo $webstream->bytes;

?>