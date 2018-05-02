using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]
	int unitLife;
	[SerializeField]
	GameObject explodeFx;
	[SerializeField]
	int deleteDelay;

	protected int maxComplexLife;
	protected Renderer _renderer;
	protected int luminousTime;

	// Use this for initialization
	void Start () {
		Initialize();
	
	}

	protected void Initialize()
	{
		maxComplexLife = ComplexLife;
		_renderer = gameObject.GetComponent<Renderer>();
		_renderer.material.EnableKeyword("_EMISSION");

	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateEnemy();

	}

	protected void UpdateEnemy()
	{
		if (luminousTime > 0)
		{
			luminousTime--;
			if (luminousTime == 0)
			{
				_renderer.material.SetColor("_EmissionColor", new Color(0, 0, 0));
			}
		}

	}

	public int ComplexLife
	{
		get
		{
			int life = 0;
			Enemy e = null;
			foreach(Transform child in transform)
			{
				e = child.gameObject.GetComponent<Enemy>();
				if (e != null)
				{
					life += e.ComplexLife;
				}
			}
			return life / 2 + unitLife;
		}
	}

	public int MaxComplexLife { get { return maxComplexLife; } }

	// 戻り値　True：Destroyed　False：Alive
	public virtual bool HitToShot(ShotBase shot)
	{
		unitLife -= shot.Power;
		_renderer.material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.1f));
		luminousTime = 2;
		if (ComplexLife <= 0)
		{
			//Destroy(Instantiate(explodeFx, transform.position, Quaternion.identity), 10);
			foreach (Transform e in transform)
			{
				if (e.GetComponent<Detonator>() != null)
				{
					e.GetComponent<Detonator>().Explode();
				}
			}
			foreach (Transform e in transform)
			{
				if (e.GetComponent<Detonator>() != null)
				{
					e.parent = null;
				}
			}
			if (explodeFx != null)
			{
				if (explodeFx.GetComponent<Detonator>() != null)
				{
					GameObject e = (GameObject)Instantiate(explodeFx, transform.position, Quaternion.identity);
					e.GetComponent<Detonator>().Explode();
					explodeFx = null;
				}
			}
			transform.parent = null;
			if (gameObject.GetComponent<Rigidbody>() == null)
			{
				Rigidbody r = gameObject.AddComponent<Rigidbody>();
				r.mass = 10000;
				r.freezeRotation = true;
			}
			Destroy(gameObject, deleteDelay);
			return true;
		}
		return false;
	}
}
