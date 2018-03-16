datablock fxDTSBrickData(Logic1x2fNANDData : Logic1x2fORData)
{
	uiName = "1x2f NAND";
	iconName = "Add-Ons/Brick_Illogic/icons/NAND";
	logicUIName = "NAND";
	logicUIDesc = "C is false if A and B are true";
};

function Logic1x2fNANDData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, !($LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]));
}

function Logic1x2fNANDData::Logic_onGateAdded(%this, %obj)
{
	%obj.Logic_SetOutput(2, !($LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]));
}
