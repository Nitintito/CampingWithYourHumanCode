using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System;

public class AI : MonoBehaviour {

    public AIStateMachine stateMachine;
    public AIIdleState idleState;
    public AICirculateState circulateState;
    public AIAttackState attackState;
    public AIGoBackState goBackState;

    [SerializeField] private AIData data;
    [SerializeField] private GameObject AIPrefab;
    [SerializeField] private GameObject campFire;

    private FmodController fmod;

    public Rigidbody rb { get; private set; }

    public NavMeshAgent agent { get; private set; }

    public bool ifCalledByTutorial;

    public Animator animator { get; private set; }

    [NonSerialized] public static UnityEvent gameOver = new UnityEvent();

    private void Awake()
    {
        stateMachine = new AIStateMachine();
        idleState = new AIIdleState(this, stateMachine, data, "idle");
        circulateState = new AICirculateState(this, stateMachine, data, "circulate");
        attackState = new AIAttackState(this, stateMachine, data, "attack");
        goBackState = new AIGoBackState(this, stateMachine, data, "goback");

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        fmod = GetComponentInChildren<FmodController>();
        agent.stoppingDistance = 0.1f;
        ifCalledByTutorial = false;

        stateMachine.Initialie(idleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();

        if (ifCalledByTutorial)
        {
            stateMachine.ChangeState(attackState);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicalUpdate();
    }

    public void AISetDestination(Vector3 walkPoint)
    {
        agent.SetDestination(walkPoint);
    }

    public void ResetPath()
    {
        agent.ResetPath();
    }

    public void ResumeAgent()
    {
        agent.isStopped = false;
    }

    public void StopRb()
    {
        rb.velocity = Vector3.zero;
    }

    public void RotateAround(Vector3 centerPos, float rotationSpeed)
    {
        transform.RotateAround(centerPos, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void DisableAgent()
    {
        agent.enabled = false;
    }

    public void Restart()
    {
        Instantiate(AIPrefab);

        AIPrefab.transform.position = data.RespawnPos;
        Destroy(gameObject);
    }

    public bool CheckArrived()
    {
        return agent.remainingDistance < 1f;
    }

    public string GetCurrentState()
    {
        return stateMachine.CurrentState.name;
    }

    public void InvokeGameOver()
    {
        gameOver.Invoke();
    }

    public void RunWalkAnimation()
    {
        animator.SetBool("stagger", false);
        animator.SetBool("scream", false);
        animator.SetBool("walk", true);

    }

    public void PlayGhostAttackVoice()
    {
        fmod.playGhostAttack();
        fmod.playGhostMusic();
    }
}
