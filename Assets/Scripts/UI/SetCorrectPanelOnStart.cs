using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCorrectPanelOnStart : MonoBehaviour
{
	[SerializeField] List<Transform> _toShow = default;

	Transform _toShowOverride = null;

	void OnEnable()
	{
		foreach (Transform child in transform)
		{
			if (_toShowOverride != null)
			{
				child.gameObject.SetActive(child == _toShowOverride);
			}
			else
			{
				child.gameObject.SetActive(_toShow.Contains(child));
			}
		}
		_toShowOverride = null;
	}

	public void OverrideToShow(Transform toShow)
	{
		_toShowOverride = toShow;
	}
}
