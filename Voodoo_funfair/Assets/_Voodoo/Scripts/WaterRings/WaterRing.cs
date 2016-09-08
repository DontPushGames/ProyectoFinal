using UnityEngine;
using System.Collections;

public class WaterRing : MonoBehaviour {

	public delegate void IncreaseScore ();
	public static event IncreaseScore OnIncreaseScore;

	public delegate void LevelCompleted ();
	public static event LevelCompleted OnLevelCompleted;


	private Rigidbody ringRigidbody;
	public float gravityMul = 8.8f;
	public float impulseForce = 5.0f;

	private int scoreSum = 0;
	public GameObject[] pin;
	public GameObject[] pinGroup;

	void Start () 
	{
		ringRigidbody = gameObject.GetComponent<Rigidbody> ();

	}

	void Update()
	{
		switch (scoreSum) 
		{
		case 1:
			pinGroup [0].SetActive (false);
			pinGroup [1].SetActive (true);
				break;
		case 3:
			pinGroup [1].SetActive (false);
			pinGroup [2].SetActive (true);
			break;
		case 6:
			pinGroup [2].SetActive (false);
			if (OnLevelCompleted != null) {
				OnLevelCompleted ();
				scoreSum++;
			}
			break;

		default:
			break;
		}
	}


	void FixedUpdate ()
	{
		if (Input.GetMouseButtonDown (0)) {
			ringRigidbody.AddForce (Vector3.up * impulseForce, ForceMode.Impulse);



		} else {
			ringRigidbody.AddForce (Vector3.up * gravityMul);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Pin") 
		{
			
			if (OnIncreaseScore != null) 
			{
				OnIncreaseScore ();
			}
			Destroy (col.transform.parent.gameObject);
			scoreSum++;
		}
	}
}
