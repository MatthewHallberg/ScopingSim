using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {

	public Text locationText;

	void OnTriggerStay(Collider col){

		locationText.text = "Location: " + col.gameObject.name;
	}
}
