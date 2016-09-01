using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneUIManager : MonoBehaviour {

	//used to pass the next level name & fade
	public delegate void LoadNextLevel(string gettedLevelName);
	public static event LoadNextLevel OnLoadNextLevel;

	//UI panel to fade
	public Image fadingPanelImage;


	//Ui Panel alpha values
	private float alphaHundred = 1.0f;
	private float alphaZero = 0.0f;

	//Level Name
	private string setLevelName;

	void Awake () 
	{
		FadeInAndLoad ();
	}





	void OnEnable()
	{
		Projectile.OnLoadLevelCollision += FadeOutBeforeLoad;
	}

	void OnDisable()
	{
		Projectile.OnLoadLevelCollision -= FadeOutBeforeLoad;
	}		

	//In transition
	void FadeInAndLoad () 
	{
		fadingPanelImage.CrossFadeAlpha(alphaZero, 0.5f, false);
	}

	//Out Transition
	void FadeOutBeforeLoad(string getLevelName)
	{
		fadingPanelImage.CrossFadeAlpha(alphaHundred, 0.5f, false);

		setLevelName = getLevelName;
			
		Invoke("InvokeNextLevel",1.5f);
	}

	void InvokeNextLevel()
	{
		if (OnLoadNextLevel != null) {
			OnLoadNextLevel (setLevelName);
		}
	}



}
