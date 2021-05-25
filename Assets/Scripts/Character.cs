using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Character : IAttacker, IDefender
{
	public int Act { get; set; } = 1;
	public int NumPlayers { get; private set; } = 4;

	List<AttackDieDef> _listAttackDice = new List<AttackDieDef>();
	List<DefenseDieDef> _listDefenseDice = new List<DefenseDieDef>();
	List<SkillDef> _listSkills = new List<SkillDef>();
	Dictionary<ItemDef, bool> _dictItems = new Dictionary<ItemDef, bool>();

	public string name { get { return Definition.name; } }
	public CharacterDef Definition { get; private set; }
	public ClassDef Class { get; private set; }

	public int Damage { get; private set; }
	public int Fatigue { get; private set; }

	public Dictionary<ItemDef, bool>.KeyCollection Items { get { return _dictItems.Keys; } }
	public ReadOnlyCollection<SkillDef> Skills { get { return _listSkills.AsReadOnly(); } }

	public List<Condition> Conditions = new List<Condition>();

	Dictionary<Modifier, int> _dictModifier = new Dictionary<Modifier, int>();

	public int Speed
	{
		get
		{
			return Definition.Speed + _dictModifier[Modifier.Speed];
		}
	}

	public int Health
	{
		get
		{
			return Definition.Health + _dictModifier[Modifier.Health];
		}
	}

	public int Stamina
	{
		get
		{
			return Definition.Stamina + _dictModifier[Modifier.Stamina];
		}
	}

	public AttackType AttackType
	{
		get
		{
			foreach (var kvp in _dictItems)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				if (item is ItemWeaponDef)
				{
					var weapon = (ItemWeaponDef)item; // TODO: handle multiple weapons. currently only uses the first
					return weapon.AttackType;
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
			foreach (var kvp in _dictItems)
			{
				if (!kvp.Value) continue;

				var item = kvp.Key;
				if (item is ItemWeaponDef)
				{
					var weapon = (ItemWeaponDef)item;
					_listAttackDice.AddRange(weapon.AttackDice);
				}
			}
			if (_listAttackDice.Count == 0) // bare handed
			{
				_listAttackDice.Add(Resources.Load("Dice/Attack/Blue") as AttackDieDef); // HARDCODED!
			}
			return _listAttackDice;
		}
	}

	public List<DefenseDieDef> DefenseDice
	{
		get
		{
			_listDefenseDice.Clear();
			foreach (var kvp in _dictItems)
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
			return _dictModifier[Modifier.Pierce];
		}
	}

	public int RangeModifier
	{
		get
		{
			return _dictModifier[Modifier.Range];
		}
	}

	#region Skills
	public void LearnSkill(SkillDef skillDef)
	{
		if (!_listSkills.Contains(skillDef))
		{
			_listSkills.Add(skillDef);
			foreach (var modifier in skillDef.Modifiers)
			{
				modifier.OnActivate(this);
			}
		}
	}

	public void UnlearnSkill(SkillDef skillDef)
	{
		if (_listSkills.Contains(skillDef))
		{
			_listSkills.Remove(skillDef);
			foreach (var modifier in skillDef.Modifiers)
			{
				modifier.OnDeactivate(this);
			}
		}
	}
	#endregion

	#region Items
	const int ALLOWED_HANDS = 2;
	const int TOTAL_ALLOWED_OTHER = 2;

	public void AddItem(ItemDef item)
	{
		_dictItems.Add(item, false);
		if (CanEquip(item))
		{
			EquipItem(item);
		}
	}

	public void RemoveItem(ItemDef item)
	{
		if (_dictItems[item])
		{
			UnequipItem(item);
		}
		_dictItems.Remove(item);
	}

	public void EquipItem(ItemDef itemDef)
	{
		if (_dictItems.TryGetValue(itemDef, out bool isEquipped))
		{
			if (!isEquipped)
			{
				_dictItems[itemDef] = true;
				foreach (var modifier in itemDef.Modifiers)
				{
					modifier.OnActivate(this);
				}
			}
		}
		else Debug.LogWarning("No item " + itemDef.name + "!");
	}

	public void UnequipItem(ItemDef itemDef)
	{
		if (_dictItems.TryGetValue(itemDef, out bool isEquipped))
		{
			if (isEquipped)
			{
				_dictItems[itemDef] = false;
				foreach (var modifier in itemDef.Modifiers)
				{
					modifier.OnDeactivate(this);
				}
			}
		}
		else Debug.LogWarning("No item " + itemDef.name + "!");
	}

	public bool HasItem(ItemDef itemDef)
	{
		return _dictItems.ContainsKey(itemDef);
	}

	public bool IsEquipped(ItemDef itemDef)
	{
		if (_dictItems.TryGetValue(itemDef, out bool isEquipped))
		{
			return isEquipped;
		}
		else Debug.LogWarning("No item " + itemDef.name + "!");
		return false;
	}

	public bool CanEquip(ItemDef item)
	{
		bool equip = false;
		switch (item.Worn)
		{
			case WornType.Hand:
				int handCount = 0;

				foreach (var kvp in _dictItems)
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
				foreach (var kvp in _dictItems)
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
				foreach (var kvp in _dictItems)
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

	public ItemWeaponDef GetEquippedWeapon()
	{
		foreach (var kvp in _dictItems)
		{
			if (!kvp.Value) continue;

			var item = kvp.Key;
			if (item is ItemWeaponDef)
			{
				return (ItemWeaponDef)item;
			}
		}
		return null;
	}
#endregion

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

	public void ModifyModifier(Modifier modifier, int amount)
	{
		_dictModifier[modifier] += amount;
	}

	// CONSTRUCTORS

	public Character(CharacterDef definition, ClassDef classDef)
	{
		Init();
		Definition = definition;
		Class = classDef;
		foreach (var item in Class.StartingItems)
		{
			_dictItems.Add(item, true);
		}
		foreach (var skill in Class.StartingSkills)
		{
			_listSkills.Add(skill);
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

		Init();

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
			if (item != null)
			{
				AddItem(item);
				EquipItem(item); // unnecessary, but just in case
			}
		}

		// Unequipped
		++i;
		splitNames = str[i].Split(DELIMITER);
		foreach (var itemName in splitNames)
		{
			var item = ItemDef.Get(itemName);
			if (item != null)
			{
				AddItem(item);
				UnequipItem(item); // unnecessary, but just in case
			}
		}

		// Skills
		++i;
		splitNames = str[i].Split(DELIMITER);
		foreach (var skillName in splitNames)
		{
			var skill = SkillDef.Get(skillName);
			if (skill != null) LearnSkill(skill);
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

	void Init()
	{
		foreach (Modifier m in System.Enum.GetValues(typeof(Modifier)))
		{
			_dictModifier.Add(m, 0);
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
		foreach (var kvp in _dictItems)
		{
			if (kvp.Value)
			{
				saveStr += kvp.Key.name + DELIMITER;
			}
		}
		RemoveLastDelimiter(ref saveStr);
		saveStr += "\n";

		// Unequipped
		foreach (var kvp in _dictItems)
		{
			if (!kvp.Value)
			{
				saveStr += kvp.Key.name + DELIMITER;
			}
		}
		RemoveLastDelimiter(ref saveStr);
		saveStr += "\n";

		// Skills
		foreach (var skill in _listSkills)
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
