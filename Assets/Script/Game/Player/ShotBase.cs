using UnityEngine;
using System.Collections;

public abstract class ShotBase : MonoBehaviour {

	[SerializeField]
	protected int power;

	public int Power { get { return power; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
