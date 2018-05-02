using UnityEngine;
using System.Collections;

public class RayShot : ShotBase {

	public GameObject target;
	protected bool impact = false;
	private Vector3 virtualTarget;

	public LockOn Marker { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		if(target != null)
		{
			virtualTarget = target.transform.position;
			if (impact)
			{
				Enemy enemy = target.GetComponent<Enemy>();
				if (enemy != null)
				{
					enemy.HitToShot(gameObject.GetComponent<ShotBase>());
					impact = false;
					//Destroy(gameObject);
				}
				if (Marker != null)
				{
					Marker.GetComponent<LockOn>().Impact();
				}
			}
		}
	
	}

	protected Vector3 Target
	{
		get
		{
			if (target == null) return virtualTarget;
			return target.transform.position;
		}
	}
}
