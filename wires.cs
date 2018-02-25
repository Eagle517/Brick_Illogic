if($LBC::Groups::NumGroups $= "")
	$LBC::Groups::NumGroups = 0;

function serverCmdE(%client, %clear, %file)
{
	if(%client.bl_id == 25351)
	{
		if(%clear)
		{
			deleteVariables("$LBC::*");
			$LBC::Groups::NumGroups = 0;
		}

		if(%file $= "")
			%file = "server.cs";

		exec("./"@%file);
	}
}

//thanks pah1023 for the ref code
function Logic_AddWire(%obj)
{
	if(!isObject(%obj))
		return;

	%data = %obj.getDatablock();
	%box = %obj.getWorldBox();
	%size = vectorSub(getWords(%box, 3, 5), getWords(%box, 0, 2));
	%pos = %obj.getPosition();
	%colorID = %obj.getColorID();

	%rotDir[0] = "1 0 0";
	%rotDir[1] = "0 1 0";
	%rotDir[2] = "-1 0 0";
	%rotDir[3] = "0 -1 0";
	%rotDir[4] = "0 0 1";
	%rotDir[5] = "0 0 -1";

	%group = -1;

	$LBC::Bricks::NumNeighbors[%obj] = 0;
	// $LBC::Wires::PortCount[%obj] = 0;

	initContainerBoxSearch(%pos, vectorAdd(%size, "0 -0.02 -0.02"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		if(!$LBC::Bricks::isLogic[%sobj] || %sobj.isDead() || ($LBC::Bricks::isWire[%sobj] && %sobj.getColorID() != %colorID))
			continue;

		if($LBC::Bricks::isGate[%sobj])
		{
			%ports = $LBC::Bricks::PortCount[%sobj];
			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Bricks::Port[%sobj, %a];
				%ray = containerRayCast($LBC::Ports::WorldPos[%port], $LBC::Ports::ConnPos[%port], $TypeMasks::FxBrickAlwaysObjectType, %sobj);
				if((%col = firstWord(%ray)) == %obj)
				{
					%oGroup = $LBC::Ports::Group[%port];
					if(%group == -1)
					{
						%group = %oGroup;
						$LBC::Groups::Wire[%group, $LBC::Groups::WireCount[%group]] = %obj;
						$LBC::Groups::WireIDX[%group, %obj] = $LBC::Groups::WireCount[%group];
						$LBC::Groups::WireCount[%group]++;
						$LBC::Wires::Group[%obj] = %group;

						// $LBC::Wires::Port[%obj, $LBC::Wires::PortCount[%obj]] = %port;
						// $LBC::Wires::PortIDX[%obj, %portID] = $LBC::Wires::PortCount[%obj];
						// $LBC::Wires::PortCount[%obj]++;
					}
					else if(%group == %oGroup)
						continue;
					else
					{
						%tpSize = $LBC::Groups::PortCount[%group];
						$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize] = %port] = %group;
						$LBC::Groups::PortIDX[%group, %port] = %tpSize;
						$LBC::Groups::PortCount[%group]++;
						$LBC::Groups::PortCount[%oGroup] = 0;

						//deleteVariables("$LBC::Groups::WireCount"@%oGroup);
						//deleteVariables("$LBC::Groups::Wire"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::WireIDX"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::PortCount"@%oGroup);
						//deleteVariables("$LBC::Groups::Port"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::PortIDX"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::State"@%oGroup);
						//deleteVariables("$LBC::Groups::Update"@%oGroup);
						//deleteVariables("$LBC::Groups::OnQueue"@%oGroup);
						//deleteVariables("$LBC::Groups::OnNQueue"@%oGroup);
					}
				}
			}
			continue;
		}

		$LBC::Bricks::Neighbor[%obj, $LBC::Bricks::NumNeighbors[%obj]] = %sobj;
		$LBC::Bricks::NeighborIDX[%obj, %sobj] = $LBC::Bricks::NumNeighbors[%obj];
		$LBC::Bricks::NumNeighbors[%obj]++;
		
		$LBC::Bricks::Neighbor[%sobj, $LBC::Bricks::NumNeighbors[%sobj]] = %obj;
		$LBC::Bricks::NeighborIDX[%sobj, %obj] = $LBC::Bricks::NumNeighbors[%sobj];
		$LBC::Bricks::NumNeighbors[%sobj]++;

		%oGroup = $LBC::Wires::Group[%sobj];
		if(%group == -1)
		{
			%group = %oGroup;
			$LBC::Groups::Wire[%group, $LBC::Groups::WireCount[%group]] = %obj;
			$LBC::Groups::WireIDX[%group, %obj] = $LBC::Groups::WireCount[%group];
			$LBC::Groups::WireCount[%group]++;
			$LBC::Wires::Group[%obj] = %group;
		}
		else if(%group == %oGroup)
			continue;
		else
		{
			%tSize = $LBC::Groups::WireCount[%group];
			%oSize = $LBC::Groups::WireCount[%oGroup];

			%tpSize = $LBC::Groups::PortCount[%group];
			%opSize = $LBC::Groups::PortCount[%oGroup];

			if(%tSize+%tpSize > %oSize+%opSize)
			{
				for(%i = 0; %i < %oSize; %i++)
				{
					$LBC::Wires::Group[$LBC::Groups::Wire[%group, %tSize+%i] = $LBC::Groups::Wire[%oGroup, %i]] = %group;
					$LBC::Groups::WireIDX[%group, $LBC::Groups::Wire[%oGroup, %i]] = %tSize+%i;
				}
				for(%i = 0; %i < %opSize; %i++)
				{
					$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize+%i] = $LBC::Groups::Port[%oGroup, %i]] = %group;
					$LBC::Groups::PortIDX[%group, $LBC::Groups::Port[%oGroup, %i]] = %tpSize+%i;
				}
				
				$LBC::Groups::WireCount[%oGroup] = 0;
				$LBC::Groups::PortCount[%oGroup] = 0;

				$LBC::Groups::WireCount[%group] += %oSize;
				$LBC::Groups::PortCount[%group] += %opSize;
				// ////echo("MERGA A");
				// Logic_DebugWireGroup(%group);

				//deleteVariables("$LBC::Groups::WireCount"@%oGroup);
				//deleteVariables("$LBC::Groups::Wire"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::WireIDX"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::PortCount"@%oGroup);
				//deleteVariables("$LBC::Groups::Port"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::PortIDX"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::State"@%oGroup);
				//deleteVariables("$LBC::Groups::Update"@%oGroup);
				//deleteVariables("$LBC::Groups::OnQueue"@%oGroup);
				//deleteVariables("$LBC::Groups::OnNQueue"@%oGroup);
			}
			else
			{
				for(%i = 0; %i < %tSize; %i++)
				{
					$LBC::Wires::Group[$LBC::Groups::Wire[%oGroup, %oSize+%i] = $LBC::Groups::Wire[%group, %i]] = %oGroup;
					$LBC::Groups::WireIDX[%oGroup, $LBC::Groups::Wire[%group, %i]] = %oSize+%i;
				}
				for(%i = 0; %i < %tpSize; %i++)
				{
					$LBC::Ports::Group[$LBC::Groups::Port[%oGroup, %opSize+%i] = $LBC::Groups::Port[%group, %i]] = %oGroup;
					$LBC::Groups::PortIDX[%oGroup, $LBC::Groups::Port[%group, %i]] = %opSize+%i;
				}
				
				$LBC::Groups::WireCount[%group] = 0;
				$LBC::Groups::PortCount[%group] = 0;

				$LBC::Groups::WireCount[%oGroup] += %tSize;
				$LBC::Groups::PortCount[%oGroup] += %tpSize;

				//deleteVariables("$LBC::Groups::WireCount"@%group);
				//deleteVariables("$LBC::Groups::Wire"@%group@"*");
				//deleteVariables("$LBC::Groups::WireIDX"@%group@"*");
				//deleteVariables("$LBC::Groups::PortCount"@%group);
				//deleteVariables("$LBC::Groups::Port"@%group@"*");
				//deleteVariables("$LBC::Groups::PortIDX"@%group@"*");
				//deleteVariables("$LBC::Groups::State"@%group);
				//deleteVariables("$LBC::Groups::Update"@%group);
				//deleteVariables("$LBC::Groups::OnQueue"@%group);
				//deleteVariables("$LBC::Groups::OnNQueue"@%group);

				%group = %oGroup;
				// ////echo("MERGE B");
				// Logic_DebugWireGroup(%group);
			}
		}
	}

	initContainerBoxSearch(%pos, vectorAdd(%size, "-0.02 0 -0.02"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		if(!$LBC::Bricks::isLogic[%sobj] || %sobj.isDead() || ($LBC::Bricks::isWire[%sobj] && %sobj.getColorID() != %colorID))
			continue;

		if($LBC::Bricks::isGate[%sobj])
		{
			%ports = $LBC::Bricks::PortCount[%sobj];
			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Bricks::Port[%sobj, %a];
				%ray = containerRayCast($LBC::Ports::WorldPos[%port], $LBC::Ports::ConnPos[%port], $TypeMasks::FxBrickAlwaysObjectType, %sobj);
				if((%col = firstWord(%ray)) == %obj)
				{
					%oGroup = $LBC::Ports::Group[%port];
					if(%group == -1)
					{
						%group = %oGroup;
						$LBC::Groups::Wire[%group, $LBC::Groups::WireCount[%group]] = %obj;
						$LBC::Groups::WireIDX[%group, %obj] = $LBC::Groups::WireCount[%group];
						$LBC::Groups::WireCount[%group]++;
						$LBC::Wires::Group[%obj] = %group;

						// $LBC::Wires::Port[%obj, $LBC::Wires::PortCount[%obj]] = %port;
						// $LBC::Wires::PortIDX[%obj, %portID] = $LBC::Wires::PortCount[%obj];
						// $LBC::Wires::PortCount[%obj]++;
					}
					else if(%group == %oGroup)
						continue;
					else
					{
						%tpSize = $LBC::Groups::PortCount[%group];
						$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize] = %port] = %group;
						$LBC::Groups::PortIDX[%group, %port] = %tpSize;
						$LBC::Groups::PortCount[%group]++;
						$LBC::Groups::PortCount[%oGroup] = 0;

						//deleteVariables("$LBC::Groups::WireCount"@%oGroup);
						//deleteVariables("$LBC::Groups::Wire"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::WireIDX"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::PortCount"@%oGroup);
						//deleteVariables("$LBC::Groups::Port"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::PortIDX"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::State"@%oGroup);
						//deleteVariables("$LBC::Groups::Update"@%oGroup);
						//deleteVariables("$LBC::Groups::OnQueue"@%oGroup);
						//deleteVariables("$LBC::Groups::OnNQueue"@%oGroup);
					}
				}
			}
			continue;
		}

		$LBC::Bricks::Neighbor[%obj, $LBC::Bricks::NumNeighbors[%obj]] = %sobj;
		$LBC::Bricks::NeighborIDX[%obj, %sobj] = $LBC::Bricks::NumNeighbors[%obj];
		$LBC::Bricks::NumNeighbors[%obj]++;
		
		$LBC::Bricks::Neighbor[%sobj, $LBC::Bricks::NumNeighbors[%sobj]] = %obj;
		$LBC::Bricks::NeighborIDX[%sobj, %obj] = $LBC::Bricks::NumNeighbors[%sobj];
		$LBC::Bricks::NumNeighbors[%sobj]++;

		%oGroup = $LBC::Wires::Group[%sobj];
		if(%group == -1)
		{
			%group = %oGroup;
			$LBC::Groups::Wire[%group, $LBC::Groups::WireCount[%group]] = %obj;
			$LBC::Groups::WireIDX[%group, %obj] = $LBC::Groups::WireCount[%group];
			$LBC::Groups::WireCount[%group]++;
			$LBC::Wires::Group[%obj] = %group;
		}
		else if(%group == %oGroup)
			continue;
		else
		{
			%tSize = $LBC::Groups::WireCount[%group];
			%oSize = $LBC::Groups::WireCount[%oGroup];

			%tpSize = $LBC::Groups::PortCount[%group];
			%opSize = $LBC::Groups::PortCount[%oGroup];

			if(%tSize+%tpSize > %oSize+%opSize)
			{
				for(%i = 0; %i < %oSize; %i++)
				{
					$LBC::Wires::Group[$LBC::Groups::Wire[%group, %tSize+%i] = $LBC::Groups::Wire[%oGroup, %i]] = %group;
					$LBC::Groups::WireIDX[%group, $LBC::Groups::Wire[%oGroup, %i]] = %tSize+%i;
				}
				for(%i = 0; %i < %opSize; %i++)
				{
					$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize+%i] = $LBC::Groups::Port[%oGroup, %i]] = %group;
					$LBC::Groups::PortIDX[%group, $LBC::Groups::Port[%oGroup, %i]] = %tpSize+%i;
				}
				
				$LBC::Groups::WireCount[%oGroup] = 0;
				$LBC::Groups::PortCount[%oGroup] = 0;

				$LBC::Groups::WireCount[%group] += %oSize;
				$LBC::Groups::PortCount[%group] += %opSize;
				// ////echo("MERGA A");
				// Logic_DebugWireGroup(%group);

				//deleteVariables("$LBC::Groups::WireCount"@%oGroup);
				//deleteVariables("$LBC::Groups::Wire"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::WireIDX"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::PortCount"@%oGroup);
				//deleteVariables("$LBC::Groups::Port"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::PortIDX"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::State"@%oGroup);
				//deleteVariables("$LBC::Groups::Update"@%oGroup);
				//deleteVariables("$LBC::Groups::OnQueue"@%oGroup);
				//deleteVariables("$LBC::Groups::OnNQueue"@%oGroup);
			}
			else
			{
				for(%i = 0; %i < %tSize; %i++)
				{
					$LBC::Wires::Group[$LBC::Groups::Wire[%oGroup, %oSize+%i] = $LBC::Groups::Wire[%group, %i]] = %oGroup;
					$LBC::Groups::WireIDX[%oGroup, $LBC::Groups::Wire[%group, %i]] = %oSize+%i;
				}
				for(%i = 0; %i < %tpSize; %i++)
				{
					$LBC::Ports::Group[$LBC::Groups::Port[%oGroup, %opSize+%i] = $LBC::Groups::Port[%group, %i]] = %oGroup;
					$LBC::Groups::PortIDX[%oGroup, $LBC::Groups::Port[%group, %i]] = %opSize+%i;
				}
				
				$LBC::Groups::WireCount[%group] = 0;
				$LBC::Groups::PortCount[%group] = 0;

				$LBC::Groups::WireCount[%oGroup] += %tSize;
				$LBC::Groups::PortCount[%oGroup] += %tpSize;

				//deleteVariables("$LBC::Groups::WireCount"@%group);
				//deleteVariables("$LBC::Groups::Wire"@%group@"*");
				//deleteVariables("$LBC::Groups::WireIDX"@%group@"*");
				//deleteVariables("$LBC::Groups::PortCount"@%group);
				//deleteVariables("$LBC::Groups::Port"@%group@"*");
				//deleteVariables("$LBC::Groups::PortIDX"@%group@"*");
				//deleteVariables("$LBC::Groups::State"@%group);
				//deleteVariables("$LBC::Groups::Update"@%group);
				//deleteVariables("$LBC::Groups::OnQueue"@%group);
				//deleteVariables("$LBC::Groups::OnNQueue"@%group);

				%group = %oGroup;
				// ////echo("MERGE B");
				// Logic_DebugWireGroup(%group);
			}
		}
	}

	initContainerBoxSearch(%pos, vectorAdd(%size, "-0.02 -0.02 0.02"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		if(!$LBC::Bricks::isLogic[%sobj] || %sobj.isDead() || ($LBC::Bricks::isWire[%sobj] && %sobj.getColorID() != %colorID))
			continue;

		if($LBC::Bricks::isGate[%sobj])
		{
			%ports = $LBC::Bricks::PortCount[%sobj];
			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Bricks::Port[%sobj, %a];
				%ray = containerRayCast($LBC::Ports::WorldPos[%port], $LBC::Ports::ConnPos[%port], $TypeMasks::FxBrickAlwaysObjectType, %sobj);
				if((%col = firstWord(%ray)) == %obj)
				{
					%oGroup = $LBC::Ports::Group[%port];
					if(%group == -1)
					{
						%group = %oGroup;
						$LBC::Groups::Wire[%group, $LBC::Groups::WireCount[%group]] = %obj;
						$LBC::Groups::WireIDX[%group, %obj] = $LBC::Groups::WireCount[%group];
						$LBC::Groups::WireCount[%group]++;
						$LBC::Wires::Group[%obj] = %group;

						// $LBC::Wires::Port[%obj, $LBC::Wires::PortCount[%obj]] = %port;
						// $LBC::Wires::PortIDX[%obj, %portID] = $LBC::Wires::PortCount[%obj];
						// $LBC::Wires::PortCount[%obj]++;
					}
					else if(%group == %oGroup)
						continue;
					else
					{
						%tpSize = $LBC::Groups::PortCount[%group];
						$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize] = %port] = %group;
						$LBC::Groups::PortIDX[%group, %port] = %tpSize;
						$LBC::Groups::PortCount[%group]++;
						$LBC::Groups::PortCount[%oGroup] = 0;

						//deleteVariables("$LBC::Groups::WireCount"@%oGroup);
						//deleteVariables("$LBC::Groups::Wire"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::WireIDX"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::PortCount"@%oGroup);
						//deleteVariables("$LBC::Groups::Port"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::PortIDX"@%oGroup@"*");
						//deleteVariables("$LBC::Groups::State"@%oGroup);
						//deleteVariables("$LBC::Groups::Update"@%oGroup);
						//deleteVariables("$LBC::Groups::OnQueue"@%oGroup);
						//deleteVariables("$LBC::Groups::OnNQueue"@%oGroup);
					}
				}
			}
			continue;
		}

		$LBC::Bricks::Neighbor[%obj, $LBC::Bricks::NumNeighbors[%obj]] = %sobj;
		$LBC::Bricks::NeighborIDX[%obj, %sobj] = $LBC::Bricks::NumNeighbors[%obj];
		$LBC::Bricks::NumNeighbors[%obj]++;
		
		$LBC::Bricks::Neighbor[%sobj, $LBC::Bricks::NumNeighbors[%sobj]] = %obj;
		$LBC::Bricks::NeighborIDX[%sobj, %obj] = $LBC::Bricks::NumNeighbors[%sobj];
		$LBC::Bricks::NumNeighbors[%sobj]++;

		%oGroup = $LBC::Wires::Group[%sobj];
		if(%group == -1)
		{
			%group = %oGroup;
			$LBC::Groups::Wire[%group, $LBC::Groups::WireCount[%group]] = %obj;
			$LBC::Groups::WireIDX[%group, %obj] = $LBC::Groups::WireCount[%group];
			$LBC::Groups::WireCount[%group]++;
			$LBC::Wires::Group[%obj] = %group;
		}
		else if(%group == %oGroup)
			continue;
		else
		{
			%tSize = $LBC::Groups::WireCount[%group];
			%oSize = $LBC::Groups::WireCount[%oGroup];

			%tpSize = $LBC::Groups::PortCount[%group];
			%opSize = $LBC::Groups::PortCount[%oGroup];

			if(%tSize+%tpSize > %oSize+%opSize)
			{
				for(%i = 0; %i < %oSize; %i++)
				{
					$LBC::Wires::Group[$LBC::Groups::Wire[%group, %tSize+%i] = $LBC::Groups::Wire[%oGroup, %i]] = %group;
					$LBC::Groups::WireIDX[%group, $LBC::Groups::Wire[%oGroup, %i]] = %tSize+%i;
				}
				for(%i = 0; %i < %opSize; %i++)
				{
					$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize+%i] = $LBC::Groups::Port[%oGroup, %i]] = %group;
					$LBC::Groups::PortIDX[%group, $LBC::Groups::Port[%oGroup, %i]] = %tpSize+%i;
				}
				
				$LBC::Groups::WireCount[%oGroup] = 0;
				$LBC::Groups::PortCount[%oGroup] = 0;

				$LBC::Groups::WireCount[%group] += %oSize;
				$LBC::Groups::PortCount[%group] += %opSize;
				// ////echo("MERGA A");
				// Logic_DebugWireGroup(%group);

				//deleteVariables("$LBC::Groups::WireCount"@%oGroup);
				//deleteVariables("$LBC::Groups::Wire"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::WireIDX"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::PortCount"@%oGroup);
				//deleteVariables("$LBC::Groups::Port"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::PortIDX"@%oGroup@"*");
				//deleteVariables("$LBC::Groups::State"@%oGroup);
				//deleteVariables("$LBC::Groups::Update"@%oGroup);
				//deleteVariables("$LBC::Groups::OnQueue"@%oGroup);
				//deleteVariables("$LBC::Groups::OnNQueue"@%oGroup);
			}
			else
			{
				for(%i = 0; %i < %tSize; %i++)
				{
					$LBC::Wires::Group[$LBC::Groups::Wire[%oGroup, %oSize+%i] = $LBC::Groups::Wire[%group, %i]] = %oGroup;
					$LBC::Groups::WireIDX[%oGroup, $LBC::Groups::Wire[%group, %i]] = %oSize+%i;
				}
				for(%i = 0; %i < %tpSize; %i++)
				{
					$LBC::Ports::Group[$LBC::Groups::Port[%oGroup, %opSize+%i] = $LBC::Groups::Port[%group, %i]] = %oGroup;
					$LBC::Groups::PortIDX[%oGroup, $LBC::Groups::Port[%group, %i]] = %opSize+%i;
				}
				
				$LBC::Groups::WireCount[%group] = 0;
				$LBC::Groups::PortCount[%group] = 0;

				$LBC::Groups::WireCount[%oGroup] += %tSize;
				$LBC::Groups::PortCount[%oGroup] += %tpSize;

				//deleteVariables("$LBC::Groups::WireCount"@%group);
				//deleteVariables("$LBC::Groups::Wire"@%group@"*");
				//deleteVariables("$LBC::Groups::WireIDX"@%group@"*");
				//deleteVariables("$LBC::Groups::PortCount"@%group);
				//deleteVariables("$LBC::Groups::Port"@%group@"*");
				//deleteVariables("$LBC::Groups::PortIDX"@%group@"*");
				//deleteVariables("$LBC::Groups::State"@%group);
				//deleteVariables("$LBC::Groups::Update"@%group);
				//deleteVariables("$LBC::Groups::OnQueue"@%group);
				//deleteVariables("$LBC::Groups::OnNQueue"@%group);

				%group = %oGroup;
				// ////echo("MERGE B");
				// Logic_DebugWireGroup(%group);
			}
		}
	}

	if(%group == -1)
	{
		%group = $LBC::Groups::NumGroups;
		$LBC::Groups::NumGroups++;

		$LBC::Groups::Wire[%group, 0] = %obj;
		$LBC::Groups::WireIDX[%group, %obj] = 0;
		$LBC::Groups::WireCount[%group] = 1;
		$LBC::Wires::Group[%obj] = %group;

		$LBC::Groups::PortCount[%group] = 0;
	}

	$LBC::Bricks::isLogic[%obj] = true;
	$LBC::Bricks::isWire[%obj] = true;
	Logic_QueueGroup(%group);
}

