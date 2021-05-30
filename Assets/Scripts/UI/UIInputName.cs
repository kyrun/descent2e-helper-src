using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInputName : MonoBehaviour
{
	[SerializeField] TMP_InputField _inputField = default;
	[SerializeField] Button _btnStart = default;
	[SerializeField] GameObject _loadingOverlay = default;

	CharacterDef _characterDef;
	ClassDef _classDef;

	void Awake()
	{
		_btnStart.onClick.AddListener(OnStart);
		_inputField.onValueChanged.AddListener((newInput) =>
		{
			VerifyInputField();
		});
	}

	void OnEnable()
	{
		VerifyInputField();
	}

	void VerifyInputField()
	{
		_btnStart.interactable = _inputField.text.Length > 0;
	}

	void OnStart()
	{
		UIConfirm.Singleton.Confirm("Begin New Character?", () =>
		{
			_loadingOverlay.SetActive(true);
			Game.PlayerCharacter = new Character(_characterDef, _classDef);
			Game.SaveName = _inputField.text;
			Util.SaveCharacter();
			Game.GoToMainScene();
		});
	}

	public void Setup(CharacterDef charDef, ClassDef classDef)
	{
		_characterDef = charDef;
		_classDef = classDef;
		var saveName = _characterDef.name;
		var firstSpace = _characterDef.name.IndexOf(' ');

		if (firstSpace > 0) saveName = saveName.Substring(0, firstSpace);
		_inputField.text = saveName + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
		gameObject.SetActive(true);
	}
}
