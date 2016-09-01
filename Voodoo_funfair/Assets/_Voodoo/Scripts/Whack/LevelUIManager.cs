using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour {

	//used to pass the next level name & fade
	public delegate void LoadNextLevel(string gettedLevelName);
	public static event LoadNextLevel OnLoadNextLevel;


	public delegate void TimeOut();
	public static event TimeOut OnTimeOut;

	//Game finished
	public delegate void GameFinished();
	public static event GameFinished OnGameFinished;

	//UI panel to fade
	public Image fadingPanelImage;

	//UI pause panel
	public GameObject pausePanelImage;

	//Ui Panel alpha values
	private float alphaHundred = 1.0f;
	private float alphaZero = 0.0f;

	//Level Name
	private string setLevelName;

	//Score
	private int score;
	public Text scoreText;

	private bool gameStarted = false;

	//inGamePanel
	public GameObject inGamePanel;
	private Animator inGamePanelAnimator;


	//Timer
	public Text secondsText;
	public Text minutesText;
	private float seconds = 30.0f;
	private int minutes = 1;

	//End UI
	public GameObject endInGamePanel;
	private Animator endInGamePanelAnimator;
	public Text totalScoreText;
	public Text bestScoreText;
	public Text ticketsText;

	public GameObject endChoisePanel;
	public Light sceneLight;

	private int ticketsAmount;

	void Awake () 
	{
		FadeInAndLoad ();

		if (OnTimeOut != null) 
		{
			OnTimeOut ();
		}
	}

	void Start () 
	{
		inGamePanelAnimator = inGamePanel.GetComponent<Animator> ();
		endInGamePanelAnimator = endInGamePanel.GetComponent<Animator> ();

	}

	void Update()
	{
		if (gameStarted == true) 
		{

			if (seconds <= 0) {

				seconds = 59;

				if (minutes >= 1) {
					minutes--;
				} else {
					minutes = 0;
					seconds = 0;
				}

			} else {
				seconds -= Time.deltaTime;
			}

			scoreText.text = TextDisplay (score, 4);
			secondsText.text = TextDisplay ((int)seconds, 2);
			minutesText.text = TextDisplay (minutes, 2);
		}

		if (minutes == 0 && seconds == 0) 
		{
			ShowEndInGameUI ();
		}
	}


	void OnEnable()
	{
		Projectile.OnLoadLevelCollision += FadeOutBeforeLoad;
		Projectile.OnIncreaseScoreCollision += IncreaseScore;
		Projectile.OnStartFunFairGame += ShowInGameUI;
		PauseGame.OnGamePaused += ShowPausedImage;
		PauseGame.OnGameUnPaused += HidePausedImage;
	}

	void OnDisable()
	{
		Projectile.OnLoadLevelCollision -= FadeOutBeforeLoad;
		Projectile.OnIncreaseScoreCollision -= IncreaseScore;
		Projectile.OnStartFunFairGame -= ShowInGameUI;
		PauseGame.OnGamePaused -= ShowPausedImage;
		PauseGame.OnGameUnPaused -= HidePausedImage;
	}

	private void ShowPausedImage()
	{
		pausePanelImage.SetActive (true);
	}

	private void HidePausedImage()
	{
		pausePanelImage.SetActive (false);
	}

	void ShowInGameUI()
	{
		gameStarted = true;
		inGamePanel.SetActive (true);
		inGamePanelAnimator.SetBool ("showGamePanel",true);
	}

	void IncreaseScore ()
	{
		score += 5;
	}

	void ShowEndInGameUI()
	{
		if (OnGameFinished != null) 
		{
			OnGameFinished ();
		}

		gameStarted = false;
		inGamePanel.SetActive (false);

		totalScoreText.text = TextDisplay (score, 4);
		bestScoreText.text = TextDisplay (score, 4);
		if (score == 0) {
			ticketsAmount = 0;
		} else {
			ticketsAmount = (score / 10) + 5;
		}

		ticketsText.text = TextDisplay (ticketsAmount, 3);

		sceneLight.intensity = 0;
		endChoisePanel.SetActive(true);
		endInGamePanel.SetActive (true);
		endInGamePanelAnimator.SetBool ("showEndPanel",true);
	}

	private string TextDisplay(int amount, int digits)
	{
		string displayAmount = "";

		for (int i = 0; i < digits - amount.ToString().Length; i++)
		{
			displayAmount += "0";
		}

		displayAmount += amount.ToString ();

		return displayAmount;
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
