using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DieDef : ScriptableObject
{
	[SerializeField] List<Texture2D> _faceImage = default;

	public int Roll()
	{
		return Random.Range(0, _faceImage.Count);
	}

	public Texture2D GetFaceImage(int index)
	{
		return _faceImage[index];
	}
}
