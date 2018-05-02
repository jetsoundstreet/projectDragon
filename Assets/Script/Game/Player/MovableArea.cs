using UnityEngine;
using System.Collections;

public class MovableArea : MonoBehaviour {

	public float lengthLimit;

	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("MovePath"), "time", 30, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
