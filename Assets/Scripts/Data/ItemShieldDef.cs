using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Item: Shield", order = 10000)]
public class ItemShieldDef : ItemDef, IDefender, IHandItem
{
	[SerializeField] List<DefenseDieDef> _defenseDice = default;

	public List<DefenseDieDef> DefenseDice { get { return _defenseDice; } }
	public override WornType Worn { get { return WornType.Hand; } }
	public Handedness Handedness { get { return Handedness.OneHanded; } }
}