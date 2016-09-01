using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public delegate void GameUnPaused();
	public static event GameUnPaused OnGameUnPaused;

	public delegate void GamePaused();
	public static event GamePaused OnGamePaused;

	public float timeToPause;


	void Start () {
	
	}
	

	void Update () {

		if (Input.GetKey (KeyCode.Escape) && Time.timeScale == 1) {
			Time.timeScale = 0;
			if (OnGamePaused != null) {
				OnGamePaused ();
			}

		} else if (Input.GetMouseButtonDown (0) && Time.timeScale == 0) {
			Time.timeScale = 1;
			if (OnGameUnPaused != null) {
				OnGameUnPaused ();
			}
		}
	
	
	}


}
