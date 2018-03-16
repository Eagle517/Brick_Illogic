registerInputEvent("fxDTSBrick", "onPowerChange", "Self fxDTSBrick", 0);
registerInputEvent("fxDTSBrick", "onPowerOn", "Self fxDTSBrick", 0);
registerInputEvent("fxDTSBrick", "onPowerOff", "Self fxDTSBrick", 0);
registerOutputEvent("fxDTSBrick", "setInputState", "bool 0");

function fxDTSBrick::onPowerChange(%this)
{
	$InputTarget_["Self"] = %this;
	%this.processInputEvent("onPowerChange");
}

function fxDTSBrick::onPowerOn(%this)
{
	$InputTarget_["Self"] = %this;
	%this.processInputEvent("onPowerOn");
}

function fxDTSBrick::onPowerOff(%this)
{
	$InputTarget_["Self"] = %this;
	%this.processInputEvent("onPowerOff");
}

function fxDTSBrick::setInputState(%this, %state, %client)
{
	if(%this.getDatablock().isLogicInput)
	{
		%ports = $LBC::Bricks::PortCount[%this];
		for(%i = 0; %i < %ports; %i++)
		{
			if($LBC::Ports::Type[$LBC::Bricks::Port[%this, %i]] == 0)
				%this.Logic_SetOutput(%i, %state);
		}

		if(%state)
			%this.setColorFX(3);
		else
			%this.setColorFX(0);
	}
}

package IllogicBricks
{
	function fxDTSBrickData::onPlant(%this, %obj)
	{
		parent::onPlant(%this, %obj);
		if(%this.isLogic)
		{
			if(%this.isLogicWire)
				Logic_AddWire(%obj);
			else if(%this.isLogicGate)
				Logic_AddGate(%obj);
		}
	}

	function fxDTSBrickData::onLoadPlant(%this, %obj)
	{
		parent::onLoadPlant(%this, %obj);
		if(%this.isLogic)
		{
			if(%this.isLogicWire)
				Logic_AddWire(%obj);
			else if(%this.isLogicGate)
				Logic_AddGate(%obj);
		}
	}

	function fxDTSBrickData::onDeath(%this, %obj)
	{
		if(%this.isLogic)
		{
			if($LBC::Bricks::isWire[%obj])
				Logic_RemoveWire(%obj);
			else if($LBC::Bricks::isGate[%obj])
				Logic_RemoveGate(%obj);
		}
		parent::onDeath(%this, %obj);
	}

	function fxDTSBrickData::onRemove(%this, %obj)
	{
		if(%this.isLogic)
		{
			if($LBC::Bricks::isWire[%obj])
				Logic_RemoveWire(%obj);
			else if($LBC::Bricks::isGate[%obj])
				Logic_RemoveGate(%obj);
		}
		parent::onRemove(%this, %obj);
	}

	function fxDTSBrickData::onColorChange(%data, %obj)
	{
		parent::onColorChange(%data, %obj);
		if($LBC::Bricks::isLogic[%obj] && $LBC::Bricks::isWire[%obj])
		{
			%group = $LBC::Wires::Group[%obj];
			if(%group != -1)
			{
				Logic_RemoveWire(%obj);
				Logic_RefreshWireGroup(%group, 1);
				Logic_AddWire(%obj);
			}
		}
	}
};
activatePackage("IllogicBricks");

//Wires
exec("./bricks/wires.cs");

//Gates
exec("./bricks/diode.cs");
exec("./bricks/NOT.cs");
exec("./bricks/OR.cs");
exec("./bricks/AND.cs");
exec("./bricks/NOR.cs");
exec("./bricks/NAND.cs");
exec("./bricks/XOR.cs");
exec("./bricks/XNOR.cs");

//Inputs
exec("./bricks/switch.cs");
exec("./bricks/button.cs");
exec("./bricks/eventgate.cs");

//Math
exec("./bricks/4bitComparator.cs");
exec("./bricks/8bitMultiplier.cs");
exec("./bricks/8bitDivisor.cs");

//Memory
exec("./bricks/DFlipFlop.cs");
exec("./bricks/4bitDLatch.cs");
exec("./bricks/256ByteRAM.cs");

//Chips
exec("./bricks/Enabler.cs");
exec("./bricks/4bitDecoder.cs");
exec("./bricks/4bitEncoder.cs");

//Special
exec("./bricks/bridge.cs");
exec("./bricks/pixel.cs");
exec("./bricks/TextBrick.cs");