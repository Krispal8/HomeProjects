using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour {

	[SerializeField]
	Animator anim;

	[SerializeField]
	GameObject raycastTrigger;

	[SerializeField]
	GameObject playerView;

	[SerializeField]
	GameObject carView;

	// Use this for initialization
	void Start () {

		carView.SetActive(false);
		playerView.SetActive(true);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.F))
		{
			anim.SetBool("Attack", true);
		}
		else
		{
			anim.SetBool("Attack", false);
		}

		if (Input.GetKey(KeyCode.E))
		{
			RaycastHit hit;
			if (Physics.Raycast(raycastTrigger.transform.position, raycastTrigger.transform.forward, out hit, 4f))
			{
				if (hit.collider.transform.root.tag == "Car")
				{
					Transform car = hit.collider.transform.root;

					car.GetComponent<NavMeshAgent>().enabled = false;
					car.GetComponent<CarNav>().enabled = false;

					transform.GetComponent<PlayerMovement>().enabled = false;
					car.GetComponent<CarMovement>().enabled = true;

					carView.GetComponent<CarViewLerp>().target = car.gameObject;

					carView.SetActive(true);
					playerView.SetActive(false);

					car.GetComponent<CarAction>().player = transform.root.gameObject;

					transform.SetParent(car.transform);
					gameObject.SetActive(false);
				}
			}
		}
	
	}

	public void EnableMovement()
	{
		transform.GetComponent<PlayerMovement>().enabled = true;
		
		carView.SetActive(false);
		playerView.SetActive(true);
		
		gameObject.SetActive(true);
	}
}
