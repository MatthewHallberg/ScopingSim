using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationOptions : MonoBehaviour {

	void Start(){
		LoadLocationOptions ();
	}

	//loads all of the possible choices into the dropdown menu for locations of tumors, lacerations, etc.
	void LoadLocationOptions(){

		List<string> options = new List<string> ();
		foreach (Transform child in Level1Behavior.Instance.possiblePassages) {
			options.Add (child.name);
		}
		Dropdown dropDown = GetComponent<Dropdown> ();
		dropDown.AddOptions (options);
	}
}
