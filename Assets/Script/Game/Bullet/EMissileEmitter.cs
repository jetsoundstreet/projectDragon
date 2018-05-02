using UnityEngine;
using System.Collections;

public class EMissileEmitter : MonoBehaviour {

	public GameObject bullet;
	private GameObject player;

	private int time = 0;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {

		if (time++ % 100 == 0)
		{
			GameObject r;
			r = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
			r.GetComponent<Missile>().Initialize(gameObject, player, Vector3.up);
		}
	}
}