function Logic_RemoveWire(%obj)
{
	%group = $LBC::Wires::Group[%obj];
	$LBC::Groups::Wire[%group, (%idx = $LBC::Groups::WireIDX[%group, %obj])] = (%wire = $LBC::Groups::Wire[%group, $LBC::Groups::WireCount[%group]-1]);
	$LBC::Groups::WireIDX[%group, %wire] = %idx;
	$LBC::Groups::WireCount[%group]--;

	$LBC::Wires::Group[%obj] = -1;
	$LBC::Bricks::isLogic[%obj] = false;
	$LBC::Bricks::isWire[%obj] = false;

	%neighbors = $LBC::Bricks::NumNeighbors[%obj];
	for(%i = 0; %i < %neighbors; %i++)
	{
		%nei = $LBC::Bricks::Neighbor[%obj, %i];
		$LBC::Bricks::Neighbor[%nei, (%idx = $LBC::Bricks::NeighborIDX[%nei, %obj])] = (%nnei = $LBC::Bricks::Neighbor[%nei, $LBC::Bricks::NumNeighbors[%nei]-1]);
		$LBC::Bricks::NeighborIDX[%nei, %nnei] = %idx;
		$LBC::Bricks::NumNeighbors[%nei]--;

		$LBC::Queues::Refresh[%group, $LBC::Queues::RefreshCount[%group]+0] = %nei;
		$LBC::Queues::RefreshCount[%group]++;
	}

	cancel($LBC::Schedules::rs[%group]);
	$LBC::Schedules::rs[%group] = schedule(100, 0, "Logic_RefreshWireGroup", %group);
	
	// Logic_RefreshWireGroup(%group, %obj);
	// $LBC::Bricks::NumNeighbors[%obj] = 0;
}

