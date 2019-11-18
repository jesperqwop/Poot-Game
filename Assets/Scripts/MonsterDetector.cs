using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDetector : MonoBehaviour {

    public RuntimeAnimatorController ayy;

    public GameObject hitting;

    public GameObject player;
    public GameObject hitBox;
    public GameObject manager;

    public AudioClip[] spotted;
    public AudioClip[] roaming;
    public AudioClip[] death;

    public bool playerInSight;
    public bool isChasingPlayer;
    public bool isSearchingForPlayer;
    public bool lostSight;
    public bool canAttack = true;
    public bool stunned;

    public float patrolSpeed;
    public float chaseSpeed;
    public float detectRange;
    public float fov;
    public float unfollowTime;
    public float searchTime;
    public float attackRange;
    float lostSightTime;
    float heardTime;

    NavMeshAgent navMeshAgent;

    Animator animator;

    Vector3 soundPosition;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager");
	}

    // Update is called once per frame
    void Update()
    {

        //test
        
        if (Input.GetMouseButtonDown(2))
        {
            animator.SetTrigger("Flip");
        }
        /*
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Fall());
        }
        */

        //test

        float angleToPlayer = Vector3.Angle((player.transform.position - transform.position),transform.forward);

        RaycastHit hit;

        Ray detector = new Ray(transform.position, ((player.transform.position + player.transform.up * 1) - transform.position));

        if(Physics.Raycast(detector, out hit, detectRange))
        {
            hitting = hit.transform.gameObject;
        }

        if (Physics.Raycast(detector, out hit, detectRange))
        {
            if (hit.collider.tag == "Player")
            {
                if (angleToPlayer >= -fov && angleToPlayer <= fov)
                {
                        PlayerDetected();
                    if(hit.distance <= attackRange && canAttack == true)
                    {
                        animator.SetTrigger("Attack");
                        canAttack = false;
                    }
                }
                else
                {
                    playerInSight = false;
                    if(isChasingPlayer == true && lostSight == false)
                    {
                        lostSight = true;
                        lostSightTime = Time.time;
                    }
                }
            
            }
            else
            {
                playerInSight = false;
                if(isChasingPlayer == true && lostSight == false)
                {
                    lostSight = true;
                    lostSightTime = Time.time;
                }
            }
        }

        if (isChasingPlayer == true && stunned != true)
        {
            navMeshAgent.speed = chaseSpeed;
            animator.SetBool("Chasing", true);
            animator.SetBool("Patrol", false);

            if (lostSight == true)
            {
                if (Time.time - lostSightTime >= unfollowTime)
                {
                    print("Escaped");
                    PlayerEscaped();
                    lostSight = false;
                    animator.runtimeAnimatorController = ayy;
                }
            }
        }
        else
        {

            if(GetComponent<AudioSource>().isPlaying != true)
            {
                //GetComponent<AudioSource>().clip = roaming[Random.Range(0, roaming.Length)];
                //GetComponent<AudioSource>().Play();
            }

            if (isSearchingForPlayer != true)
            {
                animator.SetBool("Chasing", false);
                animator.SetBool("Patrol", true);
                navMeshAgent.speed = patrolSpeed;
                GetComponent<MonsterPatrol>().isSearchingForPlayer = false;
            }
        }
        if(isSearchingForPlayer == true && isChasingPlayer != true)
        {
            navMeshAgent.speed = chaseSpeed;
            animator.SetBool("Chasing", true);
            animator.SetBool("Patrol", false);
            if(Time.time - heardTime >= searchTime)
            {
                isSearchingForPlayer = false;
            }
        }

    }

    public void PlayerDetected()
    {
        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * detectRange, Color.green);
        playerInSight = true;
        lostSight = false;
 
        if (isChasingPlayer != true)
        {
            manager.GetComponent<Manager>().PlayMusic(true);
            isChasingPlayer = true;
            GetComponent<MonsterPatrol>().chasingPlayer = true;
            GetComponent<MonsterPatrol>().isSearchingForPlayer = false;
            GetComponent<AudioSource>().clip = spotted[Random.Range(0, spotted.Length)];
            GetComponent<AudioSource>().Play();
        }
    }

    public void PlayerHeard(Vector3 newPosition)
    {
        if (GetComponent<MonsterPatrol>().chasingPlayer != true)
        {
            GetComponent<MonsterPatrol>().isSearchingForPlayer = true;
            GetComponent<MonsterPatrol>().soundPosition = newPosition;
            heardTime = Time.time;
            isSearchingForPlayer = true;
        }
    }

    void PlayerEscaped()
    {
        isChasingPlayer = false;
        GetComponent<MonsterPatrol>().chasingPlayer = false;
        animator.SetBool("Chasing", false);
        manager.GetComponent<Manager>().PlayMusic(false);
    }

    public void Stun()
    {
        
    }

    public void Die()
    {
        manager.GetComponent<Manager>().PlayMusic(false);
        animator.SetTrigger("Die");
        GetComponent<MonsterPatrol>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        enabled = false;
    }

    public void StartFall()
    {
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        float previousSpeed = navMeshAgent.speed;
        stunned = true;
        animator.SetTrigger("Fall");
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(5);
        navMeshAgent.speed = previousSpeed;
        stunned = false;
    }

}
