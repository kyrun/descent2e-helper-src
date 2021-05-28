using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICombatRoller : MonoBehaviour, IRerollable
{
	[SerializeField] ModifierAttackOptionDef _unarmedCombatOption = default;
	[SerializeField] List<DieAnimator> _attackDiceAnimator = default;
	[SerializeField] List<DieAnimator> _defenseDiceAnimator = default;
	[SerializeField] TextMeshProUGUI _textAttacker = default;
	[SerializeField] TextMeshProUGUI _textDefender = default;
	[SerializeField] TextMeshProUGUI _textPierce = default;
	[SerializeField] TextMeshProUGUI _textRangeModifier = default;
	[SerializeField] TextMeshProUGUI _textHeart = default;
	[SerializeField] TextMeshProUGUI _textDefense = default;
	[SerializeField] TextMeshProUGUI _textSurge = default;
	[SerializeField] TextMeshProUGUI _textRange = default;
	[SerializeField] TextMeshProUGUI _textDamage = default;
	[SerializeField] GameObject _gobMissed = default;

	IAttacker _attacker;
	IDefender _defender;
	Coroutine _coroutine;

	List<int> _rolledFaceIndexAttack;
	List<int> _rolledFaceIndexDefense;
	RollResult _lastRollResult;

	public bool IsRolling { get; private set; }

	void OnEnable()
	{
		foreach (var img in _attackDiceAnimator)
		{
			img.gameObject.SetActive(false);
		}
		foreach (var img in _defenseDiceAnimator)
		{
			img.gameObject.SetActive(false);
		}
		UpdatePierce(null);
		UpdateRangeModifier(null);
		//_textAttacker.text = "Attacker";
		//_textDefender.text = "Defender";
		_gobMissed.SetActive(false);
		ResetResultsText();
	}

	void ResetResultsText()
	{
		_textPierce.text = _textRangeModifier.text = _textDefense.text = _textHeart.text = _textSurge.text = _textRange.text = _textDamage.text = "--";
	}

	void UpdateResult(RollResult result, AttackType attackType)
	{
		_textHeart.text = result.heart.ToString();
		_textDefense.text = result.defense.ToString();
		if (result.pierce > 0) _textDefense.text += " (-" + result.pierce + ")";
		_textSurge.text = result.surge.ToString();
		if (attackType == AttackType.Ranged)
		{
			_textRange.text = result.range.ToString();
			if (result.bonusRange > 0)
			{
				_textRange.text += " (+" + result.bonusRange + ")";
			}
		}
		else
		{
			_textRange.text = "--";
		}
		var defense = result.defense - result.pierce;
		defense = Mathf.Max(0, defense);
		_textDamage.text = (result.heart - defense).ToString();
		_gobMissed.SetActive(result.miss);
	}

	void UpdatePierce(IAttacker attacker)
	{
		int pierce = attacker != null ? attacker.Pierce : 0;
		_textPierce.transform.parent.gameObject.SetActive(pierce > 0);
		_textPierce.text = pierce.ToString();
	}

	void UpdateRangeModifier(IAttacker attacker)
	{
		int rangeModifier = attacker != null ? attacker.RangeModifier : 0;
		_textRangeModifier.transform.parent.gameObject.SetActive(attacker != null && attacker.AttackType == AttackType.Ranged && rangeModifier > 0);
		_textRangeModifier.text = "+" + rangeModifier;
	}

	public void Setup(IAttacker attacker, IDefender defender)
	{
		gameObject.SetActive(true);
		ResetResultsText();
		_attacker = attacker;
		_textAttacker.text = "";
		if (_attacker == null)
		{
			_attacker = _unarmedCombatOption;
		}
		if ((_attacker is ItemWeaponDef) || attacker == null)
		{
			_textAttacker.text += Game.PlayerCharacter.name + " (" + _attacker.name + ")";
		}
		else
		{
			_textAttacker.text += _attacker.name;
		}
		UpdatePierce(_attacker);
		UpdateRangeModifier(_attacker);

		_defender = defender;
		_textDefender.text = "";
		if (_defender != null)
		{
			_textDefender.text += _defender.name;
		}
		else
		{
			_textDefender.text = "Roll Defense Die for each Target";
		}
		_textRange.transform.parent.gameObject.SetActive(_attacker.AttackType == AttackType.Ranged);
	}

	void ShowCombatRoll(List<AttackDieDef> attackDieDefs, List<DefenseDieDef> defenseDieDefs, List<int> attackRolledIndex, List<int> defendRolledIndex)
	{
		int order = 0;
		for (int i = 0; i < _attackDiceAnimator.Count; ++i)
		{
			_attackDiceAnimator[i].gameObject.SetActive(i < attackDieDefs.Count);
			if (i < attackDieDefs.Count)
			{
				_attackDiceAnimator[i].Roll(attackDieDefs[i], attackRolledIndex[i], order++);
			}
		}
		for (int i = 0; i < _defenseDiceAnimator.Count; ++i)
		{
			_defenseDiceAnimator[i].gameObject.SetActive(i < defenseDieDefs.Count);
			if (i < defenseDieDefs.Count)
			{
				_defenseDiceAnimator[i].Roll(defenseDieDefs[i], defendRolledIndex[i], order++);
			}
		}
	}

	public void Roll()
	{
		IsRolling = true;

		ResetResultsText();

		_lastRollResult = Roller.CombatRoll(_attacker, _defender, out _rolledFaceIndexAttack, out _rolledFaceIndexDefense);

		var defenseDice = _defender != null ? _defender.DefenseDice : new List<DefenseDieDef>();
		ShowCombatRoll(_attacker.AttackDice, defenseDice, _rolledFaceIndexAttack, _rolledFaceIndexDefense);
		UpdatePierce(_attacker);
		UpdateRangeModifier(_attacker);

		if (_coroutine != null) StopCoroutine(_coroutine);

		var listDice = new List<DieAnimator>(_attackDiceAnimator);
		listDice.AddRange(_defenseDiceAnimator); // add all die animators
		_coroutine = StartCoroutine(WaitAndUpdateResult(_lastRollResult, listDice));
	}

	public void RerollOneDie(DieAnimator dieAnimator)
	{
		IsRolling = true;

		int index;
		DieDef dieDef;
		List<int> rolledFaceIndex;
		if (_attackDiceAnimator.Contains(dieAnimator))
		{
			index = _attackDiceAnimator.IndexOf(dieAnimator);
			dieDef = _attacker.AttackDice[index];
			rolledFaceIndex = _rolledFaceIndexAttack;
		}
		else if (_defender != null && _defenseDiceAnimator.Contains(dieAnimator))
		{
			index = _defenseDiceAnimator.IndexOf(dieAnimator);
			dieDef = _defender.DefenseDice[index];
			rolledFaceIndex = _rolledFaceIndexDefense;
		}
		else
		{
			Debug.LogError("No " + typeof(DieAnimator) + " found!", this);
			return;
		}
		rolledFaceIndex[index] = dieDef.Roll();

		_lastRollResult = Roller.GetResultFromRoll(_attacker, _defender, _rolledFaceIndexAttack, _rolledFaceIndexDefense);

		if (_coroutine != null) StopCoroutine(_coroutine);

		var listDice = new List<DieAnimator>();
		listDice.Add(dieAnimator);
		dieAnimator.Roll(dieDef, rolledFaceIndex[index], 0);
		_coroutine = StartCoroutine(WaitAndUpdateResult(_lastRollResult, listDice));
	}

	IEnumerator WaitAndUpdateResult(RollResult result, List<DieAnimator> listDice)
	{
		yield return DieAnimator.WaitForUntilAllDiceFinishRolling(listDice);

		UpdateResult(result, _attacker.AttackType);

		IsRolling = false;
		_coroutine = null;
	}
}
