datablock fxDTSBrickData(Logic1x1fBridgeLeftData)
{
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Bridge Left";
	iconName = "Add-Ons/Brick_Illogic/icons/ForwardLeft";
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
	logicPortDir[0] = "0";
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
	logicPortDir[3] = "2";
	logicPortUIName[3] = "Output A";
};

datablock fxDTSBrickData(Logic1x1fBridgeRightData)
{
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Bridge Right";
	iconName = "Add-Ons/Brick_Illogic/icons/ForwardRight";
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
	logicPortDir[0] = "0";
	logicPortUIName[0] = "Input A";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = "1";
	logicPortUIName[1] = "Input B";

	logicPortType[2] = 0;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = "2";
	logicPortUIName[2] = "Output A";

	logicPortType[3] = 0;
	logicPortPos[3] = "0 0 0";
	logicPortDir[3] = "3";
	logicPortUIName[3] = "Output B";
};

datablock fxDTSBrickData(Logic1x1fBridgePerpData : Logic1x1fBridgeLeftData)
{
	uiName = "Bridge Perp";
	logicUIName = "Bridge Perp";
	logicUIDesc = "Allows wires to run through each other perpendicularly";
	logicPortUIName[2] = "Output A";
	logicPortUIName[3] = "Output B";
};

// datablock fxDTSBrickData(Logic1x1fBridgePerpRightData : Logic1x1fBridgeRightData)
// {
// 	logicUIName = "Bridge Right Perp";
// 	logicUIDesc = "Allows wires to run through each other perpendicularly";
// 	logicPortUIName[2] = "Output B";
// 	logicPortUIName[3] = "Output A";
// };


function Logic1x1fBridgeLeftData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 1]);
}

function Logic1x1fBridgeRightData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 1]);
}

function Logic1x1fBridgePerpData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 1]);
}

// function Logic1x1fBridgePerpRightData::doLogic(%this, %obj)
// {
// 	%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 0]);
// 	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 1]);
// }
