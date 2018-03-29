datablock fxDTSBrickData(LogicGate__16bit128KiBRAM_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/16bit128KiBRAM.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "16bit 128KiB RAM";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "16bit 128KiB RAM";
	logicUIDesc = "Reads and writes data to memory";

	numLogicPorts = 50;

	logicPortType[0] = 1;
	logicPortPos[0] = "15 -15 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "13 -15 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "11 -15 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "9 -15 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 1;
	logicPortPos[4] = "7 -15 0";
	logicPortDir[4] = 3;
	logicPortUIName[4] = "In4";

	logicPortType[5] = 1;
	logicPortPos[5] = "5 -15 0";
	logicPortDir[5] = 3;
	logicPortUIName[5] = "In5";

	logicPortType[6] = 1;
	logicPortPos[6] = "3 -15 0";
	logicPortDir[6] = 3;
	logicPortUIName[6] = "In6";

	logicPortType[7] = 1;
	logicPortPos[7] = "1 -15 0";
	logicPortDir[7] = 3;
	logicPortUIName[7] = "In7";

	logicPortType[8] = 1;
	logicPortPos[8] = "-1 -15 0";
	logicPortDir[8] = 3;
	logicPortUIName[8] = "In8";

	logicPortType[9] = 1;
	logicPortPos[9] = "-3 -15 0";
	logicPortDir[9] = 3;
	logicPortUIName[9] = "In9";

	logicPortType[10] = 1;
	logicPortPos[10] = "-5 -15 0";
	logicPortDir[10] = 3;
	logicPortUIName[10] = "In10";

	logicPortType[11] = 1;
	logicPortPos[11] = "-7 -15 0";
	logicPortDir[11] = 3;
	logicPortUIName[11] = "In11";

	logicPortType[12] = 1;
	logicPortPos[12] = "-9 -15 0";
	logicPortDir[12] = 3;
	logicPortUIName[12] = "In12";

	logicPortType[13] = 1;
	logicPortPos[13] = "-11 -15 0";
	logicPortDir[13] = 3;
	logicPortUIName[13] = "In13";

	logicPortType[14] = 1;
	logicPortPos[14] = "-13 -15 0";
	logicPortDir[14] = 3;
	logicPortUIName[14] = "In14";

	logicPortType[15] = 1;
	logicPortPos[15] = "-15 -15 0";
	logicPortDir[15] = 3;
	logicPortUIName[15] = "In15";

	logicPortType[16] = 1;
	logicPortPos[16] = "-15 -15 0";
	logicPortDir[16] = 0;
	logicPortUIName[16] = "Addr0";

	logicPortType[17] = 1;
	logicPortPos[17] = "-15 -13 0";
	logicPortDir[17] = 0;
	logicPortUIName[17] = "Addr1";

	logicPortType[18] = 1;
	logicPortPos[18] = "-15 -11 0";
	logicPortDir[18] = 0;
	logicPortUIName[18] = "Addr2";

	logicPortType[19] = 1;
	logicPortPos[19] = "-15 -9 0";
	logicPortDir[19] = 0;
	logicPortUIName[19] = "Addr3";

	logicPortType[20] = 1;
	logicPortPos[20] = "-15 -7 0";
	logicPortDir[20] = 0;
	logicPortUIName[20] = "Addr4";

	logicPortType[21] = 1;
	logicPortPos[21] = "-15 -5 0";
	logicPortDir[21] = 0;
	logicPortUIName[21] = "Addr5";

	logicPortType[22] = 1;
	logicPortPos[22] = "-15 -3 0";
	logicPortDir[22] = 0;
	logicPortUIName[22] = "Addr6";

	logicPortType[23] = 1;
	logicPortPos[23] = "-15 -1 0";
	logicPortDir[23] = 0;
	logicPortUIName[23] = "Addr7";

	logicPortType[24] = 1;
	logicPortPos[24] = "-15 1 0";
	logicPortDir[24] = 0;
	logicPortUIName[24] = "Addr8";

	logicPortType[25] = 1;
	logicPortPos[25] = "-15 3 0";
	logicPortDir[25] = 0;
	logicPortUIName[25] = "Addr9";

	logicPortType[26] = 1;
	logicPortPos[26] = "-15 5 0";
	logicPortDir[26] = 0;
	logicPortUIName[26] = "Addr10";

	logicPortType[27] = 1;
	logicPortPos[27] = "-15 7 0";
	logicPortDir[27] = 0;
	logicPortUIName[27] = "Addr11";

	logicPortType[28] = 1;
	logicPortPos[28] = "-15 9 0";
	logicPortDir[28] = 0;
	logicPortUIName[28] = "Addr12";

	logicPortType[29] = 1;
	logicPortPos[29] = "-15 11 0";
	logicPortDir[29] = 0;
	logicPortUIName[29] = "Addr13";

	logicPortType[30] = 1;
	logicPortPos[30] = "-15 13 0";
	logicPortDir[30] = 0;
	logicPortUIName[30] = "Addr14";

	logicPortType[31] = 1;
	logicPortPos[31] = "-15 15 0";
	logicPortDir[31] = 0;
	logicPortUIName[31] = "Addr15";

	logicPortType[32] = 1;
	logicPortPos[32] = "15 -15 0";
	logicPortDir[32] = 2;
	logicPortUIName[32] = "Read";

	logicPortType[33] = 1;
	logicPortPos[33] = "15 15 0";
	logicPortDir[33] = 2;
	logicPortUIName[33] = "Write(Clock)";

	logicPortType[34] = 0;
	logicPortPos[34] = "15 15 0";
	logicPortDir[34] = 1;
	logicPortUIName[34] = "Out0";

	logicPortType[35] = 0;
	logicPortPos[35] = "13 15 0";
	logicPortDir[35] = 1;
	logicPortUIName[35] = "Out1";

	logicPortType[36] = 0;
	logicPortPos[36] = "11 15 0";
	logicPortDir[36] = 1;
	logicPortUIName[36] = "Out2";

	logicPortType[37] = 0;
	logicPortPos[37] = "9 15 0";
	logicPortDir[37] = 1;
	logicPortUIName[37] = "Out3";

	logicPortType[38] = 0;
	logicPortPos[38] = "7 15 0";
	logicPortDir[38] = 1;
	logicPortUIName[38] = "Out4";

	logicPortType[39] = 0;
	logicPortPos[39] = "5 15 0";
	logicPortDir[39] = 1;
	logicPortUIName[39] = "Out5";

	logicPortType[40] = 0;
	logicPortPos[40] = "3 15 0";
	logicPortDir[40] = 1;
	logicPortUIName[40] = "Out6";

	logicPortType[41] = 0;
	logicPortPos[41] = "1 15 0";
	logicPortDir[41] = 1;
	logicPortUIName[41] = "Out7";

	logicPortType[42] = 0;
	logicPortPos[42] = "-1 15 0";
	logicPortDir[42] = 1;
	logicPortUIName[42] = "Out8";

	logicPortType[43] = 0;
	logicPortPos[43] = "-3 15 0";
	logicPortDir[43] = 1;
	logicPortUIName[43] = "Out9";

	logicPortType[44] = 0;
	logicPortPos[44] = "-5 15 0";
	logicPortDir[44] = 1;
	logicPortUIName[44] = "Out10";

	logicPortType[45] = 0;
	logicPortPos[45] = "-7 15 0";
	logicPortDir[45] = 1;
	logicPortUIName[45] = "Out11";

	logicPortType[46] = 0;
	logicPortPos[46] = "-9 15 0";
	logicPortDir[46] = 1;
	logicPortUIName[46] = "Out12";

	logicPortType[47] = 0;
	logicPortPos[47] = "-11 15 0";
	logicPortDir[47] = 1;
	logicPortUIName[47] = "Out13";

	logicPortType[48] = 0;
	logicPortPos[48] = "-13 15 0";
	logicPortDir[48] = 1;
	logicPortUIName[48] = "Out14";

	logicPortType[49] = 0;
	logicPortPos[49] = "-15 15 0";
	logicPortDir[49] = 1;
	logicPortUIName[49] = "Out15";
};

function LogicGate__16bit128KiBRAM_Data::doLogic(%this, %obj)
{
	%obj.addr = 0;
	for(%i=0;%i<16;%i++)
		%obj.addr += $LBC::Ports::BrickState[%obj, %i+16] * (1 << %i);

	//Write clock
	if(!$LBC::Ports::LastBrickState[%obj, 33] && $LBC::Ports::BrickState[%obj, 33])
		for(%i=0;%i<16;%i++)
			%obj.data[%obj.addr, %i] = $LBC::Ports::BrickState[%obj, %i];

	//Read
	if($LBC::Ports::BrickState[%obj, 32])
		for(%i=0;%i<16;%i++)
			%obj.Logic_SetOutput(%i+34, %obj.dOut[%obj.addr, %i]);
}