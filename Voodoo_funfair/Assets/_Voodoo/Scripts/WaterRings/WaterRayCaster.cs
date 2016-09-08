using UnityEngine;
using System.Collections;

public class WaterRayCaster : MonoBehaviour {
	
	public delegate void RaycastRightArrow ();
	public static event RaycastRightArrow OnRaycastRightArrow;

	public delegate void RaycastLeftArrow ();
	public static event RaycastLeftArrow OnRaycastLeftArrow;

	public delegate void RaycastHitExit ();
	public static event RaycastHitExit OnRaycastHitExit;


	private Ray ray;
	private RaycastHit rayHit;

	public GameObject[] Arrows;

	void Update() {

		ray = new Ray (transform.localPosition, transform.forward);



		if (Physics.Raycast (ray, out rayHit, 1000)) 
		{

			if (rayHit.collider.tag == "rayRightArrow") 
			{
				Arrows [0].GetComponent<SpriteRenderer> ().color = Color.red;
				if (OnRaycastRightArrow != null) 
				{
					OnRaycastRightArrow ();
				}
			} else if (rayHit.collider.tag == "rayLeftArrow") 
			{
				Arrows [1].GetComponent<SpriteRenderer> ().color = Color.red;
				if (OnRaycastLeftArrow != null) 
				{
					OnRaycastLeftArrow ();
				}
			} else 
			{
				Arrows [0].GetComponent<SpriteRenderer> ().color = Color.white;
				Arrows [1].GetComponent<SpriteRenderer> ().color = Color.white;
				if (OnRaycastHitExit != null) 
				{
					OnRaycastHitExit ();
				}
			}
		}
	}
}
