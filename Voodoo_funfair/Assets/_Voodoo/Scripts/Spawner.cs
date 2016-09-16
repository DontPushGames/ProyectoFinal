using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject projectile;

	public delegate void GearInputDown();
	public static event GearInputDown OnGearInputDown;

	public float timeBetweenShots = 1.0f;  // Allow 3 shots per second

	private float timeCheck;

	public AudioClip scepterAudio;
	private AudioSource audioSource;

	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
	}
	

	void Update ()
	{ 
		
			if (Input.GetMouseButtonDown (0) && Time.time >= timeCheck) {
				if (OnGearInputDown != null) {
					OnGearInputDown ();
				}
				Instantiate (projectile, transform.position, transform.rotation);
				audioSource.PlayOneShot (scepterAudio);
				timeCheck = Time.time + timeBetweenShots;
			}
	
	}
}
