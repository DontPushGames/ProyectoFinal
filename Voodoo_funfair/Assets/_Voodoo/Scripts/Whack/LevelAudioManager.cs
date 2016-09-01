using UnityEngine;
using System.Collections;

public class LevelAudioManager : MonoBehaviour {

	public AudioClip whackHeadColision;
	public AudioClip timeOut;

	private AudioSource audioSource;


	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
	}
	
	void OnEnable()
	{
		Projectile.OnIncreaseScoreCollision += PlayColisionSound;
//		LevelUIManager.OnTimeOut += PlayTimeOutSound;
	}

	void OnDisable()
	{
		Projectile.OnIncreaseScoreCollision -= PlayColisionSound;
//		LevelUIManager.OnTimeOut -= PlayTimeOutSound;
	}

	void PlayColisionSound()
	{
		audioSource.PlayOneShot (whackHeadColision);
	}

//	void PlayTimeOutSound()
//	{
//		audioSource.PlayOneShot (timeOut);
//	}
}
