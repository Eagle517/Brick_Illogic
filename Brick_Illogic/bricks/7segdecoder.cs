datablock fxDTSBrickData(LogicGate__7segdecoder_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/7segdecoder.blb";
	category = "Logic Bricks";
	subCategory = "Chips";
	uiName = "7 Segment Decoder";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "7 Segment Decoder";
	logicUIDesc = "";

	numLogicPorts = 11;

	logicPortType[0] = 1;
	logicPortPos[0] = "3 0 -10";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "In0";

	logicPortType[1] = 1;
	logicPortPos[1] = "1 0 -10";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "In1";

	logicPortType[2] = 1;
	logicPortPos[2] = "-1 0 -10";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "In2";

	logicPortType[3] = 1;
	logicPortPos[3] = "-3 0 -10";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "In3";

	logicPortType[4] = 0;
	logicPortPos[4] = "-1 0 8";
	logicPortDir[4] = 1;
	logicPortUIName[4] = "A";

	logicPortType[5] = 0;
	logicPortPos[5] = "-3 0 2";
	logicPortDir[5] = 1;
	logicPortUIName[5] = "B";

	logicPortType[6] = 0;
	logicPortPos[6] = "-3 0 -7";
	logicPortDir[6] = 1;
	logicPortUIName[6] = "C";

	logicPortType[7] = 0;
	logicPortPos[7] = "-1 0 -10";
	logicPortDir[7] = 1;
	logicPortUIName[7] = "D";

	logicPortType[8] = 0;
	logicPortPos[8] = "3 0 -7";
	logicPortDir[8] = 1;
	logicPortUIName[8] = "E";

	logicPortType[9] = 0;
	logicPortPos[9] = "3 0 2";
	logicPortDir[9] = 1;
	logicPortUIName[9] = "F";

	logicPortType[10] = 0;
	logicPortPos[10] = "-1 0 -1";
	logicPortDir[10] = 1;
	logicPortUIName[10] = "G";
};

function LogicGate__7segdecoder_Data::doLogic(%this, %obj)
{
	%obj.val =
		($LBC::Ports::BrickState[%obj,0]*1)+
		($LBC::Ports::BrickState[%obj,1]*2)+
		($LBC::Ports::BrickState[%obj,2]*4)+
		($LBC::Ports::BrickState[%obj,3]*8);

	switch(%obj.val)
	{
		case 0:
			%obj.A = 1;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 0;

		case 1:
			%obj.A = 0;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 0;
			%obj.E = 0;
			%obj.F = 0;
			%obj.G = 0;

		case 2:
			%obj.A = 1;
			%obj.B = 0;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 0;
			%obj.G = 1;

		case 3:
			%obj.A = 1;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 0;
			%obj.F = 0;
			%obj.G = 1;

		case 4:
			%obj.A = 0;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 0;
			%obj.E = 0;
			%obj.F = 1;
			%obj.G = 1;

		case 5:
			%obj.A = 1;
			%obj.B = 0;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 0;
			%obj.F = 1;
			%obj.G = 1;

		case 6:
			%obj.A = 1;
			%obj.B = 0;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 1;

		case 7:
			%obj.A = 1;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 0;
			%obj.E = 0;
			%obj.F = 0;
			%obj.G = 0;

		case 8:
			%obj.A = 1;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 1;

		case 9:
			%obj.A = 1;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 0;
			%obj.F = 1;
			%obj.G = 1;

		case 10: //A
			%obj.A = 1;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 0;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 1;

		case 11: //b
			%obj.A = 0;
			%obj.B = 0;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 1;

		case 12: //C
			%obj.A = 1;
			%obj.B = 0;
			%obj.C = 0;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 0;

		case 13: //d
			%obj.A = 0;
			%obj.B = 1;
			%obj.C = 1;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 0;
			%obj.G = 1;

		case 14: //E
			%obj.A = 1;
			%obj.B = 0;
			%obj.C = 0;
			%obj.D = 1;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 1;

		case 15: //F
			%obj.A = 1;
			%obj.B = 0;
			%obj.C = 0;
			%obj.D = 0;
			%obj.E = 1;
			%obj.F = 1;
			%obj.G = 1;
	}

	%obj.Logic_SetOutput(4,%obj.A);
	%obj.Logic_SetOutput(5,%obj.B);
	%obj.Logic_SetOutput(6,%obj.C);
	%obj.Logic_SetOutput(7,%obj.D);
	%obj.Logic_SetOutput(8,%obj.E);
	%obj.Logic_SetOutput(9,%obj.F);
	%obj.Logic_SetOutput(10,%obj.G);
}

function LogicGate__7segdecoder_Data::Logic_onGateAdded(%this, %obj)
{
	//Set it to 0
	%obj.A = 1;
	%obj.B = 1;
	%obj.C = 1;
	%obj.D = 1;
	%obj.E = 1;
	%obj.F = 1;
	%obj.G = 0;
}