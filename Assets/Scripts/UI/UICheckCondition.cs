using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ButtonOkAndNext))]
public class UICheckCondition : MonoBehaviour
{
	[SerializeField] Condition _condition = default;

	ButtonOkAndNext _btnScript;

	UIAttributeTest _uiAttributeTest;

	void Awake()
	{
		_btnScript = GetComponent<ButtonOkAndNext>();
		_uiAttributeTest = FindObjectOfType<UIAttributeTest>(true);

		_btnScript.BtnOK.onClick.AddListener(() =>
		{
			switch (_condition)
			{
				case Condition.Diseased:
				case Condition.Poisoned:
					if (Game.PlayerCharacter.Conditions.Contains(_condition))
					{
						_uiAttributeTest.OnPassTest += () =>
						{
							Game.PlayerCharacter.RemoveCondition(_condition);
						};
					}
					break;

				case Condition.Immobilized:
				case Condition.Stunned:
					Game.PlayerCharacter.RemoveCondition(_condition);
					break;
			}
		});
	}

	void OnEnable()
	{
		if (Game.PlayerCharacter != null)
		{
			if (!Game.PlayerCharacter.Conditions.Contains(_condition))
			{
				_btnScript.Next();
			}
		}
	}
}

public class MsgConditionChanged : Message
{
	public Condition Condition { get; private set; }


	public MsgConditionChanged(Condition condition)
	{
		Condition = condition;
	}
}