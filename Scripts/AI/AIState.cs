public class AIState
{
    protected AI ai;
    protected AIStateMachine stateMachine;
    protected AIData data;
    public string name;

    public AIState(AI ai, AIStateMachine StateMachine, AIData data, string name)
    {
        this.ai = ai;
        this.stateMachine = StateMachine;
        this.data = data;
        this.name = name;   
    }

    public virtual void Enter()
    {
        //Debug.Log("Enter " + name + " state.");
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicalUpdate()
    {

    }


}
