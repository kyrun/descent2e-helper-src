using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Item: Other", order = 10000)]
public class ItemOtherDef : ItemDef
{
	public override WornType Worn { get { return WornType.Other; } }
}
