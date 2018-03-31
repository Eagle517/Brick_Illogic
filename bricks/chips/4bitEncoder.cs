datablock fxDTSBrickData(LogicGate__4bitEncoder_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/4bitEncoder.blb";
	category = "Logic Bricks";
	subCategory = "Chips";
	uiName = "4bit Encoder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "4bit Encoder";
	logicUIDesc = "";

	numLogicPorts = 20;

	logicPortType[0] = 1;
	logicPortPos[0] = "13 -1 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "1";

	logicPortType[1] = 1;
	logicPortPos[1] = "11 -1 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "2";

	logicPortType[2] = 1;
	logicPortPos[2] = "9 -1 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "3";

	logicPortType[3] = 1;
	logicPortPos[3] = "7 -1 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "4";

	logicPortType[4] = 1;
	logicPortPos[4] = "5 -1 0";
	logicPortDir[4] = 3;
	logicPortUIName[4] = "5";

	logicPortType[5] = 1;
	logicPortPos[5] = "3 -1 0";
	logicPortDir[5] = 3;
	logicPortUIName[5] = "6";

	logicPortType[6] = 1;
	logicPortPos[6] = "1 -1 0";
	logicPortDir[6] = 3;
	logicPortUIName[6] = "7";

	logicPortType[7] = 1;
	logicPortPos[7] = "-1 -1 0";
	logicPortDir[7] = 3;
	logicPortUIName[7] = "8";

	logicPortType[8] = 1;
	logicPortPos[8] = "-3 -1 0";
	logicPortDir[8] = 3;
	logicPortUIName[8] = "9";

	logicPortType[9] = 1;
	logicPortPos[9] = "-5 -1 0";
	logicPortDir[9] = 3;
	logicPortUIName[9] = "10";

	logicPortType[10] = 1;
	logicPortPos[10] = "-7 -1 0";
	logicPortDir[10] = 3;
	logicPortUIName[10] = "11";

	logicPortType[11] = 1;
	logicPortPos[11] = "-9 -1 0";
	logicPortDir[11] = 3;
	logicPortUIName[11] = "12";

	logicPortType[12] = 1;
	logicPortPos[12] = "-11 -1 0";
	logicPortDir[12] = 3;
	logicPortUIName[12] = "13";

	logicPortType[13] = 1;
	logicPortPos[13] = "-13 -1 0";
	logicPortDir[13] = 3;
	logicPortUIName[13] = "14";

	logicPortType[14] = 1;
	logicPortPos[14] = "-15 -1 0";
	logicPortDir[14] = 3;
	logicPortUIName[14] = "15";

	logicPortType[15] = 1;
	logicPortPos[15] = "15 -1 0";
	logicPortDir[15] = 2;
	logicPortUIName[15] = "Disable";

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
	logicPortUIName[19] = "Out4";
};

function LogicGate__4bitEncoder_Data::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj,15])
	{
		%obj.Logic_SetOutput(16, 0);
		%obj.Logic_SetOutput(17, 0);
		%obj.Logic_SetOutput(18, 0);
		%obj.Logic_SetOutput(19, 0);
	}
	else
	{
		%obj.val = 0;
		for(%i=0;%i<15;%i++)
			%obj.val |= $LBC::Ports::BrickState[%obj, %i] * (%i+1);

		%obj.Logic_SetOutput(16, %obj.val & 1);
		%obj.Logic_SetOutput(17, %obj.val & 2);
		%obj.Logic_SetOutput(18, %obj.val & 4);
		%obj.Logic_SetOutput(19, %obj.val & 8);
	}
}