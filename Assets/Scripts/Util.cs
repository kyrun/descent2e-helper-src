using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
	const string SAVE_PATH = "";
	public const string SAVE_EXT = ".d2esav";

	public static string GetRootPath()
	{
		return
#if UNITY_EDITOR
			Directory.GetCurrentDirectory();
#else
			Application.persistentDataPath;
#endif
	}

	public static string GetSavePath()
	{
		return GetRootPath() + "/" + SAVE_PATH;
	}

	public static void SaveCharacter()
	{
		if (Game.SaveName == "")
		{
			Debug.LogWarning("Trying to save, but the save name is empty!");
			return;
		}
		using (StreamWriter sw = new StreamWriter(GetSavePath() + Game.SaveName + SAVE_EXT))
		{
			sw.Write(Game.PlayerCharacter.ToSaveString());
		}
	}
}
