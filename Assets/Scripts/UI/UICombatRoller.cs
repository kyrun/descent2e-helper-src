using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICombatRoller : MonoBehaviour
{
	[SerializeField] List<RawImage> _attackDice = default;
	[SerializeField] List<RawImage> _defenseDice = default;
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
		_textPierce.text = _textDefense.text = _textHeart.text = _textSurge.text = _textRange.text = _textDamage.text = "--";
	}

	void UpdateResult(RollResult result, IAttacker attacker)
	{
		_textHeart.text = result.heart.ToString();
		_textDefense.text = result.defense.ToString();
		_textSurge.text = result.surge.ToString();
		_textRange.text = attacker.AttackType == AttackType.Ranged ? result.range.ToString() : "N/A";
		var defense = result.defense - GetPierce(attacker);
		defense = Mathf.Max(0, defense);
		_textDamage.text = (result.heart - defense).ToString();
	}

	void UpdatePierce(IAttacker attacker)
	{
		int pierce = GetPierce(attacker);
		_textPierce.transform.parent.gameObject.SetActive(pierce > 0);
		_textPierce.text = pierce.ToString();
	}

	int GetPierce(IAttacker attacker)
	{
		int pierce = 0;
		if (attacker != null && attacker is Character)
		{
			pierce = ((Character)attacker).TotalPierce;
		}
		return pierce;
	}

	void ClearText()
	{
	}

	public void Setup(IAttacker attacker, IDefender defender)
	{
		gameObject.SetActive(true);
		_textAttacker.text = "Attacker: " + attacker.name;
		_textDefender.text = "Defender: " + defender.name;
		_attacker = attacker;
		_defender = defender;
	}

	public void ShowCombatRoll(List<AttackDieDef> attackDieDefs, List<DefenseDieDef> defenseDieDefs, List<int> attackRolledIndex, List<int> defendRolledIndex)
	{
		for (int i = 0; i < _attackDice.Count; ++i)
		{
			if (i < attackDieDefs.Count)
			{
				_attackDice[i].texture = attackDieDefs[i].GetFaceImage(attackRolledIndex[i]);
			}

			_attackDice[i].gameObject.SetActive(i < attackDieDefs.Count);
		}
		for (int i = 0; i < _defenseDice.Count; ++i)
		{
			if (i < defenseDieDefs.Count)
			{
				_defenseDice[i].texture = defenseDieDefs[i].GetFaceImage(defendRolledIndex[i]);
			}

			_defenseDice[i].gameObject.SetActive(i < defenseDieDefs.Count);
		}
	}

	public void Roll()
	{
		var result = Roller.CombatRoll(_attacker, _defender, out List<int> rolledFaceIndexAttack, out List<int> rolledFaceIndexDefense);
		ShowCombatRoll(_attacker.AttackDice, _defender.DefenseDice, rolledFaceIndexAttack, rolledFaceIndexDefense);
		UpdatePierce(_attacker);
		UpdateResult(result, _attacker);
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
