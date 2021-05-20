using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Class", order = 10000)]
public class ClassDef : ScriptableObject
{
	[SerializeField] Archetype _archetype = default;
	[SerializeField] List<ItemDef> _startingItem = default;
	[SerializeField] List<SkillDef> _startingSkill = default;
	[SerializeField] List<SkillDef> _1CostSkill = default;
	[SerializeField] List<SkillDef> _2CostSkill = default;
	[SerializeField] List<SkillDef> _3CostSkill = default;

	public Archetype Archetype { get { return _archetype; } }
	public ReadOnlyCollection<ItemDef> StartingItems { get { return _startingItem.AsReadOnly(); } }
	public ReadOnlyCollection<SkillDef> StartingSkills { get { return _startingSkill.AsReadOnly(); } }
}