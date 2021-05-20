using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDef : ScriptableObject
{
	[SerializeField] List<ItemTrait> _trait = default;

	public abstract WornType Worn { get; }

	public enum ItemTrait
	{
		Axe,
		Blade,
		Book,
		Boots,
		Bow,
		Cloak,
		Exotic,
		Hammer,
		HeavyArmor,
		Helmet,
		LightArmor,
		Magic,
		Ring,
		Rune,
		Staff,
		Shield,
		Trinket
	}
}

public enum WornType
{
	Hand,
	Armor,
	Other
}

public enum Handedness
{
	OneHanded,
	TwoHanded
}