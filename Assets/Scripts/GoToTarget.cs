using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTarget : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;
    public float idleDistance = 1f;
    public movement movescript;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) > idleDistance)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);

    }
}
