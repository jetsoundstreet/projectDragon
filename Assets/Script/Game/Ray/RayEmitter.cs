using UnityEngine;
using System.Collections;

public class RayEmitter : MonoBehaviour {
	
    public GameObject ray;
    public GameObject lastingRay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject Shot (GameObject target)
    {
        GameObject r;
        r = (GameObject)Instantiate(ray, transform.position, transform.rotation);
        r.GetComponent<HomingRay>().Initialize(gameObject, target);
		return r;
    }
}
