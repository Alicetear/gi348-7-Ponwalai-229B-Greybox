using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform[] patrolPoints;

    [SerializeField] float detectRange = 10f;
    [SerializeField] float patrolWaitTime = 2f;

    NavMeshAgent agent;

    int patrolIndex;
    float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (agent.isOnNavMesh == false)
            return;

        if (patrolPoints.Length > 0)
            GoToNextPoint();
    }

    void Update()
    {
        if (agent == null)
            return;

        if (!agent.isOnNavMesh)
            return;

        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= detectRange)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        if (agent.remainingDistance <= 0.2f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= patrolWaitTime)
            {
                GoToNextPoint();
                waitTimer = 0;
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(patrolPoints[patrolIndex].position);

        patrolIndex++;

        if (patrolIndex >= patrolPoints.Length)
            patrolIndex = 0;
    }
}
