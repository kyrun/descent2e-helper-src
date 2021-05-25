using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public abstract class UIListButton<T> : MonoBehaviour where T : ScriptableObject
{
	[SerializeField] protected Button _defaultButton = default;

	protected List<T> _items = new List<T>();
	protected List<Button> _listButton = new List<Button>();

	protected virtual void Awake()
	{
		T[] items = Resources.LoadAll<T>("");
		foreach (var item in items)
		{
			_items.Add(item);
		}

		Sort(_items);

		for (int i = 0; i < _items.Count; ++i)
		{
			var button = _defaultButton;
			var def = _items[i];
			if (i > 0) button = Instantiate(_defaultButton, _defaultButton.transform.parent);
			SetButtonText(button, ItemName(def));

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

	protected abstract void Sort(List<T> list);

	protected virtual string ItemName(T def)
	{
		return def.name;
	}
}
