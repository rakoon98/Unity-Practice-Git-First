using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Basic Stats")]
    public string characterName;
    public int maxHp;
    public float moveSpeed;

    [Header("Combat Stats")]
    public int attackPower;
    public float attackRange;

    [Header("Growth Data")]
    public int gold;
    public int level;
}