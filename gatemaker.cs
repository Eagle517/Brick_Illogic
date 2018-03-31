datablock StaticShapeData(Illogic_BrickData)
{
	shapeFile = "./gatemaker/cube.dts";
};

datablock StaticShapeData(Illogic_BrickInputData)
{
	shapeFile = "./gatemaker/input.dts";
};

datablock StaticShapeData(Illogic_BrickInputVertData)
{
	shapeFile = "./gatemaker/input_vert.dts";
};

datablock StaticShapeData(Illogic_BrickOutputData)
{
	shapeFile = "./gatemaker/output.dts";
};

datablock StaticShapeData(Illogic_BrickOutputVertData)
{
	shapeFile = "./gatemaker/output_vert.dts";
};

datablock StaticShapeData(Illogic_BrickAxisData)
{
	shapeFile = "./gatemaker/axis2.dts";
};

//pos, offset x,y,z from origin, round to x,y,z
function Logic_RoundToBrickPos(%pos, %ox, %oy, %oz, %xr, %yr, %zr)
{
	%x = getWord(%pos, 0)+%ox;
	%y = getWord(%pos, 1)+%oy;
	%z = getWord(%pos, 2)+%oz;
	
	%x = mFloor(%x*(1/%xr)+0.5)/(1/%xr)-%ox;
	%y = mFloor(%y*(1/%yr)+0.5)/(1/%yr)-%oy;
	%z = mFloor(%z*(1/%zr)+0.5)/(1/%zr)-%oz;

	return %x SPC %y SPC %z;
}

function serverCmdlMakeGate(%client, %x, %y, %z)
{
	if(!%client.isAdmin && !%client.isSuperAdmin)
		return;

	if(isObject(%player = %client.player))
	{
		%eye = %player.getEyePoint();
		%vec = %player.getEyeVector();
		%end = vectorAdd(%eye, vectorScale(%vec, 3*getWord(%player.getScale(), 2)));
		%ray = containerRayCast(%eye, %end, $TypeMasks::All, %player);
		if(isObject(%ray))
			%pos = getWords(%ray, 1, 3);
		else
			%pos = %end;

		%client.Logic_MakeGate(%pos, %x, %y, %z);
	}
}

function serverCmdlMoveGate(%client)
{
	if(isObject(%client.LGM_gate) && !isObject(%client.LGM_port))
	{
		%client.Logic_MoveGate();
		messageClient(%client, '', '\c6Click to place the gate.');
	}
}

function serverCmdlSetGateName(%client, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9)
{
	if(isObject(%gate = %client.LGM_gate))
	{
		for(%i = 0; %i < 10; %i++)
			%a = %a SPC %a[%i];
		%a = trim(%a);
		%gate.gateName = %a;
		messageClient(%client, '', '\c6Gate name set to \c3%1', %a);
	}
}

function serverCmdlSetGateDesc(%client, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19)
{
	if(isObject(%gate = %client.LGM_gate))
	{
		for(%i = 0; %i < 20; %i++)
			%a = %a SPC %a[%i];
		%a = trim(%a);
		%gate.gateDesc = %a;
		messageClient(%client, '', '\c6Gate description set to \c3%1', %a);
	}
}

function serverCmdlAddPorts(%client, %type)
{
	if(isObject(%gate = %client.LGM_gate) && !isEventPending(%client.LGM_moveSched))
	{
		%client.LGM_placing = !%client.LGM_placing;
		if(%client.LGM_placing)
			messageClient(%client, '', '\c6Click to place a port. Click it again to change port type or delete. \c3/lAddPorts\c6 to stop.');
		else
			messageClient(%client, '', '\c6You are no longer placing ports.');
	}
}

function serverCmdlSetPortName(%client, %p, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18)
{
	if(isObject(%gate = %client.LGM_gate) && isObject(%gate.ports[%p]))
	{
		for(%i = 0; %i < 20; %i++)
			%a = %a SPC %a[%i];
		
		%a = trim(%a);
		%gate.ports[%p].portName = %a;
		messageClient(%client, '', '\c6Port \c3%1\c6 name set to \c3%2', %p, %a);
	}
}

function serverCmdlSaveGate(%client, %overwrite)
{
	if(isObject(%gate = %client.LGM_gate))
	{
		if(%gate.gateName $= "")
			messageClient(%client, '', '\c6You must set the gate name before saving. \c3/lSetGateName name');
		else
		{
			%file = "config/server/IllogicGateMaker/"@%client.getBLID()@"_"@%gate.gateName@".blb";
			%file = "config/server/IllogicGateMaker/"@strReplace(%gate.gateName, " ", "");
			if(isFile(%file@".blb") && !%overwrite)
			{
				messageClient(%client, '', '\c6A gate with the name of \c3%1\c6 already exists. Use \c3/lSaveGate 1\c6 to overwrite it.', %gate.gateName);
				return;
			}
			%client.Logic_SaveBLB(%file@".blb");
			%client.Logic_SaveData(%file@".cs", %file@".blb");
			messageClient(%client, '', '\c6Your gate was saved to \c3%1', %file);
		}
	}
}

function serverCmdlDeleteGate(%client)
{
	%client.Logic_DeleteGate();
}

