using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ButtonOkAndNext))]
public class UICheckCondition : MonoBehaviour
{
	[SerializeField] Condition _condition = default;
	[SerializeField] UIEndTurn _uiEndTurn = default;

	bool _didAddCallbackRemoval = false;
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

				case Condition.Stunned:
					Game.PlayerCharacter.RemoveCondition(_condition);
					break;
				case Condition.Immobilized:
					_uiEndTurn.OnEndTurn += RemoveImmobilized;
					_didAddCallbackRemoval = true;
					break;
			}
		});

		Messenger.Subscribe<MsgConditionChanged>(OnConditionChanged);
	}

	void OnDestroy()
	{
		Messenger.UnSubscribe<MsgConditionChanged>(OnConditionChanged);
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

	void OnConditionChanged(MsgConditionChanged msg)
	{
		if (Game.PlayerCharacter.Conditions.Contains(msg.Condition)) // acquired condition
		{
		}
		else // lost condition
		{
			switch (msg.Condition)
			{
				case Condition.Immobilized:
					if (_didAddCallbackRemoval)
					{
						ClearRemoveImmobilizedCallback();
					}
					break;
			}
		}
	}

	void RemoveImmobilized()
	{
		if (Game.PlayerCharacter.Conditions.Contains(Condition.Immobilized))
		{
			Game.PlayerCharacter.RemoveCondition(Condition.Immobilized);
		}
		ClearRemoveImmobilizedCallback();
	}

	void ClearRemoveImmobilizedCallback()
	{
		_uiEndTurn.OnEndTurn -= RemoveImmobilized;
		_didAddCallbackRemoval = false;
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