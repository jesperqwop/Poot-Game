using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour {

    [SerializeField]
    Transform destination;

    NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {

        navMeshAgent = GetComponent<NavMeshAgent>();

        SetDestination();

	}

    void SetDestination()
    {
        if(destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
