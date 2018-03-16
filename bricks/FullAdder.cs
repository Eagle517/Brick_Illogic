datablock fxDTSBrickData(LogicGate_FullAdder_Data)
{
	brickFile = "config/server/IllogicGateMaker/FullAdder.blb";
	category = "Logic Bricks";
	subCategory = "Gatemaker";
	uiName = "Full Adder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Full Adder";
	logicUIDesc = "";

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
	
}

function LogicGate_FullAdder_Data::Logic_onGateAdded(%this, %obj)
{
	
}

function LogicGate_FullAdder_Data::Logic_onInput(%this, %obj, %pos, %norm)
{
	
}
