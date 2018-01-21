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

	initContainerBoxSearch(%pos, vectorAdd(%size, "0 -0.02 -0.02"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		//%sobj.setColorFX(3);
		//%sobj.schedule(1000, setColorFX, 0);
		if(!$LBCiW[%sobj] || %sobj.isDead() || %sobj.getColorID() != %colorID)
			continue;

		%oGroup = $LBCg[%sobj];
		if(%group == -1)
		{
			%group = %oGroup;
			$LBCi[%group, $LBCc[%group]] = %obj;
			$LBCc[%group]++;
			//talk("add incre" SPC $LBCc[%group]);
			$LBCg[%obj] = %group;
		}
		else if(%group == %oGroup)
			continue;
		else
		{
			%tSize = $LBCc[%group];
			%oSize = $LBCc[%oGroup];
			if(%tSize > %oSize)
			{
				for(%i = 0; %i < %oSize; %i++)
					$LBCg[$LBCi[%group, %tSize+%i] = $LBCi[%oGroup, %i]] = %group;
				
				//talk("size incre" SPC $LBCc[%group] SPC %tSize SPC %oSize);
				$LBCc[%oGroup] = 0;
				$LBCc[%group] += %oSize;
			}
			else
			{
				for(%i = 0; %i < %tSize; %i++)
					$LBCg[$LBCi[%oGroup, %oSize+%i] = $LBCi[%group, %i]] = %oGroup;
				
				//talk("size incre2" SPC $LBCc[%group] SPC %tSize SPC %oSize);
				$LBCc[%group] = 0;
				$LBCc[%oGroup] += %tSize;
			}
		}
	}

	initContainerBoxSearch(%pos, vectorAdd(%size, "-0.02 0 -0.02"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		//%sobj.setColorFX(3);
		//%sobj.schedule(1000, setColorFX, 0);
		if(!$LBCiW[%sobj] || %sobj.isDead() || %sobj.getColorID() != %colorID)
			continue;

		%oGroup = $LBCg[%sobj];
		if(%group == -1)
		{
			%group = %oGroup;
			$LBCi[%group, $LBCc[%group]] = %obj;
			$LBCc[%group]++;
			//talk("add incre" SPC $LBCc[%group]);
			$LBCg[%obj] = %group;
		}
		else if(%group == %oGroup)
			continue;
		else
		{
			%tSize = $LBCc[%group];
			%oSize = $LBCc[%oGroup];
			if(%tSize > %oSize)
			{
				for(%i = 0; %i < %oSize; %i++)
					$LBCg[$LBCi[%group, %tSize+%i] = $LBCi[%oGroup, %i]] = %group;
				
				//talk("size incre" SPC $LBCc[%group] SPC %tSize SPC %oSize);
				$LBCc[%oGroup] = 0;
				$LBCc[%group] += %oSize;
			}
			else
			{
				for(%i = 0; %i < %tSize; %i++)
					$LBCg[$LBCi[%oGroup, %oSize+%i] = $LBCi[%group, %i]] = %oGroup;
				
				//talk("size incre2" SPC $LBCc[%group] SPC %tSize SPC %oSize);
				$LBCc[%group] = 0;
				$LBCc[%oGroup] += %tSize;
			}
		}
	}

	initContainerBoxSearch(%pos, vectorAdd(%size, "-0.02 -0.02 0.02"), $TypeMasks::FxBrickAlwaysObjectType);
	while(%sobj = containerSearchNext())
	{
		//%sobj.setColorFX(3);
		//%sobj.schedule(1000, setColorFX, 0);
		if(!$LBCiW[%sobj] || %sobj.isDead() || %sobj.getColorID() != %colorID)
			continue;

		%oGroup = $LBCg[%sobj];
		if(%group == -1)
		{
			%group = %oGroup;
			$LBCi[%group, $LBCc[%group]] = %obj;
			$LBCc[%group]++;
			//talk("add incre" SPC $LBCc[%group]);
			$LBCg[%obj] = %group;
		}
		else if(%group == %oGroup)
			continue;
		else
		{
			%tSize = $LBCc[%group];
			%oSize = $LBCc[%oGroup];
			if(%tSize > %oSize)
			{
				for(%i = 0; %i < %oSize; %i++)
					$LBCg[$LBCi[%group, %tSize+%i] = $LBCi[%oGroup, %i]] = %group;
				
				//talk("size incre" SPC $LBCc[%group] SPC %tSize SPC %oSize);
				$LBCc[%oGroup] = 0;
				$LBCc[%group] += %oSize;
			}
			else
			{
				for(%i = 0; %i < %tSize; %i++)
					$LBCg[$LBCi[%oGroup, %oSize+%i] = $LBCi[%group, %i]] = %oGroup;
				
				//talk("size incre2" SPC $LBCc[%group] SPC %tSize SPC %oSize);
				$LBCc[%group] = 0;
				$LBCc[%oGroup] += %tSize;
			}
		}
	}

	if(%group == -1)
	{
		%group = atoi($LBCgc);
		if(!$LBCc[%group-1])
			%group -= 1;
		else
			$LBCgc++;
		
		$LBCi[%group, 0] = %obj;
		$LBCc[%group] = 1;
		$LBCg[%obj] = %group;
	}

	$LBCiL[%obj] = true;
	$LBCiW[%obj] = true;
}

function Logic_RemoveWire(%obj)
{
	if(!isObject(%obj))
		return;

	%group = $LBCg[%obj];
	$LBCg[%o] = -1;
	$LBCiL[%o] = false;
	$LBCiW[%o] = false;

	cancel($LBCrs[%group]);
	$LBCrs[%group] = schedule(32, 0, Logic_RefreshWireGroup, %group);
}

function Logic_RefreshWireGroup(%group)
{
	%tsize = $LBCc[%group];
	for(%i = 0; %i < %tsize; %i++)
	{
		%o = $LBCi[%group, %i];
		$LBCg[%o] = -1;
		$LBCiL[%o] = false;
		$LBCiW[%o] = false;
	}
	$LBCc[%group] = 0;

	for(%i = 0; %i < %tsize; %i++)
	{
		%o = $LBCi[%group, %i];
		if(%o == %obj)
			continue;
		Logic_AddWire(%o);
	}
}

function Logic_ShowWireConn(%obj)
{
	if(!isObject(%obj))
		return;

	%group = $LBCg[%obj];
	%tsize = $LBCc[%group];
	for(%i = 0; %i < %tsize; %i++)
	{
		%o = $LBCi[%group, %i];
		if(isObject(%o))
		{
			%o.setColorFX(3);
			%o.schedule(1000, setColorFX, 0);
		}
	}
}

function Logic_FindDupes(%obj)
{
	if(!isObject(%obj))
		return;

	%group = $LBCg[%obj];
	%tsize = $LBCc[%group];
	for(%i = 0; %i < %tsize; %i++)
	{
		%o = $LBCi[%group, %i];
		echo(%i, " - ", %o);
	}
}
