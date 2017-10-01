using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Behavior : MonoBehaviour {

	private static Level2Behavior _instance;
	public static Level2Behavior Instance {
		get {
			return _instance;
		}
	}

	[Header("Level 2")]
	public GameObject textLargeObject;
	public Text objectiveTextLarge, objectiveTextSmall, scoreText;
	public List<GameObject> labelsList = new List<GameObject> ();
	private int numPassages = 2;
	private int score = 0;
	[HideInInspector]
	public string desiredPassage;
	[HideInInspector]
	public List<string> Level2Passages = new List<string> ();
	private List<Transform> possiblePassages = new List<Transform> ();


	void Awake(){
		_instance = this;
	}

	void Start(){
		//this is in start to avoid race condition
		possiblePassages = Level1Behavior.Instance.possiblePassages;
	}

	public void Init(){
		score = 0;
		scoreText.text = "Score: " + score;

		//turn off all labels
		foreach (GameObject go in labelsList) {
			go.SetActive (false);
		}
		//add random passages to list and dont add duplicates
		StartCoroutine (AddPassagesRoutine ());
	}

	IEnumerator AddPassagesRoutine(){
		//had to make this a coroutine so that passages were removed from list
		//before we loop through again.
		for (int i = 0; i < numPassages; i++) {
			int randIndex = UnityEngine.Random.Range(0,possiblePassages.Count);
			Level2Passages.Add (possiblePassages [randIndex].name);
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
		score++;
		scoreText.text = "Score: " + score;

		yield return new WaitForSeconds (3f);

		if (Level2Passages.Count > 0) {
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
		objectiveTextLarge.text = "Level 2 Complete!!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		//turn all labels back on
		foreach (GameObject go in labelsList) {
			go.SetActive (true);
		}
		LevelManager.Instance.LevelComplete ();
	}

	void ChooseNewPassage(){
		//choose random desired passage from list
		int randPassIndex = UnityEngine.Random.Range(0,Level2Passages.Count);
		desiredPassage = Level2Passages [randPassIndex];
		Level2Passages.Remove (desiredPassage);
	}
}
