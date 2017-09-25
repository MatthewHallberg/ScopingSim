using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour {

	public GameObject bubblePopPrefab;

	void OnTriggerEnter(Collider col){

		GameObject bubblePop = Instantiate (bubblePopPrefab, transform.parent);
		bubblePop.transform.position = this.transform.position;
		bubblePop.GetComponent<BubblePopBehavior> ().Init (transform.localScale.y);
		Destroy (bubblePop, 2f);
		transform.localScale = Vector3.zero;
		Destroy (gameObject, 2f);
		print ("HERE!");
	}
}
