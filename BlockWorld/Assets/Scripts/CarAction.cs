using UnityEngine;
using System.Collections;

public class CarAction : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Q) && player != null)
		{
			player.GetComponent<PlayerAction>().EnableMovement();
			player.transform.parent = null;

			player.GetComponent<PlayerMovement>().ResetRotation();

			transform.GetComponent<CarMovement>().enabled = false;

			//transform.GetComponent<NavMeshAgent>().enabled = true;
			//transform.GetComponent<CarNav>().enabled = true;

			player = null;

		}
	
	}
}
