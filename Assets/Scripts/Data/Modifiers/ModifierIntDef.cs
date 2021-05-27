using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Modifier (Integer Value)", order = 10000)]
public class ModifierIntDef : CharacterModifierDef
{
	[SerializeField] Modifier _modifier = default;
	[SerializeField] int _amount = 1;

	public Modifier Type { get { return _modifier; } }
	public int Amount { get { return _amount; } }

	protected override void OnActivate(Character character)
	{
		character.ModifyModifier(_modifier, _amount);
	}

	protected override void OnDeactivate(Character character)
	{
		character.ModifyModifier(_modifier, -_amount);
	}

	public static int GetTotalFromModifier(Modifier modifier, IEnumerable<CharacterModifierDef> modifiers, int characterBonus)
	{
		var total = 0;
		foreach (var m in modifiers)
		{
			if (m is ModifierIntDef)
			{
				var modInt = (ModifierIntDef)m;
				if (modInt.Type == modifier) total += modInt.Amount;
			}
		}
		total += characterBonus;
		return total;
	}
}
