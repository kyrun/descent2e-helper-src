using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Modifier (Integer Value)", order = 10000)]
public class ModifierIntDef : CharacterModifierDef
{
    [SerializeField] Modifier _modifier = default;
    [SerializeField] int _amount = 1;

    public override void OnActivate(Character character)
    {
        character.ModifyModifier(_modifier, _amount);
    }

    public override void OnDeactivate(Character character)
    {
        character.ModifyModifier(_modifier, -_amount);
    }
}
