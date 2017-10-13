using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungCollisionBehavior : MonoBehaviour {

	public ParticleSystem BubbleParticles;
	public AudioSource coughAudioSource;

	private Color startColor;
	private int hitCount;

	private Vector3 desiredScale;
	private Vector3 smallScale =  new Vector3 (10f, 20f, 20f);
	private Vector3 normalScale = new Vector3 (20f, 20f, 20f);
	private bool shouldAnimate = false;

	void OnCollisionEnter(Collision col){

		hitCount++;

		if (hitCount > CoughCount()) {
			Cough ();
			hitCount = 0;
		}
	}

	void Update(){

		if (shouldAnimate) {
			transform.parent.localScale = Vector3.Lerp (transform.parent.localScale, desiredScale, 4f * Time.deltaTime);
		}
	}

	void Cough(){
		shouldAnimate = true;
		desiredScale = smallScale;
		coughAudioSource.Play ();
		CameraControl.Instance.DisableCameraControl ();
		CameraControl.Instance.rb.AddRelativeForce (Vector3.forward * -20f);
		StartCoroutine (CoughRoutine ());
	}

	int CoughCount(){
		return UnityEngine.Random.Range (3, 5);
	}

	IEnumerator CoughRoutine(){
		yield return new WaitForSeconds (.5f);
		BubbleParticles.Play ();
		yield return new WaitForSeconds (1.5f);
		desiredScale = normalScale;
		CameraControl.Instance.EnableCameraControl ();
	}
}
