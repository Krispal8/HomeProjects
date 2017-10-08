using UnityEngine;
using System.Collections;

public class CarNav : MonoBehaviour {

	[SerializeField]
	Transform[] targets;
	
	NavMeshAgent agent;

	[SerializeField]
	int offset;

	Transform pos;

	int count = 0;

	// Use this for initialization
	void Start () {

		agent = GetComponent<NavMeshAgent>();

		count += offset;
		pos = targets[count];
	
	}
	
	// Update is called once per frame
	void Update () {

		if (pos == null)
		{
			pos = targets[count];
		}
		else
		{
			agent.SetDestination(pos.position);
		}

		for (int i = 0; i < targets.Length; i++)
		{
			if (Vector3.Distance(transform.position, pos.position) < 1f)
			{
				count++;
				count = count >= targets.Length ? 0 : count;  
				pos = null;
				break;
			}
		}

	
	}
}
