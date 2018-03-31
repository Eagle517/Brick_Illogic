datablock fxDTSBrickData(LogicGate__3BitEnabler_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/3BitEnabler.blb";
	category = "Logic Bricks";
	subCategory = "Bus";
	uiName = "3 Bit Enabler";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "3 Bit Enabler";
	logicUIDesc = "3 bit enabler with enable propagate";

	numLogicPorts = 8;

	logicPortType[0] = 1;
	logicPortPos[0] = "2 0 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "D0";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "D1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-2 0 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "D2";

	logicPortType[3] = 0;
	logicPortPos[3] = "2 0 0";
	logicPortDir[3] = 1;
	logicPortUIName[3] = "Q0";

	logicPortType[4] = 0;
	logicPortPos[4] = "0 0 0";
	logicPortDir[4] = 1;
	logicPortUIName[4] = "Q1";

	logicPortType[5] = 0;
	logicPortPos[5] = "-2 0 0";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "Q2";

	logicPortType[6] = 1;
	logicPortPos[6] = "2 0 0";
	logicPortDir[6] = 2;
	logicPortUIName[6] = "EnableIn";

	logicPortType[7] = 0;
	logicPortPos[7] = "-2 0 0";
	logicPortDir[7] = 0;
	logicPortUIName[7] = "EnableOut";
};

function LogicGate__3BitEnabler_Data::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 6])
	{
		%obj.Logic_SetOutput(3, $LBC::Ports::BrickState[%obj, 0]);
		%obj.Logic_SetOutput(4, $LBC::Ports::BrickState[%obj, 1]);
		%obj.Logic_SetOutput(5, $LBC::Ports::BrickState[%obj, 2]);

		%obj.Logic_SetOutput(7, 1);
	}
	else
	{
		%obj.Logic_SetOutput(3, 0);
		%obj.Logic_SetOutput(4, 0);
		%obj.Logic_SetOutput(5, 0);

		%obj.Logic_SetOutput(7, 0);
	}
}
