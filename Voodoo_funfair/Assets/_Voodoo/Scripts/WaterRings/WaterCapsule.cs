using UnityEngine;
using System.Collections;

public class WaterCapsule : MonoBehaviour {


	private bool leftArrow = false;
	private bool rightArrow = false;

	
	private void OnEnable()
	{
		WaterRayCaster.OnRaycastRightArrow += RightArrowCasted;
		WaterRayCaster.OnRaycastLeftArrow += LeftArrowCasted;
		WaterRayCaster.OnRaycastHitExit += ArrowsRayCastExit;
	}

	private void OnDisable()
	{
		WaterRayCaster.OnRaycastRightArrow -= RightArrowCasted;
		WaterRayCaster.OnRaycastLeftArrow -= LeftArrowCasted;
		WaterRayCaster.OnRaycastHitExit -= ArrowsRayCastExit;
	}

	private void LeftArrowCasted()
	{
		leftArrow = true;
	}

	private void RightArrowCasted()
	{
		rightArrow = true;
	}

	private void ArrowsRayCastExit()
	{
		leftArrow = false;
		rightArrow = false;
	}

	void Update () 
	{
		if (leftArrow == true && rightArrow == false) {
			transform.Rotate(0,0,10 * Time.deltaTime);
		}else if(leftArrow == false && rightArrow == true){
			
			transform.Rotate(0,0,-10 * Time.deltaTime);

		}else{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 2);
		}

	}
}
