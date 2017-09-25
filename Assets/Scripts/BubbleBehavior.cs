using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour {

	private bool shouldMove = false;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name.Contains ("Camera")) {
			shouldMove = true;
			Destroy (gameObject, 2f);
		}
	}

	void Update(){

		if (shouldMove) {
			//scale to nothing
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			//move toward camera so it doesnt look like its moving away
			transform.position = Vector3.Lerp (transform.position, Camera.main.transform.position, Time.deltaTime * 10f);
		}
	}
}
