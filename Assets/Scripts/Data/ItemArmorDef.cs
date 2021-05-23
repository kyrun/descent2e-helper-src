using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Item: Armor", order = 10000)]
public class ItemArmorDef : ItemDef, IDefender
{
	[SerializeField] int _bonusHealth = 0;
	[SerializeField] List<DefenseDieDef> _defenseDice = default;

	public int BonusHealth { get { return _bonusHealth; } }
	public List<DefenseDieDef> DefenseDice { get { return _defenseDice; } }
	public override WornType Worn { get { return WornType.Armor; } }
}