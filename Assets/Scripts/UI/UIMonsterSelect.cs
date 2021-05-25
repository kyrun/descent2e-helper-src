using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIMonsterSelect : UIListButton<MonsterDef>
{
	[SerializeField] UIMonsterVarietySelect _varietySelect = default;

	bool _monsterIsAttacking;

	protected override void OnButtonPress(MonsterDef monster)
	{
		_varietySelect.Setup(monster, _monsterIsAttacking);
	}

	protected override void Sort(List<MonsterDef> list)
	{
		list.OrderBy(def => def.name);
	}

	public void Setup(bool monsterIsAttacking)
	{
		_monsterIsAttacking = monsterIsAttacking;
	}
}
