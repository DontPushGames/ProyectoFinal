using UnityEngine;
using System.Collections;

public class ScrollParallax : MonoBehaviour {

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
		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (savedOffset.x, y);
		render.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable () 
	{
		render.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
