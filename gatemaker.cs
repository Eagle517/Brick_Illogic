datablock StaticShapeData(Illogic_BrickData)
{
	shapeFile = "./cube.dts";
};

datablock StaticShapeData(Illogic_BrickInputData)
{
	shapeFile = "./input.dts";
};

datablock StaticShapeData(Illogic_BrickInputVertData)
{
	shapeFile = "./input_vert.dts";
};

datablock StaticShapeData(Illogic_BrickOutputData)
{
	shapeFile = "./output.dts";
};

datablock StaticShapeData(Illogic_BrickOutputVertData)
{
	shapeFile = "./output_vert.dts";
};

datablock StaticShapeData(Illogic_BrickAxisData)
{
	shapeFile = "./axis2.dts";
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

		//%pos = mFloor(getWord(%pos, 0)*2+0.5)/2 SPC mFloor(getWord(%pos, 1)*2+0.5)/2 SPC ((%z = (mFloor(getWord(%pos, 2)*2+0.5)/2)+(%height*0.2)/2) < 0.1 ? 0.1:%z);
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

function serverCmdlAddPort(%client, %type)
{
	if(isObject(%gate = %client.LGM_gate) && !isObject(%client.LGM_port) && !isEventPending(%client.LGM_moveSched))
	{
		if(%type == 1 || %type $= "in" || %type $= "input")
			%type = 1;
		else if(%type $= "0" || %type $= "out" || %type $= "output")
			%type = 0;
		else
		{
			messageClient(%client, '', '\c6Invalid port type. \c3/lAddPort in/out');
			return;
		}

		%client.Logic_AddGatePort(%type);
	}
}

function serverCmdlCancel(%client)
{
	if(isObject(%port = %client.LGM_port))
		%port.delete();
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

function serverCmdlSaveGate(%client)
{
	if(isObject(%gate = %client.LGM_gate))
	{
	}
}

function serverCmdlDeleteGate(%client)
{
	%client.Logic_DeleteGate();
}

function GameConnection::Logic_MakeGate(%this, %pos, %wid, %len, %height)
{
	if(!%this.isAdmin)
		return;
	if(isObject(%this.LGM_gate))
	{
		%this.centerPrint("You must delete/save your first gate in order to make another one.", 3);
		return;
	}

	if(%wid > %len)
	{
		%a = %len;
		%len = %wid;
		%wid = %a;
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
	messageClient(%this, '', '\c3/lMoveGate\c6 - toggles moving the logic gate in world space');
	messageClient(%this, '', '\c3/lSetGateName\c6 - sets the name of the gate');
	messageClient(%this, '', '\c3/lSetGateDesc\c6 - sets the description of the gate');
	messageClient(%this, '', '\c3/lAddPort\c6 - adds a port to the gate');
	messageClient(%this, '', '\c3/lSaveGate\c6 - saves a .blb and .cs to config/server/logicgates/[blid]/');
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
	if(isObject(%this.LGM_port))
		%this.LGM_port.delete();

	%shape = new StaticShape()
	{
		datablock = %type ? "Illogic_BrickInputData":"Illogic_BrickOutputData";
		position = "0 0 0";
		portType = %type;
	};
	missionCleanup.add(%shape);
	%this.LGM_port = %shape;
	%this.Logic_MoveGatePort();
	messageClient(%this, '', '\c6Click to place the port.');
	messageClient(%this, '', '\c3/lCancel\c6 to delete the port.');
}

function GameConnection::Logic_MoveGatePort(%this)
{
	cancel(%this.LGM_movePortSched);
	%gate = %this.LGM_gate;
	%port = %this.LGM_port;
	%control = %this.getControlObject();

	if(!isObject(%port))
		return;
	if(!isObject(%gate) || !isObject(%control) || ((%class = %control.getClassName()) !$= "Camera" && %class !$= "Player"))
	{
		%port.delete();
		return;
	}

	%this.LGM_movePortSched = %this.schedule(1, "Logic_MoveGatePort");

	%eye = %control.getEyePoint();
	%vec = %control.getEyeVector();
	%end = vectorAdd(%eye, vectorScale(%vec, 5*getWord(%control.getScale(), 2)));
	%ray = containerRayCast(%eye, %end, $TypeMasks::StaticObjectType, %port);
	if(isObject(%hit = firstWord(%ray)) && %hit == %gate)
	{
		%pos = mFloor(getWord(%ray, 1)*2)/2 SPC mFloor(getWord(%ray, 2)*2)/2 SPC (mFloor(getWord(%ray, 3)*5)/5)+0.1;
		%norm = getWords(%ray, 4, 6);
		if(mAbs(%x = getWord(%norm, 0)) == 1)
		{
			if(%port.portType == 0)
				%data = "Illogic_BrickOutputData";
			else if(%port.portType == 1)
				%data = "Illogic_BrickInputData";

			%offset = "0 " @ 0.25 @ " 0";
			%euler = "0 0" SPC -90*%x-90;
			%newNorm = %x SPC "0 0";
		}
		else if(mAbs(%y = getWord(%norm, 1)) == 1)
		{
			if(%port.portType == 0)
				%data = "Illogic_BrickOutputData";
			else if(%port.portType == 1)
				%data = "Illogic_BrickInputData";

			%euler = "0 0" SPC -90*%y;
			%offset = 0.25 SPC "0 0";
			%newNorm = "0" SPC %y SPC "0";
		}
		else if(mAbs(%z = getWord(%norm, 2)) == 1)
		{
			if(%port.portType == 0)
				%data = "Illogic_BrickOutputVertData";
			else if(%port.portType == 1)
				%data = "Illogic_BrickInputVertData";

			%euler = 180-(90*(%z+1)) SPC "0 0";
			%offset = "0.25 0.25 -0.1";
			%newNorm = "0 0" SPC %z;
		}

		%pos = vectorAdd(%pos, %offset);
		%rot = getWords(MatrixCreateFromEuler(vectorScale(%euler, $pi/180)), 3, 6);
		if(%pos !$= %port.getPosition() || %newNorm !$= %port.lastNorm)
		{
			for(%i = 0; %i < %gate.portCount; %i++)
			{
				%gport = %gate.ports[%i];
				if(vectorSub(%pos, %gate.getPosition()) $= %gport.portPos && %newNorm $= %gport.lastNorm)
					return;
			}

			if(%port.getDatablock() != nameToID(%data))
				%port.setDatablock(%data);
			%port.lastNorm = %newNorm;
			%port.setTransform(%pos SPC %rot);
		}
	}
}

function GameConnection::Logic_FinalizePort(%this)
{
	cancel(%this.LGM_movePortSched);

	%gate = %this.LGM_gate;
	%port = %this.LGM_port;

	if(!isObject(%gate) || !isObject(%port))
		return;

	if(%port.getPosition() $= "0 0 0")
	{
		%port.delete();
		return;
	}
	
	%rotDir["-1 0 0"] = 0;
	%rotDir["0 1 0"] = 1;
	%rotDir["1 0 0"] = 2;
	%rotDir["0 -1 0"] = 3;
	%rotDir["0 0 1"] = 4;
	%rotDir["0 0 -1"] = 5;

	%port.portDir = %rotDir[%port.lastNorm];
	%port.portPos = vectorSub(%port.getPosition(), %gate.getPosition());

	%gate.ports[%gate.portCount] = %port;
	%gate.portCount++;
	%this.LGM_port = 0;
}

function GameConnection::Logic_WritePort(%this, %port, %ofile)
{
	if(!isObject(%port))
		return;
	
	%type = %port.portType;
	%dir = %port.portDir;
	%pos = %port.portPos;

	%file = new FileObject();
	%file.openForRead("./gatemaker/" @ (%type ? "input.blb":"output.blb"));

	while(%file.isEOF())
	{
		%line = %file.readLine();
		
		if(strpos(%line, "//end") != -1 && %write)
			%write = false;

		if(strpos(%line, "UV COORDS:") != -1 && %isPos)
			%isPos = false;

		if(%write)
		{
			if(%isPos)
				%line = vectorAdd(%line, %pos);
			%ofile.writeLine(%line);
		}

		if(strpos(%line, "//DIR " @ %dir) != -1)
			%write = true;

		if(strpos(%line, "POSITION:") != -1 && %write)
			%isPos = true;
	}
}

function GameConnection::Logic_SaveBLB(%this, %filename)
{
	%gate = %this.LGM_gate;
	if(!isObject(%gate) || %gate.portCount < 1)
		return;

	%filename = expandFileName(%filename);

	%x = %this.width;
	%y = %this.length;
	%z = %this.height;

	%nQuads = 0;
	%eQuads = 0;
	%sQuads = 0;
	%wQuads = 0;
	%tQuads = 0;
	%bQuads = 0;

	%ports = %gate.portCount;
	for(%i = 0; %i < %ports; %i++)
	{
		%port = %this.ports[%i];
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
	%file.writeLine("0 1");
	%file.writeLine("0 0");
	%file.writeLine("1 0");
	%file.writeLine("1 1");

	%file.writeLine("NORMALS:");
	%file.writeLine("0 0 1");
	%file.writeLine("0 0 1");
	%file.writeLine("0 0 1");
	%file.writeLine("0 0 1");

	for(%i = 0; %i < %tQuads; %i++)
	{
		%file.writeLine("");
		%port = %tQuad[%i];
		%port.writeBLBData(%file, 0);
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
		%file.writeLine(4*(%bQuads*5));

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
		%port.writeBLBData(%file, 0);
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
		%port.writeBLBData(%file, 0);
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
		%port.writeBLBData(%file, 1);
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
		%port.writeBLBData(%file, 2);
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
		%port.writeBLBData(%file, 3);
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
				if(isObject(%client.LGM_port))
				{
					%client.Logic_FinalizePort();
					return;
				}

				if(isEventPending(%client.LGM_moveSched))
					cancel(%client.LGM_moveSched);

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
						//talk(%hitNorm);

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
							%client.bottomPrint(%nx SPC %hitNorm @" | "@ %rotDir[%port.portDir], 5);

							if(%rotDir[%port.portDir] $= %hitNorm)
							{
								%ppos = %port.getPosition();
								%xx = %hx-getWord(%ppos, 0);
								%yy = %hy-getWord(%ppos, 1);
								%zz = %hz-getWord(%ppos, 2);
								%hdistSqr = (%xx*%xx)+(%yy*%yy);
								%vdistSqr = %zz*%zz;
								%distSqr = %hdistSqr + %vdistSqr;
								//%client.bottomPrint(%hitNorm NL %hdistSqr @ " | " @ %vdistSqr @ " | "  @ %distSqr, 5);
								if((%vdistSqr < 0.01 && %hdistSqr < 0.0649) && (%distSqr < %bestDist || %bestDist == -1))
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
				if(isObject(%client.LGM_port))
				{
					%client.Logic_FinalizePort();
					return;
				}

				if(isEventPending(%client.LGM_moveSched))
					cancel(%client.LGM_moveSched);

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
						%client.centerPrint("<font:consolas:20>\c6"@%gateName NL "\c6"@%gateDesc, 3);
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
