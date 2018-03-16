datablock fxDTSBrickData(LogicGate__8bitAdder_Data)
{
	brickFile = "config/server/IllogicGateMaker/8bitAdder.blb";
	category = "Logic Bricks";
	subCategory = "Gatemaker";
	uiName = "8bit Adder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "8bit Adder";
	logicUIDesc = "";

	numLogicPorts = 26;

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

	logicPortType[16] = 1;
	logicPortPos[16] = "15 -1 0";
	logicPortDir[16] = 2;
	logicPortUIName[16] = "Carry In";

	logicPortType[17] = 0;
	logicPortPos[17] = "15 1 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "Sum0";

	logicPortType[18] = 0;
	logicPortPos[18] = "13 1 0";
	logicPortDir[18] = 1;
	logicPortUIName[18] = "Sum1";

	logicPortType[19] = 0;
	logicPortPos[19] = "11 1 0";
	logicPortDir[19] = 1;
	logicPortUIName[19] = "Sum2";

	logicPortType[20] = 0;
	logicPortPos[20] = "9 1 0";
	logicPortDir[20] = 1;
	logicPortUIName[20] = "Sum3";

	logicPortType[21] = 0;
	logicPortPos[21] = "7 1 0";
	logicPortDir[21] = 1;
	logicPortUIName[21] = "Sum4";

	logicPortType[22] = 0;
	logicPortPos[22] = "5 1 0";
	logicPortDir[22] = 1;
	logicPortUIName[22] = "Sum5";

	logicPortType[23] = 0;
	logicPortPos[23] = "3 1 0";
	logicPortDir[23] = 1;
	logicPortUIName[23] = "Sum6";

	logicPortType[24] = 0;
	logicPortPos[24] = "1 1 0";
	logicPortDir[24] = 1;
	logicPortUIName[24] = "Sum7";

	logicPortType[25] = 0;
	logicPortPos[25] = "-15 -1 0";
	logicPortDir[25] = 0;
	logicPortUIName[25] = "Carry Out";
};

function LogicGate__8bitAdder_Data::doLogic(%this, %obj)
{
	%A =
		($LBC::Ports::BrickState[%obj,0]*1)+
		($LBC::Ports::BrickState[%obj,1]*2)+
		($LBC::Ports::BrickState[%obj,2]*4)+
		($LBC::Ports::BrickState[%obj,3]*8);
		($LBC::Ports::BrickState[%obj,4]*16)+
		($LBC::Ports::BrickState[%obj,5]*32)+
		($LBC::Ports::BrickState[%obj,6]*64)+
		($LBC::Ports::BrickState[%obj,7]*128);

	%B =
		($LBC::Ports::BrickState[%obj,8]*1)+
		($LBC::Ports::BrickState[%obj,9]*2)+
		($LBC::Ports::BrickState[%obj,10]*4)+
		($LBC::Ports::BrickState[%obj,11]*8);
		($LBC::Ports::BrickState[%obj,12]*16)+
		($LBC::Ports::BrickState[%obj,13]*32)+
		($LBC::Ports::BrickState[%obj,14]*64)+
		($LBC::Ports::BrickState[%obj,15]*128);

	%sum = %A + %B + ($LBC::Ports::BrickState[%obj,16]*1);

	%obj.Logic_SetOutput(17, %sum & 1);
	%obj.Logic_SetOutput(18, %sum & 2);
	%obj.Logic_SetOutput(19, %sum & 4);
	%obj.Logic_SetOutput(20, %sum & 8);
	%obj.Logic_SetOutput(21, %sum & 16);
	%obj.Logic_SetOutput(22, %sum & 32);
	%obj.Logic_SetOutput(23, %sum & 64);
	%obj.Logic_SetOutput(24, %sum & 128);
	%obj.Logic_SetOutput(25, %sum & 256);
}