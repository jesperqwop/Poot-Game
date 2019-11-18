using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using NaughtyAttributes;

namespace VHS
{
    public class CameraMode : MonoBehaviour
    {
        public bool cameraSelected;

        public MovementInputData movementInputData;
        public CameraInputData cameraInputData;

        public bool aiming;
        public PostProcessProfile normalProfile;
        public PostProcessProfile zoomProfile;
        public GameObject motionTracker;
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
                    if(cameraSelected == true)
                    {
                        volume.profile = zoomProfile;
                        GetComponent<Animator>().SetBool("CameraUp", true);
                        aiming = true;
                    }
                }
                if (Input.GetMouseButtonUp(1))
                {
                    if (cameraSelected == true)
                    {
                        volume.profile = normalProfile;
                        GetComponent<Animator>().SetBool("CameraUp", false);
                        aiming = false;
                    }
                }
            }

            if(cameraSelected != true)
            {
                motionTracker.GetComponent<Animator>().SetBool("Up", true);
            }
            else
            {
                motionTracker.GetComponent<Animator>().SetBool("Up", false);
            }

            if (movementInputData.IsRunning != true && cameraInputData.IsZooming != true)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    cameraSelected = true;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    cameraSelected = false;
                }
            }

        }
    }
}

