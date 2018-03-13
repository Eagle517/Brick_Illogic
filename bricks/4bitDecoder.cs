datablock fxDTSBrickData(LogicGate__4bitDecoder_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/4bitDecoder.blb";
	category = "Logic Bricks";
	subCategory = "Chips";
	uiName = "4bit Decoder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "4bit Decoder";
	logicUIDesc = "";

	numLogicPorts = 21;

	logicPortType[0] = 1;
	logicPortPos[0] = "15 -1 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "13 -1 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "11 -1 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "9 -1 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 1;
	logicPortPos[4] = "15 -1 0";
	logicPortDir[4] = 2;
	logicPortUIName[4] = "Disable";

	logicPortType[5] = 0;
	logicPortPos[5] = "-15 1 0";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "15";

	logicPortType[6] = 0;
	logicPortPos[6] = "-13 1 0";
	logicPortDir[6] = 1;
	logicPortUIName[6] = "14";

	logicPortType[7] = 0;
	logicPortPos[7] = "-11 1 0";
	logicPortDir[7] = 1;
	logicPortUIName[7] = "13";

	logicPortType[8] = 0;
	logicPortPos[8] = "-9 1 0";
	logicPortDir[8] = 1;
	logicPortUIName[8] = "12";

	logicPortType[9] = 0;
	logicPortPos[9] = "-7 1 0";
	logicPortDir[9] = 1;
	logicPortUIName[9] = "11";

	logicPortType[10] = 0;
	logicPortPos[10] = "-5 1 0";
	logicPortDir[10] = 1;
	logicPortUIName[10] = "10";

	logicPortType[11] = 0;
	logicPortPos[11] = "-3 1 0";
	logicPortDir[11] = 1;
	logicPortUIName[11] = "9";

	logicPortType[12] = 0;
	logicPortPos[12] = "-1 1 0";
	logicPortDir[12] = 1;
	logicPortUIName[12] = "8";

	logicPortType[13] = 0;
	logicPortPos[13] = "1 1 0";
	logicPortDir[13] = 1;
	logicPortUIName[13] = "7";

	logicPortType[14] = 0;
	logicPortPos[14] = "3 1 0";
	logicPortDir[14] = 1;
	logicPortUIName[14] = "6";

	logicPortType[15] = 0;
	logicPortPos[15] = "5 1 0";
	logicPortDir[15] = 1;
	logicPortUIName[15] = "5";

	logicPortType[16] = 0;
	logicPortPos[16] = "7 1 0";
	logicPortDir[16] = 1;
	logicPortUIName[16] = "4";

	logicPortType[17] = 0;
	logicPortPos[17] = "9 1 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "3";

	logicPortType[18] = 0;
	logicPortPos[18] = "11 1 0";
	logicPortDir[18] = 1;
	logicPortUIName[18] = "2";

	logicPortType[19] = 0;
	logicPortPos[19] = "13 1 0";
	logicPortDir[19] = 1;
	logicPortUIName[19] = "1";

	logicPortType[20] = 0;
	logicPortPos[20] = "15 1 0";
	logicPortDir[20] = 1;
	logicPortUIName[20] = "0";
};

function LogicGate__4bitDecoder_Data::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj,4])
	{
		for(%i=20;%i>4;%i--)
			%obj.Logic_SetOutput(%i, 0);
	}
	else
	{
		
		%obj.val =
			($LBC::Ports::BrickState[%obj,0]*1)+
			($LBC::Ports::BrickState[%obj,1]*2)+
			($LBC::Ports::BrickState[%obj,2]*4)+
			($LBC::Ports::BrickState[%obj,3]*8);

		%obj.Logic_SetOutput(20, %obj.val == 0);
		%obj.Logic_SetOutput(19, %obj.val == 1);
		%obj.Logic_SetOutput(18, %obj.val == 2);
		%obj.Logic_SetOutput(17, %obj.val == 3);
		%obj.Logic_SetOutput(16, %obj.val == 4);
		%obj.Logic_SetOutput(15, %obj.val == 5);
		%obj.Logic_SetOutput(14, %obj.val == 6);
		%obj.Logic_SetOutput(13, %obj.val == 7);
		%obj.Logic_SetOutput(12, %obj.val == 8);
		%obj.Logic_SetOutput(11, %obj.val == 9);
		%obj.Logic_SetOutput(10, %obj.val == 10);
		%obj.Logic_SetOutput(9, %obj.val == 11);
		%obj.Logic_SetOutput(8, %obj.val == 12);
		%obj.Logic_SetOutput(7, %obj.val == 13);
		%obj.Logic_SetOutput(6, %obj.val == 14);
		%obj.Logic_SetOutput(5, %obj.val == 15);
	}	
}

function LogicGate__4bitDecoder_Data::Logic_onGateAdded(%this, %obj)
{
	%obj.val = 0;
}


