using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Item: Weapon", order = 10000)]
public class ItemWeaponDef : ItemDef, IHandItem
{
	[SerializeField] AttackType _attackType = default;
	[SerializeField] Handedness _handedness = default;
	[SerializeField] List<AttackDieDef> _attackDice = default;

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