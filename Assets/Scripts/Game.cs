using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
	public static int Act = 1;
	public static int NumPlayers = 4;

	public static Character PlayerCharacter;

	public static bool IsReady { get { return PlayerCharacter != null; } }
}
