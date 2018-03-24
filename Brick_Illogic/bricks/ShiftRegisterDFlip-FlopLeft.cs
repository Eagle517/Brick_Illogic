datablock fxDTSBrickData(LogicGate_ShiftRegisterDFlipDASHFlopLeft_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/ShiftRegisterDFlip-FlopLeft.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "Shift Register D Flip-Flop Left";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Shift Register D Flip-Flop Left";
	logicUIDesc = "D Flip-Flop configured for use as a shift register element, with the two outputs being identical";

	numLogicPorts = 4;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "DataIn";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 2;
	logicPortUIName[1] = "Clock";

	logicPortType[2] = 0;
	logicPortPos[2] = "0 0 0";
	logicPortDir[2] = 1;
	logicPortUIName[2] = "Q1";

	logicPortType[3] = 0;
	logicPortPos[3] = "0 0 0";
	logicPortDir[3] = 0;
	logicPortUIName[3] = "Q2";
};

function LogicGate_ShiftRegisterDFlipDASHFlopLeft_Data::doLogic(%this, %obj)
{
	
}

function LogicGate_ShiftRegisterDFlipDASHFlopLeft_Data::Logic_onGateAdded(%this, %obj)
{
	
}
