using UnityEngine;
using System.Collections;

public class HomingRay : RayShot {

    public Vector3 emitter;							// 射出位置
    //public GameObject target;						// 着弾対象
    public float divergenceScale = 10;              // 中間点1の広がり
	public float convergenceScale = 0;				// 中間点2の広がり
    public float timeScale = 30;					// 着弾所要時間(総存在時間=着弾所要時間+テイル長)
    public int tailLength = 20;						// テイル長
    public float amplitude = 0.25f;					// 弾道歪み幅
    public float undulation = 0.05f;				// テイル歪み移動速度倍率
    public float width = 0.2f;							// テイル太さ
    private Vector3[] p = new Vector3[9];			// ベジェ中間点
    private float time = 0;							// 存在時間カウンタ
    private Vector3[] oldP;							// テイル関節位置
    private Vector3[] undulations;					// テイル関節毎の歪み移動速度
    //public GameObject p1;
    //public GameObject p2;

    private MeshFilter mf;

    // Use this for initialization
    void Start () {
	
	}

    public void Initialize(GameObject emitter, GameObject target)
    {
        this.emitter = emitter.transform.position;
        this.target = target;
        p[0] = emitter.transform.position;
        p[3] = target.transform.position;
        float a = Random.value * 360;
        float r = Random.Range(0.5f, 1.0f);
        p[1] = (Quaternion.AngleAxis(a, emitter.transform.forward) * emitter.transform.up * r + emitter.transform.forward / 2) * divergenceScale + p[0];
		a = Random.value * 360;
		r = Random.Range(0.5f, 1.0f);
		p[2] = (Quaternion.AngleAxis(a, emitter.transform.forward) * emitter.transform.up * r) * convergenceScale + (p[0] + p[3]) / 2;
        //mf = transform.GetChild(0).GetComponent<MeshFilter>();
        oldP = new Vector3[tailLength + 2];
        for(int i = 0; i < oldP.Length; i++)
        {
            oldP[i] = p[0];
        }
        undulations = new Vector3[tailLength + 2];
        for (int i = 0; i < undulations.Length; i++)
        {
            undulations[i] = Random.insideUnitSphere * undulation;
        }
        mf = transform.GetChild(0).gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(tailLength + 1) * 4];
        int[] triangles = new int[tailLength * 12];
        for (int i = 0; i < tailLength; i++)
        {
            triangles[(i * 12)] = i * 4;
            triangles[(i * 12) + 1] = i * 4 + 1;
            triangles[(i * 12) + 2] = i * 4 + 4;
            triangles[(i * 12) + 3] = i * 4 + 1;
            triangles[(i * 12) + 4] = i * 4 + 5;
            triangles[(i * 12) + 5] = i * 4 + 4;
            triangles[(i * 12) + 6] = i * 4 + 2;
            triangles[(i * 12) + 7] = i * 4 + 3;
            triangles[(i * 12) + 8] = i * 4 + 6;
            triangles[(i * 12) + 9] = i * 4 + 3;
            triangles[(i * 12) + 10] = i * 4 + 7;
            triangles[(i * 12) + 11] = i * 4 + 6;
        }
        Vector2[] uv = new Vector2[(tailLength + 1) * 4];
        for(int i = 0; i < tailLength + 1; i++)
        {
            uv[i * 4] = uv[i * 4 + 2] = new Vector2(0, i / (float)tailLength);
            uv[i * 4 + 1] = uv[i * 4 + 3] = new Vector2(1, i / (float)tailLength);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mf.mesh = mesh;

    }

    // Update is called once per frame
    new void Update ()
    {
        p[3] = Target;
        //p[2] = (p[0] + p[3]) / 2;

        float t = time / timeScale;

        p[4] = (p[1] - p[0]) * t + p[0];
        p[5] = (p[2] - p[1]) * t + p[1];
        p[6] = (p[3] - p[2]) * t + p[2];
        p[7] = (p[5] - p[4]) * t + p[4];
        p[8] = (p[6] - p[5]) * t + p[5];

        if(time <= timeScale)
        transform.position = (p[8] - p[7]) * t + p[7];
        //transform.rotation = Quaternion.LookRotation(transform.position - oldP[0], Vector3.up);

        for(int i = oldP.Length - 1; i > 0; i--)
        {
            oldP[i] = oldP[i - 1];
            undulations[i] = undulations[i - 1];
            oldP[i] += undulations[i];
        }
        oldP[0] = transform.position + Random.insideUnitSphere * amplitude;
        undulations[0] = Random.insideUnitSphere * undulation;

        //p1.transform.position = p[1];
        //p2.transform.position = p[2];

        //Mesh mesh = mf.mesh;
        //mesh.vertices[0] = Vector3.zero;
        //transform.GetChild(0).GetComponent<MeshFilter>().mesh = mesh;
        Vector3[] vertices = mf.mesh.vertices;
        Vector3 dir;
        Quaternion q;
        for (int i = 0; i * 4 + 3 < vertices.Length; i++)
        {
            dir = oldP[i + 1] - oldP[i];
            if (dir != Vector3.zero)
                q = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(0, 0, (time - i) * 5);
            else
                q = Quaternion.identity;
            vertices[i * 4] = oldP[i] - transform.position + q * new Vector3(0, width, 0);
            vertices[i * 4 + 1] = oldP[i] - transform.position + q * new Vector3(0, -width, 0);
            vertices[i * 4 + 2] = oldP[i] - transform.position + q * new Vector3(width, 0, 0);
            vertices[i * 4 + 3] = oldP[i] - transform.position + q * new Vector3(-width, 0, 0);
        }
        mf.mesh.vertices = vertices;

		base.Update();

        time++;
		if (time == timeScale) impact = true;
        if(time > timeScale + oldP.Length)
        {
            Destroy(gameObject);
        }

    }
}
