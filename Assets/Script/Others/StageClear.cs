using UnityEngine;
using System.Collections;

public class StageClear : TransitionManager {
	
	[SerializeField]
	GameObject target;
	[SerializeField]
	int time = 0;

	// Use this for initialization
	void Start () {
		Trigger = () => (target == null ? --time : time) == 0;
	
	}
}
