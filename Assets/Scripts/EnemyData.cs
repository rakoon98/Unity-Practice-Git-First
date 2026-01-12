using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
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