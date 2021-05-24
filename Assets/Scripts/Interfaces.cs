using System.Collections.Generic;

public interface IAttacker
{
	string name { get; }
	int Pierce { get; }
	int RangeModifier { get; }
	List<AttackDieDef> AttackDice { get; }
	AttackType AttackType { get; }
}

public interface IDefender
{
	string name { get; }
	List<DefenseDieDef> DefenseDice { get; }
}

public interface IHandItem
{
	Handedness Handedness { get; }
}