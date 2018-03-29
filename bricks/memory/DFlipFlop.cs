datablock fxDTSBrickData(Logic1x2fDFlipFlop : Logic1x2fORData)
{
	subCategory = "Memory";
	uiName = "D Flip Flop";
	iconName = "Add-Ons/Brick_Illogic/bricks/blb/DLatch.blb";

	logicUIName = "D Flip Flop";
	logicUIDesc = "Q becomes D when C rises";

	logicPortUIName[0] = "C";
	logicPortUIName[1] = "D";
	logicPortUIName[2] = "Q";
};

function Logic1x2fDFlipFlop::doLogic(%this, %obj)
{
	if(!$LBC::Ports::LastBrickState[%obj, 0] && $LBC::Ports::BrickState[%obj, 0])
		%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 1]);
}
