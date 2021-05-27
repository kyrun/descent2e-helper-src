using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public abstract class UIListButton<T> : MonoBehaviour where T : IListable
{
	public enum Type
	{
		OnAwake,
		OnEnable
	}

	[SerializeField] protected Type _type = Type.OnAwake;
	[SerializeField] protected Button _defaultButton = default;

	protected List<T> _items = new List<T>();
	protected List<Button> _listButton = new List<Button>();

	protected virtual void Awake()
	{
		_listButton.Add(_defaultButton);
		if (_type == Type.OnAwake)
		{
			Init();
		}
	}

	protected virtual void OnEnable()
	{
		if (_type == Type.OnEnable)
		{
			Init();
		}
	}

	void Init()
	{
		T[] items = LoadItems();
		_items.Clear();
		foreach (var item in items)
		{
			_items.Add(item);
		}

		Sort(_items);

		int i = 0;
		for (; i < _items.Count; ++i)
		{
			var def = _items[i];
			if (i >= _listButton.Count)
			{
				var button = Instantiate(_defaultButton, _defaultButton.transform.parent);
				_listButton.Add(button);
			}
			SetButtonText(_listButton[i], ItemName(def));

			_listButton[i].onClick.RemoveAllListeners();
			_listButton[i].onClick.AddListener(() =>
			{
				OnButtonPress(def);
			});
		}

		for (; i < _listButton.Count; ++i)
		{
			_listButton[i].gameObject.SetActive(false);
		}
	}

	protected void SetButtonText(Button btn, string str)
	{
		btn.name = "Button " + str;
		btn.GetComponentInChildren<TextMeshProUGUI>(true).text = str;
	}

	protected abstract T[] LoadItems();

	protected abstract void OnButtonPress(T def);

	protected abstract void Sort(List<T> list);

	protected virtual string ItemName(T item)
	{
		return item.name;
	}
}