function serverCmdLLoadGates(%client)
{
	if(!%client.isAdmin && !%client.isSuperAdmin)
		return;

	setModPaths(getModPaths());
	%search = "config/server/IllogicGateMaker/*.cs";
	%file = findFirstFile(%search);
	while(%file !$= "")
	{
		exec(%file);
		%file = findNextFile(%search);
	}
	%search = "Add-Ons/Brick_Illogic/bricks/*.cs";
	%file = findFirstFile(%search);
	while(%file !$= "")
	{
		exec(%file);
		%file = findNextFile(%search);
	}
	createUINameTable();
	transmitDatablocks();
	schedule(500, 0, "commandToAll", 'BSD_LoadBricks');

	messageAll('', '\c3%1\c6 has loaded gates.', %client.name);
}

function GameConnection::Logic_MakeGate(%this, %pos, %wid, %len, %height)
{
	if(isObject(%this.LGM_gate))
	{
		%this.centerPrint("You must delete your first gate in order to make another one.", 3);
		return;
	}

	%wid = mClamp(%wid, 1, 64);
	%len = mClamp(%len, 1, 64);
	%height = mClamp(%height, 1, 160);

	%x = %wid % 2 == 0 ? mFloor(getWord(%pos, 0)*2+0.5)/2 : mFloor(getWord(%pos, 0)*2+1)/2-0.25;
	%y = %len % 2 == 0 ? mFloor(getWord(%pos, 1)*2+0.5)/2 : mFloor(getWord(%pos, 1)*2+1)/2-0.25;
	%z = (mFloor(getWord(%pos, 2)*5+0.5)/5)+((%height*0.2)/2);
	%pos = %x SPC %y SPC %z;

	%shape = new StaticShape()
	{
		datablock = "Illogic_BrickData";
		position = %pos;
		colorRGB = getColorIDTable(0);
		width = %wid;
		length = %len;
		height = %height;
		portCount = 0;
	};
	%shape.setScale(%wid SPC %len SPC %height);
	%shape.setNodeColor("ALL", %shape.colorRGB);
	missionCleanup.add(%shape);

	%axis = new StaticShape()
	{
		datablock = "Illogic_BrickAxisData";
		position = vectorAdd(%pos, "0 0 " @ %height*0.2/2+0.5);
	};
	%axis.setNodeColor("x", "1 0 0 1");
	%axis.setNodeColor("y", "0 1 0 1");
	%axis.setNodeColor("z", "0 0 1 1");
	%axis.setNodeColor("origin", "1 1 1 1");
	missionCleanup.add(%axis);
	%shape.axis = %axis;

	%this.LGM_gate = %shape;
	%this.centerPrint("Gate created!", 1);
	messageClient(%this, '', '\c3/lMoveGate\c6 - lets you move the gate');
	messageClient(%this, '', '\c3/lSetGateName\c6 - sets the name of the gate');
	messageClient(%this, '', '\c3/lSetGateDesc\c6 - sets the description of the gate');
	messageClient(%this, '', '\c3/lAddPorts\c6 - add ports to the gate');
	messageClient(%this, '', '\c3/lSaveGate\c6 - saves a .blb and .cs to config/server/logicgates/');
	messageClient(%this, '', '\c3/lDeleteGate\c6 - deletes the gate being worked on');
}

function GameConnection::Logic_DeleteGate(%this)
{
	if(isObject(%gate = %this.LGM_gate))
	{
		%ports = %gate.portCount;
		for(%i = 0; %i < %ports; %i++)
			%gate.ports[%i].delete();
		
		if(isObject(%gate.axis))
			%gate.axis.delete();

		%gate.delete();
		messageClient(%this, '', '\c6Your gate has been deleted.');
	}

	if(isObject(%this.LGM_port))
		%this.LGM_port.delete();
}

function GameConnection::Logic_MoveGate(%this)
{
	cancel(%this.LGM_moveSched);
	%gate = %this.LGM_gate;

	%control = %this.getControlObject();
	if(!isObject(%gate) || !isObject(%control) || ((%class = %control.getClassName()) !$= "Camera" && %class !$= "Player"))
		return;

	%eye = %control.getEyePoint();
	%vec = %control.getEyeVector();
	%end = vectorAdd(%eye, vectorScale(%vec, 5*getWord(%control.getScale(), 2)));
	%ray = containerRayCast(%eye, %end, $TypeMasks::All, %control, %gate);
	if(isObject(%ray))
		%pos = getWords(%ray, 1, 3);
	else
		%pos = %end;

	%pos = vectorSub(%pos, vectorScale(%vec, 0.01));
	%x = %gate.width % 2 == 0 ? mFloor(getWord(%pos, 0)*2+0.5)/2 : mFloor(getWord(%pos, 0)*2+1)/2-0.25;
	%y = %gate.length % 2 == 0 ? mFloor(getWord(%pos, 1)*2+0.5)/2 : mFloor(getWord(%pos, 1)*2+1)/2-0.25;
	%z = (mFloor(getWord(%pos, 2)*5+0.5)/5)+(%gate.height*0.2)/2;
	%pos = %x SPC %y SPC %z;
	%gpos = %gate.getPosition();
	if(%pos !$= %gpos)
	{
		%gate.setTransform(%pos);
		%gate.axis.setTransform(vectorAdd(%pos, "0 0 " @ %gate.height*0.2/2+0.5));

		%offset = vectorSub(%pos, %gpos);
		%ports = %gate.portCount;
		for(%i = 0; %i < %ports; %i++)
		{
			%port = %gate.ports[%i];
			%port.setTransform(vectorAdd(%port.getPosition(), %offset));
		}
	}

	%this.LGM_moveSched = %this.schedule(1, "Logic_MoveGate");
}

