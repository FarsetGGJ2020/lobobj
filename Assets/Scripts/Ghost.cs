﻿using System.Collections;
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
	[SerializeField] private GhostBullet bulletPrefab;
	private ParticleSystem currentActiveParticles;

	private float fireCoolDown = 0F;

	private void Awake()
	{
		SetTarget();
		agent.speed = ghostType.speed;
		fireCoolDown = Random.Range(0F, ghostType.fireRate);
	}

	private void Update()
	{
		fireCoolDown += Time.deltaTime;
		meshTransform.LookAt(Player.Instance.transform);
		if (Vector3.Distance(agent.destination, transform.position) < 0.5F)
		{
			SetTarget();
		}
		Fire();
	}

	public void Damage(float damage)
	{

	}

	private void Fire()
	{
		if (fireCoolDown >= ghostType.fireRate)
		{
			fireCoolDown = 0F;
			GhostBullet bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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
