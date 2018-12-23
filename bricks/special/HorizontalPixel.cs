datablock fxDTSBrickData(LogicGate_HorizontalPixel_000_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel000.blb";
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Horizontal Pixel";
	iconName = "";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Horizontal Pixel";
	logicUIDesc = "";

	numLogicPorts = 3;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 1 0";
	logicPortDir[0] = 5;
	logicPortUIName[0] = "R";

	logicPortType[1] = 1;
	logicPortPos[1] = "-1 -1 0";
	logicPortDir[1] = 5;
	logicPortUIName[1] = "G";

	logicPortType[2] = 1;
	logicPortPos[2] = "1 -1 0";
	logicPortDir[2] = 5;
	logicPortUIName[2] = "B";
};

datablock fxDTSBrickData(LogicGate_HorizontalPixel_001_Data : LogicGate_HorizontalPixel_000_Data) { category = ""; subCategory = ""; brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel001.blb"; uiName = "Horizontal Pixel 001"; };
datablock fxDTSBrickData(LogicGate_HorizontalPixel_010_Data : LogicGate_HorizontalPixel_001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel010.blb"; uiName = "Horizontal Pixel 010"; };
datablock fxDTSBrickData(LogicGate_HorizontalPixel_011_Data : LogicGate_HorizontalPixel_001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel011.blb"; uiName = "Horizontal Pixel 011"; };
datablock fxDTSBrickData(LogicGate_HorizontalPixel_100_Data : LogicGate_HorizontalPixel_001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel100.blb"; uiName = "Horizontal Pixel 100"; };
datablock fxDTSBrickData(LogicGate_HorizontalPixel_101_Data : LogicGate_HorizontalPixel_001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel101.blb"; uiName = "Horizontal Pixel 101"; };
datablock fxDTSBrickData(LogicGate_HorizontalPixel_110_Data : LogicGate_HorizontalPixel_001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel110.blb"; uiName = "Horizontal Pixel 110"; };
datablock fxDTSBrickData(LogicGate_HorizontalPixel_111_Data : LogicGate_HorizontalPixel_001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/HPixel111.blb"; uiName = "Horizontal Pixel 111"; };

function LogicGate_HorizontalPixel_000_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_001_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_010_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_011_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_100_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_101_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_110_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_111_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }

function LogicGate_HorizontalPixel_000_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_001_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_010_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_011_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_100_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_101_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_110_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_HorizontalPixel_111_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_HorizontalPixel_"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
