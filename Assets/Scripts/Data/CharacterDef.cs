using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Character", order = 10000)]
public class CharacterDef : ScriptableObject
{
	[SerializeField] Archetype _archetype = default;
	[Header("Characteristics")]
	[Range(2, 6)]
	[SerializeField] int _speed = 4;
	[Range(4, 16)]
	[SerializeField] int _health = 8;
	[Range(2, 6)]
	[SerializeField] int _stamina = 4;
	[SerializeField] List<DefenseDieDef> _defense = default;
	[Header("Attributes")]
	[Range(0, 6)]
	[SerializeField] int _might = 4;
	[Range(0, 6)]
	[SerializeField] int _knowledge = 4;
	[Range(0, 6)]
	[SerializeField] int _willpower = 4;
	[Range(0, 6)]
	[SerializeField] int _awareness = 4;

	public Archetype Archetype { get { return _archetype; } }
	public List<DefenseDieDef> DefenseDice { get { return _defense; } }
}
