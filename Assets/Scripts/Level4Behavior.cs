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
	public Transform SubmitButton;
	private int score = 0;
	public GameObject Passagelabel;
	[HideInInspector]
	public string desiredPassage;

	[Header("Blood Stuff")]
	public GameObject BloodParent;

	[System.Serializable]
	public class BloodFlow
	{
		public List<GameObject> bloodParticles = new List <GameObject> ();
	}

	/*List of a list of blood particles, each element is one flow made up of multiple particle systems
	 * the last gameobject in the inner list contains the name of the correct passage
	 * that the blood flow originated from.
	 */
	public List<BloodFlow> bloodStreams = new List <BloodFlow> ();

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
		//turn off all blood particles in case they are left on
		foreach(Transform child in BloodParent.transform){
			child.gameObject.SetActive (false);
		}

		//turn on all gameobjects in desired blood stream
		foreach (GameObject childGo in bloodStreams[0].bloodParticles) {
			childGo.SetActive (true);
		}
		BloodParent.GetComponent<BloodFlowBehavior> ().EnableBlood ();
		yield return new WaitForSeconds (.1f);

		//set desired passage from last gameobject in list
		desiredPassage = bloodStreams[0].bloodParticles[bloodStreams[0].bloodParticles.Count-1].name;
		correctPassage = desiredPassage;

		//remove this blood stream from list now that its been used
		bloodStreams.RemoveAt (0);

		TimerBehavior.Instance.RunTime ();
		//set defaults
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "";
		//set objective text
		textLargeObject.SetActive(true);
		objectiveTextLarge.text = "Find The \nBleed Source!";
		yield return new WaitForSeconds (3f);
		textLargeObject.SetActive(false);
		objectiveTextLarge.text = "";
		objectiveTextSmall.text = "Find The Bleed Source!";
	}

	public void FoundPassage(){
		TimerBehavior.Instance.StopTime ();
		desiredPassage = "temp";
		StartCoroutine (FoundBloodSourceRoutine ());
	}

	IEnumerator FoundBloodSourceRoutine(){
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
	}

	public void LevelComplete(){
		StartCoroutine (LevelCompleteRoutine ());
	}

	IEnumerator LevelCompleteRoutine(){
		BloodParent.SetActive (false);
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

		if (bloodStreams.Count < 1) {
			LevelComplete ();
		} else {
			CameraControl.Instance.ResetCamera ();
			CameraControl.Instance.ToggleCameraControl ();
			StartCoroutine (Level4Routine ());
		}
	}
}
