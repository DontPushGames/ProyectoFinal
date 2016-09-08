using UnityEngine;
using System.Collections;

public class WaterTextureOffset : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;
	private Renderer render;

	void Start () 
	{
		render = gameObject.GetComponent<Renderer> ();
		savedOffset = render.sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void Update () 
	{
		float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		render.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable () 
	{
		render.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
