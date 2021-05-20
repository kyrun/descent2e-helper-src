using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Attack Die", order = 10000)]
public class AttackDieDef : DieDef
{
	[SerializeField] Color _color = Color.blue;
	[SerializeField] List<AttackDieFace> _face = default;

	public AttackDieFace GetFace(int index)
	{
		return _face[index];
	}
}

[System.Serializable]
public class AttackDieFace
{
	public int heart = 0;
	public int surge = 0;
	public int range = 0;

	public bool IsMiss
	{
		get { return heart == 0 && surge == 0 && range == 0; }
	}

	public override string ToString()
	{
		return "H" + heart + " S" + surge + " R" + range;
	}
}