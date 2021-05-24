using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Class", order = 10000)]
public class ClassDef : BaseDef<ClassDef>
{
	[SerializeField] Archetype _archetype = default;
	[SerializeField] List<ItemDef> _startingItem = default;
	[SerializeField] List<SkillDef> _startingSkill = default;
	[SerializeField] List<SkillDef> _Cost1Skill = default;
	[SerializeField] List<SkillDef> _Cost2Skill = default;
	[SerializeField] List<SkillDef> _Cost3Skill = default;

	public Archetype Archetype { get { return _archetype; } }
	public ReadOnlyCollection<ItemDef> StartingItems { get { return _startingItem.AsReadOnly(); } }
	public ReadOnlyCollection<SkillDef> StartingSkills { get { return _startingSkill.AsReadOnly(); } }
	public ReadOnlyCollection<SkillDef> Cost1Skills { get { return _Cost1Skill.AsReadOnly(); } }
	public ReadOnlyCollection<SkillDef> Cost2Skills { get { return _Cost2Skill.AsReadOnly(); } }
	public ReadOnlyCollection<SkillDef> Cost3Skills { get { return _Cost3Skill.AsReadOnly(); } }
}
