using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrol : MonoBehaviour {

    [SerializeField]
    bool patrolWaiting;

    [SerializeField]
    float totalWaitTime = 3f;

    [SerializeField]
    float switchProbablitiy = 0.2f;

    [SerializeField]
    List<Waypoint> patrolPoints;

    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    public bool chasingPlayer;
    public bool isSearchingForPlayer;
    GameObject player;
    public Vector3 soundPosition;


    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");

        navMeshAgent = GetComponent<NavMeshAgent>();

        if(patrolPoints != null && patrolPoints.Count >= 2)
        {
            currentPatrolIndex = 0;
            SetDestination();
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!chasingPlayer && !isSearchingForPlayer)
        {
            if (travelling && navMeshAgent.remainingDistance <= 1f)
            {
                travelling = false;
                if (patrolWaiting)
                {
                    waiting = true;
                    waitTimer = 0f;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }

            if (waiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= totalWaitTime)
                {
                    waiting = false;
                    ChangePatrolPoint();
                    SetDestination();
                }
            }
        }
        else
        {
            if (chasingPlayer == true)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
            if(isSearchingForPlayer == true)
            {
                navMeshAgent.SetDestination(soundPosition);
            }
        }
	}

    private void SetDestination()
    {
        Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
        navMeshAgent.SetDestination(targetVector);
        travelling = true;
    }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f,1f) <= switchProbablitiy)
        {
            patrolForward = !patrolForward;
        }
        if (patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }

    public void ChasePlayer()
    {

    }

}
