using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private static LevelManager _instance;
	public static LevelManager Instance {
		get {
			return _instance;
		}
	}

	public Animation anim;

	[HideInInspector]
	public enum Level{level1,level2,level3,none};
	[HideInInspector]
	public Level currentLevel;

	void Awake(){
		_instance = this;
		currentLevel = Level.none;
	}
		
	public void StartLevel1(){
		currentLevel = Level.level1;
		anim.Play ();
		Level1Behavior.Instance.Init ();
	}

	public void StartLevel2(){
		currentLevel = Level.level2;
		anim.Play ();
		Level2Behavior.Instance.Init ();
	}

	public void StartLevel3(){


	}

	public void LevelComplete(){
		CameraControl.Instance.ResetCamera ();
		CameraControl.Instance.ToggleCameraControl ();
		transform.localScale = Vector3.one;

	}
}
