using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungCollisionBehavior : MonoBehaviour {

	public Image BloodImage;
	private Color startColor;
	private int hitCount;

	void Awake(){

		startColor = BloodImage.color;
		Color tempColor = startColor;
		tempColor.a = 0;
		BloodImage.color = tempColor;
	}

	void OnCollisionEnter(Collision col){

		BloodFlash ();
		hitCount++;

		if (hitCount > CoughCount()) {
			Cough ();
			hitCount = 0;
		}
	}

	void Cough(){
		BloodImage.GetComponent<AudioSource> ().Play ();
		CameraControl.Instance.DisableCameraControl ();
		CameraControl.Instance.rb.AddRelativeForce (Vector3.forward * -20f);
		StartCoroutine (CoughRoutine ());
	}

	int CoughCount(){
		return UnityEngine.Random.Range (3, 5);
	}

	IEnumerator CoughRoutine(){

		yield return new WaitForSeconds (.5f);
		CameraControl.Instance.EnableCameraControl ();
	}

	void BloodFlash(){

		Color tempColor = startColor;
		tempColor.a = 1;
		BloodImage.color = tempColor;
	}

	void Update(){

		Color tempColor = startColor;
		tempColor.a = 0;
		BloodImage.color = Color.Lerp (BloodImage.color, tempColor, Time.deltaTime * 1f);
	}
}
