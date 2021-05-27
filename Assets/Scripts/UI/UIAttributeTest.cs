using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAttributeTest : MonoBehaviour
{
	[SerializeField] Color _colorPass = Color.green;
	[SerializeField] Color _colorFail = Color.red;
	[SerializeField] TextMeshProUGUI _txtAttributeTitle = default;
	[SerializeField] TextMeshProUGUI _txtResult = default;
	[SerializeField] DefenseDieDef _die0 = default;
	[SerializeField] DefenseDieDef _die1 = default;
	[SerializeField] DieAnimator _dieAnimator0 = default;
	[SerializeField] DieAnimator _dieAnimator1 = default;
	[SerializeField] Button _btnRoll = default;
	[SerializeField] Button _btnClose = default;

	int _attributeValueToTest;
	bool _resultPass;
	Coroutine _coroutine;

	public System.Action OnPassTest;
	public System.Action OnFailTest;

	void Awake()
	{
		_btnRoll.onClick.AddListener(Roll);
		_btnClose.onClick.AddListener(() =>
		{
			if (_resultPass)
			{
				OnPassTest?.Invoke();
			}
			else
			{
				OnFailTest?.Invoke();
			}
			OnFailTest = null;
			OnPassTest = null;
		});
	}

	void OnEnable()
	{
		Clear();
	}

	public void Roll()
	{
		//Init((Attribute)Random.Range(0, 4)); // for testing puposes

		_txtResult.text = "";

		var roll0 = _die0.Roll();
		var roll1 = _die1.Roll();
		_dieAnimator0.Roll(_die0, roll0, 0);
		_dieAnimator1.Roll(_die1, roll1, 1);

		if (_coroutine != null) StopCoroutine(_coroutine);
		_coroutine = StartCoroutine(WaitAndUpdateResult(_die0.GetDefensePerFace(roll0) + _die1.GetDefensePerFace(roll1) <= _attributeValueToTest));
	}

	IEnumerator WaitAndUpdateResult(bool pass)
	{
		var listDice = new List<DieAnimator>();
		listDice.Add(_dieAnimator0);
		listDice.Add(_dieAnimator1);

		yield return DieAnimator.WaitForUntilAllDiceFinishRolling(listDice);

		_resultPass = pass;
		if (pass)
		{
			_txtResult.color = _colorPass;
			_txtResult.text = "PASS";
		}
		else
		{
			_txtResult.color = _colorFail;
			_txtResult.text = "FAIL";
		}

		_coroutine = null;
	}

	void Clear()
	{
		_txtResult.text = "";
		_dieAnimator0.SetFaceVisible(false);
		_dieAnimator1.SetFaceVisible(false);
	}

	public void Init(Attribute attribute)
    {
		_attributeValueToTest = Game.PlayerCharacter.GetAttribute(attribute);

		_txtAttributeTitle.text = attribute + " Test (" + _attributeValueToTest + ")";
		Clear();
		gameObject.SetActive(true);
    }
}
