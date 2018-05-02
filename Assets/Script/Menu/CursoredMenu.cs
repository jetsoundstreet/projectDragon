using UnityEngine;
using System.Collections;

public class CursoredMenu : MonoBehaviour {

	public GameObject objCursor;
	public GameObject objMenu;
	int cursorNumber;
	int moveInterval;

	const int constMoveInterval = 12;

	// Use this for initialization
	void Start () {
		cursorNumber = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		int menuCount = objMenu.transform.childCount;
		if(Input.GetButtonDown("Submit"))
		{
			objMenu.transform.GetChild(cursorNumber).gameObject.GetComponent<MenuEventBase>().Submit();
		}
		if (moveInterval == 0)
		{
			if (Input.GetAxisRaw("Vertical") > 0)
			{
				cursorNumber = (cursorNumber + menuCount - 1) % menuCount;
				moveInterval = constMoveInterval;
			}
			if (Input.GetAxisRaw("Vertical") < 0)
			{
				cursorNumber = (cursorNumber + 1) % menuCount;
				moveInterval = constMoveInterval;
			}
			Vector3 p = objCursor.transform.position;
			Vector3 c = objMenu.transform.GetChild(cursorNumber).position;
			objCursor.transform.position = new Vector3(p.x, c.y, p.z);
		}
		if (moveInterval > 0) moveInterval--;
	
	}
}
