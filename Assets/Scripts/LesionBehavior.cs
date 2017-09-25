using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesionBehavior : MonoBehaviour {

	private bool lesionFound = false;

	void OnTriggerEnter(Collider col){
		if (!lesionFound && col.gameObject.name.Contains("Camera")) {
			lesionFound = true;
			Level3Behavior.Instance.FoundLesion ();
		}
	}
}
