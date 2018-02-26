function serverCMDLT(%client)
{
	if(%client.isAdmin && %client.bl_id != 49803)
	{
		if(isEventPending($LBC::Schedules::MainSched))
		{
			cancel($LBC::Schedules::MainSched);
			talk("Logic Tick Disabled (" @ %client.name @ ")");
		}
		else
		{
			Logic_MainTick();
			talk("Logic Tick Enabled (" @ %client.name @ ")");
		}
	}
}

function Logic_MainTick()
{
	cancel($LBC::Schedules::MainSched);
	$LBC::Schedules::MainSched = schedule(1, 0, "Logic_MainTick");

	//%time = getRealTime();
	//bottomPrintAll("logic tick: "@%time-$LBC::Schedules::LastUpdate@"ms", 1, 1);
	//$LBC::Schedules::LastUpdate = %time;

	$LBC::Groups::Tick = !$LBC::Groups::Tick;
	if($LBC::Groups::Tick)
	{
		for(%i = 0; %i < $LBC::Groups::UpdateQueueCount; %i++)
		{
			%group = $LBC::Groups::UpdateQueue[%i];

			%newState = 0;
			%state = $LBC::Groups::State[%group];

			%wires = $LBC::Groups::WireCount[%group];
			%ports = $LBC::Groups::PortCount[%group];

			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Groups::Port[%group, %a];
				if($LBC::Ports::Type[%port] == 0)
				{
					if(!%newState && $LBC::Ports::State[%port])
						%newState = 1;
				}
			}

			if(%state != %newState)
			{
				$LBC::Groups::State[%group] = %newState;
				%state = %newState;
			}
			$LBC::Groups::OnQueue[%group] = false;

			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Groups::Port[%group, %a];
				if($LBC::Ports::Type[%port] == 1)
				{
					if($LBC::Ports::State[%port] != %state)
					{
						%obj = $LBC::Ports::Brick[%port];
						if(!%obj)
							continue;
						%oidx = $LBC::Ports::BrickIDX[%port];
						$LBC::Ports::State[%port] = %state;
						$LBC::Ports::BrickState[%obj, %oidx] = %state;
						$LBC::Bricks::Datablock[%obj].doLogic(%obj);
					}
				}
			}

			if(%newState)
			{
				for(%a = 0; %a < %wires; %a++)
				{
					if(!isObject($LBC::Groups::Wire[%group, %a]) && !%bw[%group])
					{
						%bw[%group] = true;
						echo("bad wire in group " @ %group);
					}
					$LBC::Groups::Wire[%group, %a].setColorFX(3);
				}
			}
			else
			{
				for(%a = 0; %a < %wires; %a++)
				{
					if(!isObject($LBC::Groups::Wire[%group, %a]) && !%bw[%group])
					{
						%bw[%group] = true;
						echo("bad wire in group " @ %group);
					}
					$LBC::Groups::Wire[%group, %a].setColorFX(0);
				}
			}
		}
		$LBC::Groups::UpdateQueueCount = 0;
	}
	else
	{
		for(%i = 0; %i < $LBC::Groups::NUpdateQueueCount; %i++)
		{
			%group = $LBC::Groups::NUpdateQueue[%i];

			%newState = 0;
			%state = $LBC::Groups::State[%group];

			%wires = $LBC::Groups::WireCount[%group];
			%ports = $LBC::Groups::PortCount[%group];

			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Groups::Port[%group, %a];
				if($LBC::Ports::Type[%port] == 0)
				{
					if(!%newState && $LBC::Ports::State[%port])
						%newState = 1;
				}
			}

			if(%state != %newState)
			{
				$LBC::Groups::State[%group] = %newState;
				%state = %newState;
			}
			$LBC::Groups::OnNQueue[%group] = false;

			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Groups::Port[%group, %a];
				if($LBC::Ports::Type[%port] == 1)
				{
					if($LBC::Ports::State[%port] != %state)
					{
						%obj = $LBC::Ports::Brick[%port];
						if(!%obj)
							continue;
						%oidx = $LBC::Ports::BrickIDX[%port];
						$LBC::Ports::State[%port] = %state;
						$LBC::Ports::BrickState[%obj, %oidx] = %state;
						$LBC::Bricks::Datablock[%obj].doLogic(%obj);
					}
				}
			}

			if(%newState)
			{
				for(%a = 0; %a < %wires; %a++)
				{
					if(!isObject($LBC::Groups::Wire[%group, %a]) && !%bw[%group])
					{
						%bw[%group] = true;
						echo("bad wire in group " @ %group);
					}
					$LBC::Groups::Wire[%group, %a].setColorFX(3);
				}
			}
			else
			{
				for(%a = 0; %a < %wires; %a++)
				{
					if(!isObject($LBC::Groups::Wire[%group, %a]) && !%bw[%group])
					{
						%bw[%group] = true;
						echo("bad wire in group " @ %group);
					}
					$LBC::Groups::Wire[%group, %a].setColorFX(0);
				}
			}
		}
		$LBC::Groups::NUpdateQueueCount = 0;
	}
}
Logic_MainTick();

