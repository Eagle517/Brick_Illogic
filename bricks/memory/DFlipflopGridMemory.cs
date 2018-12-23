datablock fxDTSBrickData(LogicGate_DFlipflopGridMemory_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/DFlipflopGridMemory.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "D Flipflop Grid Memory";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "D Flipflop Grid Memory";
	logicUIDesc = "D Flipflop where C = C1 & C2";

	numLogicPorts = 5;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 -1";
	logicPortDir[0] = 5;
	logicPortUIName[0] = "D";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "C1";

	logicPortType[2] = 1;
	logicPortPos[2] = "0 0 -1";
	logicPortDir[2] = 2;
	logicPortUIName[2] = "C2";

	logicPortType[3] = 0;
	logicPortPos[3] = "0 0 1";
	logicPortDir[3] = 4;
	logicPortUIName[3] = "Q1";

	logicPortType[4] = 0;
	logicPortPos[4] = "0 0 1";
	logicPortDir[4] = 2;
	logicPortUIName[4] = "Q2";
};

function LogicGate_DFlipflopGridMemory_Data::doLogic(%this, %obj)
{
    if(($LBC::Ports::BrickState[%obj, 1] && $LBC::Ports::BrickState[%obj, 2]) && !($LBC::Ports::LastBrickState[%obj, 1] && $LBC::Ports::LastBrickState[%obj, 2]))
        %obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 0]);
    %obj.Logic_SetOutput(4, $LBC::Ports::BrickState[%obj, 3] && $LBC::Ports::BrickState[%obj, 1] && $LBC::Ports::BrickState[%obj, 2]);
}
