using UnityEngine;
using System.Collections;

public class Missile : ShotBase {

	[SerializeField]
	GameObject explodeFx;

	public Vector3 emitter;
    public GameObject target;
    public float distortionScale = 10;
    private float time = 0;
    public float timeScale = 30;
    public float activateTime = 15;
    public Vector3 velocity = Vector3.zero;
    public float acceleration = 1;
    public float attenuation = 0.8f;
    private float accel = 0;
	public float initialVelocity = 0;
	public float maxVelocity = 1;
	public float maxSwing = 0.1f;

    // Use this for initialization
    void Start () {

    }

    public void Initialize(GameObject emitter, GameObject target, Vector3 look)
    {
        this.emitter = emitter.transform.position;
        this.target = target;
        accel = acceleration;
		velocity = initialVelocity * look;
		transform.rotation = Quaternion.LookRotation(look);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = velocity * attenuation + transform.forward * accel;
        transform.position += velocity;
        if (time >= activateTime)
        {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), maxSwing);
        }
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
			if (explodeFx != null)
			{
				if (explodeFx.GetComponent<Detonator>() != null)
				{
					GameObject e = (GameObject)Instantiate(explodeFx, transform.position, Quaternion.identity);
					e.GetComponent<Detonator>().Explode();
					explodeFx = null;
				}
			}
			Destroy(gameObject);
		}
	}
}
