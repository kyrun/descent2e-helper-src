using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnClick : MonoBehaviour
{
    void Update()
    {
        if (Game.IsReady && Game.SaveName != "" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Util.SaveCharacter();
        }
    }

	void OnApplicationQuit()
	{
        if (Game.IsReady) Util.SaveCharacter();
	}
}
