exec("./wires.cs");
exec("./gates.cs");
exec("./logic.cs");
exec("./bricks.cs");
exec("./gatemaker.cs");

if($LBC::Opts::Time $= "")
	$LBC::Opts::Time = 1;

function serverCmdLFX(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$LBC::Opts::NoFx = !$LBC::Opts::NoFx;
		messageAll('', '\c3%1\c6 has %2 logic FX updates.', %client.name, $LBC::Opts::NoFx ? "disabled":"enabled");
	}
}

function serverCmdLT(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		if(isEventPending($LBC::Schedules::MainSched))
		{
			cancel($LBC::Schedules::MainSched);
			messageAll('', '\c3%1\c6 has disabled the logic tick.', %client.name);
		}
		else
		{
			Logic_MainTick();
			messageAll('', '\c3%1\c6 has enabled the logic tick.', %client.name);
		}
	}
}

function serverCmdLST(%client, %time)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$LBC::Opts::Time = mClamp(%time, 0, 999999);
		messageAll('', '\c3%1\c6 has set the logic tick time to \c3%2\c6 millisecond%3.', %client.name, $LBC::Opts::Time, %time == 1 ? "":"s");
	}
}
