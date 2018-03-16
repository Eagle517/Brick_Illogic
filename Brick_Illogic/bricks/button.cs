datablock fxDTSBrickData(Logic1x1fButtonData)
{
	category = "Logic Bricks";
	subCategory = "Inputs";
	uiName = "Button";
	iconName = "Add-Ons/Brick_Illogic/icons/button";
	brickFile = "add-ons/brick_illogic/bricks/blb/1x1f_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	isLogicInput = 1;

	numLogicPorts = 1;

	logicPortType[0] = 0;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = "2";
};

function Logic1x1fButtonData::Logic_onInput(%this, %obj, %pos, %norm)
{

}


function Logic1x1fButtonData::Logic_onGateAdded(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 0])
		%obj.setColorFX(3);
	else
		%obj.setColorFX(0);
}

function Logic1x1fButtonData::onPlant(%this, %obj)
{
	%obj.addEvent(true, 0, "onActivate", "Self", "setInputState", true);
	%obj.addEvent(true, 100, "onActivate", "Self", "setInputState", false);
	parent::onPlant(%this, %obj);
}
