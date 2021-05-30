using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIVitals : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _txtDmg = default;
	[SerializeField] TextMeshProUGUI _txtFatigue = default;
	[SerializeField] Button _btnIncDmg = default;
	[SerializeField] Button _btnDecDmg = default;
	[SerializeField] Button _btnIncFatigue = default;
	[SerializeField] Button _btnDecFatigue = default;
	[SerializeField] RectTransform _rectBGDmg = default;
	[SerializeField] RectTransform _rectFillDmg = default;
	[SerializeField] RectTransform _rectBGFatigue = default;
	[SerializeField] RectTransform _rectFillFatigue = default;

	void Awake()
	{
		_btnIncDmg.onClick.AddListener(() =>
		{
			Game.PlayerCharacter?.IncrementDamage();
			UpdateDamage();
		});
		_btnDecDmg.onClick.AddListener(() =>
		{
			Game.PlayerCharacter?.DecrementDamage();
			UpdateDamage();
		});
		_btnIncFatigue.onClick.AddListener(() =>
		{
			Game.PlayerCharacter?.IncrementFatigue();
			UpdateFatigue();
		});
		_btnDecFatigue.onClick.AddListener(() =>
		{
			Game.PlayerCharacter?.DecrementFatigue();
			UpdateFatigue();
		});
	}

	void Update()
	{
		if (!Game.IsReady) return;

		// TODO: don't update every frame
		UpdateDamage();
		UpdateFatigue();
	}

	void SetFillBar(float numerator, float denominator, RectTransform bg, RectTransform fill)
	{
		float pct = numerator / denominator;
		pct = Mathf.Min(1, pct);
		var sizeDelta = fill.sizeDelta;
		sizeDelta.x = pct * bg.rect.width;
		fill.sizeDelta = sizeDelta;
	}

	public void UpdateDamage()
	{
		if (Game.PlayerCharacter == null) return;

		_txtDmg.text = Game.PlayerCharacter.Damage + "/" + Game.PlayerCharacter.Health;
		SetFillBar(Game.PlayerCharacter.Damage, Game.PlayerCharacter.Health, _rectBGDmg, _rectFillDmg);
	}

	public void UpdateFatigue()
	{
		if (Game.PlayerCharacter == null) return;

		_txtFatigue.text = Game.PlayerCharacter.Fatigue + "/" + Game.PlayerCharacter.Stamina;
		SetFillBar(Game.PlayerCharacter.Fatigue, Game.PlayerCharacter.Stamina, _rectBGFatigue, _rectFillFatigue);
	}
}
