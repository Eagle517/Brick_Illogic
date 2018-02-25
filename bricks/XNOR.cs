datablock fxDTSBrickData(Logic1x2fXNORData : Logic1x2fORData)
{
	uiName = "1x2f XNOR";
	iconName = "Add-Ons/Brick_Illogic/icons/XNOR";
	logicUIName = "XNOR";
	logicUIDesc = "C is true if A and B are both true or both false";
};

function Logic1x2fXNORData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, !(($LBC::Ports::BrickState[%obj, 0] || $LBC::Ports::BrickState[%obj, 1]) && !($LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1])));
}

function Logic1x2fXNORData::Logic_onGateAdded(%this, %obj)
{
	%obj.Logic_SetOutput(2, !(($LBC::Ports::BrickState[%obj, 0] || $LBC::Ports::BrickState[%obj, 1]) && !($LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1])));
}
