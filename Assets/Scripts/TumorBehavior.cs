using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TumorBehavior : MonoBehaviour {

	/// <summary>
	/// This script handles Tumor collisions for Level 3, because level 3 
	/// is the only level where collisions are not based on a specific passage. 
	/// </summary>

	private bool lesionFound = false;
	private string tumorLocation;
	private bool foundLocation = false;

	void OnTriggerEnter(Collider col){
		//first find the collider that this tumor is inside to get its location
		if (!foundLocation && !col.name.Contains ("extruded")) {
			foundLocation = true;
			tumorLocation = col.name;
			//if camera colliders with tumors collider activate identify button
		} else if (!lesionFound && col.gameObject.name.Contains("Camera")) {
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
		Level3Behavior.Instance.FoundTumor (tumorName,tumorLocation);
	}
}
