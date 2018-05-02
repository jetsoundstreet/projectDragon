using UnityEngine;
using System.Collections;

public class LockOn : MonoBehaviour {

	public GameObject Target { get; set; }
	RectTransform rectTransform;
	int time;

	// Use this for initialization
	void Start ()
	{
		rectTransform = GetComponent<RectTransform>();
		time = 15;

	}
	
	// Update is called once per frame
	void Update () {
		if(Target == null)
		{
			Destroy(gameObject);
			return;
		}
		rectTransform.position = Camera.main.WorldToScreenPoint(Target.transform.position);
		rectTransform.rotation = Quaternion.AngleAxis(time * 24, Vector3.forward);
		rectTransform.localScale = Vector3.one * (time / 2.0f + 1);
		if (time > 0) time--;
	}

	public void Impact()
	{
		Destroy(gameObject);
	}
}
