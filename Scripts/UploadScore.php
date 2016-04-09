<?php
require_once ("PHPStream.php" );

$webstream=new PHPStream();
$webstream->BeginRead("123456");
$UserID=$webstream->Read('name');			// user name
$hiscore=$webstream->Read('score');		// hi score
$b=$webstream->EndRead();
if ( !$b )
{
	exit("md5 error");
}

$myData=mysqli_connect( "mysql.student.sussex.ac.uk" ,"wcwc20" ,"vgf604" );
if ( mysqli_connect_errno()) 
{
	echo mysqli_connect_error();
	return;
}

$UserID=mysqli_real_escape_string($myData,$UserID);

mysqli_query($myData,"set names utf8") ;
mysqli_select_db( $myData ,"wcwc20" );


$sql="insert into hiscores value('$UserID','$hiscore')";
mysqli_query($myData,$sql);

mysqli_close($myData); 

?>