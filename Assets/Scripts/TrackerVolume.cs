using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerVolume : MonoBehaviour
{
    public GameObject unga;
    public AudioSource audioSource;
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = unga.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        audioSource.volume = volume;

    }
}
