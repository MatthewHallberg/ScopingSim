using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePopBehavior : MonoBehaviour {

	public void Init(float scale){
		transform.localScale = new Vector3 (scale / 10, scale / 10, scale / 10);
		transform.LookAt (Camera.main.transform);
	}

	void Update(){
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, Time.deltaTime * 6f);
	}
}
