using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuEventNewGame : MenuEventBase {

	protected override void MenuEvent()
	{
		SceneManager.LoadScene("Stage1");

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
