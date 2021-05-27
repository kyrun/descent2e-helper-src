using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAttributeSelect : MonoBehaviour
{
	[SerializeField] Button _btnWillpower = default;
	[SerializeField] Button _btnMight = default;
	[SerializeField] Button _btnAwareness = default;
	[SerializeField] Button _btnKnowledge = default;
	[SerializeField] UIAttributeTest _uiAttributeTest = default;

	void Awake()
	{
		_btnWillpower.onClick.AddListener(() =>
		{
			_uiAttributeTest.Init(Attribute.Willpower);
		});
		_btnMight.onClick.AddListener(() =>
		{
			_uiAttributeTest.Init(Attribute.Might);
		});
		_btnAwareness.onClick.AddListener(() =>
		{
			_uiAttributeTest.Init(Attribute.Awareness);
		});
		_btnKnowledge.onClick.AddListener(() =>
		{
			_uiAttributeTest.Init(Attribute.Knowledge);
		});
	}
}
