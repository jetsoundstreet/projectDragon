using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class BerserkImageEffect : MonoBehaviour {

	[SerializeField]
	[Range(0, 1)]
	private float size;

	public Material material;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Size", size);
		Graphics.Blit(source, destination, material);
	}

	public float Timing
	{
		set
		{
			if(value < 0)
			{
				size = 0;
			}
			else if(value < 1)
			{
				size = value;
			}
			else if(value < 3)
			{
				size = 1.5f - value / 2;
			}
			else
			{
				size = 0;
			}
		}
	}
}
