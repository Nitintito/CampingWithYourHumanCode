using UnityEngine;

public class AIAttackState : AIState
{
    private GameObject campFire;
    private LayerMask fireLayer;
    private bool goBack = false;
    public AIAttackState(AI ai, AIStateMachine StateMachine, AIData data, string name) : base(ai, StateMachine, data, name)
    {
    }

    public override void Enter()
    {
        base.Enter();

        campFire = GameObject.FindGameObjectWithTag("CampFireGroup");
        fireLayer = LayerMask.GetMask("Fire");
        ai.AISetDestination(campFire.transform.position);
        ai.animator.SetBool("walk", true);
    }

    public override void Exit()
    {
        base.Exit();
        ai.animator.SetBool("walk", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float diffX = ai.transform.position.x - campFire.transform.position.x;
        float diffZ = ai.transform.position.z - campFire.transform.position.z;
        float dis = Mathf.Sqrt(diffX * diffX + diffZ * diffZ);

        if(dis < 2f || ai.CheckArrived())
        {
            ai.animator.SetBool("attack", true);
            ai.ResetPath();
            ai.InvokeGameOver();
        }

        if (Physics.CheckSphere(ai.transform.position, 9.5f, fireLayer) && !goBack)
        {
            Collider[] fires = Physics.OverlapSphere(ai.transform.position, 9.5f, fireLayer);

            foreach (var fire in fires)
            {
                FireMechanics tmp;
                fire.TryGetComponent<FireMechanics>(out tmp);
                tmp = fire.transform.parent.GetComponent<FireMechanics>();
                if (tmp.Fire.CurrentTimeLeft > 0)
                {
                    Debug.Log("scared by fire");
                    goBack = true;
                }
            }
        }

        if (goBack)
        {
            stateMachine.ChangeState(ai.goBackState);
        }

    }
    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }


}
