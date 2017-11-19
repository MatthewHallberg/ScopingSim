using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationOptions : MonoBehaviour {

	/// <summary>
	/// This loads all of the possible choices into the dropdown menu for locations of tumors, lacerations, etc.
	/// This was easier than adding them manually and as long as the names of gameobjects are correct, helps to 
	/// prevent errors in transcription. This script should be placed on an object with a DropDown component.
	/// </summary>

	void Start(){
		LoadLocationOptions ();
	}

	void LoadLocationOptions(){
		List<string> options = new List<string> ();
		foreach (Transform child in Level1Behavior.Instance.possiblePassages) {
			options.Add (child.name);
		}
		Dropdown dropDown = GetComponent<Dropdown> ();
		dropDown.AddOptions (options);
	}
}
