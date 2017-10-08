using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour {

	[SerializeField]
	GameObject target;

	NavMeshAgent agent;

	[SerializeField]
	Animator anim;

	// Use this for initialization
	void Start () {

		agent = GetComponent<NavMeshAgent>();
		anim.SetInteger("Move", 1);
	
	}
	
	// Update is called once per frame
	void Update () {

		agent.SetDestination(target.transform.position);
	
	}
}
