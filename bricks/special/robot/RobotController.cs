if(!isObject(Illogic_RobotBrickGroup))
{
	new SimGroup(Illogic_RobotBrickGroup)
	{
		bl_id = 12345678;
		name = "\c1Robot";
		QuotaObject = GlobalQuota;
	};
	mainBrickGroup.add(Illogic_RobotBrickGroup);
}

datablock StaticShapeData(Illogic_RobotShapeData)
{
	shapeFile = "./cube.dts";
};

datablock fxDTSBrickData(LogicGate_RobotController_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/RobotController.blb";
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Robot Controller";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 3;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;
	isLogicAdminOnly = true;

	logicUIName = "Robot Controller";
	logicUIDesc = "Creates and controls a mobile robot that can detect and place bricks";

	numLogicPorts = 22;

	logicPortType[0] = 1;
	logicPortPos[0] = "15 -3 0";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "ColorIn0";

	logicPortType[1] = 1;
	logicPortPos[1] = "13 -3 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "ColorIn1";

	logicPortType[2] = 1;
	logicPortPos[2] = "11 -3 0";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "ColorIn2";

	logicPortType[3] = 1;
	logicPortPos[3] = "9 -3 0";
	logicPortDir[3] = 3;
	logicPortUIName[3] = "ColorIn3";

	logicPortType[4] = 1;
	logicPortPos[4] = "7 -3 0";
	logicPortDir[4] = 3;
	logicPortUIName[4] = "ColorIn4";

	logicPortType[5] = 1;
	logicPortPos[5] = "5 -3 0";
	logicPortDir[5] = 3;
	logicPortUIName[5] = "ColorIn5";

	logicPortType[6] = 1;
	logicPortPos[6] = "1 -3 0";
	logicPortDir[6] = 3;
	logicPortUIName[6] = "Destroy Brick";

	logicPortType[7] = 1;
	logicPortPos[7] = "-1 -3 0";
	logicPortDir[7] = 3;
	logicPortUIName[7] = "Plant Brick";

	logicPortType[8] = 1;
	logicPortPos[8] = "-5 -3 0";
	logicPortDir[8] = 3;
	logicPortUIName[8] = "Move Down";

	logicPortType[9] = 1;
	logicPortPos[9] = "-7 -3 0";
	logicPortDir[9] = 3;
	logicPortUIName[9] = "Move Up";

	logicPortType[10] = 1;
	logicPortPos[10] = "-9 -3 0";
	logicPortDir[10] = 3;
	logicPortUIName[10] = "Move Right";

	logicPortType[11] = 1;
	logicPortPos[11] = "-11 -3 0";
	logicPortDir[11] = 3;
	logicPortUIName[11] = "Move Left";

	logicPortType[12] = 1;
	logicPortPos[12] = "-13 -3 0";
	logicPortDir[12] = 3;
	logicPortUIName[12] = "Move Backward";

	logicPortType[13] = 1;
	logicPortPos[13] = "-15 -3 0";
	logicPortDir[13] = 3;
	logicPortUIName[13] = "Move Forward";

	logicPortType[14] = 0;
	logicPortPos[14] = "15 3 0";
	logicPortDir[14] = 1;
	logicPortUIName[14] = "ColorOut0";

	logicPortType[15] = 0;
	logicPortPos[15] = "13 3 0";
	logicPortDir[15] = 1;
	logicPortUIName[15] = "ColorOut1";

	logicPortType[16] = 0;
	logicPortPos[16] = "11 3 0";
	logicPortDir[16] = 1;
	logicPortUIName[16] = "ColorOut2";

	logicPortType[17] = 0;
	logicPortPos[17] = "9 3 0";
	logicPortDir[17] = 1;
	logicPortUIName[17] = "ColorOut3";

	logicPortType[18] = 0;
	logicPortPos[18] = "7 3 0";
	logicPortDir[18] = 1;
	logicPortUIName[18] = "ColorOut4";

	logicPortType[19] = 0;
	logicPortPos[19] = "5 3 0";
	logicPortDir[19] = 1;
	logicPortUIName[19] = "ColorOut5";

	logicPortType[20] = 0;
	logicPortPos[20] = "1 3 0";
	logicPortDir[20] = 1;
	logicPortUIName[20] = "Brick Detected";

	logicPortType[21] = 1;
	logicPortPos[21] = "-1 3 0";
	logicPortDir[21] = 1;
	logicPortUIName[21] = "Detect Brick";
};

