using UnityEngine;
using System.Collections;

public class EBulletEmitter : MonoBehaviour {

	public GameObject bullet;
	private GameObject player;

	private int ammoCount = totalAmmos;
	private float timeInterval;

	private const int totalAmmos = 8;
	private const float interval = 0.0333f;

	private int time = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ammoCount < totalAmmos && timeInterval >= interval * ammoCount)
		{
			GameObject r;
			r = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
			r.GetComponent<EnemyBullet>().Initialize(gameObject, player.transform.position);
			ammoCount++;
		}
		timeInterval += Time.deltaTime;

		if(time++ % 100 == 0)
		{
			Shot();
		}
	}

	public void Shot()
	{
		ammoCount = 0;
	}
}
