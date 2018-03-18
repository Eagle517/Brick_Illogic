datablock fxDTSBrickData(LogicGate_Shifter_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/Shifter.blb";
	category = "Logic Bricks";
	subCategory = "Chips";
	uiName = "Shifter";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Shifter";
	logicUIDesc = "";

	numLogicPorts = 18;

	logicPortType[0] = 1;
	logicPortPos[0] = "3 -3 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "1 -3 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-1 -3 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "-3 -3 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 0;
	logicPortPos[4] = "3 3 0";
	logicPortDir[4] = 1;
	logicPortUIName[4] = "Out0";

	logicPortType[5] = 0;
	logicPortPos[5] = "1 3 0";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "Out1";

	logicPortType[6] = 0;
	logicPortPos[6] = "-1 3 0";
	logicPortDir[6] = 1;
	logicPortUIName[6] = "Out2";

	logicPortType[7] = 0;
	logicPortPos[7] = "-3 3 0";
	logicPortDir[7] = 1;
	logicPortUIName[7] = "Out3";

	logicPortType[8] = 1;
	logicPortPos[8] = "3 0 0";
	logicPortDir[8] = 2;
	logicPortUIName[8] = "Clock-In";

	logicPortType[9] = 0;
	logicPortPos[9] = "-3 0 0";
	logicPortDir[9] = 0;
	logicPortUIName[9] = "Clock-Out";

	logicPortType[10] = 1;
	logicPortPos[10] = "3 -2 0";
	logicPortDir[10] = 2;
	logicPortUIName[10] = "Dir-In";

	logicPortType[11] = 0;
	logicPortPos[11] = "-3 -2 0";
	logicPortDir[11] = 0;
	logicPortUIName[11] = "Dir-Out";

	logicPortType[12] = 1;
	logicPortPos[12] = "3 -4 0";
	logicPortDir[12] = 2;
	logicPortUIName[12] = "Set-In";

	logicPortType[13] = 0;
	logicPortPos[13] = "-3 -4 0";
	logicPortDir[13] = 0;
	logicPortUIName[13] = "Set-Out";

	logicPortType[14] = 1;
	logicPortPos[14] = "3 4 0";
	logicPortDir[14] = 2;
	logicPortUIName[14] = "Overflow-In";

	logicPortType[15] = 0;
	logicPortPos[15] = "-3 4 0";
	logicPortDir[15] = 0;
	logicPortUIName[15] = "Overflow-Out";

	logicPortType[16] = 1;
	logicPortPos[16] = "-3 2 0";
	logicPortDir[16] = 0;
	logicPortUIName[16] = "Underflow-In";

	logicPortType[17] = 0;
	logicPortPos[17] = "3 2 0";
	logicPortDir[17] = 2;
	logicPortUIName[17] = "Underflow-Out";
};

//Drunk programming let's go!

function LogicGate_Shifter_Data::doLogic(%this, %obj)
{
	//Clock on high signal
	if($LBC::Ports::BrickState[%obj,8] && !%obj.clockPrevState)
	{
		%obj.clockPrevState = 1;

		if($LBC::Ports::BrickState[%obj, 10])
		{
			//Negative
			%obj.Logic_SetOutput(17, $LBC::Ports::BrickState[%obj,4]);
			%obj.Logic_SetOutput(4, $LBC::Ports::BrickState[%obj,5]);
			%obj.Logic_SetOutput(5, $LBC::Ports::BrickState[%obj,6]);
			%obj.Logic_SetOutput(6, $LBC::Ports::BrickState[%obj,7]);
			%obj.Logic_SetOutput(7, $LBC::Ports::BrickState[%obj,16]);
		}
		else
		{
			//Positive
			%obj.Logic_SetOutput(15, $LBC::Ports::BrickState[%obj,7]);
			%obj.Logic_SetOutput(7, $LBC::Ports::BrickState[%obj,6]);
			%obj.Logic_SetOutput(6, $LBC::Ports::BrickState[%obj,5]);
			%obj.Logic_SetOutput(5, $LBC::Ports::BrickState[%obj,4]);
			%obj.Logic_SetOutput(4, $LBC::Ports::BrickState[%obj,14]);
		}
	}
	else if(!$LBC::Ports::BrickState[%obj,8] && %obj.clockPrevState)
	{
		%obj.clockPrevState = 0;
	}

	//Set-In on high signal
	if($LBC::Ports::BrickState[%obj,12] && !%obj.setPrevState)
	{
		%obj.setPrevState = 1;

		%obj.Logic_SetOutput(4, $LBC::Ports::BrickState[%obj,0]);
		%obj.Logic_SetOutput(5, $LBC::Ports::BrickState[%obj,1]);
		%obj.Logic_SetOutput(6, $LBC::Ports::BrickState[%obj,2]);
		%obj.Logic_SetOutput(7, $LBC::Ports::BrickState[%obj,3]);
	}
	else if(!$LBC::Ports::BrickState[%obj,12] && %obj.setPrevState)
	{
		%obj.setPrevState = 0;
	}

	%obj.Logic_SetOutput(9, $LBC::Ports::BrickState[%obj,8]);
	%obj.Logic_SetOutput(11, $LBC::Ports::BrickState[%obj,10]);
	%obj.Logic_SetOutput(13, $LBC::Ports::BrickState[%obj,12]);
}

function LogicGate_Shifter_Data::Logic_onGateAdded(%this, %obj)
{
	%obj.clockPrevState = 0;
	%obj.setPrevState = 0;
}