function GameConnection::Logic_AddGatePort(%this, %type)
{
	%gate = %this.LGM_gate;
	if(!isObject(%gate))
		return;

	%control = %this.getControlObject();

	%eye = %control.getEyePoint();
	%vec = %control.getEyeVector();
	%end = vectorAdd(%eye, vectorScale(%vec, 5*getWord(%control.getScale(), 2)));
	%ray = containerRayCast(%eye, %end, $TypeMasks::StaticObjectType, %port);
	if(isObject(%hit = firstWord(%ray)) && %hit == %gate)
	{	
		%norm = ((stripos(%nx = getWord(%ray, 4), "e") != -1) ? 0:%nx) SPC ((stripos(%ny = getWord(%ray, 5), "e") != -1) ? 0:%ny) SPC ((stripos(%nz = getWord(%ray, 6), "e") != -1) ? 0:%nz);
		%pos = vectorSub(getWords(%ray, 1, 3), vectorScale(%norm, "0.01"));
		%pos = mFloor(getWord(%pos, 0)*2)/2+0.25 SPC mFloor(getWord(%pos, 1)*2)/2+0.25 SPC (mFloor(getWord(%pos, 2)*5)/5)+0.1;
		%data = "Illogic_BrickInputData";

		%portPos = vectorSub(%pos, %gate.getPosition());
		%ports = %gate.portCount;
		for(%i = 0; %i < %ports; %i++)
		{
			%port = %gate.ports[%i];
			if(%port.portPos $= %portPos && %port.portNorm $= %norm)
			{
				%data = %port.getDatablock();
				%port.state++;
				if(%port.state == 1)
				{
					if(%port.isVert)
						%port.setDatablock("Illogic_BrickOutputVertData");
					else
						%port.setDatablock("Illogic_BrickOutputData");
					%port.portType = 0;
				}
				else if(%port.state == 2)
				{
					for(%a = %i; %a < %ports-1; %a++)
						%gate.ports[%a] = %gate.ports[%a+1];
					%gate.portCount--;
					%port.delete();
				}
				return;
			}
		}

		if(mAbs(%nx) == 1)
			%euler = "0 0" SPC -90*%nx;
		else if(mAbs(%ny) == 1)
			%euler = "0 0" SPC 90*%ny-90;
		else if(mAbs(%nz) == 1)
		{
			%euler = 180-(90*(%nz+1)) SPC "0 0";
			%isVert = true;
			%data = "Illogic_BrickInputVertData";
		}

		%rotDir["-1 0 0"] = 0;
		%rotDir["0 1 0"] = 1;
		%rotDir["1 0 0"] = 2;
		%rotDir["0 -1 0"] = 3;
		%rotDir["0 0 1"] = 4;
		%rotDir["0 0 -1"] = 5;

		%rot = getWords(MatrixCreateFromEuler(vectorScale(%euler, $pi/180)), 3, 6);
		%shape = new StaticShape()
		{
			datablock = %data;
			portType = 1;
			portNorm = %norm;
			portPos = %portPos;
			portDir = %rotDir[%norm];
			isVert = %isVert;
			state = 0;
		};
		%shape.setTransform(%pos SPC %rot);
		missionCleanup.add(%shape);
		%gate.ports[%gate.portCount] = %shape;
		%gate.portCount++;
	}
}

