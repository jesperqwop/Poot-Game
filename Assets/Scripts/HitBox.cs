using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float pushback;
    Vector3 impact = Vector3.zero;
    GameObject player;
    public bool inRange;
    CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController.Move((impact*pushback) * Time.deltaTime);
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    public void DoDamage()
    {
        if(inRange == true)
        {
            player.GetComponent<PlayerHP>().hP -= 1;
            impact = Vector3.Normalize(player.transform.position - transform.position);
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int rng;
        if(other.tag == "Player")
        {

            inRange = true;
            rng = Random.Range(0, 2);
            if(rng == 1)
            {
                transform.parent.GetComponent<Animator>().SetTrigger("Kick1");
            }
            else
            {
                transform.parent.GetComponent<Animator>().SetTrigger("Kick2");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = false;
        }
    }

}
