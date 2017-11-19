using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionControl : MonoBehaviour {

	/// <summary>
	/// This is housed on the main camera and handles detecting collisions with desired 
	/// passages on all the different levels. This is also where the camera's current
	/// location is set in the heads up display, the current passage can also be accessed 
	/// from this script if you need to know the current location of the camera 
	/// (or last known location) at ny point. 
	/// </summary>

	private static CollisionControl _instance;
	public static CollisionControl Instance {
		get {
			return _instance;
		}
	}

	[HideInInspector]
	public string currentPassage;

	public Text locationText;
	private Collider previousCollider;

	void Awake(){
		_instance = this;
	}

	void OnTriggerStay(Collider col){

		if (previousCollider != col) {
			previousCollider = col;

			if (!col.gameObject.name.Contains ("Bubble") && col.tag != "Tumor") {
				//update location text
				locationText.text = "Location: " + col.gameObject.name;
				currentPassage = col.gameObject.name;
			}

			//if level 1
			if (LevelManager.Instance.currentLevel == LevelManager.Level.level1) {
				if (col.gameObject.name == Level1Behavior.Instance.desiredPassage) {
					Level1Behavior.Instance.FoundPassage ();
				}
			//if level 2
			} else if (LevelManager.Instance.currentLevel == LevelManager.Level.level2) {
				if (col.gameObject.name == Level2Behavior.Instance.desiredPassage) {
					Level2Behavior.Instance.FoundPassage ();
				}
			//if level 4
			} else if (LevelManager.Instance.currentLevel == LevelManager.Level.level4) {
				if (col.gameObject.name == Level4Behavior.Instance.desiredPassage) {
					Level4Behavior.Instance.FoundPassage ();
				}
			}
		}
	}
}
