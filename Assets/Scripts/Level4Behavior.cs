using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Behavior : MonoBehaviour {

	private static Level4Behavior _instance;
	public static Level4Behavior Instance {
		get {
			return _instance;
		}
	}

	[Header("Level 4")]
	public GameObject textLargeObject;
	public Text objectiveTextLarge, objectiveTextSmall;
	public GameObject AnswerCanvas;
	public Text chosenLocation, scoreText;
	private int numLesionsFound= 0;
	public Transform SubmitButton;
	private int score = 0;
	public GameObject Passagelabel;
	[Header("Blood Stuff")]
	public GameObject BloodParent;

	private string correctPassage;

	void Awake(){
		_instance = this;
	}

	public void Init(){
		StartCoroutine (Level4Routine ());
		score = 0;
		scoreText.text = "Score: " + score;
		Passagelabel.SetActive (false);
		//turn off all labels
		foreach (GameObject go in Level2Behavior.Instance.labelsList) {
			go.SetActive (false);
		}
	}

	IEnumerator Level4Routine(){
		BloodParent.SetActive (true);
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

	public void FoundLesion( string location){
		correctPassage = location;
		StartCoroutine (FoundLesionRoutine ());
	}

	IEnumerator FoundLesionRoutine(){
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
		numLesionsFound++;
	}

	public void LevelComplete(){
		StartCoroutine (LevelCompleteRoutine ());
	}

	IEnumerator LevelCompleteRoutine(){
		AnswerCanvas.SetActive (false);
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Level 4 Complete!!";
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

		string answerLocation = chosenLocation.text;

		bool gotLocation = false;

		if (answerLocation == correctPassage) {
			gotLocation = true;
		}

		if (gotLocation) {

			StartCoroutine(CorrectlyIdentifiedRoutine());

			print ("TUMOR LOCATION: " + answerLocation);

		} else {
			SubmitButton.GetChild (0).GetComponent<Text> ().text = "Try Again";
		}
	}

	IEnumerator CorrectlyIdentifiedRoutine(){

		AnswerCanvas.SetActive (false);

		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		//set objective text
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "That's Correct!";

		yield return new WaitForSeconds (3f);

		if (numLesionsFound > 2) {
			LevelComplete ();
		} else {
			CameraControl.Instance.ResetCamera ();
			CameraControl.Instance.ToggleCameraControl ();
			StartCoroutine (Level4Routine ());
		}
	}
}
