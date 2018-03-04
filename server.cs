exec("./wires.cs");
exec("./gates.cs");
exec("./logic.cs");
exec("./bricks.cs");
exec("./gatemaker.cs");

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
