using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILoadGame : MonoBehaviour
{
	[SerializeField] protected Button _defaultButton = default;
	[SerializeField] GameObject _overlay = default;

	List<string> _saveGamePaths = new List<string>();
	List<Button> _listButton = new List<Button>();

	void Awake()
	{
		_listButton.Add(_defaultButton);
	}

	void OnEnable()
	{
		_saveGamePaths.Clear();

		var path = Util.GetSavePath();

		try
		{
			var files = Directory.GetFiles(path);
			foreach (var file in files)
			{
				if (Path.GetExtension(file) == Util.SAVE_EXT)
				{
					_saveGamePaths.Add(file);
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.LogWarning(e);
		}

		int i = 0;
		for (; i < _saveGamePaths.Count; ++i)
		{
			var saveGamePath = _saveGamePaths[i];

			Button button;
			if (i < _listButton.Count) button = _listButton[i];
			else
			{
				button = Instantiate(_defaultButton, _defaultButton.transform.parent);
				_listButton.Add(button);
			}

			SetButtonText(button, Path.GetFileNameWithoutExtension(saveGamePath));

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() =>
			{
				OnButtonPress(saveGamePath);
			});

			button.gameObject.SetActive(true);
		}
		for (; i < _listButton.Count; ++i)
		{
			_listButton[i].gameObject.SetActive(false);
		}
	}

	void SetButtonText(Button btn, string str)
	{
		btn.name = "Button " + str;
		btn.GetComponentInChildren<TextMeshProUGUI>(true).text = str;
	}

	void OnButtonPress(string saveGamePath)
	{
		_overlay.SetActive(true);

		using (StreamReader sr = File.OpenText(saveGamePath))
		{
			var saveString = sr.ReadToEnd();

			Game.PlayerCharacter = new Character(saveString);
			Game.SaveName = Path.GetFileNameWithoutExtension(saveGamePath);
		}

		Game.GoToMainScene();
	}
}
