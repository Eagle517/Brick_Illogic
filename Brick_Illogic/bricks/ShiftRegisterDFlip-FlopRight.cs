datablock fxDTSBrickData(LogicGate_ShiftRegisterDFlipDASHFlopRight_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/ShiftRegisterDFlip-FlopRight.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "Shift Register D Flip-Flop Right";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Shift Register D Flip-Flop Right";
	logicUIDesc = "D Flip-Flop configured for use as a shift register element, with the two outputs being identical";

	numLogicPorts = 4;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "DataIn";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 2;
	logicPortUIName[1] = "Q2";

	logicPortType[2] = 0;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = 0;
	logicPortUIName[2] = "Q1";

	logicPortType[3] = 0;
	logicPortPos[3] = "0 0 0";
	logicPortDir[3] = 1;
	logicPortUIName[3] = "Clock";
};

function LogicGate_ShiftRegisterDFlipDASHFlop_DataRight::doLogic(%this, %obj)
{
	
}

function LogicGate_ShiftRegisterDFlipDASHFlop_DataRight::Logic_onGateAdded(%this, %obj)
{
	
}
