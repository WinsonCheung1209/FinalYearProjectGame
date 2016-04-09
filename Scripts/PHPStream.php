<?php

define("BYTE",1);
define("SHORT",2);
define("INT",4);
define("FLOAT",4);
define("HASHSIZE",16);
define ("PKEY","123456");

class PHPStream
{
	private $Key="";

	public $bytes="";

	public $Content="";

	public $index=0;

	public $ErrorRead = false;

	// ---------------------------------------------
	// write stream
	// ---------------------------------------------
	function BeginWrite( $key )
	{
		$this->index=0;
		$this->bytes="";
		$this->Content="";
		$this->ErrorRead=false;

		//total bytes length
		$this->WriteShort(0);

		if ( strlen($key)>0 )
		{
			$this->Key=$key;
		}

	}

	function WriteByte( $byte )
	{
		//$this->bytes.=pack('c',$byte);
		$this->bytes.=$byte;
		$this->index+=BYTE;
	}

	function WriteShort( $number )
	{
		$this->bytes.=pack("v",$number);
		$this->index+=SHORT;
	}

	function WriteInt( $number )
	{
		$this->bytes.=pack("V",$number);
		$this->index+=INT;
	}

	function WriteFloat( $number )
	{
		$this->bytes.=pack("f",$number);
		$this->index+=FLOAT;
	}

	function WriteString( $str )
	{
		$len=strlen($str);
		$this->WriteShort($len);

		$this->bytes.=$str;

		$this->index+=$len;
	}

	function WriteBytes( $bytes )
	{
		$len=strlen($bytes);
		$this->WriteShort($len);

		$this->bytes.=$bytes;

		$this->index+=$len;
	}

	function EndWrite()
	{
		// sum
		if (strlen($this->Key)>0)
		{
			$len=$this->index+HASHSIZE;
			$str=pack("v",$len);

			$this->bytes[0]=$str[0];
			$this->bytes[1]=$str[1];

			$hashbytes=md5($this->bytes.$this->Key,true);

			$this->bytes.=$hashbytes;

		}
		else{

			$str=pack("v",$this->index);

			$this->bytes[0]=$str[0];
			$this->bytes[1]=$str[1];

		}

	}

	// ************************************************************************************
	// ---------------------------------------------
	// read stream
	// ---------------------------------------------
	function BeginRead( $key )
	{
		$this->index=0;
		$this->bytes="";
		$this->Content="";
		$this->ErrorRead=false;

		if ( strlen($key)>0 )
		{
			$this->Key=$key;
		}

	}

	function Read( $head )
	{
		if ( isset($_POST[$head])  )
		{
			$this->Content.=$_POST[$head];
			return $_POST[$head];
		}
		else
		{
			$this->ErrorRead=true;
		}
	}


	function EndRead()
	{
		if ($this->ErrorRead)
		{
			return false;
		}

		// if not use a key
		if (strlen($this->Key)<1)
			return true;

		// record hashkey
		$hashkey="";
		if ( isset($_POST["key"])  )
		{
			$hashkey=$_POST["key"];
		}
		else
		{
			$this->ErrorRead=true;
			return false;
		}

		// hash it again
		$localhash=md5($this->Content.$this->Key);

		// if ok
		if (strcmp($hashkey,$localhash)==0)
		{
			return true;
		}
		else
		{
			$this->ErrorRead=true;
			return false;
		}

	}
}

?>