using UnityEngine;
using System.Collections;

public abstract class MenuEventBase : MonoBehaviour {

	public GameObject objCursor;

	const float submitEffectTime = 0.06f;

	public void Submit()
	{
		StartCoroutine(SubmitEffect());
	}

	IEnumerator SubmitEffect()
	{
		float time = 0;
		while(time < submitEffectTime)
		{
			float t = time / submitEffectTime;
			objCursor.transform.localScale = new Vector3(1 + t * t, (1 - t) * (1 - t), 1);
			time += Time.deltaTime;
			yield return null;
		}
		MenuEvent();

	}

	protected abstract void MenuEvent();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
