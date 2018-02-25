datablock fxDTSBrickData(Logic1x1fNOTData)
{
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "1x1f NOT";
	iconName = "";
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/1x1f_1i_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	logicUIName = "NOT";
	logicUIDesc = "B is the opposite of A";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = "2";
	logicPortUIName[0] = "A";
	logicPortUIDesc[0] = "";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = "0";
	logicPortUIName[1] = "B";
	logicPortUIDesc[1] = "";
};

function Logic1x1fNOTData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 0]);
}

function Logic1x1fNOTData::Logic_onGateAdded(%this, %obj)
{
	%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 0]);
}