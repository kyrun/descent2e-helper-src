using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Modifier (Attack Option)", order = 10000)]
public class ModifierAttackOptionDef : CharacterModifierDef, IAttacker
{
    [SerializeField] bool _includeCharacterBonuses = true;
    [SerializeField] AttackType _attackType = default;
    [SerializeField] List<AttackDieDef> _attackDie = default;
    [SerializeField] List<CharacterModifierDef> _modifiers = default;

    public bool IgnoreCharacterBonuses { get { return _includeCharacterBonuses; } }
    public AttackType AttackType { get { return _attackType; } }
    public List<AttackDieDef> AttackDice { get { return _attackDie; } }

    public int Pierce
    {
        get
        {
            return ModifierIntDef.GetTotalFromModifier(Modifier.Pierce, _modifiers, _includeCharacterBonuses ? Game.PlayerCharacter.PierceModifier : 0);
        }
    }

    public int RangeModifier
    {
        get
        {
            return ModifierIntDef.GetTotalFromModifier(Modifier.Range, _modifiers, _includeCharacterBonuses ? Game.PlayerCharacter.RangeModifier : 0);
        }
    }

    protected override void OnActivate(Character character)
    {
        character.AddAttackOption(this);
    }

    protected override void OnDeactivate(Character character)
    {
        character.RemoveAttackOption(this);
    }
}
