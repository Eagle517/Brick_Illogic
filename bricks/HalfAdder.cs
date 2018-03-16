datablock fxDTSBrickData(LogicGate_HalfAdder_Data)
{
	brickFile = "config/server/IllogicGateMaker/HalfAdder.blb";
	category = "Logic Bricks";
	subCategory = "Gatemaker";
	uiName = "Half Adder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Half Adder";
	logicUIDesc = "";

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
	logicPortUIName[2] = "Sum";

	logicPortType[3] = 0;
	logicPortPos[3] = "-1 0 0";
	logicPortDir[3] = 0;
	logicPortUIName[3] = "Carry";
};

function LogicGate_HalfAdder_Data::doLogic(%this, %obj)
{
	
}

function LogicGate_HalfAdder_Data::Logic_onGateAdded(%this, %obj)
{
	
}

function LogicGate_HalfAdder_Data::Logic_onInput(%this, %obj, %pos, %norm)
{
	
}
