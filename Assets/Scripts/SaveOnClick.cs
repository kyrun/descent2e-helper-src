using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) TrySave();
    }

	void OnApplicationQuit()
	{
        TrySave();
	}

    void TrySave()
    {
        if (Game.IsReady && Game.SaveName != "")
        {
            Util.SaveCharacter();
        }
    }
}
