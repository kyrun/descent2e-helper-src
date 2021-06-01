using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRowReminder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtTitle = default;
    [SerializeField] TextMeshProUGUI _txtStaminaCost = default;
    [SerializeField] GameObject _gobStamina = default;
    [SerializeField] GameObject _gobAction = default;
    [SerializeField] GameObject _gobExhaust = default;

    public void Init(string title, int staminaCost, bool action, bool exhaust)
    {
        _txtTitle.text = title;
        _txtStaminaCost.text = staminaCost.ToString();
        _gobStamina.SetActive(staminaCost > 0);
        _gobAction.SetActive(action);
        _gobExhaust.SetActive(exhaust);
    }
}
