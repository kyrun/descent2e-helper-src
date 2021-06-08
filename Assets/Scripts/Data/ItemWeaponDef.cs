using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Item: Weapon", order = 10000)]
public class ItemWeaponDef : ItemDef, IHandItem, IAttacker
{
	[Space]
	[Range(0, 5)]
	[SerializeField] int _pierce = 0;
	[Range(0, 5)]
	[SerializeField] int _rangeModifier = 0;
	[SerializeField] AttackType _attackType = default;
	[SerializeField] Handedness _handedness = default;
	[SerializeField] List<AttackDieDef> _attackDice = default;
	
	public int Pierce { get { return _pierce; } }
	public int RangeModifier { get { return _rangeModifier; } }
    public AttackType AttackType { get { return _attackType; } }
	public Handedness Handedness { get { return _handedness; } }
	public List<AttackDieDef> AttackDice { get { return _attackDice; } }
	public override WornType Worn { get { return WornType.Hand; } }
}

public enum AttackType
{
	Melee,
	Ranged
}