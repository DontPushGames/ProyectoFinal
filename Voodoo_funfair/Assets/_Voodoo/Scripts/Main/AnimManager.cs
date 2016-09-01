using UnityEngine;
using System.Collections;

public class AnimManager : MonoBehaviour {

	public GameObject rSphere;
	public GameObject lSphere;

	private Animator rSphereAnimator;
	private Animator lSphereAnimator;






	void Start () 
	{
		rSphereAnimator = rSphere.GetComponent<Animator> ();
		lSphereAnimator = lSphere.GetComponent<Animator> ();
	}
	

	void Update () 
	{
		
	}

	private void OnEnable()
	{
		Spawner.OnGearInputDown += FireEqualsTrue;
	}

	private void OnDisable()
	{
		Spawner.OnGearInputDown -= FireEqualsTrue;
	}

	private void FireEqualsTrue()
	{
		rSphereAnimator.SetBool ("Fire", true);
		lSphereAnimator.SetBool ("Fire", true);
		Invoke("FireEqualsFalse" ,0.1f);
	}

	private void FireEqualsFalse()
	{
		rSphereAnimator.SetBool ("Fire", false);
		lSphereAnimator.SetBool ("Fire", false);
	}



}
