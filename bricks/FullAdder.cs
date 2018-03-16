datablock fxDTSBrickData(LogicGate_FullAdder_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/FullAdder.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "Full Adder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Full Adder";
	logicUIDesc = "Adds A and B with carry in";

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
	logicPortUIName[2] = "Carry In";

	logicPortType[3] = 0;
	logicPortPos[3] = "-1 0 0";
	logicPortDir[3] = 1;
	logicPortUIName[3] = "Sum";

	logicPortType[4] = 0;
	logicPortPos[4] = "-1 0 0";
	logicPortDir[4] = 0;
	logicPortUIName[4] = "Carry Out";
};

function LogicGate_FullAdder_Data::doLogic(%this, %obj)
{
	//Sum
	%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 0] ^ ($LBC::Ports::BrickState[%obj, 1] ^ $LBC::Ports::BrickState[%obj, 2]));

	//Carry
	%obj.Logic_SetOutput(4,
		($LBC::Ports::BrickState[%obj, 1] && $LBC::Ports::BrickState[%obj, 2]) ||
		($LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 2]) ||
		($LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]));
}