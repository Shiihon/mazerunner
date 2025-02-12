using UnityEngine;
using UnityEngine.AI;

public class EnemyFollower : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(_target.position);
    }
}
