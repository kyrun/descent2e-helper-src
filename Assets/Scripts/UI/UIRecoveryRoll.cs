using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: make generic die rolling script instead of repeat
public class UIRecoveryRoll : MonoBehaviour
{
	[SerializeField] AttackDieDef _die0 = default;
	[SerializeField] AttackDieDef _die1 = default;
	[SerializeField] DieAnimator _dieAnimator0 = default;
	[SerializeField] DieAnimator _dieAnimator1 = default;
	[SerializeField] Button _btnRoll = default;
	[SerializeField] Button _btnClose = default;

	int _recoverDamage = 0;
	int _recoverFatigue = 0;
	Coroutine _coroutine;

	void Awake()
	{
		_btnRoll.onClick.AddListener(Roll);
		_btnClose.onClick.AddListener(OnClose);
	}

	void OnEnable()
	{
		_dieAnimator0.SetFaceVisible(false);
		_dieAnimator1.SetFaceVisible(false);
		_recoverDamage = _recoverFatigue = 0;
	}

	public void Roll()
	{
		var roll0 = _die0.Roll();
		var roll1 = _die1.Roll();
		_dieAnimator0.Roll(_die0, roll0, 0);
		_dieAnimator1.Roll(_die1, roll1, 1);

		var dieFace0 = _die0.GetFace(roll0);
		var dieFace1 = _die1.GetFace(roll1);

		_recoverDamage = dieFace0.heart + dieFace1.heart;
		_recoverFatigue = dieFace0.surge + dieFace1.surge;

		if (_coroutine != null) StopCoroutine(_coroutine);
		_coroutine = StartCoroutine(WaitAndUpdateResult());
	}

	IEnumerator WaitAndUpdateResult()
	{
		var listDice = new List<DieAnimator>();
		listDice.Add(_dieAnimator0);
		listDice.Add(_dieAnimator1);

		yield return DieAnimator.WaitForUntilAllDiceFinishRolling(listDice);

		_coroutine = null;
	}

	void OnClose()
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

		var uiInfo = FindObjectOfType<UIInfo>();
		uiInfo.UpdateDamage();
		uiInfo.UpdateFatigue();
	}
}