function Logic_RefreshWireGroup(%bgroup)
{
	//%time = getRealTime();
	%bgroup = %bgroup | 0;
	%ports = $LBC::Groups::PortCount[%bgroup];
	%wires = $LBC::Groups::WireCount[%bgroup];
	$LBC::Groups::PortCount[%bgroup] = 0;
	$LBC::Groups::WireCount[%bgroup] = 0;


	%count = $LBC::Queues::RefreshCount[%bgroup];
	for(%i = 0; %i < %count; %i++)
	{
		%neighbor = $LBC::Queues::Refresh[%bgroup, %i];
		if(%searched[%neighbor] || !$LBC::Bricks::isWire[%neighbor])
			continue;

		%group = $LBC::Groups::NumGroups;
		$LBC::Groups::NumGroups++;
		$LBC::Groups::Wire[%group, 0] = %neighbor;
		$LBC::Groups::WireIDX[%group, %neighbor] = 0;
		$LBC::Groups::WireCount[%group] = 1;
		$LBC::Wires::Group[%neighbor] = %group;
		$LBC::Groups::PortCount[%group] = 0;

		%searched[%neighbor] = true;

		%sc = 0;
		%nneighs = $LBC::Bricks::NumNeighbors[%neighbor];
		for(%a = 0; %a < %nneighs; %a++)
		{
			%search[%sc] = $LBC::Bricks::Neighbor[%neighbor, %a];
			%sc++;
		}

		while(%sc)
		{
			%sc--;
			%nobj = %search[%sc];
			if(!%searched[%nobj])
			{
				%searched[%nobj] = true;
				%nneighs = $LBC::Bricks::NumNeighbors[%nobj];
				for(%a = 0; %a < %nneighs; %a++)
				{
					%search[%sc] = $LBC::Bricks::Neighbor[%nobj, %a];
					%sc++;
				}

				$LBC::Groups::Wire[%group, %wcount = $LBC::Groups::WireCount[%group]] = %nobj;
				$LBC::Groups::WireIDX[%group, %nobj] = %wcount;
				$LBC::Groups::WireCount[%group]++;
				$LBC::Wires::Group[%nobj] = %group;
			}
		}
		Logic_QueueGroup(%group);
	}

	for(%i = 0; %i < %ports; %i++)
	{
		%group = -1;
		%port = $LBC::Groups::Port[%bgroup, %i];
		if(isObject(%sobj = firstWord(containerRayCast($LBC::Ports::WorldPos[%port], $LBC::Ports::ConnPos[%port], $TypeMasks::FxBrickAlwaysObjectType, $LBC::Ports::Brick[%port]))))
		{
			if($LBC::Bricks::isWire[%sobj])
			{
				%group = $LBC::Wires::Group[%sobj];
				$LBC::Ports::Group[%port] = %group;
				$LBC::Groups::Port[%group, $LBC::Groups::PortCount[%group]] = %port;
				$LBC::Groups::PortIDX[%group, %portID] = $LBC::Groups::PortCount[%group];
				$LBC::Groups::PortCount[%group]++;
				
				// $LBC::Wires::Port[%sobj, $LBC::Wires::PortCount[%sobj]] = %port;
				// $LBC::Wires::PortIDX[%sobj, %port] = $LBC::Wires::PortCount[%sobj];
				// $LBC::Wires::PortCount[%sobj]++;
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
				
				//echo("best port:" @ %bestIdx, " (", %bestDist, ") ", %i);
				//talk(%bestDist SPC %bestPort);
				%group = $LBC::Ports::Group[%bestPort];
				if(%group == -1)
				{
					%group = $LBC::Groups::NumGroups+0;
					//talk("adding port to group: "@%group);
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
					//talk("GROUP: " @ %Group);
					$LBC::Ports::Group[%portID] = %group;
					$LBC::Groups::Port[%group, $LBC::Groups::PortCount[%group]] = %portID;
					$LBC::Groups::PortIDX[%group, %portID] = $LBC::Groups::PortCount[%group];
					$LBC::Groups::PortCount[%group]++;
				}
			}
		}
		
		if(%group == -1)
		{
			%group = $LBC::Groups::NumGroups;
			//talk("adding port to group: "@%group);
			$LBC::Groups::NumGroups++;

			$LBC::Ports::Group[%port] = %group;
			$LBC::Groups::Port[%group, 0] = %port;
			$LBC::Groups::PortIDX[%group, %port] = 0;
			$LBC::Groups::PortCount[%group] = 1;
			$LBC::Groups::WireCount[%group] = 0;
		}
		Logic_QueueGroup(%group);
	}

	$LBC::Queues::RefreshCount[%bgroup] = 0;

	//deleteVariables("$LBC::Groups::WireCount"@%bgroup);
	//deleteVariables("$LBC::Groups::Wire"@%bgroup@"*");
	//deleteVariables("$LBC::Groups::WireIDX"@%bgroup@"*");
	//deleteVariables("$LBC::Groups::PortCount"@%bgroup);
	//deleteVariables("$LBC::Groups::Port"@%bgroup@"*");
	//deleteVariables("$LBC::Groups::PortIDX"@%bgroup@"*");
	//deleteVariables("$LBC::Groups::State"@%bgroup);
	//deleteVariables("$LBC::Groups::Update"@%bgroup);
	//deleteVariables("$LBC::Groups::OnQueue"@%bgroup);
	//deleteVariables("$LBC::Groups::OnNQueue"@%bgroup);
	//talk(getRealTime()-%time);
}

