datablock fxDTSBrickData(LogicGate_HalfSubtractor_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/HalfAdder.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "Half Subtractor";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Half Subtractor";
	logicUIDesc = "Subtracts B from A";

	numLogicPorts = 4;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "A";

	logicPortType[1] = 1;
	logicPortPos[1] = "1 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "B";

	logicPortType[2] = 0;
	logicPortPos[2] = "-1 0 0";
	logicPortDir[2] = 1;
	logicPortUIName[2] = "Difference";

	logicPortType[3] = 0;
	logicPortPos[3] = "-1 0 0";
	logicPortDir[3] = 0;
	logicPortUIName[3] = "Borrow";
};

function LogicGate_HalfSubtractor_Data::doLogic(%this, %obj)
{
	//Difference
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0] ^ $LBC::Ports::BrickState[%obj, 1]);

	//Borrow
	%obj.Logic_SetOutput(3, !$LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]);
}
