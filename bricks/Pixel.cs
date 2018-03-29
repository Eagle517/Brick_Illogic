datablock fxDTSBrickData(LogicGate_Pixel000_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel000.blb";
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Pixel";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Pixel";
	logicUIDesc = "";

	numLogicPorts = 3;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 0 -2";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "R";

	logicPortType[1] = 1;
	logicPortPos[1] = "-1 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "G";

	logicPortType[2] = 1;
	logicPortPos[2] = "-1 0 2";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "B";
};

datablock fxDTSBrickData(LogicGate_Pixel001_Data : LogicGate_Pixel000_Data) { category = ""; subCategory = ""; brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel001.blb"; uiName = "Pixel 001"; };
datablock fxDTSBrickData(LogicGate_Pixel010_Data : LogicGate_Pixel001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel010.blb"; uiName = "Pixel 010"; };
datablock fxDTSBrickData(LogicGate_Pixel011_Data : LogicGate_Pixel001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel011.blb"; uiName = "Pixel 011"; };
datablock fxDTSBrickData(LogicGate_Pixel100_Data : LogicGate_Pixel001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel100.blb"; uiName = "Pixel 100"; };
datablock fxDTSBrickData(LogicGate_Pixel101_Data : LogicGate_Pixel001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel101.blb"; uiName = "Pixel 101"; };
datablock fxDTSBrickData(LogicGate_Pixel110_Data : LogicGate_Pixel001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel110.blb"; uiName = "Pixel 110"; };
datablock fxDTSBrickData(LogicGate_Pixel111_Data : LogicGate_Pixel001_Data) { brickFile = "Add-Ons/Brick_Illogic/bricks/blb/pixels/Pixel111.blb"; uiName = "Pixel 111"; };

function LogicGate_Pixel000_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel001_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel010_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel011_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel100_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel101_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel110_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel111_Data::doLogic(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }

function LogicGate_Pixel000_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel001_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel010_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel011_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel100_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel101_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel110_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
function LogicGate_Pixel111_Data::Logic_onGateAdded(%this, %obj) { %obj.setDatablock("LogicGate_Pixel"@$LBC::Ports::BrickState[%obj, 0]+0 @ $LBC::Ports::BrickState[%obj, 1]+0 @ $LBC::Ports::BrickState[%obj, 2]+0@"_Data"); }
