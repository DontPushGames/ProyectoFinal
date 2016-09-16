using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectorUiManager : MonoBehaviour {

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

	public Text ticketsAmountText;
	private int ticketsAmount;

	public GameObject whackTitle;
	public GameObject storeTitle;
	public GameObject ringsTitle;

	void Awake () 
	{
		FadeInAndLoad ();
	}


	void Update()
	{
		ticketsAmountText.text = TextDisplay (ticketsAmount, 4);
	}


	void OnEnable()
	{
		Projectile.OnLoadLevelCollision += FadeOutBeforeLoad;
		RewardsSystem.OnShowRewardsUI += ShowRewardsUI;
		LevelRayCaster.OnRaycastHitOrbe += ShowLevelTitle;
		LevelRayCaster.OnRaycastHitExit += HideLevelTitle;
	}

	void ShowRewardsUI (int wonTickets)
	{
		ticketsAmount += wonTickets;
		Debug.Log ("you win" + wonTickets);
	}

	void OnDisable()
	{
		Projectile.OnLoadLevelCollision -= FadeOutBeforeLoad;
	}

	void ShowLevelTitle(string orbeName)
	{
		switch(orbeName)
		{
		case "Whack":
			whackTitle.SetActive (true);
			break;
		case "Orbe1":
			storeTitle.SetActive (true);
			break;
		case "WaterRings":
			ringsTitle.SetActive (true);
			break;

		default:
			break;
		}
	}

	void HideLevelTitle()
	{
		whackTitle.SetActive (false);
		storeTitle.SetActive (false);
		ringsTitle.SetActive (false);
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
