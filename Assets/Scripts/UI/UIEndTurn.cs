using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndTurn : MonoBehaviour
{
	public Action OnEndTurn;

	public void EndTurn()
	{
		OnEndTurn?.Invoke();
	}
}
