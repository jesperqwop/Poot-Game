using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTrigger : MonoBehaviour
{

    public GameObject monster;
    public float timing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        monster.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(timing);
        monster.GetComponent<NavMeshAgent>().enabled = true;
        monster.GetComponent<MonsterPatrol>().enabled = true;
        monster.GetComponent<MonsterDetector>().enabled = true;
    }

}
