using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class UIBtnStartAttributeTest : MonoBehaviour
{
    [SerializeField] Attribute _attribute = default;

	void Awake()
	{
        var uiAttributeTest = FindObjectOfType<UIAttributeTest>(true);
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            uiAttributeTest.Init(_attribute);
        });
	}
}
