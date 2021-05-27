using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClearAllTokens : MonoBehaviour
{
    [SerializeField] Button _btn = default;

    void Awake()
    {
        _btn.onClick.AddListener(() =>
		{
			UIConfirm.Singleton.Confirm("Are you sure you want to clear all Damage, Fatigue and Conditions on this hero?", ClearAllTokens);
		});
    }

	void ClearAllTokens()
	{
		while (Game.PlayerCharacter.Damage > 0)
		{
			Game.PlayerCharacter.DecrementDamage();
		}
		while (Game.PlayerCharacter.Fatigue > 0)
		{
			Game.PlayerCharacter.DecrementFatigue();
		}
		foreach (Condition condition in System.Enum.GetValues(typeof(Condition)))
		{
			if (Game.PlayerCharacter.Conditions.Contains(condition)) Game.PlayerCharacter.RemoveCondition(condition);
		}
		var uiInfo = FindObjectOfType<UIInfo>();
		uiInfo.UpdateDamage();
		uiInfo.UpdateFatigue();
	}
}