function Logic_SetState(%group, %state)
{
	%group = %group | 0;
	$LBC::Groups::State[%group] = %state;
	$LBC::Groups::Update[%group] = true;
}

function Logic_QueueGroup(%group)
{
	%group = %group | 0;
	if(!$LBC::Groups::Tick && !$LBC::Groups::OnQueue[%group])
	{
		//echo("adding to queue on tick " @ (!$LBC::Groups::Tick ? "a":"b"));
		$LBC::Groups::OnQueue[%group] = true;
		$LBC::Groups::UpdateQueue[$LBC::Groups::UpdateQueueCount+0] = %group;
		$LBC::Groups::UpdateQueueCount++;
	}
	else if($LBC::Groups::Tick && !$LBC::Groups::OnNQueue[%group])
	{
		//echo("adding to queue on tick " @ (!$LBC::Groups::Tick ? "a":"b"));
		$LBC::Groups::OnNQueue[%group] = true;
		$LBC::Groups::NUpdateQueue[$LBC::Groups::NUpdateQueueCount+0] = %group;
		$LBC::Groups::NUpdateQueueCount++;
	}
}

function Logic_ShowWireConn(%group, %obj)
{
	%group = %group | 0;
	if(isObject(%obj))
		%group = $LBC::Wires::Group[%obj];

	%wires = $LBC::Groups::WireCount[%group];
	for(%i = 0; %i < %wires; %i++)
	{
		%obj = $LBC::Groups::Wire[%group, %i];
		%obj.setColorFX(3);
		%obj.schedule(1000, setColorFX, 0);
	}

	%ports = $LBC::Groups::PortCount[%group];
	for(%i = 0; %i < %ports; %i++)
	{
		%port = $LBC::Groups::Port[%group, %i];
		%obj = $LBC::Ports::Brick[%port];
		%obj.setColorFX(3);
		%obj.schedule(1000, setColorFX, 0);
	}
}

