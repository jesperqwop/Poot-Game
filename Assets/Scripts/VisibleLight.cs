using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleLight : MonoBehaviour {

    public float range;
    public float x;
    public float y;
    GameObject collisionPoint;
	// Use this for initialization
	void Start () {
        collisionPoint = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, (transform.forward + (transform.right * x) + (transform.up * y)).normalized * range);
        Debug.DrawRay(transform.position,(transform.forward + (transform.right * x) + (transform.up * y)).normalized * range, Color.green);
        if(Physics.Raycast(ray, out hit, range))
        {
            collisionPoint.transform.position = hit.point - (transform.forward * 1);
        }
	}
}
