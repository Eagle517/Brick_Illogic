if($LBC::Groups::NumGroups $= "")
	$LBC::Groups::NumGroups = 0;

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

	%group = -1;

	$LBC::Bricks::NumNeighbors[%obj] = 0;
	$LBC::Wires::isVisual[%obj] = %data.isLogicVisual+0;
	$LBC::Wires::doEvents[%obj] = %data.doLogicEvents+0;

	initContainerBoxSearch(%pos, vectorAdd(%size, "0 -0.1 -0.05"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		if(!$LBC::Bricks::isLogic[%sobj] || %sobj.isDead())
			continue;

		if($LBC::Bricks::isGate[%sobj])
		{
			%ports = $LBC::Bricks::PortCount[%sobj];
			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Bricks::Port[%sobj, %a];
				if(containerRayCast($LBC::Ports::WorldPos[%port], $LBC::Ports::ConnPos[%port], $TypeMasks::FxBrickAlwaysObjectType, %sobj) == %obj)
				{
					%oGroup = $LBC::Ports::Group[%port];
					if(%group == -1)
					{
						%group = $LBC::Groups::NumGroups;
						$LBC::Groups::NumGroups++;

						$LBC::Groups::Port[%group, 0] = %port;
						$LBC::Groups::PortIDX[%group, %port] = 0;
						$LBC::Groups::PortCount[%group] = 1;
						$LBC::Ports::Group[%port] = %group;

						$LBC::Groups::Wire[%group, 0] = %obj;
						$LBC::Groups::WireIDX[%group, %obj] = 0;
						$LBC::Groups::WireCount[%group] = 1;
						$LBC::Wires::Group[%obj] = %group;
					}
					else if(%group == %oGroup)
						continue;
					else
					{
						%tpSize = $LBC::Groups::PortCount[%group];
						$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize] = %port] = %group;
						$LBC::Groups::PortIDX[%group, %port] = %tpSize;
						$LBC::Groups::PortCount[%group]++;
					}
				}
			}
		}
		else if($LBC::Bricks::isWire[%sobj] && %sobj.getColorID() == %colorID)
		{
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

					%group = %oGroup;
				}
			}
		}
	}

	initContainerBoxSearch(%pos, vectorAdd(%size, "-0.1 0 -0.05"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		if(!$LBC::Bricks::isLogic[%sobj] || %sobj.isDead())
			continue;

		if($LBC::Bricks::isGate[%sobj])
		{
			%ports = $LBC::Bricks::PortCount[%sobj];
			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Bricks::Port[%sobj, %a];
				if(containerRayCast($LBC::Ports::WorldPos[%port], $LBC::Ports::ConnPos[%port], $TypeMasks::FxBrickAlwaysObjectType, %sobj) == %obj)
				{
					%oGroup = $LBC::Ports::Group[%port];
					if(%group == -1)
					{
						%group = $LBC::Groups::NumGroups;
						$LBC::Groups::NumGroups++;

						$LBC::Groups::Port[%group, 0] = %port;
						$LBC::Groups::PortIDX[%group, %port] = 0;
						$LBC::Groups::PortCount[%group] = 1;
						$LBC::Ports::Group[%port] = %group;

						$LBC::Groups::Wire[%group, 0] = %obj;
						$LBC::Groups::WireIDX[%group, %obj] = 0;
						$LBC::Groups::WireCount[%group] = 1;
						$LBC::Wires::Group[%obj] = %group;
					}
					else if(%group == %oGroup)
						continue;
					else
					{
						%tpSize = $LBC::Groups::PortCount[%group];
						$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize] = %port] = %group;
						$LBC::Groups::PortIDX[%group, %port] = %tpSize;
						$LBC::Groups::PortCount[%group]++;
					}
				}
			}
		}
		else if($LBC::Bricks::isWire[%sobj] && %sobj.getColorID() == %colorID)
		{
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

					%group = %oGroup;
				}
			}
		}
	}

	initContainerBoxSearch(%pos, vectorAdd(%size, "-0.1 -0.1 0.05"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		if(!$LBC::Bricks::isLogic[%sobj] || %sobj.isDead())
			continue;

		if($LBC::Bricks::isGate[%sobj])
		{
			%ports = $LBC::Bricks::PortCount[%sobj];
			for(%a = 0; %a < %ports; %a++)
			{
				%port = $LBC::Bricks::Port[%sobj, %a];
				if(containerRayCast($LBC::Ports::WorldPos[%port], $LBC::Ports::ConnPos[%port], $TypeMasks::FxBrickAlwaysObjectType, %sobj) == %obj)
				{
					%oGroup = $LBC::Ports::Group[%port];
					if(%group == -1)
					{
						%group = $LBC::Groups::NumGroups;
						$LBC::Groups::NumGroups++;

						$LBC::Groups::Port[%group, 0] = %port;
						$LBC::Groups::PortIDX[%group, %port] = 0;
						$LBC::Groups::PortCount[%group] = 1;
						$LBC::Ports::Group[%port] = %group;

						$LBC::Groups::Wire[%group, 0] = %obj;
						$LBC::Groups::WireIDX[%group, %obj] = 0;
						$LBC::Groups::WireCount[%group] = 1;
						$LBC::Wires::Group[%obj] = %group;
					}
					else if(%group == %oGroup)
						continue;
					else
					{
						%tpSize = $LBC::Groups::PortCount[%group];
						$LBC::Ports::Group[$LBC::Groups::Port[%group, %tpSize] = %port] = %group;
						$LBC::Groups::PortIDX[%group, %port] = %tpSize;
						$LBC::Groups::PortCount[%group]++;
					}
				}
			}
		}
		else if($LBC::Bricks::isWire[%sobj] && %sobj.getColorID() == %colorID)
		{
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

					%group = %oGroup;
				}
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

function Logic_RemoveWire(%obj, %instCall)
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

	if(%instCall)
	{
		cancel($LBC::Schedules::rs[%group]);
		Logic_RefreshWireGroup(%group);
	}
	else
	{
		cancel($LBC::Schedules::rs[%group]);
		$LBC::Schedules::rs[%group] = schedule(100, 0, "Logic_RefreshWireGroup", %group);
	}
}

function Logic_RefreshWireGroup(%bgroup)
{
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
		%portID = $LBC::Groups::Port[%bgroup, %i];
		if(isObject(%sobj = firstWord(containerRayCast($LBC::Ports::WorldPos[%portID], $LBC::Ports::ConnPos[%portID], $TypeMasks::FxBrickAlwaysObjectType, $LBC::Ports::Brick[%portID]))))
		{
			if($LBC::Bricks::isWire[%sobj])
			{
				%group = $LBC::Wires::Group[%sobj];
				$LBC::Ports::Group[%portID] = %group;
				$LBC::Groups::Port[%group, %pc = $LBC::Groups::PortCount[%group]] = %portID;
				$LBC::Groups::PortIDX[%group, %portID] = %pc;
				$LBC::Groups::PortCount[%group]++;
			}
			else if($LBC::Bricks::isGate[%sobj])
			{
				%bestDist = -1;
				%aports = $LBC::Bricks::PortCount[%sobj];
				for(%a = 0; %a < %aports; %a++)
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
		
		if(%group == -1)
			$LBC::Ports::Group[%portID] = -1;
		else
			Logic_QueueGroup(%group);
	}

	$LBC::Queues::RefreshCount[%bgroup] = 0;
}

function Logic_ShowWires(%group, %obj)
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

function Logic_ShowNeighbors(%obj)
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
