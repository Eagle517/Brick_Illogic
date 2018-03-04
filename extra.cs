function RoundToBrickPos(%pos, %ox, %oy, %oz, %xr, %yr, %zr)
{
	%x = getWord(%pos, 0)+%ox;
	%y = getWord(%pos, 1)+%oy;
	%z = getWord(%pos, 2)+%oz;
	
	%x = mFloor(%x*(1/%xr)+0.5)/(1/%xr)-%ox;
	%y = mFloor(%y*(1/%yr)+0.5)/(1/%yr)-%oy;
	%z = mFloor(%z*(1/%zr)+0.5)/(1/%zr)-%oz;

	return %x SPC %y SPC %z;
}

function LMakeFloor(%w, %l)
{
	%z = 80;
	for(%x = -%w; %x <= %w; %x++)
	{
		for(%y = -%l; %y <= %l; %y++)
		{
			%pos = %x*32 SPC %y*32 SPC %z;
			%group = Brickgroup_888888;
			%brick = new fxDTSBrick()
			{
				datablock = brick64xCubeData;
				position = %pos;
				colorID = 31;
				isPlanted = 1;
				client = %group.client;
				bl_id = %group.bl_id;
			};
			%err = %brick.plant();
			if(%err && %err != 2)
				%brick.delete();
			else
			{
				%brick.setTrusted(1);
				%group.add(%brick);
			}
		}
	}
}

function LPlayerTick()
{
	cancel($LPsched);
	$LPSched = schedule(200, 0, LPlayerTick);
	%count = ClientGroup.getCount();
	for(%i = 0; %i < %count; %i++)
	{
		if(isObject(%player = ((%client = ClientGroup.getObject(%i)).player)))
		{
			if(vectorDist(%player.getPosition(), 0) >= 1600)
				%client.instantRespawn();
		}
	}
}

package IllogicExtra
{
	function serverCmdNextSeat(%client)
	{
		if(isObject(%player = %client.player))
		{
			if(isObject(%player.getObjectMount()))
				parent::serverCmdNextSeat(%client);
			else
			{
				%control = %client.getControlObject();
				%eye = %control.getEyePoint();
				%vec = %control.getEyeVector();
				%ray = containerRayCast(%eye, vectorAdd(%eye, vectorScale(%vec, 5*getWord(%control.getScale(), 2))), $TypeMasks::FxBrickObjectType);
				if(isObject(%hit = firstWord(%ray)))
					serverCmdUseSprayCan(%client, %hit.getColorID());
			}
		}
	}
};
activatePackage("IllogicExtra");
