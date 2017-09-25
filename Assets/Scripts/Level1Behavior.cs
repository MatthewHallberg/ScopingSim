using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Behavior : MonoBehaviour {

	private static Level1Behavior _instance;
	public static Level1Behavior Instance {
		get {
			return _instance;
		}
	}

	[Header("Level 1")]
	public GameObject textLargeObject;
	public Text objectiveTextLarge, objectiveTextSmall;
	public List<Transform> possiblePassages = new List<Transform> ();
	private int numPassages = 2;
	[HideInInspector]
	public string desiredPassage;
	[HideInInspector]
	public List<string> Level1Passages = new List<string> ();

	void Awake(){
		_instance = this;
	}

	public void Init(){
		//add random passages to list and dont add duplicates
		StartCoroutine (AddPassagesRoutine ());
	}

	IEnumerator AddPassagesRoutine(){
		//had to make this a coroutine so that passages were removed from list
		//before we loop through again.
		for (int i = 0; i < numPassages; i++) {
			int randIndex = UnityEngine.Random.Range(0,possiblePassages.Count);
			Level1Passages.Add (possiblePassages [randIndex].name);
			yield return possiblePassages.Remove(possiblePassages [randIndex]);
		}
		StartCoroutine (Level1Routine ());
	}

	IEnumerator Level1Routine(){
		yield return new WaitForSeconds (1f);
		ChooseNewPassage ();
		//set defaults
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		//set objective text
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Navigate to the \n" + desiredPassage; 
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "Get to the \n " + desiredPassage + "!";
	}

	public void FoundPassage(){
		desiredPassage = "temp";
		StartCoroutine (FoundPassageRoutine ());
	}

	IEnumerator FoundPassageRoutine(){

		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Great job! \n You found it!";
		objectiveTextSmall.text = "";

		yield return new WaitForSeconds (3f);

		if (Level1Passages.Count > 0) {
			StartCoroutine (Level1Routine ());
		} else {
			LevelComplete ();
		}
	}

	public void LevelComplete(){
		StartCoroutine (LevelCompleteRoutine ());
	}

	IEnumerator LevelCompleteRoutine(){
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Level 1 Complete!!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		LevelManager.Instance.LevelComplete ();
	}

	void ChooseNewPassage(){
		//choose random desired passage from list
		int randPassIndex = UnityEngine.Random.Range(0,Level1Passages.Count);
		desiredPassage = Level1Passages [randPassIndex];
		Level1Passages.Remove (desiredPassage);
	}
}
