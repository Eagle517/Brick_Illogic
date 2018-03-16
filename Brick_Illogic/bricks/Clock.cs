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
	logicUIDesc = "Uses brick name for half clock cycle. Format is LogicClock_TIME in milliseconds";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "Enable";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 1;
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
}

package Illogic_Clock
{
	function fxDTSBrick::setName(%this, %name)
	{
		if(%this.getDatablock().getName() $= "LogicGate_Clock_Data")
		{
			talk(%name);
		}
	}
};
activatePackage("Illogic_Clock");
