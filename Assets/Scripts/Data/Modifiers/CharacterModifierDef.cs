using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterModifierDef : ScriptableObject
{
	[SerializeField] List<CharacterModifierDef> _replaces = new List<CharacterModifierDef>();

	protected abstract void OnActivate(Character character);
	protected abstract void OnDeactivate(Character character);

	public void Activate(Character character)
	{
		foreach (var replace in _replaces)
		{
			replace.Deactivate(character);
		}
		OnActivate(character);
	}

	public void Deactivate(Character character)
	{
		foreach (var replace in _replaces)
		{
			replace.Activate(character);
		}
		OnDeactivate(character);
	}
}
