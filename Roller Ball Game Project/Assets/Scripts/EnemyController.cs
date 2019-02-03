using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _NavMeshAgent;
    // Use this for initialization
    void Start()
    {
        _NavMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_NavMeshAgent == null)
        {
            Debug.LogError("NavMesh not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }
    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _NavMeshAgent.SetDestination(targetVector);
        }
    }
    void Update()
    {
        _NavMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_NavMeshAgent == null)
        {
            Debug.LogError("NavMesh not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }
}
