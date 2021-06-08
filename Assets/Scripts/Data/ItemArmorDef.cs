using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Item: Armor", order = 10000)]
public class ItemArmorDef : ItemDef, IDefender
{
	[Space]
	[SerializeField] List<DefenseDieDef> _defenseDice = default;

	public List<DefenseDieDef> DefenseDice { get { return _defenseDice; } }
	public override WornType Worn { get { return WornType.Armor; } }
}