using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Transform[] targets;
	[SerializeField] private GameObject deathParticleEffect;
	[SerializeField] private GameObject stunParticle;
	[SerializeField] private GhostType ghostType;
	[SerializeField] private Transform particleSpawn;
	[SerializeField] private Transform meshTransform;
	[SerializeField] private GhostBullet bulletPrefab;
	private ParticleSystem currentActiveParticles;

	private float fireCoolDown = 0F;
	private int hitCount = 0;
	private float hitTimer;
	[SerializeField] private bool stunned = false;

	public float CapacitySize => ghostType.capacitySize;

	private void Awake()
	{
		SetTarget();
		SetSpeed();
		fireCoolDown = Random.Range(0F, ghostType.fireRate);
	}

	private void SetSpeed()
	{
		try
		{
			agent.speed = ghostType.speeds[hitCount];
		}
		catch (System.Exception e)
		{
			Debug.Log("WRONG INDEX: " + hitCount + "\n" + e);
		}
	}

	private void Update()
	{
		IncreaseTimers();
		Move();
		Fire();
	}

	private void IncreaseTimers()
	{
		fireCoolDown += Time.deltaTime;
		if (hitCount > 0)
		{
			hitTimer += Time.deltaTime;
		}
	}

	private void Move()
	{
		if (stunned)
		{
			if (!(hitTimer > ghostType.hitWindow))
			{
				return;
			}
			stunned = false;
			agent.isStopped = false;
			hitCount = 0;
			hitTimer = 0F;
			SetSpeed();
		}
		meshTransform.LookAt(Player.Instance.transform);
		if (Vector3.Distance(agent.destination, transform.position) < 0.5F)
		{
			SetTarget();
		}
	}

	public void Damage(float damage)
	{
		hitTimer = 0F;
		if (stunned)
		{
			return;
		}
		if (hitCount >= ghostType.speeds.Length)
		{
			GameObject stun = Instantiate(stunParticle, particleSpawn.transform.position, Quaternion.identity);
			stunned = true;
			agent.isStopped = true;
			return;
		}
		// rigidbody.AddForce(-transform.forward * 5, ForceMode.Impulse);
		hitCount++;
		SetSpeed();
	}

	private void Fire()
	{
		if (stunned)
		{
			return;
		}
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
	public float Die()
	{
		if (!stunned)
		{
			return 0F;
		}
		stunned = true;
		agent.isStopped = true;
		GameObject deathParticles = Instantiate(deathParticleEffect, meshTransform.transform.position, Quaternion.identity);
		meshTransform.gameObject.SetActive(false);
		Destroy(gameObject);
		return ghostType.capacitySize;
	}
}