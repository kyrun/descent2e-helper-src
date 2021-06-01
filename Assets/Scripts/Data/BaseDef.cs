using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDef<T> : ScriptableObject, IListable where T : BaseDef<T>
{
	static Dictionary<string, T> _dictDef = new Dictionary<string, T>();

	public static T Get(string itemName)
	{
		if (_dictDef.TryGetValue(itemName, out T def))
		{
			return def;
		}
		return null;
	}

	void OnEnable()
	{
		if (!_dictDef.ContainsKey(name)) _dictDef.Add(name, this as T);
	}
}
