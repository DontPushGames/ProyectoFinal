using UnityEngine;
using System.Collections;

public class LevelRayCaster : MonoBehaviour {

	public delegate void RaycastHitOrbe (string orbeName);
	public static event RaycastHitOrbe OnRaycastHitOrbe;

	public delegate void RaycastHitExit ();
	public static event RaycastHitExit OnRaycastHitExit;


	private Ray ray;
	private RaycastHit rayHit;

	void Update() {

		ray = new Ray (transform.localPosition, transform.forward);



		if (Physics.Raycast (ray, out rayHit, 1000)) {
			
			if (rayHit.collider.tag == "rayLevelTarget" || rayHit.collider.tag == "soonRayLevelTarget") {
				if (OnRaycastHitOrbe != null) {
					OnRaycastHitOrbe (rayHit.collider.name);
				}
			}
		} else {
			if (OnRaycastHitExit != null) {
				OnRaycastHitExit ();
			}
		}
	}
}
