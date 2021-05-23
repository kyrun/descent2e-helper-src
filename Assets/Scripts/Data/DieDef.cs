using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DieDef : ScriptableObject
{
	[SerializeField] List<Sprite> _faceImage = default;

	/// <summary>
	/// Returns the face index
	/// </summary>
	/// <returns></returns>
	public int Roll()
	{
		return Random.Range(0, _faceImage.Count);
	}

	public int NumFaces()
	{
		return _faceImage.Count;
	}

	public Sprite GetFaceImage(int index)
	{
		return _faceImage[index];
	}
}
