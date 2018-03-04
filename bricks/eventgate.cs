datablock fxDTSBrickData(Logic1x1fEventGateData : Logic1x1fButtonData)
{
	iconName = "";
	uiName = "Event Gate";
};

function Logic1x1fEventGateData::Logic_onInput(%this, %obj, %pos, %norm)
{
	if(isObject(%client = %obj.client))
		%client.centerPrint("<font:consolas:20>\c6Event Gate\n\c6Lets you use setInputState on it.", 3);
}
