using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public int evidenceFound;
    public GameObject player;
    public GameObject soundPos;
    public AudioClip chaseAmbience;

    public bool paused;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (paused != true)
            {
                paused = true;

            }
            else
            {
                paused = false;

            }
        }
    }

    public void PlayMusic(bool play)
    {
        if (play == true)
        {
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
