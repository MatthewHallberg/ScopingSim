using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFlowBehavior : MonoBehaviour {

	/// <summary>
	/// This gets called every time blood particles are activated because
	/// the flow needs to be simulated at a high speed for single frame
	/// in order to cover its desired distance. We do this becaue we want the flows to 
	/// take up space but also move slowly. 
	/// </summary>

	public void EnableBlood(){
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
