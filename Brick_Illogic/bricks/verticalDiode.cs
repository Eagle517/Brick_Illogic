datablock fxDTSBrickData(LogicGate_DiodeUp_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/DiodeUp.blb";
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "Diode Up";
	iconName = "Add-Ons/Brick_Illogic/icons/diode";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Diode Up";
	logicUIDesc = "B is A";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 5;
	logicPortUIName[0] = "A";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 4;
	logicPortUIName[1] = "B";
};

datablock fxDTSBrickData(LogicGate_DiodeDown_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/DiodeDown.blb";
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "Diode Down";
	iconName = "Add-Ons/Brick_Illogic/icons/diode";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Diode Down";
	logicUIDesc = "B is A";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = 4;
	logicPortUIName[0] = "A";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 5;
	logicPortUIName[1] = "B";
};

function LogicGate_DiodeUp_Data::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(1, $LBC::Ports::BrickState[%obj, 0]);
}

function LogicGate_DiodeDown_Data::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(1, $LBC::Ports::BrickState[%obj, 0]);
}
