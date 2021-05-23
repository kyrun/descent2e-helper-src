using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIClassSelect : UIListButton<ClassDef>
{
    CharacterDef _characterDef;

    protected override void OnButtonPress(ClassDef classDef)
    {
        Game.PlayerCharacter = new Character(_characterDef, classDef);
        SceneManager.LoadScene("Main");
    }

    public void Setup(CharacterDef charDef)
    {
        gameObject.SetActive(true);
        _characterDef = charDef;
        for (int i = 0; i < _listButton.Count; ++i)
        {
            _listButton[i].gameObject.SetActive(_items[i].Archetype == charDef.Archetype);
        }
    }
}
