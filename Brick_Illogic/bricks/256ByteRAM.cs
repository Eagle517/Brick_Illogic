datablock fxDTSBrickData(LogicGate__256ByteRAM_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/256ByteRAM.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "256 Byte RAM";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 0;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "256 Byte RAM";
	logicUIDesc = "Reads and writes data";

	numLogicPorts = 27;

	logicPortType[0] = 1;
	logicPortPos[0] = "-2 1 0";
	logicPortDir[0] = 0;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "-2 3 0";
	logicPortDir[1] = 0;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-2 5 0";
	logicPortDir[2] = 0;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "-2 7 0";
	logicPortDir[3] = 0;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 1;
	logicPortPos[4] = "-2 9 0";
	logicPortDir[4] = 0;
	logicPortUIName[4] = "In4";

	logicPortType[5] = 1;
	logicPortPos[5] = "-2 11 0";
	logicPortDir[5] = 0;
	logicPortUIName[5] = "In5";

	logicPortType[6] = 1;
	logicPortPos[6] = "-2 13 0";
	logicPortDir[6] = 0;
	logicPortUIName[6] = "In6";

	logicPortType[7] = 1;
	logicPortPos[7] = "-2 15 0";
	logicPortDir[7] = 0;
	logicPortUIName[7] = "In7";

	logicPortType[8] = 1;
	logicPortPos[8] = "-2 -15 0";
	logicPortDir[8] = 0;
	logicPortUIName[8] = "Addr0";

	logicPortType[9] = 1;
	logicPortPos[9] = "-2 -13 0";
	logicPortDir[9] = 0;
	logicPortUIName[9] = "Addr1";

	logicPortType[10] = 1;
	logicPortPos[10] = "-2 -11 0";
	logicPortDir[10] = 0;
	logicPortUIName[10] = "Addr2";

	logicPortType[11] = 1;
	logicPortPos[11] = "-2 -9 0";
	logicPortDir[11] = 0;
	logicPortUIName[11] = "Addr3";

	logicPortType[12] = 1;
	logicPortPos[12] = "-2 -7 0";
	logicPortDir[12] = 0;
	logicPortUIName[12] = "Addr4";

	logicPortType[13] = 1;
	logicPortPos[13] = "-2 -5 0";
	logicPortDir[13] = 0;
	logicPortUIName[13] = "Addr5";

	logicPortType[14] = 1;
	logicPortPos[14] = "-2 -3 0";
	logicPortDir[14] = 0;
	logicPortUIName[14] = "Addr6";

	logicPortType[15] = 1;
	logicPortPos[15] = "-2 -1 0";
	logicPortDir[15] = 0;
	logicPortUIName[15] = "Addr7";

	logicPortType[16] = 1;
	logicPortPos[16] = "-2 -15 0";
	logicPortDir[16] = 3;
	logicPortUIName[16] = "Write";

	logicPortType[17] = 1;
	logicPortPos[17] = "0 -15 0";
	logicPortDir[17] = 3;
	logicPortUIName[17] = "Read";

	logicPortType[18] = 1;
	logicPortPos[18] = "2 -15 0";
	logicPortDir[18] = 3;
	logicPortUIName[18] = "Clock";

	logicPortType[19] = 0;
	logicPortPos[19] = "2 1 0";
	logicPortDir[19] = 2;
	logicPortUIName[19] = "Out0";

	logicPortType[20] = 0;
	logicPortPos[20] = "2 3 0";
	logicPortDir[20] = 2;
	logicPortUIName[20] = "Out1";

	logicPortType[21] = 0;
	logicPortPos[21] = "2 5 0";
	logicPortDir[21] = 2;
	logicPortUIName[21] = "Out2";

	logicPortType[22] = 0;
	logicPortPos[22] = "2 7 0";
	logicPortDir[22] = 2;
	logicPortUIName[22] = "Out3";

	logicPortType[23] = 0;
	logicPortPos[23] = "2 9 0";
	logicPortDir[23] = 2;
	logicPortUIName[23] = "Out4";

	logicPortType[24] = 0;
	logicPortPos[24] = "2 11 0";
	logicPortDir[24] = 2;
	logicPortUIName[24] = "Out5";

	logicPortType[25] = 0;
	logicPortPos[25] = "2 13 0";
	logicPortDir[25] = 2;
	logicPortUIName[25] = "Out6";

	logicPortType[26] = 0;
	logicPortPos[26] = "2 15 0";
	logicPortDir[26] = 2;
	logicPortUIName[26] = "Out7";
};

function LogicGate__256ByteRAM_Data::doLogic(%this, %obj)
{
	%obj.addr =
		($LBC::Ports::BrickState[%obj, 8]*1)+
		($LBC::Ports::BrickState[%obj, 9]*2)+
		($LBC::Ports::BrickState[%obj, 10]*4)+
		($LBC::Ports::BrickState[%obj, 11]*8)+
		($LBC::Ports::BrickState[%obj, 12]*16)+
		($LBC::Ports::BrickState[%obj, 13]*32)+
		($LBC::Ports::BrickState[%obj, 14]*64)+
		($LBC::Ports::BrickState[%obj, 15]*128);

	if($LBC::Ports::BrickState[%obj,18] && !%obj.clockPrevState)
	{
		%obj.clockPrevState = 1;

		//Write
		if($LBC::Ports::BrickState[%obj,16])
		{
			%obj.dOut[%obj.addr, 0] = $LBC::Ports::BrickState[%obj, 0];
			%obj.dOut[%obj.addr, 1] = $LBC::Ports::BrickState[%obj, 1];
			%obj.dOut[%obj.addr, 2] = $LBC::Ports::BrickState[%obj, 2];
			%obj.dOut[%obj.addr, 3] = $LBC::Ports::BrickState[%obj, 3];
			%obj.dOut[%obj.addr, 4] = $LBC::Ports::BrickState[%obj, 4];
			%obj.dOut[%obj.addr, 5] = $LBC::Ports::BrickState[%obj, 5];
			%obj.dOut[%obj.addr, 6] = $LBC::Ports::BrickState[%obj, 6];
			%obj.dOut[%obj.addr, 7] = $LBC::Ports::BrickState[%obj, 7];
		}

		//Read
		if($LBC::Ports::BrickState[%obj,17])
		{
			%obj.Logic_SetOutput(19, %obj.dOut[%obj.addr, 0]);
			%obj.Logic_SetOutput(20, %obj.dOut[%obj.addr, 1]);
			%obj.Logic_SetOutput(21, %obj.dOut[%obj.addr, 2]);
			%obj.Logic_SetOutput(22, %obj.dOut[%obj.addr, 3]);
			%obj.Logic_SetOutput(23, %obj.dOut[%obj.addr, 4]);
			%obj.Logic_SetOutput(24, %obj.dOut[%obj.addr, 5]);
			%obj.Logic_SetOutput(25, %obj.dOut[%obj.addr, 6]);
			%obj.Logic_SetOutput(26, %obj.dOut[%obj.addr, 7]);
		}
	}
	else if(!$LBC::Ports::BrickState[%obj,18] && %obj.clockPrevState)
	{
		%obj.clockPrevState = 0;
	}
}

function LogicGate__256ByteRAM_Data::Logic_onGateAdded(%this, %obj)
{
	%obj.clockPrevState = 0;
}