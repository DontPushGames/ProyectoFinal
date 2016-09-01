using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenScene : MonoBehaviour {

	//UI panel to fade
	public Image dontpushImage;


	//Ui Panel alpha values
	private float alphaZero = 0;


	void Start () 
	{
		Invoke ("FadeInAndLoad",1.0f);
	}

	//In transition
	void FadeInAndLoad () 
	{
		dontpushImage.CrossFadeAlpha(alphaZero, 2.5f, false);

		Invoke ("LoadMainScene", 3.0f);
	}

	void LoadMainScene ()
	{
		SceneManager.LoadScene ("MainScene");
	}
}