function fxDTSBrick::Logic_SetOutput(%this, %port, %state)
{
	%portID = $LBC::Bricks::Port[%this, %port];
	if($LBC::Ports::State[%portID] != %state)
	{
		$LBC::Ports::State[%portID] = %state;
		$LBC::Ports::BrickState[%this, %port] = %state;
		Logic_QueueGroup($LBC::Ports::Group[%portID]);
	}
}

package IllogicLogic
{
	function Player::activateStuff(%this, %a, %b)
	{
		parent::activateStuff(%this, %a, %b);
		if(isObject(%client = %this.client))
		{
			%eye = %this.getEyePoint();
			%vec = %this.getEyeVector();
			%ray = containerRayCast(%eye, vectorAdd(%eye, vectorScale(%vec, 5*getWord(%this.getScale(), 2))), $TypeMasks::FxBrickObjectType);
			if(isObject(%hit = firstWord(%ray)))
			{
				if($LBC::Bricks::isLogic[%hit])
				{
					if($LBC::Bricks::isWire[%hit])
					{
						%group = $LBC::Wires::Group[%hit];
						%state = $LBC::Groups::State[%group];
						%client.centerPrint("<font:consolas:20>\c5Wire\n\c5State:\c6 " @ (%state ? "\c2ON":"\c0OFF") NL "\c5Color:\c6 " @ %hit.getColorID() NL "\c5Group:\c6 " @ %group, 3);
					}
					else if($LBC::Bricks::isGate[%hit])
					{
						%data = $LBC::Bricks::Datablock[%hit].getID();
						if(%data.isLogicInput)
							%data.Logic_onInput(%hit, %hitPos, %hitNorm);
						else
						{
							%hitPos = getWords(%ray, 1, 3);
							%hx = getWord(%hitPos, 0);
							%hy = getWord(%hitPos, 1);
							%hz = getWord(%hitPos, 2);

							%hitNorm = vectorNormalize(getWords(%ray, 4, 6));
							%nx = atoi(getWord(%hitNorm, 0)|0);
							if(%nx < -1 || %nx > 1)
								%nx = 0;
							%ny = atoi(getWord(%hitNorm, 1)|0);
							if(%ny < -1 || %ny > 1)
								%ny = 0;
							%nz = atoi(getWord(%hitNorm, 2)|0);
							if(%nz < -1 || %nz > 1)
								%nz = 0;

							%hitNorm = %nx SPC %ny SPC %nz;
							//talk(%hitNorm);

							%rotDir[0] = "1 0 0";
							%rotDir[1] = "0 1 0";
							%rotDir[2] = "-1 0 0";
							%rotDir[3] = "0 -1 0";
							%rotDir[4] = "0 0 1";
							%rotDir[5] = "0 0 -1";

							%bestDist = -1;
							%ports = $LBC::Bricks::PortCount[%hit];
							for(%i = 0; %i < %ports; %i++)
							{
								%port = $LBC::Bricks::Port[%hit, %i];
								if(%rotDir[$LBC::Ports::Dir[%port]] $= %hitNorm)
								{
									%xx = %hx-$LBC::Ports::ConnPos[%port, 0];
									%yy = %hy-$LBC::Ports::ConnPos[%port, 1];
									%zz = %hz-$LBC::Ports::ConnPos[%port, 2];
									%hdistSqr = (%xx*%xx)+(%yy*%yy);
									%vdistSqr = %zz*%zz;
									%distSqr = %hdistSqr + %vdistSqr;
									if((%vdistSqr < 0.01 && %hdistSqr < 0.0649) && (%distSqr < %bestDist || %bestDist == -1))
									{
										%bestDist = %distSqr;
										%bestPort = %port;
										%bestIdx = %i;
									}
								}
							}

							%gateName = %data.logicUIName;
							if(%bestDist == -1)
							{
								%gateDesc = %data.logicUIDesc;
								%client.centerPrint("<font:consolas:20>\c6"@%gateName NL "\c6"@%gateDesc, 3);
								return;
							}
							
							%type = $LBC::Ports::Type[%bestPort];
							%name = (%data.logicPortUIName[%bestIdx] $= "") ? "Port " @ %bestIdx : %data.logicPortUIName[%bestIdx];
							%desc = %data.logicPortUIDesc[%bestIdx];
							%state = $LBC::Ports::State[%bestPort];
							%group = $LBC::Ports::Group[%bestPort];

							if(trim(%desc) !$= "")
								%text = "\c6"@%gateName NL "\c5Port: "@%name NL "\c6"@%desc NL "\c5State: "@ (%state ? "\c2ON":"\c0OFF") NL "\c5Group: \c6"@%group;
							else
								%text = "\c6"@%gateName NL "\c5Port: \c6"@%name NL "\c5State: "@ (%state ? "\c2ON":"\c0OFF") NL "\c5Group: \c6"@%group;

							%client.centerPrint("<font:consolas:20>"@%text, 3);
						}
					}
				}
			}
		}
	}

	function onServerDestroyed()
	{
		cancel($LBC::Schedules::MainSched);
		deleteVariables("$LBC::*");
		parent::onServerDestroyed();
	}
};
activatePackage("IllogicLogic");
