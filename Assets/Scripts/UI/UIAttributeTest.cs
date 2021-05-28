using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAttributeTest : MonoBehaviour, IRerollable
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
	[SerializeField] Image _imgAttribute = default;
	[SerializeField] List<Sprite> _listSpriteAttribute = new List<Sprite>();

	int _attributeValueToTest;
	int _lastRoll0, _lastRoll1;
	bool _resultPass;
	Coroutine _coroutine;

	public System.Action OnPassTest;
	public System.Action OnFailTest;

	public bool IsRolling { get; private set; }

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

		IsRolling = true;

		_txtResult.text = "";

		_lastRoll0 = _die0.Roll();
		_lastRoll1 = _die1.Roll();
		_dieAnimator0.Roll(_die0, _lastRoll0, 0);
		_dieAnimator1.Roll(_die1, _lastRoll1, 1);

		if (_coroutine != null) StopCoroutine(_coroutine);
		var listDice = new List<DieAnimator>();
		listDice.Add(_dieAnimator0);
		listDice.Add(_dieAnimator1);
		_coroutine = StartCoroutine(WaitAndUpdateResult(_die0.GetDefensePerFace(_lastRoll0) + _die1.GetDefensePerFace(_lastRoll1) <= _attributeValueToTest, listDice));
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

		if (_coroutine != null) StopCoroutine(_coroutine);
		var listDice = new List<DieAnimator>();
		listDice.Add(dieAnimator);
		_coroutine = StartCoroutine(WaitAndUpdateResult(_die0.GetDefensePerFace(_lastRoll0) + _die1.GetDefensePerFace(_lastRoll1) <= _attributeValueToTest, listDice));
	}

	IEnumerator WaitAndUpdateResult(bool pass, List<DieAnimator> listDice)
	{
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

		IsRolling = false;
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
		_imgAttribute.sprite = _listSpriteAttribute[(int)attribute];
		Clear();
		gameObject.SetActive(true);
    }
}
