using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollower : MonoBehaviour
{
    private EndZone _endZone;

    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    private Vector3 _lastTargetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _endZone = FindFirstObjectByType<EndZone>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        UpdateDestination();
    }

    void FixedUpdate()
    {
        if (!_target) return;

        if (_endZone.IsLevelComplete())
        {
            _navMeshAgent.isStopped = true;
            return;
        }

        // Only update destination if the target has moved significantly
        if (Vector3.Distance(_target.position, _lastTargetPosition) > 0.5f)
        {
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        _lastTargetPosition = _target.position;
        _navMeshAgent.SetDestination(_target.position);
    }
}