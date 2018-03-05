datablock fxDTSBrickData(Logic1x1fDiodeData)
{
	category = "Logic Bricks";
	subCategory = "Gates";
	uiName = "1x1f Diode";
	iconName = "Add-Ons/Brick_Illogic/icons/diode";
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/1x1f_1i_1o.blb";
	hasPrint = 1;
	printAspectRatio = "Logic";

	isLogic = 1;
	isLogicGate = 1;
	logicUIName = "Diode";
	logicUIDesc = "B is A";

	numLogicPorts = 2;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 0";
	logicPortDir[0] = "0";
	logicPortUIName[0] = "A";
	logicPortUIDesc[0] = "";

	logicPortType[1] = 0;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = "2";
	logicPortUIName[1] = "B";
	logicPortUIDesc[1] = "";
};

function Logic1x1fDiodeData::doLogic(%this, %obj)
{
	%obj.Logic_SetOutput(1, $LBC::Ports::BrickState[%obj, 0]);
}

function Logic1x1fDiodeData::onPlant(%this, %obj)
{
	%obj.setColor(3);

	if(!$LBC::Prints::LessThan)
	{
		%count = getNumPrintTextures();
		for(%i = 0; %i < %count; %i++)
		{
			if(getPrintTexture(%i) $= "Add-Ons/Print_Letters_Default/prints/-less_than.png")
			{
				$LBC::Prints::LessThan = %i;
				break;
			}
		}
	}

	%obj.setPrint($LBC::Prints::LessThan);
	parent::onPlant(%this, %obj);
}
