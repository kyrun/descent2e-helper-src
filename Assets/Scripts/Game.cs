using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Game
{
	public static string SaveName;
	public static Character PlayerCharacter;

	public static bool IsReady { get { return PlayerCharacter != null; } }

	public static void GoToMainScene()
	{
		SceneManager.LoadScene("Main");
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Init()
	{
		Resources.LoadAll<CharacterDef>("");
		Resources.LoadAll<ClassDef>("");
		Resources.LoadAll<ItemDef>("");
		Resources.LoadAll<SkillDef>("");
		Resources.LoadAll<MonsterDef>("");
		Resources.LoadAll<DieDef>("");
		Resources.LoadAll<CharacterModifierDef>("");
	}
}
