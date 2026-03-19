using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;

    private NavMeshAgent _agent;

    private int _patrolPointIndex = 0;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Patrol()
    {
        _agent.SetDestination(_patrolPoints[_patrolPointIndex].position);

        if (HasReachedTheDestination())
        {
            _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
        }
    }

    private bool HasReachedTheDestination()
    {
        return _agent.remainingDistance <= _agent.stoppingDistance;
    }
}
