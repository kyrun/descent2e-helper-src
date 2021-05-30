using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDoKO : MonoBehaviour
{
    [SerializeField] Button _btnKO = default;

	void Awake()
	{
		_btnKO.onClick.AddListener(() =>
		{
			UIConfirm.Singleton.Confirm("Are you sure you want to KO this hero?", KO);
		});
	}

	void KO()
	{
		if (Game.PlayerCharacter == null) return;
		while (Game.PlayerCharacter.Damage < Game.PlayerCharacter.Health) Game.PlayerCharacter.IncrementDamage();
		while (Game.PlayerCharacter.Fatigue < Game.PlayerCharacter.Stamina) Game.PlayerCharacter.IncrementFatigue();

		while (Game.PlayerCharacter.Conditions.Count > 0)
		{
			Game.PlayerCharacter.RemoveCondition(Game.PlayerCharacter.Conditions[0]);
		}
	}
}
