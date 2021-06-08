using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBtnReminder : MonoBehaviour
{
	[SerializeField] ReminderPrerequisite.Type _reminderPrerequisite = default;
	[SerializeField] float _delay = 0;
	[SerializeField] Image _img = default;
	[SerializeField] Button _btn = default;
    [SerializeField] Animation _anim = default;
    UIReminder _uiReminder;

    void Awake()
    {
        _uiReminder = FindObjectOfType<UIReminder>(true);

        _btn.onClick.AddListener(OnButton);
    }

	void OnEnable()
	{
		SetVisible(false);

		StartCoroutine(WaitUntilReady());
    }

	IEnumerator WaitUntilReady()
	{
		while (!Game.IsReady) yield return null;

		if (ReminderPrerequisite.MeetPrerequisite(_reminderPrerequisite))
		{
			StartCoroutine(DelayShowButton());
		}
	}

	IEnumerator DelayShowButton()
	{
		SetVisible(false);

		var ctr = 0f;
		while (ctr < _delay)
		{
			yield return null;

			if (!enabled)
			{
				yield break;
			}
			ctr += Time.deltaTime;
		}

		SetVisible(true);
	}

	void OnButton()
    {
        _anim.Rewind();
        _anim.Sample();
        _anim.Stop();
		_uiReminder.gameObject.SetActive(true);
		_uiReminder.Refresh(_reminderPrerequisite);
	}

	void SetVisible(bool visible)
	{
		_anim.enabled = _img.enabled = _btn.enabled = visible;
	}
}
