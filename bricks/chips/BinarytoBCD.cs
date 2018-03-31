datablock fxDTSBrickData(LogicGate_BinarytoBCD_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/BinarytoBCD.blb";
	category = "Logic Bricks";
	subCategory = "Gatemaker";
	uiName = "Binary to BCD";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Binary to BCD";
	logicUIDesc = "";

	numLogicPorts = 12;

	logicPortType[0] = 1;
	logicPortPos[0] = "7 -1 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "5 -1 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "3 -1 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "1 -1 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 0;
	logicPortPos[4] = "-7 1 0";
	logicPortDir[4] = 1;
	logicPortUIName[4] = "A0";

	logicPortType[5] = 0;
	logicPortPos[5] = "-5 1 0";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "A1";

	logicPortType[6] = 0;
	logicPortPos[6] = "-3 1 0";
	logicPortDir[6] = 1;
	logicPortUIName[6] = "A2";

	logicPortType[7] = 0;
	logicPortPos[7] = "-1 1 0";
	logicPortDir[7] = 1;
	logicPortUIName[7] = "A3";

	logicPortType[8] = 0;
	logicPortPos[8] = "1 1 0";
	logicPortDir[8] = 1;
	logicPortUIName[8] = "B0";

	logicPortType[9] = 0;
	logicPortPos[9] = "3 1 0";
	logicPortDir[9] = 1;
	logicPortUIName[9] = "B1";

	logicPortType[10] = 0;
	logicPortPos[10] = "5 1 0";
	logicPortDir[10] = 1;
	logicPortUIName[10] = "B2";

	logicPortType[11] = 0;
	logicPortPos[11] = "7 1 0";
	logicPortDir[11] = 1;
	logicPortUIName[11] = "B3";
};

function LogicGate_BinarytoBCD_Data::doLogic(%this, %obj)
{
	%val =
		($LBC::Ports::BrickState[%obj,0]*1)+
		($LBC::Ports::BrickState[%obj,1]*2)+
		($LBC::Ports::BrickState[%obj,2]*4)+
		($LBC::Ports::BrickState[%obj,3]*8);

	if(%val < 10)
	{
		%A = %val;
		%B = 0;
	}
	else
	{
		%A = %val - 10;
		%B = 1;
	}

	%obj.Logic_SetOutput(4, %A & 1);
	%obj.Logic_SetOutput(5, %A & 2);
	%obj.Logic_SetOutput(6, %A & 4);
	%obj.Logic_SetOutput(7, %A & 8);

	%obj.Logic_SetOutput(8, %B & 1);
	%obj.Logic_SetOutput(9, %B & 2);
	%obj.Logic_SetOutput(10, %B & 4);
	%obj.Logic_SetOutput(11, %B & 8);
	//I know...
}
