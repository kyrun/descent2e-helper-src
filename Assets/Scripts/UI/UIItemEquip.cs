using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemEquip : MonoBehaviour
{
	[SerializeField] UIItemToggle _baseItemToggleHand = default;
	[SerializeField] UIItemToggle _baseItemToggleArmor = default;
	[SerializeField] UIItemToggle _baseItemToggleOther = default;

	List<UIItemToggle> _listToggleHand = new List<UIItemToggle>();
	List<UIItemToggle> _listToggleArmor = new List<UIItemToggle>();
	List<UIItemToggle> _listToggleOther = new List<UIItemToggle>();

	void Awake()
	{
		_listToggleHand.Add(_baseItemToggleHand);
		_listToggleArmor.Add(_baseItemToggleArmor);
		_listToggleOther.Add(_baseItemToggleOther);
	}

	void OnEnable()
	{
		int nHand = 0;
		int nArmor = 0;
		int nOther = 0;
		foreach (var item in Game.PlayerCharacter.Items)
		{
			bool equipped = Game.PlayerCharacter.IsEquipped(item);
			switch (item.Worn)
			{
				case WornType.Hand:
					SetupToggle(item, equipped, _baseItemToggleHand, _listToggleHand, ref nHand);
					break;
				case WornType.Armor:
					SetupToggle(item, equipped, _baseItemToggleArmor, _listToggleArmor, ref nArmor);
					break;
				case WornType.Other:
					SetupToggle(item, equipped, _baseItemToggleOther, _listToggleOther, ref nOther);
					break;
			}
		}
		ShowToggleUpTo(_listToggleHand, nHand);
		ShowToggleUpTo(_listToggleArmor, nArmor);
		ShowToggleUpTo(_listToggleOther, nOther);
	}

	void SetupToggle(ItemDef item, bool equipped, UIItemToggle baseToggle, List<UIItemToggle> list, ref int count)
	{
		UIItemToggle uiToggle;
		if (count == list.Count)
		{
			uiToggle = Instantiate(baseToggle, baseToggle.transform.parent);
			list.Add(uiToggle);
		}

		uiToggle = list[count];
		uiToggle.Init(item, equipped);

		++count;
	}

	void ShowToggleUpTo(List<UIItemToggle> list, int count)
	{
		for (int i = 0; i < list.Count; ++i)
		{
			list[i].gameObject.SetActive(i < count);
		}
	}

	public void RefreshToggles(ItemDef item)
	{
		List<UIItemToggle> list;
		switch (item.Worn)
		{
			case WornType.Hand:
				list = _listToggleHand;
				break;
			case WornType.Armor:
				list = _listToggleArmor;
				break;
			case WornType.Other:
				list = _listToggleOther;
				break;
			default:
				return;
		}

		foreach (var toggle in list) toggle.RefreshInteractableState();
	}
}
