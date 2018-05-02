using UnityEngine;
using System.Collections;

public class EnemyRoot : Enemy {

	[SerializeField]
	Meter meter;

	// Use this for initialization
	void Start () {
		Initialize();

	}
	
	// Update is called once per frame
	void Update () {
		meter.Value = ComplexLife * 1000 / maxComplexLife;
		UpdateEnemy();
	
	}

	//public override bool HitToShot(ShotBase shot)
	//{
	//	bool destroyed = base.HitToShot(shot);
	//	return destroyed;
	//}
}
