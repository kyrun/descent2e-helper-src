using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISkillSelect : UIListToggle<SkillDef>
{
	protected override void PopulateItemList()
	{

		var classFolder = "Classes/" + Game.PlayerCharacter.Class.Archetype + "/" + Game.PlayerCharacter.Class.name + "/Skills";

		var itemArray = Resources.LoadAll<SkillDef>(classFolder);
		if (itemArray.Length > 0) _items.AddRange(itemArray);

		int i = 0;
		while (true)
		{
			itemArray = Resources.LoadAll<SkillDef>(classFolder + "/" + i);
			if (itemArray.Length > 0) _items.AddRange(itemArray);
			else break;

			++i;
		}

		_items = new List<SkillDef>(_items.OrderBy(item => item.XP).ThenBy(item => item.name));
	}

	protected override void OnToggleOn(SkillDef skill)
	{
		if (!Game.PlayerCharacter.Skills.Contains(skill))
		{
			Game.PlayerCharacter.Skills.Add(skill);
		}
	}

	protected override void OnToggleOff(SkillDef skill)
	{
		if (Game.PlayerCharacter.Skills.Contains(skill))
		{
			Game.PlayerCharacter.Skills.Remove(skill);
		}
	}

	protected override bool ItemIsToggled(int index)
	{
		return Game.PlayerCharacter.Skills.Contains(_items[index]);
	}

	protected override string ItemName(SkillDef skill)
	{
		return skill.XP + ": " + skill.name;
	}
}