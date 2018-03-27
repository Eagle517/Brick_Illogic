datablock fxDTSBrickData(LogicGate_NotUp_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/NotUp.blb";
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "Not Up";
	iconName = "Add-Ons/Brick_Illogic/icons/NOT";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Not Up";
	logicUIDesc = "B is the inverse of A";

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

datablock fxDTSBrickData(LogicGate_NotDown_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/NotDown.blb";
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "Not Down";
	iconName = "Add-Ons/Brick_Illogic/icons/NOT";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Not Down";
	logicUIDesc = "B is the inverse of A";

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

function LogicGate_NotUp_Data::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 0]);
}

function LogicGate_NotUp_Data::Logic_onGateAdded(%this, %obj)
{
	%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 0]);
}

function LogicGate_NotDown_Data::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 0]);
}

function LogicGate_NotDown_Data::Logic_onGateAdded(%this, %obj)
{
	%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 0]);
}
