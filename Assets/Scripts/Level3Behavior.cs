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
	public GameObject AnswerCanvas;
	public Text choice1, choice2;

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
		objectiveTextLarge.text = "Find The Problem!";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "Find The Problem.";
	}

	public void FoundLesion(){
		StartCoroutine (FoundPassageRoutine ());
	}

	IEnumerator FoundPassageRoutine(){

		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Great job! \n You found it!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (2f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		AnswerCanvas.SetActive (true);
		CameraControl.Instance.DisableCameraControl ();
		//LevelComplete ();
	}

	public void LevelComplete(){
		StartCoroutine (LevelCompleteRoutine ());
	}

	IEnumerator LevelCompleteRoutine(){
		AnswerCanvas.SetActive (false);
		BubblesParent.gameObject.SetActive (false);
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Level 3 Complete!!";
		objectiveTextSmall.text = "";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		CameraControl.Instance.EnableCameraControl ();
		LevelManager.Instance.LevelComplete ();
	}

	public void SubmitChoice(){
		
		string answerType = choice1.text;
		string answerLocation = choice2.text;

		print (answerType);
		print (answerLocation);

		//HACK: not validating answer, level complete for now.
		LevelComplete();
	}
}
