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
	public GameObject textLargeObject;
	public Text objectiveTextLarge, objectiveTextSmall;
	public GameObject AnswerCanvas;
	public Text choice1, choice2, scoreText, inputText;
	private int numTumorsFound= 0;
	public Transform SubmitButton;
	private int score = 0;
	public GameObject Passagelabel;

	private string correctPassage;
	private string correctTumor;

	void Awake(){
		_instance = this;
	}

	public void Init(){
		StartCoroutine (Level3Routine ());
		score = 0;
		scoreText.text = "Score: " + score;
		Passagelabel.SetActive (false);
		//turn off all labels
		foreach (GameObject go in Level2Behavior.Instance.labelsList) {
			go.SetActive (false);
		}
	}

	IEnumerator Level3Routine(){
		TimerBehavior.Instance.RunTime ();
		//generate random tumor and location
		TumorLocationBehavior.Instance.PlaceRandomTumor();
		//set defaults
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		//set objective text
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Find The Problem!";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "Find The Problem.";
	}

	public void FoundTumor(string type, string location){
		correctTumor = type;
		correctPassage = location;
		StartCoroutine (FoundTumorRoutine ());
	}

	IEnumerator FoundTumorRoutine(){
		TimerBehavior.Instance.StopTime ();
		score++;
		scoreText.text = "Score: " + score;
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Great job! \n You found it!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (2f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		AnswerCanvas.SetActive (true);
		CameraControl.Instance.DisableCameraControl ();
		numTumorsFound++;
	}

	public void LevelComplete(){
		StartCoroutine (LevelCompleteRoutine ());
	}

	IEnumerator LevelCompleteRoutine(){
		AnswerCanvas.SetActive (false);
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Level 3 Complete!!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		Passagelabel.SetActive (true);
		CameraControl.Instance.EnableCameraControl ();
		LevelManager.Instance.LevelComplete ();
	}

	public void SubmitChoice(){
		
		string answerType = choice1.text;
		string answerLocation = choice2.text;
		string answerText = inputText.text;

		bool gotTumor = false;
		bool gotLocation = false;
		bool gotInput = false;

		if (answerType == correctTumor) {
			gotTumor = true;
		}

		if (answerLocation == correctPassage) {
			gotLocation = true;
		}

		if (answerText.Length > 0) {
			gotInput = true;
		}

		if (gotTumor && gotLocation && gotInput) {

			StartCoroutine(CorrectlyIdentifiedRoutine());

			print ("TUMOR TYPE: " + answerType);
			print ("TUMOR LOCATION: " + answerLocation);
			print ("TEXT INPUT: " + answerText);

		} else {
			SubmitButton.GetChild (0).GetComponent<Text> ().text = "Try Again";
		}
	}

	IEnumerator CorrectlyIdentifiedRoutine(){

		TumorLocationBehavior.Instance.DestroyTumor ();
		AnswerCanvas.SetActive (false);

		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		//set objective text
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "That's Correct!";

		yield return new WaitForSeconds (3f);

		if (numTumorsFound > 2) {
			LevelComplete ();
		} else {
			CameraControl.Instance.ResetCamera ();
			CameraControl.Instance.ToggleCameraControl ();
			StartCoroutine (Level3Routine ());
		}
	}
}
