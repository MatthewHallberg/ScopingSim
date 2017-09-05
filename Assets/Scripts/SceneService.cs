using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour {

	public static SceneService instance;

	void Awake() {
		if(instance == null)  {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else if(instance != this) {
			Destroy(gameObject);
		}
	}

	public void LoadScene(int sceneIndex){
		StartCoroutine (LoadSceneRoutine (sceneIndex));
	}

	IEnumerator LoadSceneRoutine(int sceneIndex){
		BlackFadeBehavior.Instance.FadeToBlack ();
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (sceneIndex);
	}
}
