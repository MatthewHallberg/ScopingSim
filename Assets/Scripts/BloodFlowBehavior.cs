using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFlowBehavior : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		StartCoroutine (BloodFlowRoutine ());
	}

	IEnumerator BloodFlowRoutine(){

		//first turn on blood
		foreach (Transform child in this.transform) {
			ParticleSystem ps = child.GetComponent<ParticleSystem> ();
			var main = ps.main;
			main.simulationSpeed = 1000f;
		}

		yield return new WaitForEndOfFrame ();

		//now slow simulation speed
		foreach (Transform child in this.transform) {
			ParticleSystem ps = child.GetComponent<ParticleSystem> ();
			var main = ps.main;
			main.simulationSpeed = .1f;
		}
	}
}
