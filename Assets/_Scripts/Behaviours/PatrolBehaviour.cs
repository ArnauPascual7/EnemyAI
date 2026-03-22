using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;

    private NavMeshAgent _agent;

    private int _patrolPointIndex = 0;
    private bool starting;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Patrol()
    {
        _agent.SetDestination(_patrolPoints[_patrolPointIndex].position);

        Debug.DrawLine(transform.position, _patrolPoints[_patrolPointIndex].position, Color.green);

        if (HasReachedTheDestination() && !starting)
        {
            _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;

            StartCoroutine(WaitToStart());
        }
    }

    private bool HasReachedTheDestination()
    {
        return _agent.remainingDistance <= _agent.stoppingDistance;
    }

    private IEnumerator WaitToStart()
    {
        starting = true;
        yield return new WaitForSeconds(1f);
        starting = false;
    }
}
