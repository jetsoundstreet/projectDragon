using UnityEngine;
using System.Collections;

public class ConeCollider : MonoBehaviour {

	[SerializeField]
	Camera camera;
	[SerializeField]
	float angleRatio = 1.0f;
	[SerializeField]
	float length = 1.0f;
	[SerializeField]
	Vector3 position;
	
	Quaternion coneRot;
	
	public Vector3 ray;

	// Use this for initialization
	void Start () {
		coneRot = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		float fieldScale = Mathf.Tan(camera.fieldOfView * angleRatio / 360 * Mathf.PI) * length;
		transform.localScale = new Vector3(fieldScale, fieldScale, length);

		position = Input.mousePosition + Vector3.forward;
		coneRot = Quaternion.LookRotation(camera.ScreenToWorldPoint(position) - camera.transform.position, Vector3.up);
		ray = camera.ScreenToWorldPoint(position) - camera.transform.position;
		transform.rotation = coneRot;
	
	}
}
