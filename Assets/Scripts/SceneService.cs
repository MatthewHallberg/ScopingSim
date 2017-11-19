using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour {

	/// <summary>
	/// This handles switching of scenes, this instance persists
	/// between scene loads so it can be called at any time and 
	/// does not die. 
	/// </summary>

	public static SceneService instance;

	//dont destroy this instance 
	void Awake() {
		if(instance == null)  {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else if(instance != this) {
			Destroy(gameObject);
		}
	}

	//loads scene and starts coroutine to fade to black
	public void LoadScene(int sceneIndex){
		StartCoroutine (LoadSceneRoutine (sceneIndex));
	}

	IEnumerator LoadSceneRoutine(int sceneIndex){
		BlackFadeBehavior.Instance.FadeToBlack ();
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (sceneIndex);
	}
}
