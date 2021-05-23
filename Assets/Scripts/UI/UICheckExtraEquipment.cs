using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ButtonOkAndNext))]
public class UICheckExtraEquipment : MonoBehaviour
{
	ButtonOkAndNext _btn;

	void Awake()
	{
		_btn = GetComponent<ButtonOkAndNext>();
	}

	void OnEnable()
	{
		if (Game.PlayerCharacter != null)
		{
			bool hasUnequipped = false;
			foreach (var kvp in Game.PlayerCharacter.Items)
			{
				if (!kvp.Value)
				{
					hasUnequipped = true;
					break;
				}
			}
			if (!hasUnequipped) _btn.Next();
		}
		
	}
}
