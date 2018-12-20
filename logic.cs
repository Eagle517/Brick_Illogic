function Logic_MainTick()
{
	cancel($LBC::Schedules::MainSched);
	$LBC::Schedules::MainSched = schedule($LBC::Opts::Time, 0, "Logic_MainTick");

	for(%i = 0; %i < $LBC::Gates::UpdateQueueCount; %i++)
	{
		%gate = $LBC::Gates::UpdateQueue[%i];
		$LBC::Gates::OnQueue[%gate] = false;

		$LBC::Bricks::Datablock[%gate].doLogic(%gate);
		
		%ports = $LBC::Bricks::PortCount[%gate];
		for(%a = 0; %a < %ports; %a++)
		{
			%port = $LBC::Bricks::Port[%gate, %a];
			$LBC::Ports::LastState[%port] = $LBC::Ports::State[%port];
			$LBC::Ports::LastBrickState[%gate, %a] = $LBC::Ports::State[%port];
		}
	}

	// for(%i = 0; %i < $LBC::Gates::UpdateQueueCount; %i++)
	// 	$LBC::Gates::OnQueue[$LBC::Gates::UpdateQueue[%i]] = false;
	$LBC::Gates::UpdateQueueCount = 0;

	for(%i = 0; %i < $LBC::Groups::UpdateQueueCount; %i++)
	{
		%group = $LBC::Groups::UpdateQueue[%i];

		%newState = 0;
		%state = $LBC::Groups::State[%group];

		%wires = $LBC::Groups::WireCount[%group];
		%ports = $LBC::Groups::PortCount[%group];
		%inPorts = 0;

		for(%a = 0; %a < %ports; %a++)
		{
			%port = $LBC::Groups::Port[%group, %a];
			if($LBC::Ports::Type[%port] == 0)
			{
				if(!%newState && $LBC::Ports::State[%port])
					%newState = 1;
			}
			else
			{
				%inPort[%inPorts] = %port;
				%inPorts++;
			}
		}

		if(%state != %newState)
		{
			$LBC::Groups::State[%group] = %newState;
			%state = %newState;
		}

		for(%a = 0; %a < %inPorts; %a++)
		{
			%port = %inPort[%a];
			if($LBC::Ports::State[%port] != %state)
			{
				%obj = $LBC::Ports::Brick[%port];
				$LBC::Ports::LastState[%port] = $LBC::Ports::State[%port];
				$LBC::Ports::LastBrickState[%obj, $LBC::Ports::BrickIDX[%port]] = $LBC::Ports::State[%port];
				$LBC::Ports::State[%port] = %state;
				$LBC::Ports::BrickState[%obj, $LBC::Ports::BrickIDX[%port]] = %state;

				Logic_QueueGate(%obj);
			}
		}

		if($LBC::Opts::NoFx)
		{
			if(%newState)
			{
				for(%a = 0; %a < %wires; %a++)
				{
					%wire = $LBC::Groups::Wire[%group, %a];
					if($LBC::Wires::doEvents[%wire] && $LBC::Wires::eventState[%wire] == false)
					{
						%wire.onPowerChange();
						%wire.onPowerOn();
						$LBC::Wires::eventState[%wire] = true;
					}

					if($LBC::Wires::isVisual[%wire = $LBC::Groups::Wire[%group, %a]])
						%wire.setColorFX(3);
				}
			}
			else
			{
				for(%a = 0; %a < %wires; %a++)
				{
					%wire = $LBC::Groups::Wire[%group, %a];
					if($LBC::Wires::doEvents[%wire] && $LBC::Wires::eventState[%wire] == true)
					{
						%wire.onPowerChange();
						%wire.onPowerOff();
						$LBC::Wires::eventState[%wire] = false;
					}
					%wire.setColorFX(0);
				}
			}
		}
		else
		{
			if(%newState)
			{
				for(%a = 0; %a < %wires; %a++)
				{
					%wire = $LBC::Groups::Wire[%group, %a];
					if($LBC::Wires::doEvents[%wire] && $LBC::Wires::eventState[%wire] == false)
					{
						%wire.onPowerChange();
						%wire.onPowerOn();
						$LBC::Wires::eventState[%wire] = true;
					}
					%wire.setColorFX(3);
				}
			}
			else
			{
				for(%a = 0; %a < %wires; %a++)
				{
					%wire = $LBC::Groups::Wire[%group, %a];
					if($LBC::Wires::doEvents[%wire] && $LBC::Wires::eventState[%wire] == true)
					{
						%wire.onPowerChange();
						%wire.onPowerOff();
						$LBC::Wires::eventState[%wire] = false;
					}
					%wire.setColorFX(0);
				}
			}
		}

		$LBC::Groups::OnQueue[%group] = false;
	}

	// for(%i = 0; %i < $LBC::Groups::UpdateQueueCount; %i++)
	// 	$LBC::Groups::OnQueue[$LBC::Groups::UpdateQueue[%i]] = false;
	$LBC::Groups::UpdateQueueCount = 0;
}
Logic_MainTick();


