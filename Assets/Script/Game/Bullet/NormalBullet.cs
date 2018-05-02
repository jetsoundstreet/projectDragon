using UnityEngine;
using System.Collections;

public class NormalBullet : BulletShot {

	public float timeScale = 300;                    // 存在時間
	public float speed = 1;
	private float time = 0;                         // 存在時間カウンタ
	private Vector3 direction;
	private Vector3 turningDirection;                    // 照準到達後の曲げ方向
	private float turningTime;

	// Use this for initialization
	void Start () {
	
	}

	public void Initialize(GameObject emitter, Vector3 target)
	{
		transform.position = emitter.transform.position;
		direction = (target - transform.position).normalized * speed;
		transform.rotation = Quaternion.LookRotation(direction, emitter.transform.up);
		turningDirection = Vector3.Lerp(direction, (target - Camera.main.transform.position).normalized * speed, 0.9f);
		turningTime = (target - transform.position).magnitude / speed;
		this.target = target;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction;
		time++;
		if(turningTime - 1 < time && time < turningTime)
		{
			direction = Vector3.Lerp(direction, turningDirection, (1-turningTime % 1.0f));
			transform.rotation = Quaternion.LookRotation(direction, transform.up);
		}
		else if (turningTime - 1 < time && time < turningTime + 1)
		{
			direction = turningDirection;
			transform.rotation = Quaternion.LookRotation(direction, transform.up);
		}
		if (time > timeScale)
		{
			Destroy(gameObject);
		}
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Target")
		{
			Enemy enemy = collider.gameObject.GetComponent<Enemy>();
			enemy.HitToShot(gameObject.GetComponent<ShotBase>());
			Destroy(gameObject);
			//Destroy(collider.gameObject);
		}
	}
}
