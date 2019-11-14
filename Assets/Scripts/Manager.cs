using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public GameObject player;
    public GameObject inventory;
    public GameObject cronenberg;
    public GameObject soundPos;
    public GameObject slimeSFX;

    public AudioClip chaseAmbience;

    public Text message;
    public Text HP;
    public Text Ammo;
    public Text loadedAmmo;
    public Text key1;
    public Text key2;
    public Text key3;
    public Text key4;

    public bool paused;
    public bool hasKey1;
    public bool hasKey2;
    public bool hasKey3;
    public bool hasKey4;

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
                inventory.SetActive(true);
                //player.GetComponent<vp_SimpleHUD>().ShowHUD = true;
                //player.GetComponent<vp_FPInput>().MouseCursorForced = true;
            }
            else
            {
                paused = false;
                inventory.SetActive(false);
                //player.GetComponent<vp_SimpleHUD>().ShowHUD = false;
                //player.GetComponent<vp_FPInput>().MouseCursorForced = false;
            }
        }
    }

    public void getKey(int keyID)
    {
        if (keyID == 1)
        {
            hasKey1 = true;
            key1.gameObject.SetActive(true);
        }
        if (keyID == 2)
        {
            hasKey2 = true;
            key2.gameObject.SetActive(true);
            cronenberg.SetActive(true);
            Instantiate(slimeSFX);
        }
        if (keyID == 3)
        {
            hasKey3 = true;
            key3.gameObject.SetActive(true);
        }
        if (keyID == 4)
        {
            hasKey4 = true;
            key4.gameObject.SetActive(true);
        }

    }

    public void DisplayText(string text)
    {
        message.text = text;
        message.GetComponent<Animator>().SetTrigger("display");
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
