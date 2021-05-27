using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Roller
{
	public static RollResult CombatRoll(IAttacker attacker, IDefender defender, out List<int> rolledFaceIndexAttack, out List<int> rolledFaceIndexDefense)
	{
		rolledFaceIndexAttack = new List<int>();
		rolledFaceIndexDefense = new List<int>();

		var result = new RollResult();

		var attackerDice = attacker.AttackDice;
		for (int i = 0; i < attackerDice.Count; ++i)
		{
			var roll = attackerDice[i].Roll();
			var face = attackerDice[i].GetFace(roll);
			result.heart += face.heart;
			result.surge += face.surge;
			result.range += face.range;
			if (face.IsMiss) result.miss = true;
			rolledFaceIndexAttack.Add(roll);
		}
		if (defender != null)
		{
			var defenderDice = defender.DefenseDice;
			for (int i = 0; i < defenderDice.Count; ++i)
			{
				var roll = defenderDice[i].Roll();
				result.defense += defenderDice[i].GetDefensePerFace(roll);
				rolledFaceIndexDefense.Add(roll);
			}
		}
		result.defense = Mathf.Max(0, result.defense);
		result.pierce = attacker.Pierce;
		result.bonusRange = attacker.RangeModifier;

		return result;
	}
}

public class RollResult
{
	public int heart = 0;
	public int defense = 0;
	public int surge = 0;
	public int range = 0;
	public int pierce = 0;
	public int bonusRange = 0;
	public bool miss = false;

	public override string ToString()
	{
		var str = "RESULT: ";

		if (miss) str += "MISSED!";
		else
		{
			str += "Heart (" + heart + ") Defense (" + defense + ") Surge (" + surge + ") Range (" + range + ")";
		}

		return str;
	}
}