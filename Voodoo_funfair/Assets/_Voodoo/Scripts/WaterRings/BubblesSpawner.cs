using UnityEngine;
using System.Collections;

public class BubblesSpawner : MonoBehaviour {

	public ParticleSystem bubblesParticles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Instantiate(bubblesParticles,gameObject.transform.position,gameObject.transform.localRotation);
		}
	}
}
