using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDef : BaseDef<ItemDef>, IHasReminder
{
	[SerializeField] protected List<ItemTrait> _traits = default;
	[SerializeField] protected List<CharacterModifierDef> _modifiers = default;
	[SerializeField] protected List<ReminderPrerequisite.Type> _reminderPrerequisites = default;
	[SerializeField] protected int _staminaCost = 0;
	[SerializeField] protected bool _isAction = false;
	[SerializeField] protected bool _isExhaustable = false;

	public IEnumerable<ItemTrait> Traits { get { return _traits; } }
	public IEnumerable<CharacterModifierDef> Modifiers { get { return _modifiers; } }
	public IEnumerable<ReminderPrerequisite.Type> ReminderPrerequisites => _reminderPrerequisites;
	public abstract WornType Worn { get; }

	public int StaminaCost => _staminaCost;

	public bool IsAction => _isAction;

	public bool IsExhaustable => _isExhaustable;

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