function GameConnection::Logic_SaveData(%this, %filename, %blbFile)
{
	%gate = %this.LGM_gate;
	if(!isObject(%gate))
		return;

	%file = new FileObject();
	%file.openForWrite(%filename);

	%dataName = "LogicGate_"@getSafeVariableName(strReplace(%gate.gateName, " ", ""))@"_Data";
	%file.writeLine("datablock fxDTSBrickData("@%dataName@")");
	%file.writeLine("{");
	%file.writeLine("\tbrickFile = \""@%blbFile@"\";");
	%file.writeLine("\tcategory = \"Logic Bricks\";");
	%file.writeLine("\tsubCategory = \""@ (%gate.subCat $= "" ? "Gatemaker":%gate.subCat) @ "\";");
	%file.writeLine("\tuiName = \""@%gate.gateName@"\";");
	%file.writeLine("\ticonName = \"\";");
	%file.writeLine("\thasPrint = 1;");
	%file.writeLine("\tprintAspectRatio = \"Logic\";");
	%file.writeLine("\torientationFix = 3;\n");
	%file.writeLine("\tisLogic = true;");
	%file.writeLine("\tisLogicGate = true;");
	%file.writeLine("\tisLogicInput = false;\n");
	%file.writeLine("\tlogicUIName = \""@%gate.gateName@"\";");
	%file.writeLine("\tlogicUIDesc = \""@%gate.gateDesc@"\";\n");
	%file.writeLine("\tnumLogicPorts = "@%gate.portCount@";\n");

	for(%i = 0; %i < %gate.portCount; %i++)
	{
		%port = %gate.ports[%i];
		%pos = %port.portPos;
		%pos = mFloatLength(getWord(%pos, 0)*4, 0) SPC mFloatLength(getWord(%pos, 1)*4, 0) SPC mFloatLength(getWord(%pos, 2)*5, 0);

		%file.writeLine("\tlogicPortType["@%i@"] = "@%port.portType@";");
		%file.writeLine("\tlogicPortPos["@%i@"] = \""@%pos@"\";");
		%file.writeLine("\tlogicPortDir["@%i@"] = "@%port.portDir@";");
		%file.writeLine("\tlogicPortUIName["@%i@"] = \""@%port.portName@"\";");
		if(%i+1 != %gate.portCount)
			%file.writeLine("");
	}

	%file.writeLine("};\n");

	%file.writeLine("function "@%dataName@"::doLogic(%this, %obj)\n{\n\t\n}\n");
	%file.writeLine("function "@%dataName@"::Logic_onGateAdded(%this, %obj)\n{\n\t\n}\n");
	%file.writeLine("function "@%dataName@"::Logic_onInput(%this, %obj, %pos, %norm)\n{\n\t\n}");

	%file.close();
	%file.delete();
}

function GameConnection::Logic_WritePort(%this, %port, %ofile)
{
	if(!isObject(%port))
		return;
	
	%type = %port.portType;
	%dir = %port.portDir;
	%pos = vectorScale(%port.portPos, 2);
	%pos = getWord(%port.portPos, 0)*2 SPC getWord(%port.portPos, 1)*2 SPC getWord(%port.portPos, 2)*5;

	%fname = expandFileName("./gatemaker/" @ (%type ? "input2.blb":"output2.blb"));
	%file = new FileObject();
	%file.openForRead(%fname);

	while(!%file.isEOF())
	{
		%line = %file.readLine();
		
		if(%write && strpos(%line, "//end") != -1)
			break;

		if(%write)
		{
			if(strpos(%line, "UV COORDS:") != -1 && %isPos)
				%isPos = false;

			if(%isPos)
				%line = vectorAdd(%line, %pos);
			%ofile.writeLine(%line);

			if(strpos(%line, "POSITION:") != -1)
				%isPos = true;
		}

		if(strpos(%line, "//DIR " @ %dir) != -1)
			%write = true;
	}

	%file.close();
	%file.delete();
}

