using UnityEngine;
using System.Collections;

public class GameOver : TransitionManager {

	[SerializeField]
	Meter target;
	[SerializeField]
	int time = 0;

	// Use this for initialization
	void Start()
	{
		Trigger = () => (target.Value <= 0 ? --time : time) == 0;

	}
}
