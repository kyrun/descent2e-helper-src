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

		PreDefend		= 1 << 4,
		PostDefend		= 1 << 5,
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

	public static bool MeetPrerequisite(Type flag)
	{
		var listReminders = GetAllReminders();

		foreach (var reminder in listReminders)
		{
			if (MeetPrerequisite(reminder, flag)) return true;
		}

		return false;
	}

	public static List<IHasReminder> GetAllReminders()
	{
		List<IHasReminder> listReminders = new List<IHasReminder>(Game.PlayerCharacter.Skills);
		foreach (var item in Game.PlayerCharacter.Items)
		{
			if (Game.PlayerCharacter.IsEquipped(item))
			{
				listReminders.Add(item);
			}
		}

		return listReminders;
	}
}