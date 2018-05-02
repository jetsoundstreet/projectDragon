using UnityEngine;
using System.Collections;

public class EnemyBullet : BulletShot {

	public float timeScale = 600;                    // 存在時間
	public float speed = 0.1f;
	private float time = 0;                         // 存在時間カウンタ
	private Vector3 direction;

	// Use this for initialization
	void Start () {

	}

	public void Initialize(GameObject emitter, Vector3 target)
	{
		transform.position = emitter.transform.position;
		direction = (target - transform.position).normalized * speed;
		transform.rotation = Quaternion.LookRotation(direction, emitter.transform.up);
		this.target = target;
	}

	// Update is called once per frame
	void Update () {
		transform.position += direction;
		time++;
		if (time > timeScale)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			Player player = collider.gameObject.GetComponent<Player>();
			player.HitToShot(gameObject.GetComponent<ShotBase>());
			Destroy(gameObject);
		}
	}
}
