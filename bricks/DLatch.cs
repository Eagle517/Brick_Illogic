datablock fxDTSBrickData(Logic1x2fDLatch : Logic1x2fORData)
{
	subCategory = "Memory";
	uiName = "D Latch";
	iconName = "Add-Ons/Brick_Illogic/bricks/blb/DLatch.blb";

	logicUIName = "D Latch";
	logicUIDesc = "Q becomes D when C is on, otherwise it holds its state";

	logicPortUIName[0] = "C";
	logicPortUIName[1] = "D";
	logicPortUIName[2] = "Q";
};

function Logic1x2fDLatch::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 0])
		%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 1]);
}