function GameConnection::Logic_SaveBLB(%this, %filename)
{
	%gate = %this.LGM_gate;
	if(!isObject(%gate))
		return;

	%filename = expandFileName(%filename);

	%x = %gate.width;
	%y = %gate.length;
	%z = %gate.height;

	%nQuads = 0;
	%eQuads = 0;
	%sQuads = 0;
	%wQuads = 0;
	%tQuads = 0;
	%bQuads = 0;

	%ports = %gate.portCount;
	for(%i = 0; %i < %ports; %i++)
	{
		%port = %gate.ports[%i];
		%dir = %port.portDir;
		switch(%dir)
		{
			case 1:
				%nQuad[%nQuads] = %port;
				%nQuads++;
			case 2:
				%eQuad[%eQuads] = %port;
				%eQuads++;
			case 3:
				%sQuad[%sQuads] = %port;
				%sQuads++;
			case 0:
				%wQuad[%wQuads] = %port;
				%wQuads++;
			case 4:
				%tQuad[%tQuads] = %port;
				%tQuads++;
			case 5:
				%bQuad[%bQuads] = %port;
				%bQuads++;
		}
	}

	%file = new FileObject();
	%file.openForWrite(%filename);
	%file.writeLine(%x SPC %y SPC %z);
	%file.writeLine("SPECIAL\n");

	for(%i = 0; %i < %x; %i++)
		%b = %b @ "b";

	for(%i = 0; %i < %x; %i++)
		%u = %u @ "u";

	for(%i = 0; %i < %x; %i++)
		%XX = %XX @ "X";

	for(%i = 0; %i < %x; %i++)
		%d = %d @ "d";

	for(%ia = 0; %ia < %y; %ia++)
	{
		if(%z < 2)
		{
			%file.writeLine(%b);
			%file.writeLine("");
		}

		if(%z == 2)
		{
			%file.writeLine(%u);
			%file.writeLine(%d);
			%file.writeLine("");
		}

		if(%z > 2)
		{
			%file.writeLine(%u);

			for(%ib = 0; %ib <= %z - 3; %ib++)
			{
				%file.writeLine(%XX);
			}

			%file.writeLine(%d);
			%file.writeLine("");
		}
	}

	//Collision
	%file.writeLine("1\n");
	%file.writeLine("0 0 0");
	%file.writeLine(%x SPC %y SPC %z);

	//Coverage
	%file.writeLine("COVERAGE:");
	%file.writeLine("1 : " @ %x * %y);
	%file.writeLine("1 : " @ %x * %y);
	%file.writeLine("1 : " @ %x * %z);
	%file.writeLine("1 : " @ %y * %z);
	%file.writeLine("1 : " @ %x * %z);
	%file.writeLine("1 : " @ %y * %z);

	//Top quads
	%file.writeLine("----------------top quads:");
	%file.writeLine(1+(%tQuads*5));

	%file.writeLine("");
	%file.writeLine("TEX:PRINT");
	%file.writeLine("POSITION:");
	%file.writeLine(0.5 * %x SPC 0.5 * %y SPC %z * 0.5);
	%file.writeLine(0.5 * %x SPC -0.5 * %y SPC %z * 0.5);
	%file.writeLine(-0.5 * %x SPC -0.5 * %y SPC %z * 0.5);
	%file.writeLine(-0.5 * %x SPC 0.5 * %y SPC %z * 0.5);

	%file.writeLine("UV COORDS:");
	// %file.writeLine("0" SPC %y);
	// %file.writeLine("0 0");
	// %file.writeLine(%x SPC "0");
	// %file.writeLine(%x SPC %y);
	%file.writeLine("1 0");
	%file.writeLine("1 1");
	%file.writeLine("0 1");
	%file.writeLine("0 0");

	%file.writeLine("NORMALS:");
	%file.writeLine("0 0 1");
	%file.writeLine("0 0 1");
	%file.writeLine("0 0 1");
	%file.writeLine("0 0 1");

	for(%i = 0; %i < %tQuads; %i++)
	{
		%file.writeLine("");
		%port = %tQuad[%i];
		%this.Logic_WritePort(%port, %file);
	}

	//Bottom quads
	%file.writeLine("----------------bottom quads:");

	if(%x > 1 && %y > 1)
	{
		%file.writeLine(5+(%bQuads*5));

		%file.writeLine("");
		%file.writeLine("TEX:BOTTOMLOOP");
		%file.writeLine("POSITION:");
		%file.writeLine(%x / 2 - 0.5 SPC -0.5 * %y + 0.5 SPC %z * -0.5);
		%file.writeLine(%x / 2 - 0.5 SPC %y / 2 - 0.5 SPC %z * -0.5);
		%file.writeLine(-0.5 * %x + 0.5 SPC %y / 2 - 0.5 SPC %z * -0.5);
		%file.writeLine(-0.5 * %x + 0.5 SPC -0.5 * %y + 0.5 SPC %z * -0.5);
		%file.writeLine("UV COORDS:");
		%file.writeLine("0 0");
		%file.writeLine("0" SPC %y - 1);
		%file.writeLine(%x - 1 SPC %y - 1);
		%file.writeLine(%x - 1 SPC "0");
		%file.writeLine("NORMALS:");
		%file.writeLine("0 0 -1");
		%file.writeLine("0 0 -1");
		%file.writeLine("0 0 -1");
		%file.writeLine("0 0 -1");
	}
	else
		%file.writeLine(4+(%bQuads*5));

	%file.writeLine("");
	%file.writeLine("TEX:BOTTOMEDGE");
	%file.writeLine("POSITION:");
	%file.writeLine(-0.5 * %x SPC -0.5 * %y SPC  %z * -0.5);
	%file.writeLine(0.5 * %x SPC -0.5 * %y SPC  %z * -0.5);
	%file.writeLine(0.5 * %x - 0.5 SPC -0.5 * %y + 0.5 SPC  %z * -0.5);
	%file.writeLine(-0.5 * %x + 0.5 SPC -0.5 * %y + 0.5 SPC  %z * -0.5);
	%file.writeLine("UV COORDS:");
	%file.writeLine("-0.5 0");
	%file.writeLine(%x - 0.5 SPC "0");
	%file.writeLine(%x - 1 SPC "0.5");
	%file.writeLine("0 0.5");
	%file.writeLine("NORMALS:");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");

	%file.writeLine("");
	%file.writeLine("TEX:BOTTOMEDGE");
	%file.writeLine("POSITION:");
	%file.writeLine(0.5 * %x SPC 0.5 * %y SPC %z * -0.5);
	%file.writeLine(-0.5 * %x SPC 0.5 * %y SPC %z * -0.5);
	%file.writeLine(-0.5 * %x + 0.5 SPC 0.5 * %y - 0.5 SPC %z * -0.5);
	%file.writeLine(0.5 * %x - 0.5 SPC 0.5 * %y - 0.5 SPC %z * -0.5);
	%file.writeLine("UV COORDS:");
	%file.writeLine("-0.5 0");
	%file.writeLine(%x - 0.5 SPC "0");
	%file.writeLine(%x - 1 SPC "0.5");
	%file.writeLine("0 0.5");
	%file.writeLine("NORMALS:");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");

	%file.writeLine("");
	%file.writeLine("TEX:BOTTOMEDGE");
	%file.writeLine("POSITION:");
	%file.writeLine(0.5 * %x SPC -0.5 * %y SPC %z * -0.5);
	%file.writeLine(0.5 * %x SPC 0.5 * %y SPC %z * -0.5);
	%file.writeLine(0.5 * %x - 0.5 SPC 0.5 * %y - 0.5 SPC %z * -0.5);
	%file.writeLine(0.5 * %x - 0.5 SPC -0.5 * %y + 0.5 SPC %z * -0.5);
	%file.writeLine("UV COORDS:");
	%file.writeLine("-0.5 0");
	%file.writeLine(%y - 0.5 SPC "0");
	%file.writeLine(%y - 1 SPC "0.5");
	%file.writeLine("0 0.5");
	%file.writeLine("NORMALS:");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");

	%file.writeLine("");
	%file.writeLine("TEX:BOTTOMEDGE");
	%file.writeLine("POSITION:");
	%file.writeLine(-0.5 * %x SPC 0.5 * %y SPC %z * -0.5);
	%file.writeLine(-0.5 * %x SPC -0.5 * %y SPC %z * -0.5);
	%file.writeLine(-0.5 * %x + 0.5 SPC -0.5 * %y + 0.5 SPC %z * -0.5);
	%file.writeLine(-0.5 * %x + 0.5 SPC 0.5 * %y - 0.5 SPC %z * -0.5);
	%file.writeLine("UV COORDS:");
	%file.writeLine("-0.5 0");
	%file.writeLine(%y - 0.5 SPC "0");
	%file.writeLine(%y - 1 SPC "0.5");
	%file.writeLine("0 0.5");
	%file.writeLine("NORMALS:");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");
	%file.writeLine("0 0 -1");

	for(%i = 0; %i < %bQuads; %i++)
	{
		%file.writeLine("");
		%port = %bQuad[%i];
		%this.Logic_WritePort(%port, %file);
	}

	//North quad
	%file.writeLine("----------------north quads:");
	%file.writeLine(1+(%nQuads*5));

	%file.writeLine("");
	%file.writeLine("TEX:SIDE");
	%file.writeLine("POSITION:");
	%file.writeLine(-0.5 * %x SPC %y / 2 SPC %z * 0.5);  //-0.5 0.5
	%file.writeLine(-0.5 * %x SPC %y / 2 SPC %z * -0.5); //-0.5 0.5
	%file.writeLine(%x / 2 SPC %y / 2 SPC %z * -0.5);       //0.5 0.5
	%file.writeLine(%x / 2 SPC %y / 2 SPC %z * 0.5);        //0.5 0.5

	%file.writeLine("UV COORDS:");
	%file.writeLine((-((11 - ((11 / %x) * 2)) / 512) + 1) SPC (11 - ((11 / %z) * 5)) / 512);
	%file.writeLine((-((11 - ((11 / %x) * 2)) / 512) + 1) SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));
	%file.writeLine((11 - ((11 / %x) * 2)) / 512 SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));
	%file.writeLine((11 - ((11 / %x) * 2)) / 512 SPC (11 - ((11 / %z) * 5)) / 512);

	%file.writeLine("NORMALS:");
	%file.writeLine("0 1 0");
	%file.writeLine("0 1 0");
	%file.writeLine("0 1 0");
	%file.writeLine("0 1 0");

	for(%i = 0; %i < %nQuads; %i++)
	{
		%file.writeLine("");
		%port = %nQuad[%i];
		%this.Logic_WritePort(%port, %file);
	}

	//East quad
	%file.writeLine("----------------east quads:");
	%file.writeLine(1+(%eQuads*5));

	%file.writeLine("");
	%file.writeLine("TEX:SIDE");
	%file.writeLine("POSITION:");
	%file.writeLine(%x / 2 SPC %y / 2 - %y SPC %z * 0.5);
	%file.writeLine(%x / 2 SPC %y / 2 SPC %z * 0.5);
	%file.writeLine(%x / 2 SPC %y / 2 SPC %z * -0.5);
	%file.writeLine(%x / 2 SPC %y / 2 - %y SPC %z * -0.5);

	%file.writeLine("UV COORDS:");
	%file.writeLine((11 - ((11 / %y) * 2)) / 512 SPC (11 - ((11 / %z) * 5)) / 512);
	%file.writeLine((-((11 - ((11 / %y) * 2)) / 512) + 1) SPC (11 - ((11 / %z) * 5)) / 512);
	%file.writeLine((-((11 - ((11 / %y) * 2)) / 512) + 1) SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));
	%file.writeLine((11 - ((11 / %y) * 2)) / 512 SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));

	%file.writeLine("NORMALS:");
	%file.writeLine("1 0 0");
	%file.writeLine("1 0 0");
	%file.writeLine("1 0 0");
	%file.writeLine("1 0 0");

	for(%i = 0; %i < %eQuads; %i++)
	{
		%file.writeLine("");
		%port = %eQuad[%i];
		%this.Logic_WritePort(%port, %file);
	}

	//South quad
	%file.writeLine("----------------south quads:");
	%file.writeLine(1+(%sQuads*5));

	%file.writeLine("");
	%file.writeLine("TEX:SIDE");
	%file.writeLine("POSITION:");
	%file.writeLine(%x / 2 SPC %y / 2 - %y SPC %z * 0.5);
	%file.writeLine(%x / 2 SPC %y / 2 - %y SPC %z * -0.5);
	%file.writeLine(%x / 2 - %x SPC %y / 2 - %y SPC %z * -0.5);
	%file.writeLine(%x / 2 - %x SPC %y / 2 - %y SPC %z * 0.5);

	%file.writeLine("UV COORDS:");
	%file.writeLine((-((11 - ((11 / %x) * 2)) / 512) + 1) SPC (11 - ((11 / %z) * 5)) / 512);
	%file.writeLine((-((11 - ((11 / %x) * 2)) / 512) + 1) SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));
	%file.writeLine((11 - ((11 / %x) * 2)) / 512 SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));
	%file.writeLine((11 - ((11 / %x) * 2)) / 512 SPC (11 - ((11 / %z) * 5)) / 512);

	%file.writeLine("NORMALS:");
	%file.writeLine("0 -1 0");
	%file.writeLine("0 -1 0");
	%file.writeLine("0 -1 0");
	%file.writeLine("0 -1 0");

	for(%i = 0; %i < %sQuads; %i++)
	{
		%file.writeLine("");
		%port = %sQuad[%i];
		%this.Logic_WritePort(%port, %file);
	}

	//West quad
	%file.writeLine("----------------west quads:");
	%file.writeLine(1+(%wQuads*5));

	%file.writeLine("");
	%file.writeLine("TEX:SIDE");
	%file.writeLine("POSITION:");
	%file.writeLine(%x / 2 - %x SPC %y / 2 - %y SPC %z * -0.5);
	%file.writeLine(-0.5 * %x SPC %y / 2 SPC %z * -0.5); //-0.5 0.5
	%file.writeLine(-0.5 * %x SPC %y / 2 SPC %z * 0.5); //-0.5 0.5
	%file.writeLine(%x / 2 - %x SPC %y / 2 - %y SPC %z * 0.5);

	%file.writeLine("UV COORDS:");
	%file.writeLine((-((11 - ((11 / %y) * 2)) / 512) + 1) SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));
	%file.writeLine((11 - ((11 / %y) * 2)) / 512 SPC (-((11 - ((11 / %z) * 5)) / 512) + 1));
	%file.writeLine((11 - ((11 / %y) * 2)) / 512 SPC (11 - ((11 / %z) * 5)) / 512);
	%file.writeLine((-((11 - ((11 / %y) * 2)) / 512) + 1) SPC (11 - ((11 / %z) * 5)) / 512);

	%file.writeLine("NORMALS:");
	%file.writeLine("-1 0 0");
	%file.writeLine("-1 0 0");
	%file.writeLine("-1 0 0");
	%file.writeLine("-1 0 0");

	for(%i = 0; %i < %wQuads; %i++)
	{
		%file.writeLine("");
		%port = %wQuad[%i];
		%this.Logic_WritePort(%port, %file);
	}

	//Omni quads
	%file.writeLine("----------------omni quads:");
	%file.writeLine("0");
	%file.close();
	%file.delete();
}

