using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIListScriptableObj<T> : UIListButton<T> where T: ScriptableObject, IListable
{
	protected override T[] LoadItems()
	{
		return Resources.LoadAll<T>("");
	}
}
