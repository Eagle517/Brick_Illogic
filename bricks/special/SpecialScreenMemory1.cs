datablock fxDTSBrickData(LogicGate_SpecialScreenMemory1_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/SpecialScreenMemory1.blb";
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Special Screen Memory 1";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Special Screen Memory 1";
	logicUIDesc = "<color:ffffff>C = A & B;<br>C D Q form a D Flipflop;<br>P = Q & C;";

	numLogicPorts = 5;

	logicPortType[0] = 1;
	logicPortPos[0] = "-2 0 0";
	logicPortDir[0] = 0;
	logicPortUIName[0] = "A";

	logicPortType[1] = 1;
	logicPortPos[1] = "-2 0 0";
	logicPortDir[1] = 4;
	logicPortUIName[1] = "B";

	logicPortType[2] = 1;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = 4;
	logicPortUIName[2] = "D";

	logicPortType[3] = 0;
	logicPortPos[3] = "2 0 0";
	logicPortDir[3] = 2;
	logicPortUIName[3] = "Q";

	logicPortType[4] = 0;
	logicPortPos[4] = "2 0 0";
	logicPortDir[4] = 4;
	logicPortUIName[4] = "P";
};

function LogicGate_SpecialScreenMemory1_Data::doLogic(%this, %obj)
{
	%clock = $LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1];
	%lastClock = $LBC::Ports::LastBrickState[%obj, 0] && $LBC::Ports::LastBrickState[%obj, 1];
	
	if(%clock && !%lastClock)
		%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 2]);

	%obj.Logic_SetOutput(4, $LBC::Ports::BrickState[%obj, 3] && %clock);
}