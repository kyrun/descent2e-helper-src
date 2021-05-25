using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCorrectPanelOnStart : MonoBehaviour
{
	[SerializeField] List<Transform> _toShow = default;

	Transform _toShowOverride = null;

	void OnEnable()
	{
		// Disable all first
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
		}

		if (_toShowOverride != null)
		{
			_toShowOverride.gameObject.SetActive(true);
			_toShowOverride = null;
		}
		else
		{
			foreach (Transform toShow in _toShow)
			{
				toShow.gameObject.SetActive(true);
			}
		}
	}

	public void OverrideToShow(Transform toShow)
	{
		_toShowOverride = toShow;
	}
}
