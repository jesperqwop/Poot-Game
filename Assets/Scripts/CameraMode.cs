using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using NaughtyAttributes;

namespace VHS
{
    public class CameraMode : MonoBehaviour
    {
        public MovementInputData movementInputData;

        public bool aiming;
        public PostProcessProfile normalProfile;
        public PostProcessProfile zoomProfile;
        PostProcessVolume volume;

        // Start is called before the first frame update
        void Start()
        {
            volume = GameObject.FindGameObjectWithTag("Post").GetComponent<PostProcessVolume>();
        }

        // Update is called once per frame
        void Update()
        {
            if (movementInputData.IsRunning != true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    volume.profile = zoomProfile;
                    GetComponent<Animator>().SetBool("CameraUp", true);
                    aiming = true;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    volume.profile = normalProfile;
                    GetComponent<Animator>().SetBool("CameraUp", false);
                    aiming = false;
                }
            }
        }
    }
}

