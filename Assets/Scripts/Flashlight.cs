using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    Light light;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            audioSource.Play();

            if(light.enabled != true)
            {
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }
        }
    }
}
