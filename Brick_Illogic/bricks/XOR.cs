datablock fxDTSBrickData(Logic1x2fXORData : Logic1x2fORData)
{
	uiName = "1x2f XOR";
	iconName = "Add-Ons/Brick_Illogic/icons/XOR";
	logicUIName = "XOR";
	logicUIDesc = "C is true if A or B are true but false if A and B are true";
};

function Logic1x2fXORData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, ($LBC::Ports::BrickState[%obj, 0] || $LBC::Ports::BrickState[%obj, 1]) && !($LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]));
}
