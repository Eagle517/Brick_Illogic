datablock fxDTSBrickData(LogicGate_FullSubtractor_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/FullAdder.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "Full Subtractor";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Full Subtractor";
	logicUIDesc = "Subtracts B from A with borrow in";

	numLogicPorts = 5;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "A";

	logicPortType[1] = 1;
	logicPortPos[1] = "1 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "B";

	logicPortType[2] = 1;
	logicPortPos[2] = "1 0 0";
	logicPortDir[2] = 2;
	logicPortUIName[2] = "Borrow In";

	logicPortType[3] = 0;
	logicPortPos[3] = "-1 0 0";
	logicPortDir[3] = 1;
	logicPortUIName[3] = "Difference";

	logicPortType[4] = 0;
	logicPortPos[4] = "-1 0 0";
	logicPortDir[4] = 0;
	logicPortUIName[4] = "Borrow Out";
};

function LogicGate_FullSubtractor_Data::doLogic(%this, %obj)
{
	//Difference
	%obj.Logic_SetOutput(3, ($LBC::Ports::BrickState[%obj, 0] ^ $LBC::Ports::BrickState[%obj, 1]) ^ $LBC::Ports::BrickState[%obj, 2]);

	//Borrow
	%obj.Logic_SetOutput(4, !$LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1] || !($LBC::Ports::BrickState[%obj, 0] ^ $LBC::Ports::BrickState[%obj, 1]) && $LBC::Ports::BrickState[%obj, 2]);
}