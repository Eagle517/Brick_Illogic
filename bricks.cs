// registerInputEvent("fxDTSBrick", "onPowerChange", "Self fxDTSBrick" TAB "Client GameConnection" TAB "Player Player", 0);
// registerInputEvent("fxDTSBrick", "onPowerOn", "Self fxDTSBrick" TAB "Client GameConnection" TAB "Player Player", 0);
// registerInputEvent("fxDTSBrick", "onPowerOff", "Self fxDTSBrick" TAB "Client GameConnection" TAB "Player Player", 0);
// registerOutputEvent(fxDTSBrick, "setPower", "string 30 50", 1);

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
			{
				Logic_RemoveWire(%obj);
				//deleteVariables("$LBC::Bricks::isWire"@%obj);
				//deleteVariables("$LBC::Bricks::Neighbor"@%obj@"*");
				//deleteVariables("$LBC::Bricks::NeighborIDX"@%obj@"*");
				//deleteVariables("$LBC::Bricks::NumNeighbors"@%obj@"*");
				//deleteVariables("$LBC::Wires::Group"@%obj);
				//deleteVariables("$LBC::Wires::Port"@%obj@"*");
				//deleteVariables("$LBC::Wires::PortIDX"@%obj@"*");
				//deleteVariables("$LBC::Wires::PortCount"@%obj);
			}
			else if($LBC::Bricks::isGate[%obj])
			{
				Logic_RemoveGate(%obj);
				//deleteVariables("$LBC::Bricks::isGate"@%obj);
				//deleteVariables("$LBC::Bricks::PortCount"@%obj);
				//deleteVariables("$LBC::Bricks::Port"@%obj@"*");
				//deleteVariables("$LBC::Bricks::PortIDX"@%obj@"*");
				//deleteVariables("$LBC::Bricks::Datablock"@%obj);
			}

			//deleteVariables("$LBC::Bricks::isLogic"@%obj);
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
exec("./bricks/NOT.cs");
exec("./bricks/OR.cs");
exec("./bricks/AND.cs");
exec("./bricks/NOR.cs");
exec("./bricks/NAND.cs");
exec("./bricks/XOR.cs");
exec("./bricks/XNOR.cs");
exec("./bricks/diode.cs");

//Inputs
exec("./bricks/switch.cs");
exec("./bricks/button.cs");

//Memory
exec("./bricks/DFlipFlop.cs");

//Special
exec("./bricks/bridge.cs");
