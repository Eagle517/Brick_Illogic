exec("./bricks/wires.cs");

package IllogicalBricks
{
	function fxDTSBrickData::onPlant(%this, %obj)
	{
		parent::onPlant(%this, %obj);
		if(%this.isLogic)
		{
			if(%this.isLogicWire)
				Logic_AddWire(%obj);
			else if(%this.isLogicComp)
				Logic_AddComp(%obj);
		}
	}

	function fxDTSBrickData::onLoadPlant(%this, %obj)
	{
		parent::onLoadPlant(%this, %obj);
		if(%this.isLogic)
		{
			if(%this.isLogicWire)
				Logic_AddWire(%obj);
			else if(%this.isLogicComp)
				Logic_AddComp(%obj);
		}
	}

	function fxDTSBrickData::onRemove(%this, %obj)
	{
		if(%this.isLogic)
		{
			if(%this.isLogicWire)
				Logic_RemoveWire(%obj);
			else if(%this.isLogicComp)
				Logic_RemoveComp(%obj);
		}
		parent::onRemove(%this, %obj);
	}
};
activatePackage("IllogicalBricks");
