using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: make generic die rolling script instead of repeat
public class UIRecoveryRoll : MonoBehaviour, IRerollable
{
	[SerializeField] AttackDieDef _die0 = default;
	[SerializeField] AttackDieDef _die1 = default;
	[SerializeField] DieAnimator _dieAnimator0 = default;
	[SerializeField] DieAnimator _dieAnimator1 = default;
	[SerializeField] Button _btnRoll = default;
	[SerializeField] Button _btnApply = default;

	int _recoverDamage = 0;
	int _recoverFatigue = 0;
	int _lastRoll0, _lastRoll1;
	Coroutine _coroutine;

	public bool IsRolling { get; private set; }

	void Awake()
	{
		_btnRoll.onClick.AddListener(Roll);
		_btnApply.onClick.AddListener(OnApply);
	}

	void OnEnable()
	{
		_dieAnimator0.SetFaceVisible(false);
		_dieAnimator1.SetFaceVisible(false);
		_recoverDamage = _recoverFatigue = 0;
		_btnApply.gameObject.SetActive(false);
	}

	public void Roll()
	{
		IsRolling = true;

		_lastRoll0 = _die0.Roll();
		_lastRoll1 = _die1.Roll();
		_dieAnimator0.Roll(_die0, _lastRoll0, 0);
		_dieAnimator1.Roll(_die1, _lastRoll1, 1);

		UpdateRecoveryVarsFromDieFace();

		if (_coroutine != null) StopCoroutine(_coroutine);
		var listDice = new List<DieAnimator>();
		listDice.Add(_dieAnimator0);
		listDice.Add(_dieAnimator1);
		_coroutine = StartCoroutine(WaitAndUpdateResult(listDice));
	}

	public void RerollOneDie(DieAnimator dieAnimator)
	{
		IsRolling = true;

		if (dieAnimator == _dieAnimator0)
		{
			_lastRoll0 = _die0.Roll();
			_dieAnimator0.Roll(_die0, _lastRoll0, 0);
		}
		else if (dieAnimator == _dieAnimator1)
		{
			_lastRoll1 = _die1.Roll();
			_dieAnimator1.Roll(_die1, _lastRoll1, 0);
		}

		UpdateRecoveryVarsFromDieFace();

		if (_coroutine != null) StopCoroutine(_coroutine);
		var listDice = new List<DieAnimator>();
		listDice.Add(dieAnimator);
		_coroutine = StartCoroutine(WaitAndUpdateResult(listDice));
	}

	IEnumerator WaitAndUpdateResult(List<DieAnimator> listDice)
	{
		yield return DieAnimator.WaitForUntilAllDiceFinishRolling(listDice);

		_btnApply.gameObject.SetActive(true);

		IsRolling = false;
		_coroutine = null;
	}

	void UpdateRecoveryVarsFromDieFace()
	{
		var dieFace0 = _die0.GetFace(_lastRoll0);
		var dieFace1 = _die1.GetFace(_lastRoll1);

		_recoverDamage = dieFace0.heart + dieFace1.heart;
		_recoverFatigue = dieFace0.surge + dieFace1.surge;
	}

	void OnApply()
	{
		if (Game.PlayerCharacter == null) return;

		for (int i = 0; i < _recoverDamage; ++i)
		{
			Game.PlayerCharacter.DecrementDamage();
		}
		for (int i = 0; i < _recoverFatigue; ++i)
		{
			Game.PlayerCharacter.DecrementFatigue();
		}

		var uiInfo = FindObjectOfType<UIVitals>();
		uiInfo.UpdateDamage();
		uiInfo.UpdateFatigue();
	}
}
