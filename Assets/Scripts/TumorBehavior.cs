using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TumorBehavior : MonoBehaviour {

	private bool lesionFound = false;

	void OnTriggerEnter(Collider col){
		if (!lesionFound && col.gameObject.name.Contains("Camera")) {
			lesionFound = true;
			//add listener to identify tumor button and activate
			GameObject identifyButton = TumorLocationBehavior.Instance.IdentifyButton;
			identifyButton.GetComponent<Button> ().onClick.AddListener (IdentifyTumor);
			identifyButton.SetActive (true);
		}
	}

	public void IdentifyTumor(){
		TumorLocationBehavior.Instance.IdentifyButton.SetActive (false);
		//remove "clone" from gameobject name since it was instantiated from a prefab.
		string tumorName = gameObject.name.Replace ("(Clone)", "");
		//call found tumor with correct tumor and passage name
		Level3Behavior.Instance.FoundTumor (tumorName,CollisionControl.Instance.currentPassage);
	}
}
