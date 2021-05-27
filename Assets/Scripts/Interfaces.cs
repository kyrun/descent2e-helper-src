using System.Collections.Generic;

public interface IAttacker : IListable
{
	int Pierce { get; }
	int RangeModifier { get; }
	List<AttackDieDef> AttackDice { get; }
	AttackType AttackType { get; }
}

public interface IDefender : IListable
{
	List<DefenseDieDef> DefenseDice { get; }
}

public interface IHandItem
{
	Handedness Handedness { get; }
}

public interface IListable
{
	string name { get; }
}