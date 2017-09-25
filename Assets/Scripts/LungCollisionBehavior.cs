using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungCollisionBehavior : MonoBehaviour {

	Quaternion enterRotation;

//	void OnCollisionEnter(Collision col){
////		CameraControl.Instance.DisableCameraControl ();
////		enterRotation = col.gameObject.GetComponent<Rigidbody> ().rotation;
////		col.gameObject.GetComponent<Rigidbody> ().rotation = enterRotation;
//		Vector3 dir = col.contacts[0].point - transform.position;
//		dir = -dir.normalized;
//		col.gameObject.GetComponent<Rigidbody>().AddForce(dir*10);
//	}
//
//	void OnCollisionStay(Collision col){
//
//		Vector3 dir = col.contacts[0].point - transform.position;
//		dir = -dir.normalized;
//		col.gameObject.GetComponent<Rigidbody>().AddForce(dir*10);
//	}
}
