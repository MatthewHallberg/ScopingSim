using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Rigidbody rb;

	private float mouseSensitivity = 100.0f;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis

	// Use this for initialization
	void Start () {
		
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}
	
	// Update is called once per frame
	void Update () {

		//handle forward backward movementand strafing
		float vertical = Input.GetAxis("Vertical") * 2f;
		float horizontal = Input.GetAxis("Horizontal") * 4f;

		if (vertical != 0 || horizontal != 0) {
			rb.AddRelativeForce (Vector3.forward * vertical);
			rb.AddRelativeForce (Vector3.right * horizontal);

		} else {
			rb.velocity = Vector3.zero;
		}

		//handle mouse look rotation
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");

		rotY += mouseX * mouseSensitivity * Time.deltaTime;
		rotX += mouseY * mouseSensitivity * Time.deltaTime;

		//clamp rotation at 30 and 160 so we cant turn all the way around
		rotX = Mathf.Clamp(rotX, 10, 180);

		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		transform.rotation = localRotation;
	}
}
