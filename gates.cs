if($LBC::Ports::NumPorts $= "")
	$LBC::Ports::NumPorts = 0;

function Logic_AddGate(%obj)
{
	%data = %obj.getDatablock();
	%box = %obj.getWorldBox();
	%size = vectorSub(getWords(%box, 3, 5), getWords(%box, 0, 2));
	
	%pos = %obj.getPosition();
	%px = getWord(%pos, 0);
	%py = getWord(%pos, 1);
	%pz = getWord(%pos, 2);

	%dataName = %data.getName();
	$LBC::Bricks::Datablock[%obj] = %dataName;
	
	initContainerBoxSearch(%pos, vectorAdd(%size, "0.02 0.02 0.02"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		if($LBC::Bricks::isLogic[%sobj])
		{
			%doRay = true;
			break;
		}
	}

	%rot = %obj.angleID;
	%rotDir[0] = "-1 0 0";
	%rotDir[1] = "0 1 0";
	%rotDir[2] = "1 0 0";
	%rotDir[3] = "0 -1 0";
	%rotDir[4] = "0 0 1";
	%rotDir[5] = "0 0 -1";

	%numPorts = %data.numLogicPorts;
	$LBC::Bricks::PortCount[%obj] = %numPorts;
	for(%i = 0; %i < %numPorts; %i++)
	{
		%group = -1;
		%portID = $LBC::Ports::NumPorts;
		$LBC::Ports::NumPorts++;

		%ppos = %data.logicPortPos[%i];
		%dir = %data.logicPortDir[%i];
		if(%dir < 4)
			%dir = (%dir+%rot) % 4;

		%type = %data.logicPortType[%i];
		if(%type)
			%hasInputs = true;

		$LBC::Bricks::Port[%obj, %i] = %portID;
		$LBC::Ports::State[%portID] = 0;
		$LBC::Ports::LastState[%portID] = 0;
		$LBC::Ports::BrickState[%obj, %i] = 0;
		$LBC::Ports::LastBrickState[%obj, %i] = 0;

		$LBC::Ports::Brick[%portID] = %obj;
		$LBC::Ports::Type[%portID] = %type;
		$LBC::Ports::Dir[%portID] = %dir;
		$LBC::Ports::BrickIDX[%portID] = %i;

		%x = getWord(%ppos, 0);
		%y = getWord(%ppos, 1);
		%z = getWord(%ppos, 2);
		%ax = %x;
		switch(%rot)
		{
			case 1:
				%x = %y;
				%y = -%ax;
			case 2:
				%x = -%x;
				%y = -%y;
			case 3:
				%x = -%y;
				%y = %ax;
		}

		%sx = %px+(%x*0.25);
		%sy = %py+(%y*0.25);
		%sz = %pz+(%z*0.2);
		%start = %sx SPC %sy SPC %sz;

		$LBC::Ports::WorldPos[%portID] = %start;
		$LBC::Ports::WorldPos[%portID, 0] = %sx;
		$LBC::Ports::WorldPos[%portID, 1] = %sy;
		$LBC::Ports::WorldPos[%portID, 2] = %sz;

		%end = vectorAdd(%start, vectorScale(%rotDir[%dir], 0.3));
		$LBC::Ports::ConnPos[%portID] = %end;
		$LBC::Ports::ConnPos[%portID, 0] = getWord(%end, 0);
		$LBC::Ports::ConnPos[%portID, 1] = getWord(%end, 1);
		$LBC::Ports::ConnPos[%portID, 2] = getWord(%end, 2);

		if(%doRay)
		{
			%ray = containerRayCast(%start, %end, $TypeMasks::FxBrickAlwaysObjectType, %obj);

			if($LBC::Bricks::isLogic[%sobj = firstWord(%ray)] && !%sobj.isDead())
			{
				if($LBC::Bricks::isWire[%sobj])
				{
					%group = $LBC::Wires::Group[%sobj];
					if(%group == -1)
					{
						%group = $LBC::Groups::NumGroups;
						$LBC::Groups::NumGroups++;

						$LBC::Ports::Group[%portID] = %group;
						$LBC::Wires::Group[%sobj] = %group;
						$LBC::Groups::Port[%group, 0] = %portID;
						$LBC::Groups::PortIDX[%group, %portID] = 0;
						$LBC::Groups::PortCount[%group] = 1;
						$LBC::Groups::Wire[%group, 0] = %sobj;
						$LBC::Groups::WireIDX[%group, %sobj] = 0;
						$LBC::Groups::WireCount[%group] = 1;
					}
					else
					{
						$LBC::Ports::Group[%portID] = %group;
						$LBC::Groups::Port[%group, $LBC::Groups::PortCount[%group]] = %portID;
						$LBC::Groups::PortIDX[%group, %portID] = $LBC::Groups::PortCount[%group];
						$LBC::Groups::PortCount[%group]++;
					}
				}
				else if($LBC::Bricks::isGate[%sobj])
				{
					%bestDist = -1;
					%ports = $LBC::Bricks::PortCount[%sobj];
					for(%a = 0; %a < %ports; %a++)
					{
						%aport = $LBC::Bricks::Port[%sobj, %a];
						%adir = $LBC::Ports::Dir[%aport];

						if(%dir > 3)
						{
							if(%dir == %adir)
								continue;
						}
						else if(mabs(%adir-%dir) != 2)
							continue;

						%xx = $LBC::Ports::ConnPos[%portID, 0]-$LBC::Ports::ConnPos[%aport, 0];
						%yy = $LBC::Ports::ConnPos[%portID, 1]-$LBC::Ports::ConnPos[%aport, 1];
						%zz = $LBC::Ports::ConnPos[%portID, 2]-$LBC::Ports::ConnPos[%aport, 2];
						%distSqr = (%xx*%xx)+(%yy*%yy)+(%zz*%zz);
						// talk(%obj.getDatablock().logicPortUIName[%i] SPC %sobj.getDatablock().logicPortUIName[%a]);
						// talk(%dir SPC %adir SPC %distSqr SPC %zz);
						if((%distSqr <= 0.011 || %zz <= 0.41) && (%distSqr < %bestDist || %bestDist == -1))
						{
							%bestDist = %distSqr;
							%bestPort = %aport;
							%bestIdx = %a;
						}
					}

					if(%bestDist == -1)
						continue;
					
					%group = $LBC::Ports::Group[%bestPort];
					if(%group == -1)
					{
						%group = $LBC::Groups::NumGroups;
						$LBC::Groups::NumGroups++;

						$LBC::Ports::Group[%portID] = %group;
						$LBC::Groups::Port[%group, 0] = %portID;
						$LBC::Groups::PortIDX[%group, %portID] = 0;

						$LBC::Ports::Group[%bestPort] = %group;
						$LBC::Groups::Port[%group, 1] = %bestPort;
						$LBC::Groups::PortIDX[%group, %bestPort] = 1;

						$LBC::Groups::PortCount[%group] = 2;
						$LBC::Groups::WireCount[%group] = 0;
					}
					else
					{
						$LBC::Ports::Group[%portID] = %group;
						$LBC::Groups::Port[%group, $LBC::Groups::PortCount[%group]] = %portID;
						$LBC::Groups::PortIDX[%group, %portID] = $LBC::Groups::PortCount[%group];
						$LBC::Groups::PortCount[%group]++;
					}
				}
			}
		}

		if(%group == -1)
			$LBC::Ports::Group[%portID] = -1;
		else
			Logic_QueueGroup(%group);
	}

	$LBC::Bricks::isGate[%obj] = true;
	$LBC::Bricks::isLogic[%obj] = true;

	if(%hasInputs)
		Logic_QueueGate(%obj);
	if(isFunction(%dataName, "Logic_onGateAdded"))
		%data.Logic_onGateAdded(%obj);
}

function Logic_RemoveGate(%obj)
{
	%ports = $LBC::Bricks::PortCount[%obj];
	for(%i = 0; %i < %ports; %i++)
	{
		%port = $LBC::Bricks::Port[%obj, %i];
		%group = $LBC::Ports::Group[%port];

		$LBC::Ports::State[%port] = false;
		$LBC::Ports::BrickState[%obj, %i] = false;

		if(%group != -1)
		{
			if($LBC::Groups::PortCount[%group] == 2 && $LBC::Groups::WireCount[%group] == 0)
			{
				%gport = $LBC::Groups::Port[%group, 0];
				if(%gport != %port)
				{
					$LBC::Ports::Group[%gport] = -1;
					if($LBC::Ports::Type[%gport] == 1)
					{
						%brick = $LBC::Ports::Brick[%gport];
						$LBC::Ports::State[%gport] = 0;
						$LBC::Ports::BrickState[%brick, $LBC::Ports::BrickIDX[%gport]] = 0;
						$LBC::Bricks::Datablock[%brick].doLogic(%brick);
					}
				}
				else
				{
					%gport = $LBC::Groups::Port[%group, 1];
					$LBC::Ports::Group[%gport] = -1;
					if($LBC::Ports::Type[%gport] == 1)
					{
						%brick = $LBC::Ports::Brick[%gport];
						$LBC::Ports::State[%gport] = 0;
						$LBC::Ports::BrickState[%brick, $LBC::Ports::BrickIDX[%gport]] = 0;
						$LBC::Bricks::Datablock[%brick].doLogic(%brick);
					}
				}

				$LBC::Groups::PortCount[%group] = 0;
			}
			else
			{
				$LBC::Groups::Port[%group, (%idx = $LBC::Groups::PortIDX[%group, %port])] = (%gport = $LBC::Groups::Port[%group, $LBC::Groups::PortCount[%group]-1]);
				$LBC::Groups::PortIDX[%group, %gport] = %idx;
				$LBC::Groups::PortCount[%group]--;
				//Logic_QueueGate($LBC::Ports::Brick[%port]);
				Logic_QueueGroup(%group);
			}
			$LBC::Ports::Group[%port] = -1;
		}
		$LBC::Ports::Brick[%port] = -1;
	}

	$LBC::Bricks::isLogic[%obj] = false;
	$LBC::Bricks::isGate[%obj] = false;
}
