using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	//used to pass the next level name & fade
	public delegate void LoadLevelCollision(string nextLevelName);
	public static event LoadLevelCollision OnLoadLevelCollision;

	//Start Reward System
	public delegate void StartRewardSystem();
	public static event StartRewardSystem OnStartRewardSystem;

	//Start fair game
	public delegate void StartFunFairGame();
	public static event StartFunFairGame OnStartFunFairGame;

	//Score++
	public delegate void IncreaseScoreCollision();
	public static event IncreaseScoreCollision OnIncreaseScoreCollision;

	//access to the projectile's rigidbody
	private Rigidbody projectileRigidbody;

	//Projectile speed & life time
	private float projectileSpeed = 32.0f;
	private float projectileLifeTime = 1.5f;

	public ParticleSystem projectileParticle;
//	public ParticleSystem startGameParticle;
	public ParticleSystem batParticle;

	void Start () 
	{
		projectileRigidbody = gameObject.GetComponent<Rigidbody> ();
	}
	

	void Update () 
	{
		projectileRigidbody.velocity = transform.forward * projectileSpeed;
		Destroy (gameObject, projectileLifeTime);
	}

	void OnCollisionEnter(Collision col)
	{
		//cases : loadlevel , Score++
		switch (col.gameObject.tag) 
		{
		case "rayLevelTarget":
			

				if(OnLoadLevelCollision != null)
				{
					OnLoadLevelCollision (col.gameObject.name);
				}
			
			break;

		case "pointsTargets":
			if (OnIncreaseScoreCollision != null) 
			{
				OnIncreaseScoreCollision ();
			}
			break;

		case "StartGame":
			Destroy (col.gameObject);
			Instantiate (batParticle,col.gameObject.transform.position, col.gameObject.transform.rotation);
			if (OnStartFunFairGame != null) 
			{
				OnStartFunFairGame ();
			}
			break;

		case "RewardSystem":
			Destroy (col.gameObject);
			Instantiate (batParticle,col.gameObject.transform.position, col.gameObject.transform.rotation);
			if (OnStartRewardSystem != null) 
			{
				OnStartRewardSystem ();
			}
			break;

		case "Quit":
			Application.Quit ();
			break;
		

		default:
			Destroy (this.gameObject);
			break;

		}

		Destroy (this.gameObject);
		Instantiate (projectileParticle, transform.position, transform.rotation);
	}

	void OnTriggerEnter(Collider colli)
	{
		Instantiate (projectileParticle, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
