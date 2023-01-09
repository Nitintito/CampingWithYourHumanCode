using UnityEngine;

[CreateAssetMenu(menuName = "Data/AIData", fileName = "AIData")]
public class AIData : ScriptableObject
{
    [Header("Patrol State")]

    [SerializeField] private float patrolInterval = 3f;
    public float PatrolInterval { get => patrolInterval; set => patrolInterval = value; }
    
    [SerializeField] private float patrolPointRange = 3f;
    public float PatrolPointRange { get => patrolPointRange; set => patrolPointRange = value; }

    [SerializeField] private float maxPatrolTime = 5f;
    public float MaxPatrolTime { get => maxPatrolTime; }

    [Header("Circulate State")]

    [SerializeField] private float circulateSpeed = 3f;
    public float CirculateSpeed { get => circulateSpeed; set => circulateSpeed = value; }

    [SerializeField] private float maxCirculateTime = 15f;
    public float MaxCirculateTime { get => maxCirculateTime; set => maxCirculateTime = value; }

    [SerializeField] private float waitingTimeBeforeAttack = 10f;
    public float WaitingTimeBeforeAttack { get => waitingTimeBeforeAttack; set => waitingTimeBeforeAttack = value; }

    private float lightRange;

    private Vector3 respawnPos;
    public Vector3 RespawnPos { get => respawnPos; set => respawnPos = value; }

}
