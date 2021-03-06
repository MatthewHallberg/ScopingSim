﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	/// <summary>
	/// This sets the current level enum and also handles resetting the camera 
	/// to the origin position in the esophagus and resets the
	/// heads up display when each level is complete. These functions are called when their respective
	/// level buttons are pressed on the main canvas. The onclick listeners were added to each button.
	/// </summary>

	private static LevelManager _instance;
	public static LevelManager Instance {
		get {
			return _instance;
		}
	}

	public Animation anim;

	[HideInInspector]
	public enum Level{level1,level2,level3,level4,none};
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
		currentLevel = Level.level3;
		anim.Play ();
		Level3Behavior.Instance.Init ();
	}

	public void StartLevel4(){
		currentLevel = Level.level4;
		anim.Play ();
		Level4Behavior.Instance.Init ();
	}

	public void LevelComplete(){
		CameraControl.Instance.ResetCamera ();
		CameraControl.Instance.ToggleCameraControl ();
		transform.localScale = Vector3.one;
		//turn on all labels
		foreach (GameObject go in Level2Behavior.Instance.labelsList) {
			go.SetActive (true);
		}
	}
}
