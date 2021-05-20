using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IAttacker, IDefender
{
	List<AttackDieDef> _listAttackDice = new List<AttackDieDef>();
	List<DefenseDieDef> _listDefenseDice = new List<DefenseDieDef>();

	public string name { get { return Definition.name; } }
	public CharacterDef Definition { get; private set; }
	public ClassDef Class { get; private set; }

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

	public int TotalPierce
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

	public Dictionary<ItemDef, bool> Items = new Dictionary<ItemDef, bool>();
	public List<SkillDef> Skills = new List<SkillDef>();

	public List<Condition> Conditions = new List<Condition>();

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

	// TODO: load from string
	public Character(string serializedString)
	{
	}

	// TODO: save as string
	public string ToSaveString()
	{
		return base.ToString();
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
