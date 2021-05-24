using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Skill", order = 10000)]
public class SkillDef : BaseDef<SkillDef>
{
	[SerializeField] int _xp = default;
	[SerializeField] int _staminaCost = default;
	[SerializeField] bool _isAction = default;
	[SerializeField] bool _isExhaustable = default;
	[SerializeField] List<CharacterModifierDef> _modifiers = default;


	public int XP { get { return _xp; } }
	public int StaminaCost { get { return _staminaCost; } }
	public bool IsAction { get { return _isAction; } }
	public bool IsExhaustable { get { return _isExhaustable; } }
	public IEnumerable<CharacterModifierDef> Modifiers { get { return _modifiers; } }
}
