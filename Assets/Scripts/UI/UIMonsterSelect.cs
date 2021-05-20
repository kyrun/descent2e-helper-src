using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMonsterSelect : UIListSelect<MonsterDef>
{
	[SerializeField] UIMonsterVarietySelect _varietySelect = default;

	bool _monsterIsAttacking;

	protected override void OnButtonPress(MonsterDef monster)
	{
		_varietySelect.Setup(monster, _monsterIsAttacking);
	}

	public void Setup(bool monsterIsAttacking)
	{
		_monsterIsAttacking = monsterIsAttacking;
	}
}
