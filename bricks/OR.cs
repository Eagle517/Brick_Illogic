datablock fxDTSBrickData(Logic1x2fORData)
{
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "1x2f OR";
	iconName = "Add-Ons/Brick_Illogic/icons/OR";
	brickFile = "add-ons/brick_illogic/bricks/blb/1x2f_2i_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	logicUIName = "OR";
	logicUIDesc = "C is true if A or B are true";

	numLogicPorts = 3;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 1 0";
	logicPortDir[0] = "2";
	logicPortUIName[0] = "A";
	logicPortUIDesc[0] = "";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 -1 0";
	logicPortDir[1] = "2";
	logicPortUIName[1] = "B";
	logicPortUIDesc[1] = "";

	logicPortType[2] = 0;
	logicPortPos[2] = "0 -1 0";
	logicPortDir[2] = "0";
	logicPortUIName[2] = "C";
	logicPortUIDesc[2] = "";
};

function Logic1x2fORData::doLogic(%this, %obj)
{
	//talk("PORTS " @ $LBC::Ports::BrickState[%obj, 0] SPC $LBC::Ports::BrickState[%obj, 1]);
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0] || $LBC::Ports::BrickState[%obj, 1]);
}
