using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIListCharAttack : UIListButton<IAttacker>
{
	[SerializeField] UICombatRoller _combatRoller = default;

	MonsterDef.Properties _targetMonster;

	protected override IAttacker[] LoadItems()
	{
		return Game.PlayerCharacter.AttackOptions;
	}

	protected override void OnButtonPress(IAttacker attacker)
	{
		_combatRoller.Setup(attacker, _targetMonster);
	}

	protected override void Sort(List<IAttacker> list)
	{
		list.OrderBy(def => def.name);
	}

	public void Setup(MonsterDef.Properties monster)
	{

		if (Game.PlayerCharacter.AttackOptions.Length > 1)
		{
			gameObject.SetActive(true);
			_targetMonster = monster;
		}
		else if (Game.PlayerCharacter.AttackOptions.Length == 1)
		{
			_combatRoller.Setup(Game.PlayerCharacter.AttackOptions[0], monster);
		}
		else
		{
			_combatRoller.Setup(null, monster);
		}
	}
}
