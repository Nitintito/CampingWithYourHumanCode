using UnityEngine;

public class AIGoBackState : AIState
{
    private GameObject campFire;
    private Vector3 des;
    public AIGoBackState(AI ai, AIStateMachine StateMachine, AIData data, string name) : base(ai, StateMachine, data, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        campFire = GameObject.FindGameObjectWithTag("CampFireGroup");

        ai.animator.SetBool("stagger", true);
        des = 3 * ai.transform.position - 2 * campFire.transform.position;
        ai.AISetDestination(des);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float diffX = ai.transform.position.x - des.x;
        float diffZ = ai.transform.position.z - des.z;
        float dis = Mathf.Sqrt(diffX * diffX + diffZ* diffZ);

        if (dis < 1f || ai.CheckArrived())
        {
            data.RespawnPos = ai.transform.position;
            ai.Restart();
        }
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }
}
