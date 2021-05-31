using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetSelect : MonoBehaviour
{
	[SerializeField] Button _btnMinion = default;
	[SerializeField] Button _btnMaster = default;
	[SerializeField] Button _btnOther = default;
	[SerializeField] UIListMonster _monsterSelect = default;
	[SerializeField] UIListCharAttack _listCharAttack = default;
	[SerializeField] UICombatRoller _combatRoller = default;

	bool _monsterIsAttacking;

	void Awake()
	{
		_btnMinion.onClick.AddListener(() =>
		{
			_monsterSelect.Setup(_monsterIsAttacking, MonsterVariety.Minion);
		});
		_btnMaster.onClick.AddListener(() =>
		{
			_monsterSelect.Setup(_monsterIsAttacking, MonsterVariety.Master);
		});
		_btnOther.onClick.AddListener(() =>
		{
			if (_monsterIsAttacking)
			{
				_combatRoller.Setup(null, Game.PlayerCharacter);
			}
			else
			{
				_listCharAttack.Setup(null);
			}
		});
	}

	public void Setup(bool monsterIsAttacking)
	{
		gameObject.SetActive(true);
		_monsterIsAttacking = monsterIsAttacking;
	}
}

public enum MonsterVariety
{
	Minion,
	Master
}