using UnityEngine;
using System.Collections;

public class DisplayLife : MonoBehaviour {

	[SerializeField]
	GameObject lifeObj;
	[SerializeField]
	Meter meter;

	IDisplayLife life;

	// Use this for initialization
	void Start () {
		life = lifeObj.GetComponent<IDisplayLife>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeObj != null)
		{
			meter.Value = life.Lifepm;
		}
	
	}
}

public interface IDisplayLife
{
	int Lifepm { get; }
}
