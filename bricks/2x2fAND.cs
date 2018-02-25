datablock fxDTSBrickData(Logic2x2fANDData)
{
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "2x2f AND";
	iconName = "";
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/2x2fAND.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;

	numLogicPorts = 3;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 -1 0";
	logicPortDir[0] = "1";
	//rfPortCauseUpdates[0] = 0;

	logicPortType[1] = 1;
	logicPortPos[1] = "-1 1 0";
	logicPortDir[1] = "1";
	//rfPortCauseUpdates[1] = 0;

	logicPortType[2] = 0;
	logicPortPos[2] = "1 1 0";
	logicPortDir[2] = "3";
};

function Logic2x2fANDData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]);
}
