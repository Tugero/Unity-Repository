using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeNav : MonoBehaviour
{
    public Transform destination;
    NavMeshAgent _NaveMeshAgent;
    public bool isFlipped;
    public GameObject FlippableModel;
    public bool attack;
    public Animator Enemy;

    private void Start()
    {
        /*_NaveMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_NaveMeshAgent == null)
        {
            Debug.LogError("NavMesh not attached to " + gameObject.name);
        }
        else
        {
            setDestination();
        }*/
        isFlipped = false;
        attack = false;
        //Enemy = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            attack = true;
            Enemy.SetInteger("Anime State", 2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            attack = false;
            Enemy.SetInteger("Anime State", 0);
            _NaveMeshAgent.SetDestination(transform.position);
        }
    }

    private void Update()
    {
        if (attack == true)
        {
            FlippableModel.transform.position = transform.position;
            EnemyFlipper();
            _NaveMeshAgent = this.GetComponent<NavMeshAgent>();
            if (_NaveMeshAgent == null)
            {
                Debug.LogError("NavMesh not attached to " + gameObject.name);
            }
            else
            {
                setDestination();
            }
        }
    }

    private void setDestination()
    {
        if (destination != null)
        {
            Vector3 target = destination.transform.position;
            _NaveMeshAgent.SetDestination(target);
        }
    }

    private void EnemyFlipper()
    {
        if (Physics.gravity == new Vector3(0, 9.81f, 0))
        {
            if (isFlipped == false)
            {
                Debug.Log("Flipping Enemy");
                FlippableModel.transform.rotation = Quaternion.Euler(180, 0, 0);
                isFlipped = true;
            }
        }
        if (Physics.gravity == new Vector3(0, -9.81f, 0))
        {
            if (isFlipped == true)
            {
                Debug.Log("Flipping Enemy");
                FlippableModel.transform.rotation = Quaternion.Euler(0, 0, 0);
                isFlipped = false;
            }
        }
    }

}
