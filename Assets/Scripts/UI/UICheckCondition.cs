using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ButtonOkAndNext))]
public class UICheckCondition : MonoBehaviour
{
	[SerializeField] Condition _condition = default;

	ButtonOkAndNext _btn;

	void Awake()
	{
		_btn = GetComponent<ButtonOkAndNext>();
	}

	void OnEnable()
	{
		if (Game.PlayerCharacter != null && !Game.PlayerCharacter.Conditions.Contains(_condition))
		{
			_btn.Next();
		}
	}
}
