using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	Rigidbody rigidBody;

	[SerializeField] float acceleration = 0.05f;
	[SerializeField] float decelerationMultiplier = 10f;

	[SerializeField] float minMoveSpeed = 60f;
	[SerializeField] float maxMoveSpeed = 120f;
	[SerializeField] float currentMoveSpeed = 0f;

	[SerializeField] float maxTurnSensitivity = 1.5f;
	[SerializeField] float currentTurnSensitivity = 0f;
	
	[SerializeField]float yRotation;
	[SerializeField]float currentyRotation;
	float yRotationV;
	
	float lookSmoothDamp = 0.05f;
	float smoothSpeed = 0.45f;

	float currentForwardSpeed;
	float forwardSpeedV;
	Vector3 currentMovement;

	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Mathf.Abs(currentForwardSpeed) > 1f)
		{
			yRotation += (Input.GetAxisRaw ("Horizontal") * maxTurnSensitivity) * (Input.GetAxisRaw ("Vertical") < 0f ? -1f : 1f);
			currentyRotation = Mathf.SmoothDamp (currentyRotation, yRotation, ref yRotationV, lookSmoothDamp);
			transform.rotation = Quaternion.Euler (0, currentyRotation, 0);
		}

		if (Input.GetAxisRaw ("Vertical") != 0) currentMoveSpeed = currentMoveSpeed + acceleration * Time.deltaTime > maxMoveSpeed ? maxMoveSpeed : currentMoveSpeed + acceleration * Time.deltaTime;
		else currentMoveSpeed = currentMoveSpeed - acceleration * Time.deltaTime * decelerationMultiplier < minMoveSpeed ? minMoveSpeed : currentMoveSpeed - acceleration * Time.deltaTime * decelerationMultiplier;


		currentForwardSpeed = Mathf.SmoothDamp (currentForwardSpeed, Input.GetAxisRaw ("Vertical") * currentMoveSpeed, ref forwardSpeedV, smoothSpeed);
		currentMovement = new Vector3 (currentMovement.x, currentMovement.y, currentForwardSpeed);
		rigidBody.transform.Translate(currentMovement * Time.deltaTime);
	
	}

}
