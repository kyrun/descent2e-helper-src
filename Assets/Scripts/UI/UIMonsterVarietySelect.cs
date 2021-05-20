using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterVarietySelect : MonoBehaviour
{
	[SerializeField] Button _btnMaster = default;
	[SerializeField] Button _btnMinion = default;
	[SerializeField] UICombatRoller _combatRoller = default;

	MonsterDef _monster;
	bool _monsterIsAttacking;

	void Awake()
	{
		_btnMaster.onClick.AddListener(() =>
		{
			var group = _monster.ActGroup;

			if (_monsterIsAttacking)
			{
				SetupCombatRoller(group.Master, Game.PlayerCharacter);
			}
			else
			{
				SetupCombatRoller(Game.PlayerCharacter, group.Master);
			}
		});
		_btnMinion.onClick.AddListener(() =>
		{
			var group = _monster.ActGroup;
			if (_monsterIsAttacking)
			{
				SetupCombatRoller(group.Minion, Game.PlayerCharacter);
			}
			else
			{
				SetupCombatRoller(Game.PlayerCharacter, group.Minion);
			}
		});
	}

	void SetupCombatRoller(IAttacker attacker, IDefender defender)
	{
		_combatRoller.Setup(attacker, defender);
		gameObject.SetActive(false);
	}

	public void Setup(MonsterDef monster, bool monsterIsAttacking)
	{
		_monster = monster;
		_monsterIsAttacking = monsterIsAttacking;
		gameObject.SetActive(true);
	}
}
