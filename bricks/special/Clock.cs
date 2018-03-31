datablock fxDTSBrickData(LogicGate_Clock_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/1x1f_1i_1o.blb";
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Clock";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Clock";
	logicUIDesc = "Uses brick name for half clock cycle. Format is LogicClock_TIME";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 0;
	logicPortUIName[0] = "Enable";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 2;
	logicPortUIName[1] = "Output";
};

function LogicGate_Clock_Data::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 0])
	{
		cancel(%obj.LBCSched);
		%obj.LBCSched = %this.schedule(%obj.logicClockSpeed, "doLogic", %obj);
		%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 1]);
	}
	else
		%obj.Logic_SetOutput(1, false);
}

function LogicGate_Clock_Data::Logic_onGateAdded(%this, %obj)
{
	if(%obj.logicClockSpeed < 8)
		%obj.setName("_LogicClock_8");
}

package Illogic_Clock
{
	function fxDTSBrick::setName(%this, %name)
	{
		if((%data = %this.getDatablock()).getName() $= "LogicGate_Clock_Data")
		{
			%rname = getSubStr(%name, 1, strlen(%name));
			if(stripos(%rname, "LogicClock") == 0)
			{
				%time = mClamp(getWord(strReplace(%rname, "_", " "), 1), 8, 999999);
				%this.logicClockSpeed = %time;
				%name = "_LogicClock_"@%time;
				%data.doLogic(%this);
			}
		}
		parent::setName(%this, %name);
	}
};
activatePackage("Illogic_Clock");
