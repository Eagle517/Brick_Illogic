datablock fxDTSBrickData(LogicGate_Shifter_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/Shifter.blb";
	category = "Logic Bricks";
	subCategory = "Chips";
	uiName = "Shifter";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Shifter";
	logicUIDesc = "";

	numLogicPorts = 14;

	logicPortType[0] = 1;
	logicPortPos[0] = "3 -2 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "1 -2 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-1 -2 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "-3 -2 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 0;
	logicPortPos[4] = "3 2 0";
	logicPortDir[4] = 1;
	logicPortUIName[4] = "Out0";

	logicPortType[5] = 0;
	logicPortPos[5] = "1 2 0";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "Out1";

	logicPortType[6] = 0;
	logicPortPos[6] = "-1 2 0";
	logicPortDir[6] = 1;
	logicPortUIName[6] = "Out2";

	logicPortType[7] = 0;
	logicPortPos[7] = "-3 2 0";
	logicPortDir[7] = 1;
	logicPortUIName[7] = "Out3";

	logicPortType[8] = 1;
	logicPortPos[8] = "3 2 0";
	logicPortDir[8] = 2;
	logicPortUIName[8] = "Clock";

	logicPortType[9] = 0;
	logicPortPos[9] = "-3 2 0";
	logicPortDir[9] = 0;
	logicPortUIName[9] = "Overflow";

	logicPortType[10] = 1;
	logicPortPos[10] = "3 0 0";
	logicPortDir[10] = 2;
	logicPortUIName[10] = "Dir-In";

	logicPortType[11] = 0;
	logicPortPos[11] = "-3 0 0";
	logicPortDir[11] = 0;
	logicPortUIName[11] = "Dir-Out";

	logicPortType[12] = 1;
	logicPortPos[12] = "3 -2 0";
	logicPortDir[12] = 2;
	logicPortUIName[12] = "Set-In";

	logicPortType[13] = 0;
	logicPortPos[13] = "-3 -2 0";
	logicPortDir[13] = 0;
	logicPortUIName[13] = "Set-Out";
};

function LogicGate_Shifter_Data::doLogic(%this, %obj)
{
	//Clock on high signal
	if($LBC::Ports::BrickState[%obj,8] && !%obj.clockPrevState)
	{
		%obj.clockPrevState = 1;
	}
	else if(!$LBC::Ports::BrickState[%obj,8] && %obj.clockPrevState)
	{
		%obj.clockPrevState = 0;
	}

	//Set-In on high signal
	if($LBC::Ports::BrickState[%obj,12] && !%obj.setPrevState)
	{
		%obj.setPrevState = 1;
	}
	else if(!$LBC::Ports::BrickState[%obj,12] && %obj.setPrevState)
	{
		%obj.setPrevState = 0;
	}
}

function LogicGate_Shifter_Data::Logic_onGateAdded(%this, %obj)
{
	%obj.clockPrevState = 0;
	%obj.setPrevState = 0;
}