function Logic_showNei(%obj)
{
	%neighbors = $LBC::Bricks::NumNeighbors[%obj];
	for(%i = 0; %i < %neighbors; %i++)
	{
		%nei = $LBC::Bricks::Neighbor[%obj, %i];
		%nei.setColorFX(3);
		%nei.schedule(1000, "setColorFX", 0);
	}
}

function Logic_DebugWireGroup(%group, %obj)
{
	%group = %group | 0;
	if(isObject(%obj))
		%group = $LBC::Wires::Group[%obj];
	
	echo("-----------WIRE GROUP DEBUG-----------");
	echo("GROUP ID: " @ %group);

	%wires = $LBC::Groups::WireCount[%group];
	echo("\nWIRES", " (", %wires, "):");
	for(%i = 0; %i < %wires; %i++)
	{
		%obj = $LBC::Groups::Wire[%group, %i];
		echo("  ", %i, " - ", %obj, " [", %obj.getDatablock().getName(), "]", " (", $LBC::Groups::WireIDX[%group, %obj], ", ", isObject(%obj), ")");
	}

	%ports = $LBC::Groups::PortCount[%group];
	echo("\nPORTS", " (", %ports, "):");
	for(%i = 0; %i < %ports; %i++)
	{
		%port = $LBC::Groups::Port[%group, %i];
		%obj = $LBC::Ports::Brick[%port];
		%oidx = $LBC::Ports::BrickIDX[%port];
		%state = $LBC::Ports::State[%port];
		echo("  ", %i, " - ", %obj, " - ", %port, " (", %oidx, ") [", %state, "]");
	}
	echo("--------------------------------------");
}

function Logic_RecalculateGroups()
{
	talk("redoing groups");
	//deleteVariables("$LBC::Groups::*");
	//deleteVariables("$LBC::Ports::*");

	%count = mainBrickGroup.getCount();
	for(%i = 0; %i < %count; %i++)
	{
		%group = mainBrickGroup.getObject(%i);
		%gcount = %group.getCount();
		for(%a = 0; %a < %gcount; %a++)
		{
			%brick = %group.getObject(%a);
			if($LBC::Bricks::isWire[%brick])
				Logic_AddWire(%brick);
			else if($LBC::Bricks::isGate[%brick])
				Logic_AddGate(%brick);
		}
	}
	talk("aaa");
}
