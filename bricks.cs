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
exec("./bricks/gates/diode.cs");
exec("./bricks/gates/NOT.cs");
exec("./bricks/gates/OR.cs");
exec("./bricks/gates/AND.cs");
exec("./bricks/gates/NOR.cs");
exec("./bricks/gates/NAND.cs");
exec("./bricks/gates/XOR.cs");
exec("./bricks/gates/XNOR.cs");

	//Vertical
exec("./bricks/gates/verticalDiode.cs");
exec("./bricks/gates/verticalNOT.cs");


//Bus
exec("./bricks/bus/3BitEnabler.cs");
exec("./bricks/bus/8BitEnabler.cs");
exec("./bricks/bus/3BitDFlipFlop.cs");
exec("./bricks/bus/4bitDFlipFlop.cs"); //-make flip flop, move to bus
exec("./bricks/bus/8BitDFlipFlop.cs");

//Inputs
exec("./bricks/inputs/switch.cs");
exec("./bricks/inputs/button.cs");
exec("./bricks/inputs/eventgate.cs");

//Math
	//Addition
exec("./bricks/math/HalfAdder.cs");
exec("./bricks/math/FullAdder.cs");
exec("./bricks/math/8bitAdder.cs");

	//Subtraction
exec("./bricks/math/HalfSubtractor.cs");
exec("./bricks/math/FullSubtractor.cs");
exec("./bricks/math/8bitSubtractor.cs");

	//Multiplication
exec("./bricks/math/8bitMultiplier.cs");

	//Division
exec("./bricks/math/8bitDivisor.cs");

	//Other weird shit
//exec("./bricks/4bitComparator.cs");

//Memory
exec("./bricks/memory/DFlipFlop.cs");
exec("./bricks/memory/256ByteRAM.cs");
exec("./bricks/memory/16bit128KiBRAM.cs");
 //-make flip flop

//Chips
exec("./bricks/chips/Enabler.cs"); //-make legacy version, replace with 1x4x1 (use 4 bit latch blb)
exec("./bricks/chips/Shifter.cs");
exec("./bricks/chips/4bitDecoder.cs");
exec("./bricks/chips/4bitEncoder.cs");
exec("./bricks/chips/8bitComparator.cs");
exec("./bricks/chips/7segdecoder.cs");
exec("./bricks/chips/LegacyEnabler.cs");
//exec("./bricks/chips/BinarytoBCD.cs"); //Debating on implementing BCD stuff

//Special
exec("./bricks/special/Clock.cs");
exec("./bricks/special/bridge.cs");
exec("./bricks/special/TextBrick.cs");
exec("./bricks/special/pixel.cs");
exec("./bricks/special/SpecialScreenMemory1.cs");
