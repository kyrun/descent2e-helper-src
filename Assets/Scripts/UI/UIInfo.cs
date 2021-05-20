using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textCharacter = default;

	void Start()
	{
		if (Game.PlayerCharacter != null) _textCharacter.text = Game.PlayerCharacter.Definition.name + ", " + Game.PlayerCharacter.Class.name;
		else
		{
			_textCharacter.text = "Undefined Character, Undefined Class";
			Game.PlayerCharacter = new Character(Resources.Load("Characters/Healer/Avric Albright") as CharacterDef,
				Resources.Load("Classes/Healer/Disciple/Disciple") as ClassDef);
		}
	}
}
