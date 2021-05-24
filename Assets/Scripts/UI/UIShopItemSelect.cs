using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShopItemSelect : UIListToggle<ItemDef>
{
	[SerializeField] List<string> _subfolders = new List<string>();

	protected override void PopulateItemList()
	{
		foreach (var item in Game.PlayerCharacter.Class.StartingItems)
		{
			_items.Add(item);
		}

		var allOtherItems = new List<ItemDef>();
		foreach (var subfolder in _subfolders)
		{
			var itemArray = Resources.LoadAll<ItemDef>("Items/" + subfolder);
			allOtherItems.AddRange(itemArray);
		}
		_items.AddRange(allOtherItems.OrderBy(itm => itm.name));
	}

	protected override void OnToggleOn(ItemDef item)
	{
		if (!Game.PlayerCharacter.HasItem(item))
		{
			Game.PlayerCharacter.AddItem(item);
		}
	}

	protected override void OnToggleOff(ItemDef item)
	{
		if (Game.PlayerCharacter.HasItem(item))
		{
			Game.PlayerCharacter.RemoveItem(item);
		}
	}

	protected override bool ItemIsToggled(int index)
	{
		return Game.PlayerCharacter.HasItem(_items[index]);
	}
}
