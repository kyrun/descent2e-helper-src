using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOkAndNext : MonoBehaviour
{
	[SerializeField] Button _btnOK = default;

	public Button BtnOK { get { return _btnOK; } }

	void Awake()
	{
		_btnOK.onClick.AddListener(Next);
	}

	public void Next()
	{
		gameObject.SetActive(false);
		if (transform.parent == null) return;

		var nextIndex = transform.GetSiblingIndex() + 1;
		var next = transform.parent.GetChild(nextIndex);
		if (next != null)
		{
			next.gameObject.SetActive(true);
		}
	}
}
