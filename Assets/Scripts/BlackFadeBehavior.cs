using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFadeBehavior : MonoBehaviour {

	private static BlackFadeBehavior _instance;

	public static BlackFadeBehavior Instance {
		get {
			return _instance;
		}
	}

	private CanvasGroup cv;
	private float desiredAlpha;
	private bool shouldFade = false;

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		cv = GetComponent<CanvasGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldFade) {
			cv.alpha = Mathf.Lerp (cv.alpha, desiredAlpha, Time.deltaTime * 6f);
		}
	}

	public void FadeToBlack(){
		StartCoroutine (Fade ());
	}

	IEnumerator Fade(){
		desiredAlpha = 1f;
		shouldFade = true;
		yield return new WaitForSeconds (1f);
		desiredAlpha = 0f;
		yield return new WaitForSeconds (1f);
		shouldFade = false;
	}
}
