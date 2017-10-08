using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//Movement Variables
	
	float lookSensitivity = 4f;

		
	float lookSmoothDamp = 0.05f;
	float lerpTime = 6f;

	float yRotation;
	float yRotationA;

	public float currentyRotation;
	public float currentyRotationA;
	
	float yRotationV;
	float yRotationVA;
	
	float currentForwardSpeed;
	float forwardSpeedV;
	
	float currentRightSpeed;
	float rightSpeedV;
	
	float smoothSpeed = 0.15f;
	float currentRotation;

	[SerializeField]
	float lookSpeed = 5;
	
	[SerializeField]
	float moveSpeed = 10;
	
	[SerializeField]
	float runSpeed = 15;
	
	[SerializeField]
	float gravity = 30f;
	
	public float jumpSpeed = 20;
	
	CharacterController controller;	
	Vector3 currentMovement;

	[SerializeField]
	Animator anim;

	[SerializeField]
	GameObject PlayerAnimator;

	bool gotInitialDirection = false;
	float direction = 0f;

	// Use this for initialization
	void Start () {

		controller = GetComponent<CharacterController> ();

		controller.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y + 10f, controller.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxisRaw("Vertical") > 0)
		{
			yRotationA = 0f;
		}
		if (Input.GetAxisRaw("Vertical") < 0)
		{
			yRotationA = 180f;
		}
		if (Input.GetAxisRaw("Horizontal") > 0)
		{
			yRotationA = 90f;
		}
		if (Input.GetAxisRaw("Horizontal") < 0)
		{
			yRotationA = -90f;
		}

		if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0)
		{
			yRotationA = 45f;
		}
		if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0)
		{
			yRotationA = -135f;
		}
		if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0)
		{
			yRotationA = -45f;
		}
		if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0)
		{
			yRotationA = 135f;
		}

		currentyRotationA = Mathf.SmoothDamp (currentyRotationA, yRotationA, ref yRotationVA, lookSmoothDamp);
		if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) PlayerAnimator.transform.localRotation = Quaternion.Lerp(PlayerAnimator.transform.localRotation, Quaternion.Euler(0f, yRotationA, 0f), lerpTime * Time.deltaTime);



		if (Input.GetMouseButton(1))
		{
			yRotation += (Input.GetAxisRaw ("Mouse X") * lookSensitivity);
			currentyRotation = Mathf.SmoothDamp (currentyRotation, yRotation, ref yRotationV, lookSmoothDamp);

			direction = PlayerAnimator.transform.rotation.eulerAngles.y;
			transform.rotation = Quaternion.Euler (0, currentyRotation, 0);
			PlayerAnimator.transform.rotation = Quaternion.Euler (0, direction, 0);
		}


		if (Input.GetButton ("Run"))
		{
			currentForwardSpeed = Mathf.SmoothDamp (currentForwardSpeed, Input.GetAxisRaw ("Vertical") * runSpeed, ref forwardSpeedV, smoothSpeed);			
			currentRightSpeed = Mathf.SmoothDamp (currentRightSpeed, Input.GetAxisRaw ("Horizontal") * runSpeed, ref rightSpeedV, smoothSpeed);
		}
		else
		{
			currentForwardSpeed = Mathf.SmoothDamp (currentForwardSpeed, Input.GetAxisRaw ("Vertical") * moveSpeed, ref forwardSpeedV, smoothSpeed);			
			currentRightSpeed = Mathf.SmoothDamp (currentRightSpeed, Input.GetAxisRaw ("Horizontal") * moveSpeed, ref rightSpeedV, smoothSpeed);
		}

		AnimatorStateInfo action = anim.GetCurrentAnimatorStateInfo(1);

		if (controller.isGrounded && Input.GetButton ("Jump") && action.IsName("NoAction")) {
			currentMovement.y = jumpSpeed;
			anim.SetBool("Jump", true);
		}
		else if (controller.isGrounded) anim.SetBool("Jump", false);



		if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && controller.isGrounded)
		{
			anim.SetInteger("Move", Input.GetButton ("Run") ? 2 : 1);
		}
		else
		{
			anim.SetInteger("Move", 0);
		}
		
		
		currentMovement = new Vector3 (currentRightSpeed, currentMovement.y, currentForwardSpeed);
		currentMovement -= new Vector3 (0, gravity * Time.deltaTime, 0);
		currentMovement = transform.rotation * currentMovement;
		controller.Move (currentMovement * Time.deltaTime);
		
		
		

		
	}

	public void ResetRotation()
	{
		yRotation += (Input.GetAxisRaw ("Mouse X") * lookSensitivity);
		currentyRotation = Mathf.SmoothDamp (currentyRotation, yRotation, ref yRotationV, lookSmoothDamp);
		
		direction = PlayerAnimator.transform.rotation.eulerAngles.y;
		transform.rotation = Quaternion.Euler (0, currentyRotation, 0);
		PlayerAnimator.transform.rotation = Quaternion.Euler (0, direction, 0);
	}
	
}
