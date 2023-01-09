using UnityEngine;

public class AICirculateState : AIState
{
    private GameObject campFire;
    private float circulateSpeed;
    private float closestFireDis = Mathf.Infinity;
    private Transform closestFire;

    private bool startCirculate = false;
    private float startCirculateTime = -1f;
    private float startWaitinToAttackTime = -1f;

    public AICirculateState(AI ai, AIStateMachine StateMachine, AIData data, string name) : base(ai, StateMachine, data, name)
    {
    }

    public override void Enter()
    {
        base.Enter();

        circulateSpeed = data.CirculateSpeed;

        FindCloestFire();

        ai.AISetDestination(closestFire.position);

        ai.animator.SetBool("walk", true);

        ai.PlayGhostAttackVoice();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckShouldStartCirculate();
        
        StartCirculate();   
        
        WaitForAttacking();
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }

    public void FindCloestFire()
    {

        campFire = GameObject.FindGameObjectWithTag("CampFireGroup");

        foreach (Transform child in campFire.transform)
        {
            if (Vector3.Distance(child.position, ai.transform.position) < closestFireDis)
            {
                closestFireDis = Vector3.Distance(child.position, ai.transform.position);
                closestFire = child.Find("CirculatePoint");
            }
        }
    }

    public void CheckShouldStartCirculate()
    {
        float diffX = ai.transform.position.x - closestFire.position.x;
        float diffZ = ai.transform.position.z - closestFire.position.z;
        float dis = Mathf.Sqrt(diffX * diffX + diffZ * diffZ);

        if ((dis < 2f || ai.CheckArrived()) && !startCirculate)
        {

            ai.ResetPath();
            
            startCirculate = true;
            startCirculateTime = Time.time;
            ai.animator.SetBool("walk", false);
            ai.animator.SetBool("scream", true);
        }
    }

    public void StartCirculate()
    {
        if (startCirculate)
        {

            Vector3 campV = new Vector3(campFire.transform.position.x, ai.transform.position.y, campFire.transform.position.z);
            Vector3 aiV = ai.transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(campV - aiV, aiV);
            var str = Mathf.Min(5 * Time.deltaTime, 1);
            ai.transform.rotation = Quaternion.Lerp(ai.transform.rotation, targetRotation, str);
            ai.transform.RotateAround(campFire.transform.position, Vector3.up, circulateSpeed * Time.deltaTime);
        }

        
        if (Time.time > startCirculateTime + data.MaxCirculateTime && startCirculateTime != -1f && startWaitinToAttackTime == -1f)
        {
            startWaitinToAttackTime = Time.time;
            startCirculate = false;
        }
    }

    public void WaitForAttacking()
    {
        if(Time.time > startWaitinToAttackTime + data.WaitingTimeBeforeAttack && startWaitinToAttackTime != -1f)
        {
            stateMachine.ChangeState(ai.attackState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ai.animator.SetBool("scream", false);
    }
}
