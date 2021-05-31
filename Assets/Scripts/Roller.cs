using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Roller
{
	public static RollResult CombatRoll(IAttacker attacker, IDefender defender, out List<int> rolledFaceIndexAttack, out List<int> rolledFaceIndexDefense)
	{
		rolledFaceIndexAttack = new List<int>();
		rolledFaceIndexDefense = new List<int>();

		if (attacker != null)
		{
			var attackerDice = attacker.AttackDice;
			for (int i = 0; i < attackerDice.Count; ++i)
			{
				rolledFaceIndexAttack.Add(attackerDice[i].Roll());
			}
		}
		if (defender != null)
		{
			var defenderDice = defender.DefenseDice;
			for (int i = 0; i < defenderDice.Count; ++i)
			{
				rolledFaceIndexDefense.Add(defenderDice[i].Roll());
			}
		}

		return GetResultFromRoll(attacker, defender, rolledFaceIndexAttack, rolledFaceIndexDefense);
	}

	public static RollResult GetResultFromRoll(IAttacker attacker, IDefender defender, List<int> rolledFaceIndexAttack, List<int> rolledFaceIndexDefense)
	{
		var result = new RollResult();

		if (attacker != null)
		{
			var attackerDice = attacker.AttackDice;
			for (int i = 0; i < attackerDice.Count; ++i)
			{
				var face = attackerDice[i].GetFace(rolledFaceIndexAttack[i]);
				result.heart += face.heart;
				result.surge += face.surge;
				result.range += face.range;
				result.pierce = attacker.Pierce;
				result.bonusRange = attacker.RangeModifier;
				if (face.IsMiss) result.miss = true;
			}
		}
		if (defender != null)
		{
			var defenderDice = defender.DefenseDice;
			for (int i = 0; i < defenderDice.Count; ++i)
			{
				result.defense += defenderDice[i].GetDefensePerFace(rolledFaceIndexDefense[i]);
			}
		}
		result.defense = Mathf.Max(0, result.defense);

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