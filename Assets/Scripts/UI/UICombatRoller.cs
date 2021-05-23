using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICombatRoller : MonoBehaviour
{
	[SerializeField] List<DieAnimator> _attackDice = default;
	[SerializeField] List<DieAnimator> _defenseDice = default;
	[SerializeField] TextMeshProUGUI _textAttacker = default;
	[SerializeField] TextMeshProUGUI _textDefender = default;
	[SerializeField] TextMeshProUGUI _textPierce = default;
	[SerializeField] TextMeshProUGUI _textHeart = default;
	[SerializeField] TextMeshProUGUI _textDefense = default;
	[SerializeField] TextMeshProUGUI _textSurge = default;
	[SerializeField] TextMeshProUGUI _textRange = default;
	[SerializeField] TextMeshProUGUI _textDamage = default;

	IAttacker _attacker;
	IDefender _defender;
	Coroutine _coroutine;

	void OnEnable()
	{
		foreach (var img in _attackDice)
		{
			img.gameObject.SetActive(false);
		}
		foreach (var img in _defenseDice)
		{
			img.gameObject.SetActive(false);
		}
		UpdatePierce(null);
		_textAttacker.text = "Attacker";
		_textDefender.text = "Defender";
		ResetResultsText();
	}

	void ResetResultsText()
	{
		_textPierce.text = _textDefense.text = _textHeart.text = _textSurge.text = _textRange.text = _textDamage.text = "--";
	}

	void UpdateResult(RollResult result, IAttacker attacker)
	{
		_textHeart.text = result.heart.ToString();
		_textDefense.text = result.defense.ToString();
		_textSurge.text = result.surge.ToString();
		_textRange.text = attacker.AttackType == AttackType.Ranged ? result.range.ToString() : "N/A";
		var defense = result.defense - attacker.Pierce;
		defense = Mathf.Max(0, defense);
		_textDamage.text = (result.heart - defense).ToString();
	}

	void UpdatePierce(IAttacker attacker)
	{
		int pierce = attacker != null ? attacker.Pierce : 0;
		_textPierce.transform.parent.gameObject.SetActive(pierce > 0);
		_textPierce.text = pierce.ToString();
	}

	public void Setup(IAttacker attacker, IDefender defender)
	{
		gameObject.SetActive(true);
		_textAttacker.text = "Attacker: " + attacker.name;
		_textDefender.text = "Defender: " + defender.name;
		_attacker = attacker;
		_defender = defender;
		_textRange.transform.parent.gameObject.SetActive(attacker.AttackType == AttackType.Ranged);
	}

	public void ShowCombatRoll(List<AttackDieDef> attackDieDefs, List<DefenseDieDef> defenseDieDefs, List<int> attackRolledIndex, List<int> defendRolledIndex)
	{
		int order = 0;
		for (int i = 0; i < _attackDice.Count; ++i)
		{
			_attackDice[i].gameObject.SetActive(i < attackDieDefs.Count);
			if (i < attackDieDefs.Count)
			{
				_attackDice[i].Roll(attackDieDefs[i], attackRolledIndex[i], order++);
			}
		}
		for (int i = 0; i < _defenseDice.Count; ++i)
		{
			_defenseDice[i].gameObject.SetActive(i < defenseDieDefs.Count);
			if (i < defenseDieDefs.Count)
			{
				_defenseDice[i].Roll(defenseDieDefs[i], defendRolledIndex[i], order++);
			}
		}
	}

	public void Roll()
	{
		ResetResultsText();

		var result = Roller.CombatRoll(_attacker, _defender, out List<int> rolledFaceIndexAttack, out List<int> rolledFaceIndexDefense);
		ShowCombatRoll(_attacker.AttackDice, _defender.DefenseDice, rolledFaceIndexAttack, rolledFaceIndexDefense);
		UpdatePierce(_attacker);

		if (_coroutine != null) StopCoroutine(_coroutine);
		_coroutine = StartCoroutine(WaitAndUpdateResult(result, _attacker));
	}

	IEnumerator WaitAndUpdateResult(RollResult result, IAttacker attacker)
	{
		var listDice = new List<DieAnimator>(_attackDice);
		listDice.AddRange(_defenseDice);

		yield return DieAnimator.WaitForUntilAllDiceFinishRolling(listDice);

		UpdateResult(result, _attacker);

		_coroutine = null;
	}


	// DEBUG -----

	void Start()
	{
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Roll();
		}
	}
}
