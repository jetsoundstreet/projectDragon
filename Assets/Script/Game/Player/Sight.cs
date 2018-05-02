using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Sight : MonoBehaviour {

    public RayEmitter rayEmitter;
	public BulletEmitter bulletEmitter;
    public new Camera camera;
	public GameObject lockOnMarker;
    Vector3 position;
    List<LockOn> lockedOn;
	RectTransform rectTransform;
	int lockOnInterval;

    Ray ray;

	// Use this for initialization
	void Start () {
        position = new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 1);
		rectTransform = GetComponent<RectTransform>();
		rectTransform.position = position;
		lockedOn = new List<LockOn>();
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(camera.pixelHeight / 10, camera.pixelHeight / 10);
		lockOnInterval = 0;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        position = Input.mousePosition + Vector3.forward;
		rectTransform.position = position;
        ray = camera.ScreenPointToRay(position);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		if (Input.GetMouseButtonDown(0))
		{
			bulletEmitter.Shot();
		}
        if (Input.GetMouseButton(0))
        {
			if (lockOnInterval == 0)
			{
				// todo: targetを事前に配列化する(親gameobject:TargetManagerの子にtargetを追加する？)
				GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
				for (int i = 0; i < targets.Length; i++)
				{
					if (((Vector2)(camera.WorldToScreenPoint(targets[i].transform.position) - position)).magnitude < camera.pixelHeight / 10 &&
						(camera.WorldToScreenPoint(targets[i].transform.position) - position).z < 300)
					{
						if (!lockedOn.Find(s => s.Target != null && s.Target == targets[i].transform.gameObject))
						{
							GameObject marker = (GameObject)Instantiate(lockOnMarker, targets[i].transform.position, Quaternion.identity);
							marker.GetComponent<LockOn>().Target = targets[i];
							marker.transform.SetParent(transform.parent);
							lockedOn.Add(marker.GetComponent<LockOn>());
							lockOnInterval = 10;
						}
					}
				}
			}
			//RaycastHit[] hits = Physics.RaycastAll(ray, camera.farClipPlane, 1 << 8);
   //         for (int i = 0; i < hits.Length; i++)
   //         {
   //             if (!lockedOn.Contains(hits[i].transform.gameObject))
   //             {
   //                 lockedOn.Add(hits[i].transform.gameObject);
   //             }
   //         }
        }
        else if (lockedOn.Count > 0)
        {
			if (lockedOn[0] != null && lockedOn[0].Target != null)
			{
				// 生成したホーミングレーザーにロックオンマーカーを渡す(着弾時にマーカーに通知するため)
				rayEmitter.Shot(lockedOn[0].Target).GetComponent<RayShot>().Marker = lockedOn[0];
			}
            lockedOn.RemoveAt(0);
        }
		if (lockOnInterval > 0)
		{
			lockOnInterval--;
		}

    }

	public Vector3 InSight()
	{
		RaycastHit hit;
		Vector3 targetPosition;
		if (Physics.Raycast(ray, out hit, camera.farClipPlane, 1 << 8) == false)
		{
			targetPosition = camera.ScreenToWorldPoint(position + 16 * Vector3.forward);
		}
		else
		{
			targetPosition = hit.point;
		}
		return targetPosition;

	}

	public Vector3 Position
	{
		get
		{
			return position;
		}
	}
}
