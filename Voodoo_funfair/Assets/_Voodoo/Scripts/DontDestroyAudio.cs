using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

	}

	void Update()
	{
		if(SceneManager.GetActiveScene().name == "Whack" )
		{
			Destroy(GameObject.FindGameObjectWithTag("AudioVoices"));
		}
	}
}
