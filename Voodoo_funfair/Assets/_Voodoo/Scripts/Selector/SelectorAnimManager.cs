using UnityEngine;
using System.Collections;

public class SelectorAnimManager : MonoBehaviour {

	public GameObject rSphere;
	public GameObject lSphere;

	private Animator rSphereAnimator;
	private Animator lSphereAnimator;


	//$$$$$$$$LevelSelector$$$$$$$$$$


	public GameObject whackLevelOrbe;

	private Animator whackLevelOrbeAnimator;

	public GameObject waterRingsLevelOrbe;

	private Animator waterRingsOrbeAnimator;

	public GameObject selectorTicketsAmount;

	private Animator selectorTicketsAmountAnimator;

	void Start () 
	{
		rSphereAnimator = rSphere.GetComponent<Animator> ();
		lSphereAnimator = lSphere.GetComponent<Animator> ();
		whackLevelOrbeAnimator = whackLevelOrbe.GetComponent<Animator> ();
		waterRingsOrbeAnimator = waterRingsLevelOrbe.GetComponent<Animator> ();
		selectorTicketsAmountAnimator = selectorTicketsAmount.GetComponent<Animator> ();
	}


	void Update () 
	{

	}

	private void OnEnable()
	{
		Spawner.OnGearInputDown += FireEqualsTrue;
		LevelRayCaster.OnRaycastHitOrbe += PlayOrbeAnimation;
		LevelRayCaster.OnRaycastHitExit += StopOrbeAnimation;
		RewardsSystem.OnRewardsUIAnim += LevelSelectorUiAnim;
	}

	private void OnDisable()
	{
		Spawner.OnGearInputDown -= FireEqualsTrue;
		LevelRayCaster.OnRaycastHitOrbe -= PlayOrbeAnimation;
		LevelRayCaster.OnRaycastHitExit -= StopOrbeAnimation;
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

	private void PlayOrbeAnimation(string orbeName)
	{
		switch(orbeName)
		{
		case "Whack":
			whackLevelOrbeAnimator.SetBool ("FadeOrbe", true);
			break;

		case "WaterRings":
			waterRingsOrbeAnimator.SetBool ("FadeOrbe", true);
			break;

		default:
			break;
		}
	}

	void LevelSelectorUiAnim()
	{
		selectorTicketsAmountAnimator.SetBool ("ShowTicketsAmount", true);
	}

	private void StopOrbeAnimation()
	{
		if (whackLevelOrbeAnimator.GetBool ("FadeOrbe") == true) 
		{
			whackLevelOrbeAnimator.SetBool ("FadeOrbe", false);
		}else if (waterRingsOrbeAnimator.GetBool ("FadeOrbe") == true) 
		{
			waterRingsOrbeAnimator.SetBool ("FadeOrbe", false);
		}
	}
}
