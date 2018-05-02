using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Berserk : MonoBehaviour {

	[SerializeField]
	BerserkImageEffect berserkImageEffect;
	public RayEmitter rayEmitter;
	public Sight sight;

	int time;
	List<GameObject> lockedOn;

	// Use this for initialization
	void Start () {
		lockedOn = new List<GameObject>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (time % 3 == 0)
		{
			float sightRing = Camera.main.pixelHeight / 20.0f * (time % 20);
			float distance;
			float minDistance = -1;
			// todo: targetを事前に配列化する(親gameobject:TargetManagerの子にtargetを追加する？)Sightと同様
			GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
			if (targets.Length == lockedOn.Count) lockedOn.Clear();
			for (int i = 0; i < targets.Length; i++)
			{
				if (lockedOn.Contains(targets[i]) == false)
				{
					distance = ((Vector2)(Camera.main.WorldToScreenPoint(targets[i].transform.position) - sight.Position)).magnitude - sightRing;
					if (minDistance < 0 || distance < minDistance)
					{
						minDistance = distance;
						lockedOn.Insert(0, targets[i]);
					}
				}
			}
			lockedOn.RemoveAll(s => s == null);
			if (lockedOn.Count > 0)
			{
				rayEmitter.Shot(lockedOn[0]);
			}
		}
		berserkImageEffect.Timing = time / 120.0f;

		time++;
		if (time > 360) this.enabled = false;
	
	}

	void OnEnable()
	{
		time = 0;

	}
}
