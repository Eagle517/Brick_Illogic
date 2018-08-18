exec("./wires.cs");
exec("./gates.cs");
exec("./logic.cs");
exec("./bricks.cs");
exec("./gatemaker.cs");

if($LBC::Opts::Time $= "")
	$LBC::Opts::Time = 1;
if($LBC::Opts::Enabled $= "")
	$LBC::Opts::Enabled = true;

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
			$LBC::Opts::Enabled = false;
			cancel($LBC::Schedules::MainSched);
			messageAll('', '\c3%1\c6 has disabled the logic tick.', %client.name);
		}
		else
		{
			$LBC::Opts::Enabled = true;
			Logic_MainTick();
			messageAll('', '\c3%1\c6 has enabled the logic tick.', %client.name);
		}
	}
}

function serverCmdLST(%client, %time)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		$LBC::Opts::Time = mClamp(%time, 1, 999999); //Weird jittery behavior when set to 0
		messageAll('', '\c3%1\c6 has set the logic tick time to \c3%2\c6 millisecond%3.', %client.name, $LBC::Opts::Time, %time == 1 ? "":"s");
	}
}

function serverCmdLS(%client)
{
	if(%client.isAdmin || %client.isSuperAdmin)
	{
		Logic_MainTick();
		if($LBC::Opts::Enabled == false)
			cancel($LBC::Schedules::MainSched);
		messageAll('', '\c3%1\c6 has forced a logic tick.', %client.name);
	}
}
