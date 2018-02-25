datablock fxDTSBrickData(Logic1x2fANDData : Logic1x2fORData)
{
	uiName = "1x2f AND";
	logicUIName = "AND";
	logicUIDesc = "C is true if A and B are true";
};

function Logic1x2fANDData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]);
}
