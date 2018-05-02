using UnityEngine;
using System.Collections;

public class Meter : MonoBehaviour {

	[SerializeField]
	RectTransform fillRect;

	[SerializeField]
	float minValue;
	[SerializeField]
	float maxValue;
	[SerializeField]
	float value;

	RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
	
	}
	
	// Update is called once per frame
	void Update () {
		fillRect.sizeDelta = new Vector2((value - minValue) / (maxValue - minValue) * rt.sizeDelta.x, fillRect.sizeDelta.y);
	
	}

	public float MaxValue { get { return maxValue; } }
	public float MinValue { get { return minValue; } }

	public float Value
	{
		get { return value; }
		set { this.value = Mathf.Clamp(value, minValue, maxValue); }
	}
}
