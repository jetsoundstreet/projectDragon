using UnityEngine;
using System.Collections;

public class BulletEmitter : MonoBehaviour {

	public GameObject bullet;
	public GameObject objSight;

	private Sight sight;
	private int ammoCount = totalAmmos;
	private int timeInterval;

	private const int totalAmmos = 4;
	private const int interval = 4;

	// Use this for initialization
	void Start () {
		sight = objSight.GetComponent<Sight>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ammoCount < totalAmmos && timeInterval >= interval * ammoCount)
		{
			GameObject r;
			r = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
			r.GetComponent<NormalBullet>().Initialize(gameObject, sight.InSight());
			ammoCount++;
		}
		timeInterval += 1;
	
	}

	public void Shot()
	{
		if (ammoCount < totalAmmos)
		{
			timeInterval = timeInterval % interval;
		}
		else
		{
			timeInterval = 0;
		}
		ammoCount = 0;
	}
}
