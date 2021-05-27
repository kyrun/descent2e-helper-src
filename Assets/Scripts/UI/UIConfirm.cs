using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIConfirm : MonoBehaviour
{
    public static UIConfirm Singleton { get; private set; }

    [SerializeField] GameObject _root = default;
    [SerializeField] Button _btnYes = default;
    [SerializeField] Button _btnNo = default;
    [SerializeField] TextMeshProUGUI _txtTitle = default;

    void Awake()
	{
        Singleton = this;
        _btnNo.onClick.AddListener(Close);

    }

	void OnDestroy()
	{
        Singleton = null;
	}

    void Close()
    {
        _root.SetActive(false);
    }

    public void Confirm(string textTitle, UnityAction action)
    {
        _root.SetActive(true);
        _txtTitle.text = textTitle;
        _btnYes.onClick.RemoveAllListeners();
        _btnYes.onClick.AddListener(()=>
        {
            action();
            Close();
        });
    }
}