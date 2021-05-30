using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeAct : MonoBehaviour
{
    [SerializeField] Button _btn = default;
    [SerializeField] Sprite _act1 = default;
    [SerializeField] Sprite _act2 = default;

    void Awake()
    {
        _btn.onClick.AddListener(OnButton);
    }

	void OnEnable()
	{
        StartCoroutine(WaitForPlayer());
	}

	IEnumerator WaitForPlayer()
	{
        while (!Game.IsReady) yield return null;

        _btn.image.sprite = Game.PlayerCharacter.Act == 1 ? _act1 : _act2;
	}

	void OnButton()
    {
        if (!Game.IsReady) return;

        var nextAct = Game.PlayerCharacter.Act + 1 > 2 ? "I" : "II";
        UIConfirm.Singleton.Confirm("Change to Act " + nextAct + "?", ToggleAct);
    }

    void ToggleAct()
    {
        ++Game.PlayerCharacter.Act;

        if (Game.PlayerCharacter.Act > 2) Game.PlayerCharacter.Act = 1;

        switch (Game.PlayerCharacter.Act)
        {
            case 1:
                _btn.image.sprite = _act1;
                break;
            case 2:
                _btn.image.sprite = _act2;
                break;
        }
    }
}
