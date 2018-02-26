datablock fxDTSBrickData(Logic1x2fDFlipFlop : Logic1x2fORData)
{
	uiName = "D Flip Flop";
	iconName = "";

	logicUIName = "D Flip Flop";
	logicUIDesc = "Q becomes D when C is on, otherwise it holds its state";

	logicPortUIName[0] = "D";
	logicPortUIName[1] = "C";
	logicPortUIName[2] = "Q";
};

function Logic1x2fDFlipFlop::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 1])
		%obj.Logic_SetOutput(2, $LBC::Ports::BrickState[%obj, 0]);
}
