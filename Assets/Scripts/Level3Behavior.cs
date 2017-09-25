using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Behavior : MonoBehaviour {

	private static Level3Behavior _instance;
	public static Level3Behavior Instance {
		get {
			return _instance;
		}
	}

	[Header("Level 3")]
	public Transform BubblesParent;
	public GameObject textLargeObject;
	public Text objectiveTextLarge, objectiveTextSmall;

	void Awake(){
		_instance = this;
		BubblesParent.gameObject.SetActive (false);
	}

	public void Init(){
		StartCoroutine (Level3Routine ());
	}

	IEnumerator Level3Routine(){
		BubblesParent.gameObject.SetActive (true);
		//set defaults
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		//set objective text
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Find The Lesion!";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "Find The Lesion.";
	}

	public void FoundLesion(){
		StartCoroutine (FoundPassageRoutine ());
	}

	IEnumerator FoundPassageRoutine(){

		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Great job! \n You found it!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (3f);
		LevelComplete ();
	}

	public void LevelComplete(){
		StartCoroutine (LevelCompleteRoutine ());
	}

	IEnumerator LevelCompleteRoutine(){
		BubblesParent.gameObject.SetActive (false);
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Level 3 Complete!!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		LevelManager.Instance.LevelComplete ();
	}
}
