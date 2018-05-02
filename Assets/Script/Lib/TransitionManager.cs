using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour {
	public delegate bool TransitionTrigger();
	public TransitionTrigger Trigger { get; set; }
	public int NextSceneIndex { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Trigger() == true)
		{
			SceneManager.LoadScene(NextSceneIndex);
		}
	}
}
