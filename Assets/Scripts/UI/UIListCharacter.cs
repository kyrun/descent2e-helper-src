using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIListCharacter : UIListScriptableObj<CharacterDef>
{
    [SerializeField] UIListClass _classSelect = default;

	protected override void OnButtonPress(CharacterDef character)
    {
        _classSelect.Setup(character);
	}

	protected override void Sort(List<CharacterDef> list)
	{
		list.OrderBy(def => def.Archetype).ThenBy(def => def.name);
	}

	protected override string ItemName(CharacterDef def)
	{
		return def.name + " (" + def.Archetype + ")";
	}
}
