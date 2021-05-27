using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIListMonster : UIListScriptableObj<MonsterDef>
{
	[SerializeField] UIListCharAttack _listCharAttack = default;
	[SerializeField] UICombatRoller _combatRoller = default;

	bool _monsterIsAttacking;
	MonsterVariety _variety;

	protected override void OnButtonPress(MonsterDef monster)
	{
		MonsterDef.Properties monsterProperty;
		switch (_variety)
		{
			case MonsterVariety.Minion:
			default:
				monsterProperty = monster.ActGroup.Minion;
				break;
			case MonsterVariety.Master:
				monsterProperty = monster.ActGroup.Master;
				break;
		}

		if (_monsterIsAttacking)
		{
			_combatRoller.Setup(monsterProperty, Game.PlayerCharacter);
		}
		else
		{
			if (Game.PlayerCharacter.AttackOptions.Length > 1)
			{
				_listCharAttack.Setup(monsterProperty);
			}
			else if (Game.PlayerCharacter.AttackOptions.Length == 1)
			{
				_combatRoller.Setup(Game.PlayerCharacter.AttackOptions[0], monsterProperty);
			}
			else
			{
				_combatRoller.Setup(null, monsterProperty);
			}
		}
		gameObject.SetActive(false);
	}

	protected override void Sort(List<MonsterDef> list)
	{
		list.OrderBy(def => def.name);
	}

	public void Setup(bool monsterIsAttacking, MonsterVariety variety)
	{
		_monsterIsAttacking = monsterIsAttacking;
		_variety = variety;
		gameObject.SetActive(true);
	}
}
