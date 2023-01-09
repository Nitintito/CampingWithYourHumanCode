using UnityEngine;

public class AIIdleState : AIState
{

    private bool patrolPointSet = false;
    private Vector3 patrolPoint;
    private float startTime;
    private float recordTime;
    public float RecordTime { get => recordTime; set => recordTime = value; }

    [SerializeField] private Transform campFire;
    public AIIdleState(AI ai, AIStateMachine StateMachine, AIData data, string name) : base(ai, StateMachine, data, name)
    {

    }

    public override void Enter()
    {
        base.Enter();
        campFire = GameObject.FindWithTag("CampFireGroup").transform;

        ai.animator.SetBool("walk", true);
        startTime = Time.time;
        ai.ResetPath();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Patrolling();
        CheckShouldChangeState();
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }

    private void Patrolling()
    {
        if (!patrolPointSet && Time.time > startTime + data.PatrolInterval) SetWalkPoint();

        if(patrolPointSet)
        {
            startTime = Time.time;  
            ai.ResetPath();
            ai.AISetDestination(patrolPoint);
            patrolPointSet = false;
        }

    }

    private void SetWalkPoint()
    {
        int cnt = 0;

        cnt += 1;
        float randomZ = Random.Range(-data.PatrolPointRange, data.PatrolPointRange);
        float randomX = Random.Range(-data.PatrolPointRange, data.PatrolPointRange);
        patrolPoint = new Vector3(ai.transform.position.x + randomX, ai.transform.position.y, ai.transform.position.z + randomZ);

        float diffX = patrolPoint.x - campFire.position.x;
        float diffZ = patrolPoint.z - campFire.position.z;
        float dis = Mathf.Sqrt(diffX * diffX + diffZ * diffZ);

        patrolPointSet = true;
    //    if (dis > 50f || cnt > Mathf.Pow(10, 6))
    //    {
    //    }
        

    }

    private void CheckShouldChangeState()
    {
        recordTime += Time.deltaTime;

        if (recordTime > data.MaxPatrolTime)
        {
            recordTime = 0;
            stateMachine.ChangeState(ai.circulateState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ai.animator.SetBool("walk", false);
    }
}
