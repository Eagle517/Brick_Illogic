datablock fxDTSBrickData(LogicGate__4bitComparator_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/4bitComparator.blb";
	category = "Logic Bricks";
	subCategory = "Math";
	uiName = "4bit Comparator";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 1;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "4bit Comparator";
	logicUIDesc = "Compares A to B";

	numLogicPorts = 14;

	logicPortType[0] = 1;
	logicPortPos[0] = "2 3 0";
	logicPortDir[0] = 2;
	logicPortUIName[0] = "A0";

	logicPortType[1] = 1;
	logicPortPos[1] = "2 1 0";
	logicPortDir[1] = 2;
	logicPortUIName[1] = "A1";

	logicPortType[2] = 1;
	logicPortPos[2] = "2 -1 0";
	logicPortDir[2] = 2;
	logicPortUIName[2] = "A2";

	logicPortType[3] = 1;
	logicPortPos[3] = "2 -3 0";
	logicPortDir[3] = 2;
	logicPortUIName[3] = "A3";

	logicPortType[4] = 1;
	logicPortPos[4] = "-2 -3 0";
	logicPortDir[4] = 0;
	logicPortUIName[4] = "B0";

	logicPortType[5] = 1;
	logicPortPos[5] = "-2 -1 0";
	logicPortDir[5] = 0;
	logicPortUIName[5] = "B1";

	logicPortType[6] = 1;
	logicPortPos[6] = "-2 1 0";
	logicPortDir[6] = 0;
	logicPortUIName[6] = "B2";

	logicPortType[7] = 1;
	logicPortPos[7] = "-2 3 0";
	logicPortDir[7] = 0;
	logicPortUIName[7] = "B3";

	logicPortType[8] = 1;
	logicPortPos[8] = "-2 3 0";
	logicPortDir[8] = 1;
	logicPortUIName[8] = "In >";

	logicPortType[9] = 1;
	logicPortPos[9] = "0 3 0";
	logicPortDir[9] = 1;
	logicPortUIName[9] = "In =";

	logicPortType[10] = 1;
	logicPortPos[10] = "2 3 0";
	logicPortDir[10] = 1;
	logicPortUIName[10] = "In <";

	logicPortType[11] = 0;
	logicPortPos[11] = "2 -3 0";
	logicPortDir[11] = 3;
	logicPortUIName[11] = "Out <";

	logicPortType[12] = 0;
	logicPortPos[12] = "0 -3 0";
	logicPortDir[12] = 3;
	logicPortUIName[12] = "Out =";

	logicPortType[13] = 0;
	logicPortPos[13] = "-2 -3 0";
	logicPortDir[13] = 3;
	logicPortUIName[13] = "Out >";
};

function LogicGate__4bitComparator_Data::doLogic(%this, %obj)
{
	%a =
		($LBC::Ports::BrickState[%obj,0]*1)+
		($LBC::Ports::BrickState[%obj,1]*2)+
		($LBC::Ports::BrickState[%obj,2]*4)+
		($LBC::Ports::BrickState[%obj,3]*8);

	%b =
		($LBC::Ports::BrickState[%obj,4]*1)+
		($LBC::Ports::BrickState[%obj,5]*2)+
		($LBC::Ports::BrickState[%obj,6]*4)+
		($LBC::Ports::BrickState[%obj,7]*8);

	if(%a < %b)
	{
		%obj.Logic_SetOutput(11, 1);
		%obj.Logic_SetOutput(12, 0);
		%obj.Logic_SetOutput(13, 0);
	}
	if(%a == %b)
	{
		%obj.Logic_SetOutput(11, 0);
		%obj.Logic_SetOutput(12, 1);
		%obj.Logic_SetOutput(13, 0);
	}
	if(%a > %b)
	{
		%obj.Logic_SetOutput(11, 0);
		%obj.Logic_SetOutput(12, 0);
		%obj.Logic_SetOutput(13, 1);
	}

	talk(%a SPC %b);
}