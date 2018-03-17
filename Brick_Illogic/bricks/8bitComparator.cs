datablock fxDTSBrickData(LogicGate__8bitComparator_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/8bitComparator.blb";
	category = "Logic Bricks";
	subCategory = "Chips";
	uiName = "8bit Comparator";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "8bit Comparator";
	logicUIDesc = "Compares A to B";

	numLogicPorts = 19;

	logicPortType[0] = 1;
	logicPortPos[0] = "15 -1 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "A0";

	logicPortType[1] = 1;
	logicPortPos[1] = "13 -1 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "A1";

	logicPortType[2] = 1;
	logicPortPos[2] = "11 -1 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "A2";

	logicPortType[3] = 1;
	logicPortPos[3] = "9 -1 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "A3";

	logicPortType[4] = 1;
	logicPortPos[4] = "7 -1 0";
	logicPortDir[4] = 3;
	logicPortUIName[4] = "A4";

	logicPortType[5] = 1;
	logicPortPos[5] = "5 -1 0";
	logicPortDir[5] = 3;
	logicPortUIName[5] = "A5";

	logicPortType[6] = 1;
	logicPortPos[6] = "3 -1 0";
	logicPortDir[6] = 3;
	logicPortUIName[6] = "A6";

	logicPortType[7] = 1;
	logicPortPos[7] = "1 -1 0";
	logicPortDir[7] = 3;
	logicPortUIName[7] = "A7";

	logicPortType[8] = 1;
	logicPortPos[8] = "-1 -1 0";
	logicPortDir[8] = 3;
	logicPortUIName[8] = "B0";

	logicPortType[9] = 1;
	logicPortPos[9] = "-3 -1 0";
	logicPortDir[9] = 3;
	logicPortUIName[9] = "B1";

	logicPortType[10] = 1;
	logicPortPos[10] = "-5 -1 0";
	logicPortDir[10] = 3;
	logicPortUIName[10] = "B2";

	logicPortType[11] = 1;
	logicPortPos[11] = "-7 -1 0";
	logicPortDir[11] = 3;
	logicPortUIName[11] = "B3";

	logicPortType[12] = 1;
	logicPortPos[12] = "-9 -1 0";
	logicPortDir[12] = 3;
	logicPortUIName[12] = "B4";

	logicPortType[13] = 1;
	logicPortPos[13] = "-11 -1 0";
	logicPortDir[13] = 3;
	logicPortUIName[13] = "B5";

	logicPortType[14] = 1;
	logicPortPos[14] = "-13 -1 0";
	logicPortDir[14] = 3;
	logicPortUIName[14] = "B6";

	logicPortType[15] = 1;
	logicPortPos[15] = "-15 -1 0";
	logicPortDir[15] = 3;
	logicPortUIName[15] = "B7";

	logicPortType[16] = 0;
	logicPortPos[16] = "15 1 0";
	logicPortDir[16] = 1;
	logicPortUIName[16] = ">";

	logicPortType[17] = 0;
	logicPortPos[17] = "13 1 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "=";

	logicPortType[18] = 0;
	logicPortPos[18] = "11 1 0";
	logicPortDir[18] = 1;
	logicPortUIName[18] = "<";
};

function LogicGate__8bitComparator_Data::doLogic(%this, %obj)
{
	%a =
		($LBC::Ports::BrickState[%obj,0]*1)+
		($LBC::Ports::BrickState[%obj,1]*2)+
		($LBC::Ports::BrickState[%obj,2]*4)+
		($LBC::Ports::BrickState[%obj,3]*8)+
		($LBC::Ports::BrickState[%obj,4]*16)+
		($LBC::Ports::BrickState[%obj,5]*32)+
		($LBC::Ports::BrickState[%obj,6]*64)+
		($LBC::Ports::BrickState[%obj,7]*128);

	%b =
		($LBC::Ports::BrickState[%obj,8]*1)+
		($LBC::Ports::BrickState[%obj,9]*2)+
		($LBC::Ports::BrickState[%obj,10]*4)+
		($LBC::Ports::BrickState[%obj,11]*8)+
		($LBC::Ports::BrickState[%obj,12]*16)+
		($LBC::Ports::BrickState[%obj,13]*32)+
		($LBC::Ports::BrickState[%obj,14]*64)+
		($LBC::Ports::BrickState[%obj,15]*128);

	if(%a < %b)
	{
		%obj.Logic_SetOutput(18, 1);
		%obj.Logic_SetOutput(17, 0);
		%obj.Logic_SetOutput(16, 0);
	}
	if(%a == %b)
	{
		%obj.Logic_SetOutput(18, 0);
		%obj.Logic_SetOutput(17, 1);
		%obj.Logic_SetOutput(16, 0);
	}
	if(%a > %b)
	{
		%obj.Logic_SetOutput(18, 0);
		%obj.Logic_SetOutput(17, 0);
		%obj.Logic_SetOutput(16, 1);
	}
}