using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{

    public bool inRange;
    GameObject poot;
    AudioSource audioSource;
    Light blitz;
    public GameObject cameraController;
    // Start is called before the first frame update
    void Start()
    {
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Poot")
        {
            inRange = false;
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
        yield return new WaitForSeconds(0.1f);
        blitz.enabled = false;
    }

}
