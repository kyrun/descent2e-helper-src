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
}
