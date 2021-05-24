using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterModifierDef : ScriptableObject
{
	public abstract void OnActivate(Character character);
	public abstract void OnDeactivate(Character character);
}
