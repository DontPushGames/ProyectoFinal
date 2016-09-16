using UnityEngine;
using System.Collections;

public class BubblesSpawner : MonoBehaviour {

	public ParticleSystem bubblesParticles;

	public AudioClip bubblesShot;
	private AudioSource bubblesSpawnerAudioSource;

	// Use this for initialization
	void Start () {
		bubblesSpawnerAudioSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Instantiate(bubblesParticles,gameObject.transform.position,gameObject.transform.localRotation);
			bubblesSpawnerAudioSource.PlayOneShot (bubblesShot);
		}
	}
}