function LogicGate_RobotController_Data::getRelativeVector(%this, %obj, %vec)
{
	%rot = %obj.angleID;
	%x = getWord(%vec, 0);
	%y = getWord(%vec, 1);
	%z = getWord(%vec, 2);
	%ax = %x;
	switch(%rot)
	{
		case 1:
			%x = %y;
			%y = -%ax;
		case 2:
			%x = -%x;
			%y = -%y;
		case 3:
			%x = -%y;
			%y = %ax;
	}

	return %x SPC %y SPC %z;
}

function LogicGate_RobotController_Data::onDeath(%this, %obj)
{
	if(isObject(%obj.illogicRobot))
		%obj.illogicRobot.delete();
}

function LogicGate_RobotController_Data::onRemove(%this, %obj)
{
	if(isObject(%obj.illogicRobot))
		%obj.illogicRobot.delete();
}

function LogicGate_RobotController_Data::doLogic(%this, %obj)
{
	%x = (!$LBC::Ports::LastBrickState[%obj, 10] && $LBC::Ports::BrickState[%obj, 10]) - (!$LBC::Ports::LastBrickState[%obj, 11] && $LBC::Ports::BrickState[%obj, 11]);
	%y = (!$LBC::Ports::LastBrickState[%obj, 13] && $LBC::Ports::BrickState[%obj, 13]) - (!$LBC::Ports::LastBrickState[%obj, 12] && $LBC::Ports::BrickState[%obj, 12]);
	%z = (!$LBC::Ports::LastBrickState[%obj, 9] && $LBC::Ports::BrickState[%obj, 9]) - (!$LBC::Ports::LastBrickState[%obj, 8] && $LBC::Ports::BrickState[%obj, 8]);
	%vec = %this.getRelativeVector(%obj, %x*0.5 SPC %y*0.5 SPC %z*0.2);

	%robot = %obj.illogicRobot;

	if(%vec !$= "0 0 0")
		%robot.setTransform(vectorAdd(%robot.getPosition(), %vec));

	%pos = %robot.getPosition();

	if(!$LBC::Ports::LastBrickState[%obj, 21] && $LBC::Ports::BrickState[%obj, 21])
	{
		initContainerBoxSearch(%pos, "0.49 0.49 0.19", $TypeMasks::FxBrickObjectType);
		if(isObject(%sobj = containerSearchNext()))
		{
			%obj.Logic_SetOutput(20, true);

			%color = %sobj.getColorID();
			for(%i = 0; %i < 6; %i++)
				%obj.Logic_SetOutput(14+%i, (%color >> %i) & 1);

			%obj.illogicRobotBrick = %sobj;
		}
		else
		{
			%obj.Logic_SetOutput(20, false);

			for(%i = 0; %i < 6; %i++)
				%obj.Logic_SetOutput(14+%i, false);

			%obj.illogicRobotBrick = 0;
		}
	}

	if(!$LBC::Ports::LastBrickState[%obj, 6] && $LBC::Ports::BrickState[%obj, 6])
	{
		initContainerBoxSearch(%pos, "0.49 0.49 0.19", $TypeMasks::FxBrickObjectType);
		if(isObject(%sobj = containerSearchNext()) && %sobj.getGroup() == nameToID(Illogic_RobotBrickGroup))
			%sobj.delete();
	}

	if(!$LBC::Ports::LastBrickState[%obj, 7] && $LBC::Ports::BrickState[%obj, 7])
	{
		for(%i = 0; %i < 6; %i++)
			%color += mPow(2, %i) * $LBC::Ports::BrickState[%obj, %i];

		%brick = new fxDTSBrick()
		{
			datablock = brick1x1fData;
			position = %pos;
			colorID = %color;
			isPlanted = 1;
		};

		%err = %brick.plant();
		if(%err != 0 && %err != 2)
			%brick.delete();
		else
		{
			Illogic_RobotBrickGroup.add(%brick);
			%brick.setTrusted(true);
		}
	}
}

function LogicGate_RobotController_Data::Logic_onGateAdded(%this, %obj)
{
	if(isObject(%obj.illogicRobot))
		%obj.illogicRobot.delete();

	%pos = %obj.getPosition();
	%rpos = vectorAdd(%pos, %this.getRelativeVector(%obj, "0.25 7.75 0"));
	%robot = new StaticShape()
	{
		datablock = Illogic_BrickData;
		position = %rpos;
	};
	%robot.setNodeColor("ALL", "1 1 1 1");
	missionCleanup.add(%robot);

	%obj.illogicRobot = %robot;
}
