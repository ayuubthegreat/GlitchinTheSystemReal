using UnityEngine;

public enum MoveCategory
{
    Physical,
    Special,
    Status
}

public enum MoveTarget
{
    Opponent,
    Self,
    All, 
}

[CreateAssetMenu(fileName = "Move", menuName = "RPG/Move")]
public class Move : ScriptableObject
{
    public string moveName;
    public int power; // formerly attackDamage
    public int accuracy; // 0-100
    public int priority; // 0 is normal, higher goes first
    public MoveCategory category; // Physical, Special, Status
    public string effectDescription; // e.g., "May lower defense"
    public MoveTarget target; // e.g., Opponent, Self

    // Stat changes (if any)
    public int attackChange;
    public int defenseChange;
    public int speedChange;
    public int accuracyChange;
}
