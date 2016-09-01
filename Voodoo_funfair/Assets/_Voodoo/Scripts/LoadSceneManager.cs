using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour {


	void OnEnable () 
	{
		SceneUIManager.OnLoadNextLevel += LoadNextLevel;
		LevelUIManager.OnLoadNextLevel += LoadNextLevel;
		SelectorUiManager.OnLoadNextLevel += LoadNextLevel;
	}
	

	void OnDisable () 
	{
		SceneUIManager.OnLoadNextLevel -= LoadNextLevel;
		LevelUIManager.OnLoadNextLevel -= LoadNextLevel;
		SelectorUiManager.OnLoadNextLevel -= LoadNextLevel;
	}



	private void LoadNextLevel(string gettedString)
	{
		SceneManager.LoadScene (gettedString);
	}
}
