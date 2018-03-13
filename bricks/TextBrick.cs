datablock fxDTSBrickData(LogicGate_TextBrick_Data)
{
	brickFile = "Add-Ons/Brick_Illogic/bricks/blb/TextBrick.blb";
	category = "Logic Bricks";
	subCategory = "Special";
	uiName = "Text Brick";
	iconName = "";
	hasPrint = 1;
	printAspectRatio = "Logic";
	orientationFix = 1;

	isLogic = true;
	isLogicGate = true;
	isLogicInput = false;

	logicUIName = "Text Brick";
	logicUIDesc = "Steps up or down between prints on pulse";

	numLogicPorts = 3;

	logicPortType[0] = 1;
	logicPortPos[0] = "0 0 -1";
	logicPortDir[0] = 3;
	logicPortUIName[0] = "Reset";

	logicPortType[1] = 1;
	logicPortPos[1] = "0 0 0";
	logicPortDir[1] = 3;
	logicPortUIName[1] = "Decrement";

	logicPortType[2] = 1;
	logicPortPos[2] = "0 0 1";
	logicPortDir[2] = 3;
	logicPortUIName[2] = "Increment";
};

//space, a-z, 0-9, punc
function LogicGate_TextBrick_Data::doLogic(%this, %obj)
{
	if($LBC::Ports::BrickState[%obj, 1])
	{
		%obj.LBC_print--;
		if(%obj.LBC_print < 0)
			%obj.LBC_print = 71;

		%obj.setPrint($LBC::TextBrick::Print[%obj.LBC_print]);
	}

	if($LBC::Ports::BrickState[%obj, 2])
	{
		%obj.LBC_print = (%obj.LBC_print+1) % 72;
		%obj.setPrint($LBC::TextBrick::Print[%obj.LBC_print]);
	}
	
	if($LBC::Ports::BrickState[%obj, 0])
	{
		%obj.LBC_print = 0;
		%obj.setPrint($LBC::TextBrick::Print[0]);
	}
}

function LogicGate_TextBrick_Data::Logic_onGateAdded(%this, %obj)
{
	if(!$LBC::TextBrick::Prints)
	{
		%count = getNumPrintTextures();
		%start = 1;
		%id = 0;
		for(%i = 0; %i < %count; %i++)
		{
			%print = getPrintTexture(%i);
			if(stripos(%print, "print_letters_default") != -1 || stripos(%print, "print_letters_extra") != -1)
			{
				if(stripos(%print, "-space") != -1)
					$LBC::TextBrick::Print[0] = %i;
				else
				{
					if(%id <= 25)
						$LBC::TextBrick::Print[27-(%start+%id)] = %i;
					else if(%id <= 35)
						$LBC::TextBrick::Print[63-(%start+%id)] = %i;
					else
						$LBC::TextBrick::Print[109-(%start+%id)] = %i;
				}
				%id++;
			}
		}

		$LBC::TextBrick::PrintCount = %id;
		$LBC::TextBrick::Prints = true;
	}

	%obj.LBC_print = 0;
	%obj.setPrint($LBC::TextBrick::Print[0]);
}

// function getprints()
// {
// 	deleteVariables("$LBC::TextBrick::Print*");
// 	%count = getNumPrintTextures();
// 	%start = 1;
// 	%id = 0;
// 	for(%i = 0; %i < %count; %i++)
// 	{
// 		%print = getPrintTexture(%i);
// 		if(stripos(%print, "print_letters_default") != -1 || stripos(%print, "print_letters_extra") != -1)
// 		{
// 			//echo(%id);
// 			if(stripos(%print, "-space") != -1)
// 				$LBC::TextBrick::Print[0] = %i;
// 			else
// 			{
// 				if(%id <= 25)
// 				{
// 					//echo(%print);
// 					echo(27-(%start+%id), " --- ", %i, " --- ", %print);
// 					$LBC::TextBrick::Print[27-(%start+%id)] = %i;
// 				}
// 				else if(%id <= 35)
// 				{
// 					echo(63-(%start+%id), " --- ", %i, " --- ", %print);
// 					$LBC::TextBrick::Print[63-(%start+%id)] = %i;
// 				}
// 				else
// 				{
// 					echo(109-(%start+%id), " --- ", %i, " --- ", %print);
// 					$LBC::TextBrick::Print[109-(%start+%id)] = %i;
// 				}
// 			}

// 			%id++;
// 		}
// 	}

// 	$LBC::TextBrick::PrintCount = %id;
// }