package Illogic_GateMaker
{
	function Armor::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%slot == 0 && %val)
		{
			if(isObject(%client = %obj.client))
			{
				if(isEventPending(%client.LGM_moveSched))
					cancel(%client.LGM_moveSched);

				if(%client.LGM_placing)
				{
					%client.Logic_AddGatePort();
					return;
				}

				%eye = %obj.getEyePoint();
				%vec = %obj.getEyeVector();
				%ray = containerRayCast(%eye, vectorAdd(%eye, vectorScale(%vec, 5*getWord(%obj.getScale(), 2))), $TypeMasks::StaticObjectType);
				if(isObject(%hit = firstWord(%ray)))
				{
					if(%hit.getClassName() $= "fxPlane")
						return;

					if(%hit.getDatablock().getName() $= "Illogic_BrickData")
					{
						%gateName = %hit.gateName;
						%gateDesc = %hit.gateDesc;

						%hitPos = getWords(%ray, 1, 3);
						%hx = getWord(%hitPos, 0);
						%hy = getWord(%hitPos, 1);
						%hz = getWord(%hitPos, 2);

						%hitNorm = vectorNormalize(getWords(%ray, 4, 6));
						%nx = getWord(%hitNorm, 0);
						if(stripos(%nx, "e") != -1)
							%nx = 0;
						%ny = getWord(%hitNorm, 1);
						if(stripos(%ny, "e") != -1)
							%ny = 0;
						%nz = getWord(%hitNorm, 2);
						if(stripos(%nz, "e") != -1)
							%nz = 0;

						%hitNorm = %nx SPC %ny SPC %nz;

						%rotDir[0] = "-1 0 0";
						%rotDir[1] = "0 1 0";
						%rotDir[2] = "1 0 0";
						%rotDir[3] = "0 -1 0";
						%rotDir[4] = "0 0 1";
						%rotDir[5] = "0 0 -1";

						%bestDist = -1;
						%ports = %hit.portCount;
						for(%i = 0; %i < %ports; %i++)
						{
							%port = %hit.ports[%i];
							if(%rotDir[%port.portDir] $= %hitNorm)
							{
								%ppos = %port.getPosition();
								%xx = %hx-getWord(%ppos, 0);
								%yy = %hy-getWord(%ppos, 1);
								%zz = %hz-getWord(%ppos, 2);
								%hdistSqr = (%xx*%xx)+(%yy*%yy);
								%vdistSqr = %zz*%zz;
								%distSqr = %hdistSqr + %vdistSqr;
								if((%vdistSqr < 0.01 && %hdistSqr < 0.125) && (%distSqr < %bestDist || %bestDist == -1))
								{
									%bestDist = %distSqr;
									%bestPort = %port;
									%bestIdx = %i;
								}
							}
						}

						if(%bestDist == -1)
						{
							%client.centerPrint("<font:consolas:20>\c6"@%gateName NL "\c6"@%gateDesc, 3);
							return;
						}
						
						%type = $LBC::Ports::Type[%bestPort];
						%name = (%bestPort.portName $= "") ? "Port " @ %bestIdx : %bestPort.portName;
						%desc = %bestPort.portDesc;

						if(trim(%desc) !$= "")
							%text = "\c6"@%gateName NL "\c5Port #: \c6"@%bestIdx NL "\c5Port Name: \c6"@%name NL "\c6"@%desc;
						else
							%text = "\c6"@%gateName NL "\c5Port #: \c6"@%bestIdx NL "\c5Port Name: \c6"@%name;

						if(%client.LGM_gate == %hit)
							%text = %text NL "\c3/lSetPortName # name\c6 - Set the port name";
						%client.centerPrint("<font:consolas:20>"@%text, 3);
					}
				}
			}
		}
	}

	function Observer::onTrigger(%this, %obj, %slot, %val)
	{
		parent::onTrigger(%this, %obj, %slot, %val);
		if(%slot == 0 && %val)
		{
			if(isObject(%client = %obj.getControllingClient()))
			{
				if(isEventPending(%client.LGM_moveSched))
					cancel(%client.LGM_moveSched);

				if(%client.LGM_placing)
				{
					%client.Logic_AddGatePort();
					return;
				}

				%eye = %obj.getEyePoint();
				%vec = %obj.getEyeVector();
				%ray = containerRayCast(%eye, vectorAdd(%eye, vectorScale(%vec, 5*getWord(%obj.getScale(), 2))), $TypeMasks::StaticObjectType);
				if(isObject(%hit = firstWord(%ray)))
				{
					if(%hit.getClassName() $= "fxPlane")
						return;

					if(%hit.getDatablock().getName() $= "Illogic_BrickData")
					{
						%gateName = %hit.gateName;
						%gateDesc = %hit.gateDesc;

						%hitPos = getWords(%ray, 1, 3);
						%hx = getWord(%hitPos, 0);
						%hy = getWord(%hitPos, 1);
						%hz = getWord(%hitPos, 2);

						%hitNorm = vectorNormalize(getWords(%ray, 4, 6));
						%nx = getWord(%hitNorm, 0);
						if(stripos(%nx, "e") != -1)
							%nx = 0;
						%ny = getWord(%hitNorm, 1);
						if(stripos(%ny, "e") != -1)
							%ny = 0;
						%nz = getWord(%hitNorm, 2);
						if(stripos(%nz, "e") != -1)
							%nz = 0;

						%hitNorm = %nx SPC %ny SPC %nz;

						%rotDir[0] = "-1 0 0";
						%rotDir[1] = "0 1 0";
						%rotDir[2] = "1 0 0";
						%rotDir[3] = "0 -1 0";
						%rotDir[4] = "0 0 1";
						%rotDir[5] = "0 0 -1";

						%bestDist = -1;
						%ports = %hit.portCount;
						for(%i = 0; %i < %ports; %i++)
						{
							%port = %hit.ports[%i];
							if(%rotDir[%port.portDir] $= %hitNorm)
							{
								%ppos = %port.getPosition();
								%xx = %hx-getWord(%ppos, 0);
								%yy = %hy-getWord(%ppos, 1);
								%zz = %hz-getWord(%ppos, 2);
								%hdistSqr = (%xx*%xx)+(%yy*%yy);
								%vdistSqr = %zz*%zz;
								%distSqr = %hdistSqr + %vdistSqr;
								if((%vdistSqr < 0.01 && %hdistSqr < 0.125) && (%distSqr < %bestDist || %bestDist == -1))
								{
									%bestDist = %distSqr;
									%bestPort = %port;
									%bestIdx = %i;
								}
							}
						}

						if(%bestDist == -1)
						{
							%client.centerPrint("<font:consolas:20>\c6"@%gateName NL "\c6"@%gateDesc, 3);
							return;
						}
						
						%type = $LBC::Ports::Type[%bestPort];
						%name = (%bestPort.portName $= "") ? "Port " @ %bestIdx : %bestPort.portName;
						%desc = %bestPort.portDesc;

						if(trim(%desc) !$= "")
							%text = "\c6"@%gateName NL "\c5Port #: \c6"@%bestIdx NL "\c5Port Name: \c6"@%name NL "\c6"@%desc;
						else
							%text = "\c6"@%gateName NL "\c5Port #: \c6"@%bestIdx NL "\c5Port Name: \c6"@%name;

						if(%client.LGM_gate == %hit)
							%text = %text NL "\c3/lSetPortName # name\c6 - Set the port name";
						%client.centerPrint("<font:consolas:20>"@%text, 3);
					}
				}
			}
		}
	}

	function GameConnection::onClientLeaveGame(%this)
	{
		%this.Logic_DeleteGate();
		parent::onClientLeaveGame(%this);
	}
};
activatePackage("Illogic_GateMaker");
