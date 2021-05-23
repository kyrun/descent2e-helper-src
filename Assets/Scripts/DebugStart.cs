using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStart : MonoBehaviour
{
	void Awake()
	{
		if (Game.PlayerCharacter == null)
		{
			Game.PlayerCharacter = new Character(Resources.Load("Characters/Healer/Avric Albright") as CharacterDef,
				Resources.Load("Classes/Healer/Disciple/Disciple") as ClassDef);

			//Game.PlayerCharacter.AddItem(Resources.Load("Items/Armor/Leather Armor") as ItemArmorDef);
			//Game.PlayerCharacter.AddItem(Resources.Load("Items/Armor/Chainmail") as ItemArmorDef);
			//Game.PlayerCharacter.AddItem(Resources.Load("Items/OneHand/Steel Broadsword") as ItemWeaponDef);
			//Game.PlayerCharacter.AddItem(Resources.Load("Items/OneHand/Iron Shield") as ItemShieldDef);
			//Game.PlayerCharacter.AddItem(Resources.Load("Items/TwoHand/Sunburst") as ItemWeaponDef);
			//Game.PlayerCharacter.AddItem(Resources.Load("Items/Other/Ring of Power") as ItemOtherDef);
			//Game.PlayerCharacter.AddItem(Resources.Load("Items/Other/Mana Weave") as ItemOtherDef);
			//Game.PlayerCharacter.AddItem(Resources.Load("Items/Other/Scorpion Helm") as ItemOtherDef);
		}
	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	var newChar = new Character(Game.PlayerCharacter.ToSaveString());
		//	print(newChar.ToSaveString());
		//}
		//if (Input.GetKeyDown(KeyCode.Return))
		//{
		//	print(Game.PlayerCharacter.ToSaveString());
		//}
	}
}
