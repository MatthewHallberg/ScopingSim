using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour {

	/// <summary>
	/// Singleton instance of the class allows us to start and stop time from anywhere.
	/// </summary>

	private static TimerBehavior _instance;
	public static TimerBehavior Instance {
		get {
			return _instance;
		}
	}

	private bool runTimer = false;
	private float timeSinceAppStart;
	public Text timerTextSeconds;

	void Awake(){
		_instance = this;
	}

	void FormatTime (float time){
		int intTime = (int)time;
		int minutes = intTime / 60;
		int seconds = intTime % 60;
		var fraction = time * 100;
		fraction = fraction % 100;

		if (minutes != 0) {
			string timeText = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
			timerTextSeconds.text = "Time: " + timeText;
		} else {
			string timeText = String.Format ("{0:00}:{1:00}", seconds, fraction);
			timerTextSeconds.text = "Time: " + timeText;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (runTimer) {
			float timeTaken = -timeSinceAppStart + Time.time;
			FormatTime (timeTaken);
		}
	}

	public void RunTime(){
		timeSinceAppStart = Time.time;
		runTimer = true;
	}

	public void StopTime(){
		runTimer = false;
	}
}
