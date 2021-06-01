using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIReminder : MonoBehaviour
{
	[SerializeField] ReminderPrerequisite.Type _reminderPrerequisite = default;
	[SerializeField] UIRowReminder _defaultRow = default;

	List<UIRowReminder> _listRow = new List<UIRowReminder>();

	void Awake()
	{
		_listRow.Add(_defaultRow);
	}

	void OnEnable()
	{
		StartCoroutine(WaitForReady());
	}

	IEnumerator WaitForReady()
	{
		while (!Game.IsReady) yield return null;

		Refresh();
	}

	void Refresh()
	{
		if (!Game.IsReady) return;

		var uiRow = _defaultRow;

		int i = 0;

		var listReminders = new List<IHasReminder>(Game.PlayerCharacter.Skills);
		listReminders = new List<IHasReminder>(listReminders
			.OrderBy(r => r.StaminaCost)
			.ThenBy(r => r.IsAction)
			.ThenBy(r => r.IsExhaustable)
			.ThenBy(r => r.name));

		foreach (var reminder in listReminders)
		{
			if (ReminderPrerequisite.MeetPrerequisite(reminder, _reminderPrerequisite))
			{
				if (i >= _listRow.Count)
				{
					uiRow = Instantiate(_defaultRow, _defaultRow.transform.parent);
					uiRow.name = _defaultRow.name + " " + i;
					_listRow.Add(uiRow);
				}
				else
				{
					uiRow = _listRow[i];
				}

				uiRow.Init(reminder.name, reminder.StaminaCost, reminder.IsAction, reminder.IsExhaustable);
				uiRow.gameObject.SetActive(true);

				++i;
			}
		}

		for (; i < _listRow.Count; ++i)
		{
			_listRow[i].gameObject.SetActive(false);
		}
	}
}
