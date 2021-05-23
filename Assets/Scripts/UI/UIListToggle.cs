using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UIListToggle<T> : MonoBehaviour where T : ScriptableObject
{
	[SerializeField] protected Toggle _defaultToggle = default;

	protected List<T> _items = new List<T>();
	protected List<Toggle> _listToggle = new List<Toggle>();

	IEnumerator Start()
	{
		while (!Game.IsReady) yield return null;

		if (_items.Count == 0)
		{
			PopulateItemList();

			for (int i = 0; i < _items.Count; ++i)
			{
				var toggle = _defaultToggle;
				var def = _items[i];
				if (i > 0) toggle = Instantiate(_defaultToggle, _defaultToggle.transform.parent);
				SetButtonText(toggle, ItemName(def));

				toggle.onValueChanged.AddListener((isOn) =>
				{
					OnToggle(def, isOn);
				});
				_listToggle.Add(toggle);
			}
		}

		OnEnable();
	}

	void OnEnable()
	{
		if (!Game.IsReady) return;

		for (int i = 0; i < _items.Count; ++i)
		{
			_listToggle[i].SetIsOnWithoutNotify(ItemIsToggled(i));
		}
	}

	void OnToggle(T def, bool isOn)
	{
		if (isOn)
		{
			OnToggleOn(def);
		}
		else
		{
			OnToggleOff(def);
		}
	}

	void SetButtonText(Toggle toggle, string str)
	{
		toggle.name = "Toggle " + str;
		toggle.GetComponentInChildren<TextMeshProUGUI>(true).text = str;
	}

	protected abstract void PopulateItemList();

	protected abstract void OnToggleOn(T item);

	protected abstract void OnToggleOff(T item);

	protected abstract bool ItemIsToggled(int index);

	protected virtual string ItemName(T def)
	{
		return def.name;
	}
}
