using UnityEngine;
using System.Collections;

public class LastingRay : RayShot
{

    public GameObject emitter;
    //public GameObject target;
    private Vector3[] p = new Vector3[9];
    public float distortionScale = 10;
    public float time = 0;
    public float timeScale = 120;
    public int tailLength = 20;
    private Vector3[] oldP;
    private float a1;
    private float a2;
    private float r1;
    private float r2;
    private float a1Vel;
    private float a2Vel;

    private MeshFilter mf;
    float w = 0.2f;

    public void Initialize(GameObject emitter, GameObject target)
    {
        this.emitter = emitter;
        this.target = target;
        p[0] = emitter.transform.position;
        p[3] = target.transform.position;
        a1 = Random.value * 360;
        a2 = Random.value * 360;
        r1 = Random.Range(0.25f, 0.5f);
        r2 = Random.Range(0.25f, 0.5f);
        a1Vel = Random.Range(-5, 5);
        a2Vel = Random.Range(-5, 5);
        p[1] = (Quaternion.AngleAxis(a1, emitter.transform.forward) * Vector3.up * r1 + emitter.transform.forward / 2) * distortionScale + p[0];
        p[2] = (Quaternion.AngleAxis(a2, p[3] - p[0]) * Vector3.up * r2) * distortionScale + (p[0] + p[3]) / 2;
        //mf = transform.GetChild(0).GetComponent<MeshFilter>();
        oldP = new Vector3[tailLength + 2];
        for (int i = 0; i < oldP.Length; i++)
        {
            oldP[i] = p[0];
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
        for (int i = 0; i < tailLength + 1; i++)
        {
            //uv[i * 4] = uv[i * 4 + 2] = new Vector2(0, i / (float)tailLength);
            //uv[i * 4 + 1] = uv[i * 4 + 3] = new Vector2(1, i / (float)tailLength);
            uv[i * 4] = uv[i * 4 + 2] = new Vector2(0, i % 2);
            uv[i * 4 + 1] = uv[i * 4 + 3] = new Vector2(1, i % 2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mf.mesh = mesh;

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	new void Update ()
    {
        a1 += a1Vel;
        a2 += a2Vel;
		p[0] = emitter.transform.position;
        p[3] = Target;
        p[1] = (Quaternion.AngleAxis(a1, emitter.transform.forward) * Vector3.up * r1 + emitter.transform.forward / 2) * distortionScale + p[0];
        p[2] = (Quaternion.AngleAxis(a2, p[3] - p[0]) * Vector3.up * r2) * distortionScale + (p[0] + p[3]) / 2;
        //transform.rotation = Quaternion.LookRotation(transform.position - oldP[0], Vector3.up);

        for (int i = 0; i < oldP.Length; i++)
        {

            float t = (oldP.Length - 2 - i) / (float)(oldP.Length - 2);

            p[4] = (p[1] - p[0]) * t + p[0];
            p[5] = (p[2] - p[1]) * t + p[1];
            p[6] = (p[3] - p[2]) * t + p[2];
            p[7] = (p[5] - p[4]) * t + p[4];
            p[8] = (p[6] - p[5]) * t + p[5];

            oldP[i] = (p[8] - p[7]) * t + p[7];
        }
            if (time <= timeScale)
                transform.position = p[3];

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
            vertices[i * 4] = oldP[i] - transform.position + q * new Vector3(0, w, 0);
            vertices[i * 4 + 1] = oldP[i] - transform.position + q * new Vector3(0, -w, 0);
            vertices[i * 4 + 2] = oldP[i] - transform.position + q * new Vector3(w, 0, 0);
            vertices[i * 4 + 3] = oldP[i] - transform.position + q * new Vector3(-w, 0, 0);
        }
        mf.mesh.vertices = vertices;

        time++;
        if (time > timeScale)
        {
            Destroy(gameObject);
        }

    }
}
