using UnityEngine;
using System.Collections;

public class RewardsSystem : MonoBehaviour {

	public delegate void RewardsUIAnim();
	public static event RewardsUIAnim OnRewardsUIAnim;

	public delegate void ShowRewardsUI(int wonTicketsAmount);
	public static event ShowRewardsUI OnShowRewardsUI;

	public Material[] orbeMaterials;
	private MeshRenderer orbeMeshRenderer;
	private int matIndex;
	private int timeToStop;
	private float stopOrbeTimer;
	private bool RewardSystemEnabled = false;

	private int wonTickets;

	public ParticleSystem rewardparticle;

	public AudioClip changeMatAudio;
	public AudioClip wonTicketsAudio;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		orbeMeshRenderer = gameObject.GetComponent<MeshRenderer>();
		orbeMeshRenderer.enabled = true;
		orbeMeshRenderer.sharedMaterial = orbeMaterials[0];

		audioSource = GetComponent<AudioSource> ();

		timeToStop = Random.Range (3, 5); 

	}
	
	// Update is called once per frame
	void Update () {
		
		if (RewardSystemEnabled == true)
		{
			if ((int)stopOrbeTimer <= timeToStop) 
			{
				stopOrbeTimer += Time.deltaTime;
			} else {
				
				CancelInvoke ("ChangeMaterial");
				Instantiate (rewardparticle, gameObject.transform.position, gameObject.transform.rotation);
				audioSource.PlayOneShot (wonTicketsAudio);
				if (OnShowRewardsUI != null) 
				{
					switch (matIndex) 
					{
					case 1:
						wonTickets = 10;
						break;
					case 2:
						wonTickets = 20;
						break;
					case 3:
						wonTickets = 50;
						break;
					case 4:
						wonTickets = 100;
						break;
					}
					OnShowRewardsUI (wonTickets);
					if (OnRewardsUIAnim != null) 
					{
						OnRewardsUIAnim ();
					}
				}
				RewardSystemEnabled = false;
			}
		}



	}

	private void OnEnable()
	{
		Projectile.OnStartRewardSystem += ShowReward;
	}


	private void ChangeMaterial()
	{
		
		matIndex = Random.Range (1,5);
		orbeMeshRenderer.sharedMaterial = orbeMaterials[matIndex];
		audioSource.PlayOneShot (changeMatAudio);

	}

	private void ShowReward()
	{
		InvokeRepeating ("ChangeMaterial",0.01f,0.1f);
		RewardSystemEnabled = true;


	}
}
