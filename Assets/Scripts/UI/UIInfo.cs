using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIInfo : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _textCharacter = default;
	[SerializeField] TextMeshProUGUI _txtDmg = default;
	[SerializeField] TextMeshProUGUI _txtFatigue = default;
	[SerializeField] Button _btnIncDmg = default;
	[SerializeField] Button _btnDecDmg = default;
	[SerializeField] Button _btnIncFatigue = default;
	[SerializeField] Button _btnDecFatigue = default;
	[SerializeField] Button _btnKO = default;
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

	void Start()
	{
		if (Game.IsReady) _textCharacter.text = Game.PlayerCharacter.Definition.name + ", " + Game.PlayerCharacter.Class.name;
		else
		{
			_textCharacter.text = "Undefined Character, Undefined Class";
			Game.PlayerCharacter = new Character(Resources.Load("Characters/Healer/Avric Albright") as CharacterDef,
				Resources.Load("Classes/Healer/Disciple/Disciple") as ClassDef);
		}
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
