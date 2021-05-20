using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharSelect : UIListSelect<CharacterDef>
{
    [SerializeField] UIClassSelect _classSelect = default;

	protected override void OnButtonPress(CharacterDef character)
    {
        _classSelect.Setup(character);
	}
}
