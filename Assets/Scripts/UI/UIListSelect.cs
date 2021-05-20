using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public abstract class UIListSelect<T> : MonoBehaviour where T : ScriptableObject
{
	[SerializeField] protected List<T> _items = default;
	[SerializeField] protected Button _defaultButton = default;

	protected List<Button> _listButton = new List<Button>();

	void Awake()
	{
		for (int i = 0; i < _items.Count; ++i)
		{
			var button = _defaultButton;
			var def = _items[i];
			if (i > 0) button = Instantiate(_defaultButton, _defaultButton.transform.parent);
			SetButtonText(button, def.name);

			button.onClick.AddListener(() =>
			{
				OnButtonPress(def);
			});
			_listButton.Add(button);
		}
	}

	protected void SetButtonText(Button btn, string str)
	{
		btn.name = "Button " + str;
		btn.GetComponentInChildren<TextMeshProUGUI>(true).text = str;
	}

	protected abstract void OnButtonPress(T def);
}
