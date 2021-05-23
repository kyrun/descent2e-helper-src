using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIClassSelect : UIListButton<ClassDef>
{
    [SerializeField] UIInputName _uiInputName = default;

    CharacterDef _characterDef;

    protected override void OnButtonPress(ClassDef classDef)
    {
        _uiInputName.Setup(_characterDef, classDef);
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
