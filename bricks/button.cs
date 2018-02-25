datablock fxDTSBrickData(Logic1x1fButtonData)
{
	category = "Logic Bricks";
	subCategory = "Inputs";
	uiName = "Button";
	iconName = "base/client/ui/brickIcons/1x1F.png";
	brickFile = "add-ons/brick_illogic/bricks/blb/1x1f_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	isLogicInput = 1;

	numLogicPorts = 1;

	logicPortType[0] = 0;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = "0";
};

function Logic1x1fButtonData::Logic_onInput(%this, %obj, %pos, %norm)
{
	//talk("O" SPC !$LBC::Ports::BrickState[%obj, 0]);
	%obj.Logic_SetOutput(0, 1);
	%obj.schedule(100, "Logic_SetOutput", 0, 0);

	%obj.setColorFX(3);
	%obj.schedule(100, "setColorFX", 0);
}


function Logic1x1fButtonData::Logic_onGateAdded(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 0])
		%obj.setColorFX(3);
	else
		%obj.setColorFX(0);
}
