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
	%height = mClamp(%height, 1, 192);

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
		position = vectorAdd(%pos, "0 0 " @ %height/2+0.5);
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
				if(vectorSub(%gate.getPosition(), %pos) $= %gport.portPos && %newNorm $= %gport.lastNorm)
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
	
	%rotDir["1 0 0"] = 0;
	%rotDir["0 1 0"] = 1;
	%rotDir["-1 0 0"] = 2;
	%rotDir["0 -1 0"] = 3;
	%rotDir["0 0 1"] = 4;
	%rotDir["0 0 -1"] = 5;

	%port.portDir = %rotDir[%port.lastNorm];
	%port.portPos = vectorSub(%gate.getPosition(), %port.getPosition());

	%gate.ports[%gate.portCount] = %port;
	%gate.portCount++;
	%this.LGM_port = 0;
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
						%client.centerPrint("<font:consolas:20>\c6"@%gateName NL "\c6"@%gateDesc, 3);
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
