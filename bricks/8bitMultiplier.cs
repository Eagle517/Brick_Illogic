datablock fxDTSBrickData(LogicGate__8bitMultiplier_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/8bitMultiplier.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "8bit Multiplier";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "8bit Multiplier";
	logicUIDesc = "Multiplies A by B";

	numLogicPorts = 32;

	logicPortType[0] = 1;
	logicPortPos[0] = "-1 -1 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "A0";

	logicPortType[1] = 1;
	logicPortPos[1] = "-3 -1 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "A1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-5 -1 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "A2";

	logicPortType[3] = 1;
	logicPortPos[3] = "-7 -1 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "A3";

	logicPortType[4] = 1;
	logicPortPos[4] = "-9 -1 0";
	logicPortDir[4] = 3;
	logicPortUIName[4] = "A4";

	logicPortType[5] = 1;
	logicPortPos[5] = "-11 -1 0";
	logicPortDir[5] = 3;
	logicPortUIName[5] = "A5";

	logicPortType[6] = 1;
	logicPortPos[6] = "-13 -1 0";
	logicPortDir[6] = 3;
	logicPortUIName[6] = "A6";

	logicPortType[7] = 1;
	logicPortPos[7] = "-15 -1 0";
	logicPortDir[7] = 3;
	logicPortUIName[7] = "A7";

	logicPortType[8] = 1;
	logicPortPos[8] = "15 -1 0";
	logicPortDir[8] = 3;
	logicPortUIName[8] = "B0";

	logicPortType[9] = 1;
	logicPortPos[9] = "13 -1 0";
	logicPortDir[9] = 3;
	logicPortUIName[9] = "B1";

	logicPortType[10] = 1;
	logicPortPos[10] = "11 -1 0";
	logicPortDir[10] = 3;
	logicPortUIName[10] = "B2";

	logicPortType[11] = 1;
	logicPortPos[11] = "9 -1 0";
	logicPortDir[11] = 3;
	logicPortUIName[11] = "B3";

	logicPortType[12] = 1;
	logicPortPos[12] = "7 -1 0";
	logicPortDir[12] = 3;
	logicPortUIName[12] = "B4";

	logicPortType[13] = 1;
	logicPortPos[13] = "5 -1 0";
	logicPortDir[13] = 3;
	logicPortUIName[13] = "B5";

	logicPortType[14] = 1;
	logicPortPos[14] = "3 -1 0";
	logicPortDir[14] = 3;
	logicPortUIName[14] = "B6";

	logicPortType[15] = 1;
	logicPortPos[15] = "1 -1 0";
	logicPortDir[15] = 3;
	logicPortUIName[15] = "B7";

	logicPortType[16] = 0;
	logicPortPos[16] = "15 1 0";
	logicPortDir[16] = 1;
	logicPortUIName[16] = "Out0";

	logicPortType[17] = 0;
	logicPortPos[17] = "13 1 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "Out1";

	logicPortType[18] = 0;
	logicPortPos[18] = "11 1 0";
	logicPortDir[18] = 1;
	logicPortUIName[18] = "Out2";

	logicPortType[19] = 0;
	logicPortPos[19] = "9 1 0";
	logicPortDir[19] = 1;
	logicPortUIName[19] = "Out3";

	logicPortType[20] = 0;
	logicPortPos[20] = "7 1 0";
	logicPortDir[20] = 1;
	logicPortUIName[20] = "Out4";

	logicPortType[21] = 0;
	logicPortPos[21] = "5 1 0";
	logicPortDir[21] = 1;
	logicPortUIName[21] = "Out5";

	logicPortType[22] = 0;
	logicPortPos[22] = "3 1 0";
	logicPortDir[22] = 1;
	logicPortUIName[22] = "Out6";

	logicPortType[23] = 0;
	logicPortPos[23] = "1 1 0";
	logicPortDir[23] = 1;
	logicPortUIName[23] = "Out7";

	logicPortType[24] = 0;
	logicPortPos[24] = "-1 1 0";
	logicPortDir[24] = 1;
	logicPortUIName[24] = "Out8";

	logicPortType[25] = 0;
	logicPortPos[25] = "-3 1 0";
	logicPortDir[25] = 1;
	logicPortUIName[25] = "Out9";

	logicPortType[26] = 0;
	logicPortPos[26] = "-5 1 0";
	logicPortDir[26] = 1;
	logicPortUIName[26] = "Out10";

	logicPortType[27] = 0;
	logicPortPos[27] = "-7 1 0";
	logicPortDir[27] = 1;
	logicPortUIName[27] = "Out11";

	logicPortType[28] = 0;
	logicPortPos[28] = "-9 1 0";
	logicPortDir[28] = 1;
	logicPortUIName[28] = "Out12";

	logicPortType[29] = 0;
	logicPortPos[29] = "-11 1 0";
	logicPortDir[29] = 1;
	logicPortUIName[29] = "Out13";

	logicPortType[30] = 0;
	logicPortPos[30] = "-13 1 0";
	logicPortDir[30] = 1;
	logicPortUIName[30] = "Out14";

	logicPortType[31] = 0;
	logicPortPos[31] = "-15 1 0";
	logicPortDir[31] = 1;
	logicPortUIName[31] = "Out15";
};

function LogicGate__8bitMultiplier_Data::doLogic(%this, %obj)
{
	%a =
		($LBC::Ports::BrickState[%obj, 0]*1)+
		($LBC::Ports::BrickState[%obj, 1]*2)+
		($LBC::Ports::BrickState[%obj, 2]*4)+
		($LBC::Ports::BrickState[%obj, 3]*8)+
		($LBC::Ports::BrickState[%obj, 4]*16)+
		($LBC::Ports::BrickState[%obj, 5]*32)+
		($LBC::Ports::BrickState[%obj, 6]*64)+
		($LBC::Ports::BrickState[%obj, 7]*128);

	%b =
		($LBC::Ports::BrickState[%obj, 8]*1)+
		($LBC::Ports::BrickState[%obj, 9]*2)+
		($LBC::Ports::BrickState[%obj, 10]*4)+
		($LBC::Ports::BrickState[%obj, 11]*8)+
		($LBC::Ports::BrickState[%obj, 12]*16)+
		($LBC::Ports::BrickState[%obj, 13]*32)+
		($LBC::Ports::BrickState[%obj, 14]*64)+
		($LBC::Ports::BrickState[%obj, 15]*128);

	%finalValue = %a * %b;

	%obj.Logic_SetOutput(16, %finalValue & 1);
	%obj.Logic_SetOutput(17, %finalValue & 2);
	%obj.Logic_SetOutput(18, %finalValue & 4);
	%obj.Logic_SetOutput(19, %finalValue & 8);
	%obj.Logic_SetOutput(20, %finalValue & 16);
	%obj.Logic_SetOutput(21, %finalValue & 32);
	%obj.Logic_SetOutput(22, %finalValue & 64);
	%obj.Logic_SetOutput(23, %finalValue & 128);
	%obj.Logic_SetOutput(24, %finalValue & 256);
	%obj.Logic_SetOutput(25, %finalValue & 512);
	%obj.Logic_SetOutput(26, %finalValue & 1024);
	%obj.Logic_SetOutput(27, %finalValue & 2048);
	%obj.Logic_SetOutput(28, %finalValue & 4096);
	%obj.Logic_SetOutput(29, %finalValue & 8192);
	%obj.Logic_SetOutput(30, %finalValue & 16384);
	%obj.Logic_SetOutput(31, %finalValue & 32768);
}