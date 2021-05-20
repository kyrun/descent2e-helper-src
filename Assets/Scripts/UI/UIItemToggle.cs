using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemToggle : MonoBehaviour
{
	[SerializeField] Toggle _toggle = default;
	[SerializeField] TextMeshProUGUI _text = default;

	UIItemEquip _ui;
	ItemDef _item;

	void Awake()
	{
		_ui = GetComponentInParent<UIItemEquip>();
	}

	public void Init(ItemDef item, bool equipped)
	{
		_item = item;
		_text.text = _item.name;
		_toggle.onValueChanged.RemoveAllListeners();
		_toggle.isOn = equipped;
		_toggle.onValueChanged.AddListener(OnToggle);
		RefreshInteractableState();
	}

	void OnToggle(bool isOn)
	{
		if (_item == null)
		{
			Debug.LogWarning("Null item!", this);
			return;
		}

		Game.PlayerCharacter.Items[_item] = isOn;

		_ui.RefreshToggles(_item);
	}

	public void RefreshInteractableState()
	{
		bool isEquipped;
		Game.PlayerCharacter.Items.TryGetValue(_item, out isEquipped);

		_toggle.interactable = _item != null && (isEquipped || Game.PlayerCharacter.CanEquip(_item));
	}
}
