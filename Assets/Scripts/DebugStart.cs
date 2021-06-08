using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStart : MonoBehaviour
{
	void Awake()
	{
		if (Game.PlayerCharacter == null)
		{
			Game.PlayerCharacter = new Character(CharacterDef.Get("Ashrian"), ClassDef.Get("Knight"));
			//Game.PlayerCharacter.AddItem(ItemDef.Get("Steel Broadsword"));
			//Game.PlayerCharacter.AddItem(ItemDef.Get("Light Hammer"));
		}
	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//}
		//if (Input.GetKeyDown(KeyCode.Return))
		//{
		//}
	}
}
