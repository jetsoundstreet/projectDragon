using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	float height = 9 / 4.0f;
	float width = 4;
	float speed = 0.3f;
	float cameraPitch = 0;
	float deltaCameraPitch = 0;
	[SerializeField]
	GameObject movableArea;
	[SerializeField]
	GameObject cameraTarget;
	[SerializeField]
	Meter meter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// 移動
		transform.position += (Camera.main.transform.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical") + (Input.GetButton("Jump") ? Camera.main.transform.forward : Vector3.zero)) * speed;

		// カメラ追従
		Vector2 horizontal = new Vector2(transform.position.x - cameraTarget.transform.position.x, transform.position.z - cameraTarget.transform.position.z);
		if(horizontal.sqrMagnitude > width * width)
		{
			horizontal -= horizontal.normalized * width;
		}
		else
		{
			horizontal = Vector2.zero;
		}
		float vertical = transform.position.y - cameraTarget.transform.position.y - Mathf.Clamp(transform.position.y - cameraTarget.transform.position.y, -height, height);
		Vector3 m = new Vector3(horizontal.x, vertical, horizontal.y);
		cameraTarget.transform.position += m;
		//Camera.main.transform.position += m;

		// 移動範囲制限
		Vector3 distance = cameraTarget.transform.position - movableArea.transform.position;
		if(distance.magnitude > movableArea.GetComponent<MovableArea>().lengthLimit)
		{
			Vector3 overLength = distance - distance.normalized * movableArea.GetComponent<MovableArea>().lengthLimit;
			transform.position -= overLength;
			cameraTarget.transform.position -= overLength;
			//Camera.main.transform.position -= overLength;
		}

		// カメラ回転
		Quaternion q = transform.rotation;
		transform.RotateAround(cameraTarget.transform.position, Vector3.up, Input.GetAxisRaw("CameraRotate"));
		transform.rotation = q;
		Camera.main.transform.RotateAround(cameraTarget.transform.position, Vector3.up, Input.GetAxisRaw("CameraRotate"));
		// カメラ回転(画面端ポイント)
		q = transform.rotation;
		Vector2 rotate = Vector2.zero;
		Vector2 rotateRange = new Vector2(Camera.main.pixelWidth / 6, Camera.main.pixelHeight / 6);
		if (Input.mousePosition.x < rotateRange.x)
		{
			rotate.x = (Input.mousePosition.x - rotateRange.x) / rotateRange.x;
		}
		else if(Input.mousePosition.x > Camera.main.pixelWidth - rotateRange.x)
		{
			rotate.x = (Input.mousePosition.x - (Camera.main.pixelWidth - rotateRange.x)) / rotateRange.x;
		}
		if (Input.mousePosition.y < rotateRange.y)
		{
			rotate.y = (Input.mousePosition.y - rotateRange.y) / rotateRange.y;
		}
		else if (Input.mousePosition.y > Camera.main.pixelHeight - rotateRange.y)
		{
			rotate.y = (Input.mousePosition.y - (Camera.main.pixelHeight - rotateRange.y)) / rotateRange.y;
		}
		deltaCameraPitch = cameraPitch;
		cameraPitch += rotate.y;
		cameraPitch = Mathf.Clamp(cameraPitch, -30, 30);
		deltaCameraPitch = cameraPitch - deltaCameraPitch;
		transform.RotateAround(cameraTarget.transform.position, Vector3.up, rotate.x);
		transform.rotation = q;
		Camera.main.transform.RotateAround(cameraTarget.transform.position, Vector3.up, rotate.x);
		Camera.main.transform.RotateAround(cameraTarget.transform.position, -Camera.main.transform.right, deltaCameraPitch);

		// バーサク
		if (Input.GetMouseButtonDown(1))
		{
			gameObject.GetComponent<Berserk>().enabled = true;
		}

	}

	public bool HitToShot(ShotBase shot)
	{
		meter.Value -= shot.Power;
		return false;
	}
}
