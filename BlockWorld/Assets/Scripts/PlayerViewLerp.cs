﻿using UnityEngine;
using System.Collections;

public class PlayerViewLerp : MonoBehaviour {

	[SerializeField]
	GameObject target;

	[SerializeField]
	float lerpTime = 10f;

	[SerializeField]float xRotation;
	[SerializeField]float yRotation;
	
	[SerializeField]float currentxRotation;
	[SerializeField]float currentyRotation;

	float lookSensitivity = 4f;

	float xRotationV;
	float yRotationV;
	
	float lookSmoothDamp = 0.05f;

	[SerializeField]float targetRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		targetRotation = target.transform.rotation.eulerAngles.y;

		if (Input.GetMouseButton(1))
		{
			//Loops and rotates getting mouse positions
			xRotation -= (Input.GetAxisRaw ("Mouse Y") * lookSensitivity);
			yRotation += (Input.GetAxisRaw ("Mouse X") * lookSensitivity);
			
			//Restricts Y Axis Movemnt
			xRotation = Mathf.Clamp (xRotation, -80, 80);
			
			//Smooths Rotation
			currentxRotation = Mathf.SmoothDamp (currentxRotation, xRotation, ref xRotationV, lookSmoothDamp);
			currentyRotation = Mathf.SmoothDamp (currentyRotation, yRotation, ref yRotationV, lookSmoothDamp);
			
			//Outputs the values
			transform.rotation = Quaternion.Euler (currentxRotation, currentyRotation, 0);

			transform.position = Vector3.Lerp(transform.position, target.transform.position, lerpTime * Time.deltaTime);
		}
		else
		{
			//transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, lerpTime * Time.deltaTime);
			
			transform.position = Vector3.Lerp(transform.position, target.transform.position, lerpTime * Time.deltaTime);
		}

	
	}
}
