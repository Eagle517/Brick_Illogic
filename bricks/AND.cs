datablock fxDTSBrickData(Logic1x2fANDData : Logic1x2fORData)
{
	uiName = "1x2f AND";
	logicUIName = "AND";
	logicUIDesc = "C is true if A and B are true";
};

datablock fxDTSBrickData(Logic1x2fANDLData : Logic1x2fANDData)
{
	brickFile = "add-ons/brick_illogic/bricks/blb/1x2f_2i_1oL.blb";
	uiName = "AND Left";
	logicPortPos[2] = "0 1 0";
};

function Logic1x2fANDData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]);
}

function Logic1x2fANDLData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 1]);
}
