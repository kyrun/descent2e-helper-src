using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IAttacker, IDefender
{
	public int Act { get; private set; } = 1;
	public int NumPlayers { get; private set; } = 1;

	List<AttackDieDef> _listAttackDice = new List<AttackDieDef>();
	List<DefenseDieDef> _listDefenseDice = new List<DefenseDieDef>();

	public string name { get { return Definition.name; } }
	public CharacterDef Definition { get; private set; }
	public ClassDef Class { get; private set; }

	public int Damage { get; private set; }
	public int Fatigue { get; private set; }

	public Dictionary<ItemDef, bool> Items = new Dictionary<ItemDef, bool>();
	public List<SkillDef> Skills = new List<SkillDef>();

	public List<Condition> Conditions = new List<Condition>();

	public int Speed
	{
		get
		{
			var speed = Definition.Speed;
			foreach (var kvp in Items)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				// TODO: get from items
			}
			return speed;
		}
	}
	public int Health
	{
		get
		{
			var health = Definition.Health;
			foreach (var kvp in Items)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				// TODO: get from items
			}
			return health;
		}
	}
	public int Stamina
	{
		get
		{
			var stamina = Definition.Stamina;
			foreach (var kvp in Items)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				// TODO: get from items
			}
			return stamina;
		}
	}

	public AttackType AttackType
	{
		get
		{
			foreach (var kvp in Items)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				if (item is IAttacker)
				{
					var attackItem = (IAttacker)item;
					return attackItem.AttackType;
				}
			}
			return AttackType.Melee;
		}
	}

	public List<AttackDieDef> AttackDice
	{
		get
		{
			_listAttackDice.Clear();
			foreach (var kvp in Items)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				if (item is IAttacker)
				{
					var attackItem = (IAttacker)item;
					_listAttackDice.AddRange(attackItem.AttackDice);
				}
			}
			if (_listAttackDice.Count == 0) // bare handed
			{
				_listAttackDice.Add(Resources.Load("Dice/Attack/Blue") as AttackDieDef); // HARDCODED
			}
			return _listAttackDice;
		}
	}

	public List<DefenseDieDef> DefenseDice
	{
		get
		{
			_listDefenseDice.Clear();
			foreach (var kvp in Items)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				if (item is IDefender)
				{
					var defenseItem = (IDefender)item;
					_listDefenseDice.AddRange(defenseItem.DefenseDice);
				}
			}
			_listDefenseDice.AddRange(Definition.DefenseDice);
			return _listDefenseDice;
		}
	}

	public int Pierce
	{
		get
		{
			int pierce = 0;
			foreach (var kvp in Items)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				if (item is ItemWeaponDef)
				{
					var weapon = (ItemWeaponDef)item;
					pierce += weapon.Pierce;
				}
			}
			return pierce;
		}
	}

	public Character(CharacterDef definition, ClassDef classDef)
	{
		Definition = definition;
		Class = classDef;
		foreach (var item in Class.StartingItems)
		{
			Items.Add(item, true);
		}
		foreach (var skill in Class.StartingSkills)
		{
			Skills.Add(skill);
		}
	}

	const int ALLOWED_HANDS = 2;
	const int TOTAL_ALLOWED_OTHER = 2;

	public void AddItem(ItemDef item)
	{
		bool equip = CanEquip(item);
		Items.Add(item, equip);
	}

	public void RemoveItem(ItemDef item)
	{
		Items.Remove(item);
	}

	public bool CanEquip(ItemDef item)
	{
		bool equip = false;
		switch (item.Worn)
		{
			case WornType.Hand:
				int handCount = 0;

				foreach (var kvp in Items)
				{
					if (!kvp.Value) continue;

					var handItem = kvp.Key;
					if (handItem is IHandItem)
					{
						handCount += GetHandCount(handItem);
					}
				}

				if (GetHandCount(item) <= (ALLOWED_HANDS - handCount))
				{
					equip = true;
				}
				break;
			case WornType.Armor:
				equip = true;
				foreach (var kvp in Items)
				{
					if (!kvp.Value) continue;

					var armor = kvp.Key;
					if (armor is ItemArmorDef)
					{
						equip = false;
						break;
					}
				}
				break;
			case WornType.Other:
				int otherCount = 0;
				foreach (var kvp in Items)
				{
					if (!kvp.Value) continue;

					var other = kvp.Key;
					if (other is ItemOtherDef)
					{
						++otherCount;
					}
				}

				if ((TOTAL_ALLOWED_OTHER - otherCount) > 0)
				{
					equip = true;
				}

				break;
		}

		return equip;
	}

	public void IncrementDamage()
	{
		++Damage;
	}
	public void DecrementDamage()
	{
		--Damage;
		if (Damage < 0) Damage = 0;
	}
	public void IncrementFatigue()
	{
		++Fatigue;
	}
	public void DecrementFatigue()
	{
		--Fatigue;
		if (Fatigue < 0) Fatigue = 0;
	}

	public int GetAttribute(Attribute attribute)
	{
		switch (attribute) // TODO: add additional from item, and less hardcoded
		{
			case Attribute.Might:
				return Definition.Might;
			case Attribute.Knowledge:
				return Definition.Knowledge;
			case Attribute.Willpower:
				return Definition.Willpower;
			case Attribute.Awareness:
				return Definition.Awareness;
		}
		return -1;
	}

	public void AddCondition(Condition condition)
	{
		if (!Conditions.Contains(condition))
		{
			Conditions.Add(condition);
			Messenger.Send(new MsgConditionChanged(condition));
		}
		else
		{
			Debug.LogWarning("Trying to add condtion but already exists! " + condition);
		}
	}

	public void RemoveCondition(Condition condition)
	{
		if (Conditions.Contains(condition))
		{
			Conditions.Remove(condition);
			Messenger.Send(new MsgConditionChanged(condition));
		}
		else
		{
			Debug.LogWarning("Trying to remove condtion but does not exists! " + condition);
		}
	}

	const char DELIMITER = '|';

	public Character(string saveStr)
	{
		var str = saveStr.Split('\n');

		int i = 0;
		if (int.TryParse(str[i], out int numPlayers))
		{
			NumPlayers = numPlayers;
		}
		else
		{
			Debug.LogWarning("Failed to initialize character!");
			return;
		}

		++i;
		if (int.TryParse(str[i], out int act))
		{
			Act = act;
		}
		else
		{
			Debug.LogWarning("Failed to initialize character!");
			return;
		}

		++i;
		Definition = CharacterDef.Get(str[i]);

		++i;
		Class = ClassDef.Get(str[i]);

		++i;
		Damage = int.Parse(str[i]);

		++i;
		Fatigue = int.Parse(str[i]);

		// Equipped
		++i;
		var splitNames = str[i].Split(DELIMITER);
		foreach (var itemName in splitNames)
		{
			var item = ItemDef.Get(itemName);
			if (item != null) Items.Add(item, true);
		}

		// Unequipped
		++i;
		splitNames = str[i].Split(DELIMITER);
		foreach (var itemName in splitNames)
		{
			var item = ItemDef.Get(itemName);
			if (item != null) Items.Add(item, false);
		}

		// Skills
		++i;
		splitNames = str[i].Split(DELIMITER);
		foreach (var skillName in splitNames)
		{
			var skill = SkillDef.Get(skillName);
			if (skill != null) Skills.Add(skill);
		}

		// Conditions
		++i;
		splitNames = str[i].Split(DELIMITER);
		foreach (var condName in splitNames)
		{
			if (System.Enum.TryParse(condName, out Condition condition))
			{
				Conditions.Add(condition);
			}
		}
	}

	// TODO: save as string
	public string ToSaveString()
	{
		string saveStr =
			NumPlayers + "\n" +
			Act + "\n" +
			name + "\n" +
			Class.name + "\n" +
			Damage + "\n" +
			Fatigue + "\n";

		// Equipped
		foreach (var kvp in Items)
		{
			if (kvp.Value)
			{
				saveStr += kvp.Key.name + DELIMITER;
			}
		}
		RemoveLastDelimiter(ref saveStr);
		saveStr += "\n";

		// Unequipped
		foreach (var kvp in Items)
		{
			if (!kvp.Value)
			{
				saveStr += kvp.Key.name + DELIMITER;
			}
		}
		RemoveLastDelimiter(ref saveStr);
		saveStr += "\n";

		// Skills
		foreach (var skill in Skills)
		{
			saveStr += skill.name + DELIMITER;
		}
		RemoveLastDelimiter(ref saveStr);
		saveStr += "\n";

		// Conditions
		foreach (var condition in Conditions)
		{
			saveStr += condition.ToString() + DELIMITER;
		}
		RemoveLastDelimiter(ref saveStr);
		saveStr += "\n";

		return saveStr;
	}

	void RemoveLastDelimiter(ref string str)
	{
		if (str[str.Length - 1] == DELIMITER)
		{
			str = str.Substring(0, str.Length - 1);
		}
	}

	static int GetHandCount(ItemDef item)
	{
		if (item is IHandItem)
		{
			IHandItem handItem = (IHandItem)item;
			switch (handItem.Handedness)
			{
				case Handedness.OneHanded:
					return 1;
				case Handedness.TwoHanded:
					return 2;
			}
		}

		return 0;
	}
}
