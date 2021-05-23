using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle), typeof(Image))]
public class UIToggleCondition : MonoBehaviour
{
	[SerializeField] Condition _condition = default;

	Toggle _toggle;
	Image _image;
	Color _colorOff;
	Color _colorOn;

	void Awake()
	{
		_image = GetComponent<Image>();
		_toggle = GetComponent<Toggle>();
		_colorOn = _colorOff = _image.color;
		_colorOn.a = 1;
		_toggle.onValueChanged.AddListener((isOn) =>
		{
			if (Game.PlayerCharacter == null && isOn)
			{
				_toggle.SetIsOnWithoutNotify(false);
				return;
			}

			if (isOn)
			{
				if (Game.PlayerCharacter.Conditions.Contains(_condition))
				{
					Debug.LogWarning("Trying to add condition " + _condition + " but already added!");
				}
				else
				{
					Game.PlayerCharacter.AddCondition(_condition);
				}
			}
			else
			{
				if (!Game.PlayerCharacter.Conditions.Contains(_condition))
				{
					Debug.LogWarning("Trying to remove condition " + _condition + " but doesn't exist!");
				}
				else
				{
					Game.PlayerCharacter.RemoveCondition(_condition);
				}
			}
		});
	}

	void OnEnable()
	{
		Messenger.Subscribe<MsgConditionChanged>(OnConditionChange);

		UpdateCondition(_condition);
	}

	void OnDisable()
	{
		Messenger.UnSubscribe<MsgConditionChanged>(OnConditionChange);
	}

	void OnConditionChange(MsgConditionChanged msg)
	{
		UpdateCondition(msg.Condition);
	}

	void UpdateCondition(Condition condition)
	{
		if (Game.PlayerCharacter == null) return;

		if (_condition == condition)
		{
			var hasCondition = Game.PlayerCharacter.Conditions.Contains(_condition);
			_image.color = hasCondition ? _colorOn : _colorOff;
			_toggle.SetIsOnWithoutNotify(hasCondition);
		}
	}
}
