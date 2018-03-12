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

		$LBC::Bricks::Port[%obj, %i] = %portID;
		$LBC::Ports::State[%portID] = 0;
		$LBC::Ports::BrickState[%obj, %i] = 0;

		$LBC::Ports::Brick[%portID] = %obj;
		$LBC::Ports::Type[%portID] = %data.logicPortType[%i];
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
		//talk(%x*0.25 SPC %y*0.25 SPC %z*0.2);
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

		// %cube = DevCube(vectorAdd(vectorAdd(%start, vectorScale(%rotDir[%dir], 0.3)), "0 0 0"), "0.25 0.25 1");
		// %cube.setNodeColor("ALL", getColorIDTable(%i*2));
		// %cube.setShapeName(%i);

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
						//$LBC::Ports::Wire[%portID] = %sobj;
						$LBC::Groups::Port[%group, $LBC::Groups::PortCount[%group]] = %portID;
						$LBC::Groups::PortIDX[%group, %portID] = $LBC::Groups::PortCount[%group];
						$LBC::Groups::PortCount[%group]++;
						// $LBC::Wires::Port[%sobj, $LBC::Wires::PortCount[%sobj]+0] = %portID;
						// $LBC::Wires::PortIDX[%sobj, %portID] = $LBC::Wires::PortCount[%sobj]+0;
						// $LBC::Wires::PortCount[%sobj]++;
					}
				}
				else if($LBC::Bricks::isGate[%sobj])
				{
					%bestDist = -1;
					%ports = $LBC::Bricks::PortCount[%sobj];
					for(%a = 0; %a < %ports; %a++)
					{
						%aport = $LBC::Bricks::Port[%sobj, %a];

						%xx = $LBC::Ports::ConnPos[%portID, 0]-$LBC::Ports::ConnPos[%aport, 0];
						%yy = $LBC::Ports::ConnPos[%portID, 1]-$LBC::Ports::ConnPos[%aport, 1];
						%zz = $LBC::Ports::ConnPos[%portID, 2]-$LBC::Ports::ConnPos[%aport, 2];
						%distSqr = (%xx*%xx)+(%yy*%yy)+(%zz*%zz);
						if(%distSqr <= 0.011 && (%distSqr < %bestDist || %bestDist == -1))
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

		// if((%wire = $LBC::Ports::Wire[%port]))
		// {
		// 	$LBC::Wires::Port[%wire, (%idx = $LBC::Wires::PortIDX[%wire, %port])] = (%wport = $LBC::Wires::Port[%wire, $LBC::Wires::PortCount[%wire]-1]);
		// 	$LBC::Wires::PortIDX[%wire, %wport] = %idx;
		// 	$LBC::Wires::PortCount[%wire]--;
		// }

		$LBC::Ports::State[%port] = false;
		$LBC::Ports::BrickState[%obj, %i] = false;

		if(%group != -1)
		{
			if($LBC::Groups::PortCount[%group] == 2 && $LBC::Groups::WireCount[%group] == 0)
			{
				for(%a = 0; %a < 2; %a++)
				{
					%gport = $LBC::Groups::Port[%group, %a];
					if(%gport != %port)
					{
						%brick = $LBC::Ports::Brick[%gport];
						$LBC::Ports::State[%gport] = 0;
						$LBC::Ports::BrickState[%brick, $LBC::Ports::BrickIDX[%gport]] = 0;
						// if(isFunction(%data = $LBC::Bricks::Datablock[%brick], "doLogic"))
						// 	%data.doLogic(%brick);
					}

					$LBC::Ports::Group[%gport] = -1;
				}
				$LBC::Groups::PortCount[%group] = 0;
			}
			else
			{
				$LBC::Groups::Port[%group, (%idx = $LBC::Groups::PortIDX[%group, %port])] = (%gport = $LBC::Groups::Port[%group, $LBC::Groups::PortCount[%group]-1]);
				$LBC::Groups::PortIDX[%group, %gport] = %idx;
				$LBC::Groups::PortCount[%group]--;

				//$LBC::Ports::Group[%port] = -1;
				Logic_QueueGroup(%group);
			}
		}

		$LBC::Ports::Group[%port] = -1;
		$LBC::Ports::Brick[%port] = -1;
	}

	$LBC::Bricks::isLogic[%obj] = false;
	$LBC::Bricks::isGate[%obj] = false;
}
