using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Ghost : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Transform[] targets;
	[SerializeField] private GameObject deathParticleEffect;
	[SerializeField] private GameObject mainGhost;
	[SerializeField] private GhostType ghostType;
	[SerializeField] private Transform meshTransform;
	private ParticleSystem currentActiveParticles;

	private void Awake()
	{
		SetTarget();
		agent.speed = ghostType.speed; ;
	}

	private void Update()
	{
		meshTransform.LookAt(Player.Instance.transform);
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
	[ContextMenu("Die")]
	public void Die()
	{
		agent.isStopped = true;
		Destroy(mainGhost);
		GameObject deathParticles = Instantiate(deathParticleEffect, mainGhost.transform.position, Quaternion.identity);

	}
}


[CreateAssetMenu(menuName = "Enemies/Ghost Type")]
public class GhostType : ScriptableObject
{
	public float speed;
	public float strength;
	public DamageType weakness;
}
