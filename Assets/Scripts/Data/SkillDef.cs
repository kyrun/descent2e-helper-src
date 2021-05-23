using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Skill", order = 10000)]
public class SkillDef : ScriptableObject
{
	[SerializeField] int _xp = default;
	[SerializeField] int _staminaCost = default;
	[SerializeField] bool _isAction = default;
	[SerializeField] bool _isExhaustable = default;

	public int XP { get { return _xp; } }
}
