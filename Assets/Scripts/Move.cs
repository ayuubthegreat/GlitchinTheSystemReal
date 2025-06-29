using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "RPG/Move")]
public class Move : ScriptableObject
{
    public string moveName;
    public int attackDamage;
    public bool isPhysicalMove;
    public bool isSpecialMove;
    public int attackLoweringOrRaisingNum;
    public int defenseLoweringOrRaisingNum;
    public int speedLoweringOrRaisingNum;
    public int accuracyLoweringOrRaisingNum;
}
