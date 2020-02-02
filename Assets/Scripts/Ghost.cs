using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Ghost : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Transform[] targets;
	[SerializeField] private GameObject deathParticleEffect;
	[SerializeField] private ParticleSystem stunParticle;
	[SerializeField] private GameObject hitParticle;
	[SerializeField] private GhostType ghostType;
	[SerializeField] private Transform particleSpawn;
	[SerializeField] private Transform meshTransform;
	[SerializeField] private GhostBullet bulletPrefab;

	[SerializeField] private UnityEvent onDeath;
	public UnityEvent OnDeath => onDeath;

	private GameObject currentActiveParticles;

	private float fireCoolDown = 0F;
	private int hitCount = 0;
	private float hitTimer;
	[SerializeField] private bool stunned = false;

	public float CapacitySize => ghostType.capacitySize;

	private void Awake()
	{
		fireCoolDown = Random.Range(0F, ghostType.fireRate);
	}

	public void SetTargets(Transform[] sentTargets)
	{
		targets = sentTargets;
	}

	private void Start()
	{
		SetTarget();
		SetSpeed();
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
			Destroy(currentActiveParticles);
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
			ParticleSystem vortex = Instantiate(stunParticle, particleSpawn.transform.position, Quaternion.identity, particleSpawn);
			currentActiveParticles = vortex.transform.gameObject;
			stunned = true;
			agent.isStopped = true;
			return;
		}
		GameObject hit = Instantiate(hitParticle, particleSpawn.transform.position, Quaternion.identity, particleSpawn);
			
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
		onDeath.Invoke();
		Destroy(gameObject);
		return ghostType.capacitySize;
	}
}