using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ReminderPrerequisite
{
	[System.Flags]
	public enum Type
	{
		None = 0,

		OutOfTurn		= 1 << 0,
		DuringTurn		= 1 << 1,

		PreAttack		= 1 << 2,
		PostAttack		= 1 << 3,
	}

	public static bool MeetPrerequisite(IHasReminder item, Type flag)
	{
		foreach (var prereq in item.ReminderPrerequisites)
		{
			if (prereq.HasFlag(flag))
			{
				return true;
			}
		}

		return false;
	}
}