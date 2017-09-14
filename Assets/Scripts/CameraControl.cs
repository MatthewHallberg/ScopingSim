using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls camera by moving a rigid body
/// on the camera so that it will be affected
/// by collisions on the walls of the lung model.
/// </summary>

public class CameraControl : MonoBehaviour {

	private static CameraControl _instance;
	public static CameraControl Instance {
		get {
			return _instance;
		}
	}

	public Rigidbody rb;

	private float mouseSensitivity = 100.0f;

	private Vector3 startPosition;
	private Quaternion startRotation;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis
	private bool controlsEnabled = false;

	void Awake(){

		_instance = this;

		startPosition = transform.position;
		startRotation = transform.rotation;

		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (controlsEnabled) {
			//handle forward backward movementand strafing
			float vertical = Input.GetAxis ("Vertical") * 2f;
			float horizontal = Input.GetAxis ("Horizontal") * 4f;

			if (vertical != 0 || horizontal != 0) {
				rb.AddRelativeForce (Vector3.forward * vertical);			
				rb.AddRelativeForce (Vector3.right * horizontal);
			} else {
				rb.velocity = Vector3.zero;
			}

			//handle mouse look rotation
			float mouseX = Input.GetAxis ("Mouse X");
			float mouseY = -Input.GetAxis ("Mouse Y");

			rotY += mouseX * mouseSensitivity * Time.deltaTime;
			rotX += mouseY * mouseSensitivity * Time.deltaTime;

			//rotX = Mathf.Clamp(rotX, -100, 100);
			//rotY = Mathf.Clamp(rotY, -100, 100);

			Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
			transform.rotation = localRotation;
		}
	}

	public void ResetCamera(){
		transform.position = startPosition;
		transform.rotation = startRotation;
	}

	public void ToggleCameraControl(){
		if (controlsEnabled) {
			controlsEnabled = false;
		} else {
			controlsEnabled = true;
		}
	}
}
