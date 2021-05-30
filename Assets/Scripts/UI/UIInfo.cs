using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textCharacter = default;

	void Start()
	{
		if (Game.IsReady) _textCharacter.text = Game.PlayerCharacter.Definition.name + ", " + Game.PlayerCharacter.Class.name;
		else
		{
			_textCharacter.text = "Undefined Character, Undefined Class";
		}
	}
}
