datablock fxDTSBrickData(Logic1x1fBridgeRightData)
{
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Bridge Right";
	iconName = "";
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/bridge_right.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;

	logicUIName = "Bridge Right";
	logicUIDesc = "Allows wires to run through each other";

	numLogicPorts = 4;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = "2";
	logicPortUIName[0] = "Input A";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = "1";
	logicPortUIName[1] = "Input B";

	logicPortType[2] = 0;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = "0";
	logicPortUIName[2] = "Output A";

	logicPortType[3] = 0;
	logicPortPos[3] = "0 0 0";
	logicPortDir[3] = "3";
	logicPortUIName[3] = "Output B";
};

datablock fxDTSBrickData(Logic1x1fBridgeLeftData)
{
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Bridge Left";
	iconName = "";
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/bridge_left.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;

	logicUIName = "Bridge Left";
	logicUIDesc = "Allows wires to run through each other";

	numLogicPorts = 4;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = "2";
	logicPortUIName[0] = "Input A";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = "3";
	logicPortUIName[1] = "Input B";

	logicPortType[2] = 0;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = "1";
	logicPortUIName[2] = "Output B";

	logicPortType[3] = 0;
	logicPortPos[3] = "0 0 0";
	logicPortDir[3] = "0";
	logicPortUIName[3] = "Output A";
};

function Logic1x1fBridgeRightData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 1]);
}

function Logic1x1fBridgeLeftData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 1]);
}
