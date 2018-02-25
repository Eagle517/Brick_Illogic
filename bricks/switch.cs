datablock fxDTSBrickData(Logic1x2fSwitchData)
{
	category = "Logic Bricks";
	subCategory = "Inputs";
	uiName = "Switch";
	iconName = "base/client/ui/brickIcons/1x2F.png";
	brickFile = "add-ons/brick_illogic/bricks/blb/switch.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 1;

	isLogic = 1;
	isLogicGate = 1;
	isLogicInput = 1;

	numLogicPorts = 2;

	logicPortType[0] = 0;
	logicPortPos[0] = "0 1 0";
	logicPortDir[0] = "1";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 -1 0";
	logicPortDir[1] = "3";
};

function Logic1x2fSwitchData::Logic_onInput(%this, %obj, %pos, %norm)
{
	//talk("O" SPC !$LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(0, !$LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(1, !$LBC::Ports::BrickState[%obj, 1]);

	if($LBC::Ports::BrickState[%obj, 0])
		%obj.setColorFX(3);
	else
		%obj.setColorFX(0);
}

function Logic1x2fSwitchData::Logic_onGateAdded(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 0])
		%obj.setColorFX(3);
	else
		%obj.setColorFX(0);
}
