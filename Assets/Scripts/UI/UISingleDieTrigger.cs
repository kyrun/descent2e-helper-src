using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(DieAnimator))]
public class UISingleDieTrigger : MonoBehaviour
{
    const float DOUBLE_CLICK_DURATION_MAX = 0.5f;

    [SerializeField] Button _btn = default;

    DieAnimator _dieAnimator;
    IRerollable _roller = default;

    DateTime _clickTimeStamp = DateTime.MinValue;

    void Awake()
    {
        _roller = GetComponentInParent<IRerollable>(); // HARDCODED: hierarchy
        _dieAnimator = GetComponent<DieAnimator>();
        _btn.onClick.AddListener(OnClick);
    }

	void Update()
    {
        if (_clickTimeStamp != DateTime.MinValue)
        {
            var diff = DateTime.Now - _clickTimeStamp;
            if (diff.TotalSeconds > DOUBLE_CLICK_DURATION_MAX)
            {
                _clickTimeStamp = DateTime.MinValue;
            }
        }
    }

	void OnClick()
    {
        if (_clickTimeStamp == DateTime.MinValue)
        {
            _clickTimeStamp = DateTime.Now;
        }
        else
        {
            var diff = DateTime.Now - _clickTimeStamp;
            if (diff.TotalSeconds <= DOUBLE_CLICK_DURATION_MAX)
            {
                if (!_roller.IsRolling)
                {
                    _roller.RerollOneDie(_dieAnimator);
                }
                else print("still rolling!");
            }

            _clickTimeStamp = DateTime.MinValue;
        }
    }
}
