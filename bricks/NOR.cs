datablock fxDTSBrickData(Logic1x2fNORData : Logic1x2fORData)
{
	uiName = "1x2f NOR";
	iconName = "Add-Ons/Brick_Illogic/icons/NOR";
	logicUIName = "NOR";
	logicUIDesc = "C is false if A or B are true";
};

function Logic1x2fNORData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, !($LBC::Ports::BrickState[%obj, 0] || $LBC::Ports::BrickState[%obj, 1]));
}

function Logic1x2fNORData::Logic_onGateAdded(%this, %obj)
{
	%obj.Logic_SetOutput(2, !($LBC::Ports::BrickState[%obj, 0] || $LBC::Ports::BrickState[%obj, 1]));
}
