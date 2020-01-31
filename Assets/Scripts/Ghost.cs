using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Transform[] targets;

	private void Awake()
	{
		SetTarget();
	}

	private void Update()
	{
		if (Vector3.Distance(agent.destination, transform.position) < 0.5F)
		{
			SetTarget();
		}
	}

	private void SetTarget()
	{
		int index = Mathf.RoundToInt(Random.Range(0, targets.Length));
		Transform target = targets[index];
		agent.destination = target.position;
	}
}
