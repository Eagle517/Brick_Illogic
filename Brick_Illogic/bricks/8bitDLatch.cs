datablock fxDTSBrickData(LogicGate__8bitDLatch_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/8bitDLatch.blb";
	category = "Logic Bricks";
	subCategory = "Memory";
	uiName = "8bit D Latch";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "8bit D Latch";
	logicUIDesc = "Output is set while clock is high";

	numLogicPorts = 18;

	logicPortType[0] = 1;
	logicPortPos[0] = "7 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "5 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "3 0 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "1 0 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 1;
	logicPortPos[4] = "-1 0 0";
	logicPortDir[4] = 3;
	logicPortUIName[4] = "In4";

	logicPortType[5] = 1;
	logicPortPos[5] = "-3 0 0";
	logicPortDir[5] = 3;
	logicPortUIName[5] = "In5";

	logicPortType[6] = 1;
	logicPortPos[6] = "-5 0 0";
	logicPortDir[6] = 3;
	logicPortUIName[6] = "In6";

	logicPortType[7] = 1;
	logicPortPos[7] = "-7 0 0";
	logicPortDir[7] = 3;
	logicPortUIName[7] = "In7";

	logicPortType[8] = 1;
	logicPortPos[8] = "7 0 0";
	logicPortDir[8] = 2;
	logicPortUIName[8] = "Clock-In";

	logicPortType[9] = 0;
	logicPortPos[9] = "-7 0 0";
	logicPortDir[9] = 0;
	logicPortUIName[9] = "Clock-Out";

	logicPortType[10] = 0;
	logicPortPos[10] = "7 0 0";
	logicPortDir[10] = 1;
	logicPortUIName[10] = "Out0";

	logicPortType[11] = 0;
	logicPortPos[11] = "5 0 0";
	logicPortDir[11] = 1;
	logicPortUIName[11] = "Out1";

	logicPortType[12] = 0;
	logicPortPos[12] = "3 0 0";
	logicPortDir[12] = 1;
	logicPortUIName[12] = "Out2";

	logicPortType[13] = 0;
	logicPortPos[13] = "1 0 0";
	logicPortDir[13] = 1;
	logicPortUIName[13] = "Out3";

	logicPortType[14] = 0;
	logicPortPos[14] = "-1 0 0";
	logicPortDir[14] = 1;
	logicPortUIName[14] = "Out4";

	logicPortType[15] = 0;
	logicPortPos[15] = "-3 0 0";
	logicPortDir[15] = 1;
	logicPortUIName[15] = "Out5";

	logicPortType[16] = 0;
	logicPortPos[16] = "-5 0 0";
	logicPortDir[16] = 1;
	logicPortUIName[16] = "Out6";

	logicPortType[17] = 0;
	logicPortPos[17] = "-7 0 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "Out7";
};

function LogicGate__8bitDLatch_Data::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(9, $LBC::Ports::BrickState[%obj, 8]);
	if($LBC::Ports::BrickState[%obj, 8])
	{
		for(%i=0;%i<8;%i++)
		{
			%obj.Logic_SetOutput(%i + 10, $LBC::Ports::BrickState[%obj, %i]);
		}
	}
}