﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionControl : MonoBehaviour {

	public Text locationText;
	private Collider previousCollider;

	void OnTriggerStay(Collider col){

		if (previousCollider != col) {
			previousCollider = col;
			//update location text
			locationText.text = "Location: " + col.gameObject.name;

			//if level 1
			if (LevelManager.Instance.currentLevel == LevelManager.Level.level1) {
				if (col.gameObject.name == Level1Behavior.Instance.desiredPassage) {
					Level1Behavior.Instance.FoundPassage ();
				}
			}
		}
	}
}
