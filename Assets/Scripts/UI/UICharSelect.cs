using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharSelect : UIListButton<CharacterDef>
{
    [SerializeField] UIClassSelect _classSelect = default;

	protected override void OnButtonPress(CharacterDef character)
    {
        _classSelect.Setup(character);
	}

	protected override string ItemName(CharacterDef def)
	{
		return def.name + " (" + def.Archetype + ")";
	}
}
