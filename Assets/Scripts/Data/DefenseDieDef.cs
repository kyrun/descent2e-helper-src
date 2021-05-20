using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Defense Die", order = 10000)]
public class DefenseDieDef : DieDef
{
	[SerializeField] Color _color = Color.blue;
	[SerializeField] List<int> _defensePerFace = default;

	public int GetDefensePerFace(int index)
	{
		return _defensePerFace[index];
	}
}