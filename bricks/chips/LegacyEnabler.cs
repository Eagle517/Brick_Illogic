datablock fxDTSBrickData(LogicGate_LegacyEnabler_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/LegacyEnabler.blb";
	category = "Logic Bricks";
	subCategory = "Chips";
	uiName = "Legacy Enabler";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Legacy Enabler";
	logicUIDesc = "Allows or blocks wire signals";

	numLogicPorts = 10;

	logicPortType[0] = 1;
	logicPortPos[0] = "3 -1 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "1 -1 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-1 -1 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "-3 -1 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 1;
	logicPortPos[4] = "3 -1 0";
	logicPortDir[4] = 2;
	logicPortUIName[4] = "Enable";

	logicPortType[5] = 0;
	logicPortPos[5] = "3 1 0";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "Out0";

	logicPortType[6] = 0;
	logicPortPos[6] = "1 1 0";
	logicPortDir[6] = 1;
	logicPortUIName[6] = "Out1";

	logicPortType[7] = 0;
	logicPortPos[7] = "-1 1 0";
	logicPortDir[7] = 1;
	logicPortUIName[7] = "Out2";

	logicPortType[8] = 0;
	logicPortPos[8] = "-3 1 0";
	logicPortDir[8] = 1;
	logicPortUIName[8] = "Out3";

	logicPortType[9] = 0;
	logicPortPos[9] = "-3 -1 0";
	logicPortDir[9] = 0;
	logicPortUIName[9] = "Enable Carry";
};

function LogicGate_LegacyEnabler_Data::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj,4])
	{
		%obj.Logic_SetOutput(5, $LBC::Ports::BrickState[%obj,0]);
		%obj.Logic_SetOutput(6, $LBC::Ports::BrickState[%obj,1]);
		%obj.Logic_SetOutput(7, $LBC::Ports::BrickState[%obj,2]);
		%obj.Logic_SetOutput(8, $LBC::Ports::BrickState[%obj,3]);
		%obj.Logic_SetOutput(9, 1);
	}
	else
	{
		%obj.Logic_SetOutput(5, 0);
		%obj.Logic_SetOutput(6, 0);
		%obj.Logic_SetOutput(7, 0);
		%obj.Logic_SetOutput(8, 0);
		%obj.Logic_SetOutput(9, 0);
	}
}
