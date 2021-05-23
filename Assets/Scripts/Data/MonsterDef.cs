using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Monster", order = 10000)]
public class MonsterDef : ScriptableObject
{
	readonly Color COLOR_MASTER = new Color(0.882353f, 0.06666667f, 0.0627451f);
	[SerializeField] List<VarietyGroup> _act = default;

	void OnEnable()
	{
		for (int i = 0; i < _act.Count; ++i)
		{
			_act[i].Minion.InitName(name + " (Minion, Act " + (i + 1) + ")");
			_act[i].Master.InitName(name + " (<color=#"+ ColorUtility.ToHtmlStringRGB(COLOR_MASTER) + ">Master</color>, Act " + (i + 1) + ")");
		}
	}

	public VarietyGroup ActGroup
	{
		get
		{
			return _act[Game.Act - 1];
		}
	}

	[System.Serializable]
	public class VarietyGroup
	{
		[SerializeField] Properties _minion = default;
		[SerializeField] Properties _master = default;

		public Properties Minion { get { return _minion; } }
		public Properties Master { get { return _master; } }
	}

	[System.Serializable]
	public class Properties : IAttacker, IDefender
	{
		[Range(0, 10)]
		[SerializeField] int _speed = 4;
		[Range(0, 20)]
		[SerializeField] int _health = 4;
		[Range(0, 3)]
		[SerializeField] int _pierce = 0;
		[SerializeField] AttackType _attackType = default;
		[SerializeField] List<AttackDieDef> _attack = default;
		[SerializeField] List<DefenseDieDef> _defense = default;

		public int Speed { get { return _speed; } }
		public int Health { get { return _health; } }
		public int Pierce { get { return _pierce; } }
		public AttackType AttackType { get { return _attackType; } }

		public void InitName(string initname)
		{
			name = initname;
		}

		public string name { get; private set; }

		public List<AttackDieDef> AttackDice
		{
			get
			{
				return _attack;
			}
		}

		public List<DefenseDieDef> DefenseDice
		{
			get
			{
				return _defense;
			}
		}
	}

	public Properties GetMonsterDef(bool isMaster)
	{
		var group = _act[Game.Act - 1];

		if (isMaster) return group.Master;
		else return group.Minion;
	}
}
