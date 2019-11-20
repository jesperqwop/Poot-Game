using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{
    public bool inRange;
    public bool inEvidenceRange;
    GameObject lookingAt;
    GameObject poot;
    AudioSource audioSource;
    Light blitz;
    public GameObject cameraController;
    public Manager manager;
    public GameObject foundList;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        poot = GameObject.FindGameObjectWithTag("Poot");
        audioSource = GetComponent<AudioSource>();
        blitz = transform.GetChild(0).GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && cameraController.GetComponent<VHS.CameraMode>().cameraSelected == true)
        {
            StartCoroutine(Blitz());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Poot")
        {
            inRange = true;
        }
        if (other.tag == "Evidence")
        {
            inEvidenceRange = true;
            lookingAt = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Poot")
        {
            inRange = false;
        }
        if (other.tag == "Evidence")
        {
            inEvidenceRange = false;
            lookingAt = null;
        }
    }

   IEnumerator Blitz()
    {
        audioSource.Play();
        blitz.enabled = true;
        if(inRange == true)
        {
            poot.GetComponent<MonsterDetector>().StartFall();
        }
        if(inEvidenceRange == true)
        {
            manager.evidenceFound += 1;
            lookingAt.GetComponent<Objective>().EnableSlash();
            Destroy(lookingAt);
            lookingAt = null;
            inEvidenceRange = false;
            foundList.GetComponent<Animator>().SetTrigger("Display");
        }
        yield return new WaitForSeconds(0.1f);
        blitz.enabled = false;
    }

}