function Logic_QueueGroup(%group)
{
	%group = %group | 0;
	if(%group != -1 && !$LBC::Groups::OnQueue[%group])
	{
		$LBC::Groups::OnQueue[%group] = true;
		$LBC::Groups::UpdateQueue[$LBC::Groups::UpdateQueueCount+0] = %group;
		$LBC::Groups::UpdateQueueCount++;
	}
}

function Logic_QueueGate(%gate)
{
	if(!$LBC::Gates::OnQueue[%gate])
	{
		$LBC::Gates::OnQueue[%gate] = true;
		$LBC::Gates::UpdateQueue[$LBC::Gates::UpdateQueueCount+0] = %gate;
		$LBC::Gates::UpdateQueueCount++;
	}
}

function fxDTSBrick::Logic_SetOutput(%this, %port, %state)
{
	%portID = $LBC::Bricks::Port[%this, %port];
	if($LBC::Ports::State[%portID] != %state)
	{
		$LBC::Ports::State[%portID] = %state;
		$LBC::Ports::BrickState[%this, %port] = %state;
		
		%group = $LBC::Ports::Group[%portID];
		if(%group != -1 && !$LBC::Groups::OnQueue[%group] && !$LBC::Gates::OnQueue[%this])
		{
			$LBC::Groups::OnQueue[%group] = true;
			$LBC::Groups::UpdateQueue[$LBC::Groups::UpdateQueueCount+0] = %group;
			$LBC::Groups::UpdateQueueCount++;
		}
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
						%wires = $LBC::Groups::WireCount[%group];
						%ports = $LBC::Groups::PortCount[%group];
						%ins = 0;
						%outs = 0;
						for(%i = 0; %i < %ports; %i++)
						{
							%port = $LBC::Groups::Port[%i];
							if($LBC::Ports::Type[$LBC::Groups::Port[%group, %i]] == 0)
								%outs++;
							else
								%ins++;
						}
						%visual = $LBC::Wires::isVisual[%hit] ? "Visual ":"";
						%client.centerPrint("<font:consolas:20>\c5"@%visual@"Wire\n\c5State:\c6 " @ (%state ? "\c2ON":"\c0OFF") NL "\c5Color:\c6 " @ %hit.getColorID() NL "\c5Group:\c6 " @ %group NL "\c5Wires:\c6 "@%wires NL "\c5Ports: \c6"@%ins@" in, "@%outs@" out", 3);
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

							%vCheck = 0.01;

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
							else if(%nz == 1 || %nz == -1)
								%vCheck = 0.1;

							%hitNorm = %nx SPC %ny SPC %nz;

							%rotDir[0] = "-1 0 0";
							%rotDir[1] = "0 1 0";
							%rotDir[2] = "1 0 0";
							%rotDir[3] = "0 -1 0";
							%rotDir[4] = "0 0 1";
							%rotDir[5] = "0 0 -1";

							%bestDist = -1;
							%ports = $LBC::Bricks::PortCount[%hit];
							%states = "";
							for(%i = 0; %i < %ports; %i++)
							{
								%port = $LBC::Bricks::Port[%hit, %i];
								%state = $LBC::Ports::State[%port];
								if(%state != %lastState)
								{
									%lastState = %state;
									%states = %states @ (%state ? "\c2":"\c0");
								}
								%states = %i == 0 ? %states @ %data.logicPortUIName[%i] : %states SPC %data.logicPortUIName[%i];

								if(%rotDir[$LBC::Ports::Dir[%port]] $= %hitNorm)
								{
									%xx = %hx-$LBC::Ports::ConnPos[%port, 0];
									%yy = %hy-$LBC::Ports::ConnPos[%port, 1];
									%zz = %hz-$LBC::Ports::ConnPos[%port, 2];
									%hdistSqr = (%xx*%xx)+(%yy*%yy);
									%vdistSqr = %zz*%zz;
									%distSqr = %hdistSqr + %vdistSqr;
									if((%vdistSqr < %vCheck && %hdistSqr < 0.0649) && (%distSqr < %bestDist || %bestDist == -1))
									{
										%bestDist = %distSqr;
										%bestPort = %port;
										%bestIdx = %i;
									}
									//%client.bottomPrint(%vCheck, 5);
								}
							}

							%gateName = %data.logicUIName;
							if(%bestDist == -1)
							{
								%states = trim(%states);
								%gateDesc = %data.logicUIDesc;
								%client.centerPrint("<font:consolas:20>\c6"@%gateName NL "<color:ffffff>"@%gateDesc NL "<color:ff0000>"@%states, 3);
								return;
							}
							
							%type = $LBC::Ports::Type[%bestPort];
							%name = (%data.logicPortUIName[%bestIdx] $= "") ? "Port " @ %bestIdx : %data.logicPortUIName[%bestIdx];
							%desc = %data.logicPortUIDesc[%bestIdx];
							%state = $LBC::Ports::State[%bestPort];
							%group = $LBC::Ports::Group[%bestPort];

							if(trim(%desc) !$= "")
								%text = "\c6"@%gateName NL "\c5Port: "@%name NL "<color:ffffff>"@%desc NL "\c5State: "@ (%state ? "\c2ON":"\c0OFF") NL "\c5Group: \c6"@%group;
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
