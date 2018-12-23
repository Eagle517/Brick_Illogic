datablock fxDTSBrickData(LogicGate_DFlipflopGridMemory2_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/DFlipflopGridMemory2.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "D Flipflop Grid Memory 2";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "D Flipflop Grid Memory 2";
	logicUIDesc = "D Flipflop where Clk = C & A & B, R = Q & A & B";

	numLogicPorts = 6;

	logicPortType[0] = 0;
	logicPortPos[0] = "0 0 2";
	logicPortDir[0] = 4;
	logicPortUIName[0] = "Q";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 2";
	logicPortDir[1] = 1;
	logicPortUIName[1] = "Readout";

	logicPortType[2] = 1;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = 2;
	logicPortUIName[2] = "A";

	logicPortType[3] = 1;
	logicPortPos[3] = "0 0 1";
	logicPortDir[3] = 1;
	logicPortUIName[3] = "B";

	logicPortType[4] = 1;
	logicPortPos[4] = "0 0 -2";
	logicPortDir[4] = 1;
	logicPortUIName[4] = "Data";

	logicPortType[5] = 1;
	logicPortPos[5] = "0 0 -1";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "Clock";
};

function LogicGate_DFlipflopGridMemory2_Data::doLogic(%this, %obj)
{
	%A = $LBC::Ports::BrickState[%obj, 2];
	%B = $LBC::Ports::BrickState[%obj, 3];
	%D = $LBC::Ports::BrickState[%obj, 4];
	%C = $LBC::Ports::BrickState[%obj, 5];
	%lastC = $LBC::Ports::LastBrickState[%obj, 5];

	if(%A && %B && %C && !%lastC)
		%obj.Logic_SetOutput(0, %D);
	%obj.Logic_SetOutput(1, %A && %B && $LBC::Ports::BrickState[%obj, 0]);
}
