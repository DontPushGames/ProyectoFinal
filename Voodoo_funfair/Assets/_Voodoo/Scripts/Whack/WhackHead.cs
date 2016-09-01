using UnityEngine;
using System.Collections;

public class WhackHead : MonoBehaviour {

	private float upSpeed = 0.07f;
	private float downSpeed = 0.01f;
	private float randomNum;

	private bool gameStarted = false;

	float posX;
	float posZ;
	float posY;
	bool onTop = true;
	Vector3 whackHeadInitialPos;
	Vector3 whackHeadNewPos;



	void Awake ()
	{
		
		whackHeadInitialPos = gameObject.transform.position;
		posX = gameObject.transform.position.x;
		posZ = gameObject.transform.position.z;
		posY = gameObject.transform.position.y + 2.0f;
		whackHeadNewPos = new Vector3(posX, posY, posZ);
		upSpeed = Random.Range (0.07f, 0.09f);
		downSpeed = Random.Range (0.01f, 0.03f);
	}

	void OnEnable()
	{
		Projectile.OnStartFunFairGame += GameTarted;
		LevelUIManager.OnGameFinished += GameEnded;
	}

	void OnDisable()
	{
		Projectile.OnStartFunFairGame += GameTarted;
		LevelUIManager.OnGameFinished -= GameEnded;
	}

	void GameTarted()
	{
		gameStarted = true;
	}

	void GameEnded()
	{
		gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, whackHeadInitialPos, downSpeed);
		if (gameObject.transform.position == whackHeadInitialPos) 
		{
			gameObject.SetActive (false);
		}
		gameStarted = false;
	}
	

	void Update () 
	{
		if (gameStarted == true) 
		{

			if (gameObject.transform.position == whackHeadNewPos) {
				onTop = true;
			} else if (gameObject.transform.position == whackHeadInitialPos) {
				RandomNum ();
				Invoke ("raiseWhackHead", randomNum);
			}
			
			if (onTop == true) {
				gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, whackHeadInitialPos, downSpeed);
			} else {
				CancelInvoke ("raiseWhackHead");
				gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, whackHeadNewPos, upSpeed);

			}
		}
		
	}

	void RandomNum()
	{
		randomNum = Random.Range (1.0f, 30.0f);
	}

	void raiseWhackHead () 
	{
		onTop = false;
	}
}
