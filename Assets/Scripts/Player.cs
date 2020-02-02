using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
	[SerializeField] private ScriptableFloat health;
	[SerializeField] private float startingHealth;
	public string playerName;
	[SerializeField] private ParticleSystem movementParticles;
	public DamageType damageType;
	public LayerMask enemyLayers;
	public float range;
	public float damageWidth;
	[SerializeField] private Transform particleSpawnPoint;
	[SerializeField] private ParticleSystem hooverParticle;
	[SerializeField] private ParticleSystem damageParticle;
	[SerializeField] private PlayerBullet bulletPrefab;
	[SerializeField] private float fireRate = 2F;
	[SerializeField] private LayerMask hooverCast;
	[SerializeField] private float hooverRange = 2F;
	[SerializeField] private Transform robotModel;
	[SerializeField] private LayerMask emptyCast;
	[SerializeField] private float emptyRange = 3F;
	[SerializeField] private Rigidbody rb;

	public AnimationCurve bobbingCurve;
	[SerializeField] private AudioSource hooverAudio;
	private ParticleSystem spawnedWhirlwind, spawnedDmg;
	private float fireCoolDown = 0F;

	private Vector3 RayOrigin => transform.position + Vector3.up;

	[SerializeField] private ScriptableFloat capacity;
	[SerializeField] private AudioSource hooverEmptySound;

	private void Awake()
	{
		health.value = startingHealth;
		capacity.value = 0F;
		GameEvents.PlayerDamage += OnDamage;
	}

	private void OnDestroy()
	{
		GameEvents.PlayerDamage -= OnDamage;
	}

	private void OnDamage(float damage)
	{
		spawnedDmg = Instantiate(damageParticle, particleSpawnPoint.position, Quaternion.identity, particleSpawnPoint.transform);
		rb.AddForce(transform.forward * -1 * 5f);
		health.value -= damage;
		if (health <= 0)
		{
			GameEvents.OnGameEnd();
		}
	}

	public void StartMoving()
	{
		movementParticles.Play();
	}

	public void StopMoving()
	{
		movementParticles.Stop();
	}

	public void Update()
	{
		IncreaseTimers();
		if (!SecondaryFire())
		{
			PrimaryFire();
			EmptyHoover();
		}
	}

	private void IncreaseTimers()
	{
		fireCoolDown += Time.deltaTime;
	}

	private void PrimaryFire()
	{
		if (Input.GetMouseButton(0) && fireCoolDown >= fireRate)
		{
			Shoot();
		}
		robotModel.position = new Vector3(robotModel.position.x, bobbingCurve.Evaluate((Time.time % bobbingCurve.length)) / 6, robotModel.position.z);

	}

	private bool SecondaryFire()
	{
		if (capacity >= 100F)
		{
			hooverAudio.Stop();
			if (Input.GetMouseButtonDown(1))
			{
				hooverEmptySound.Play();
				return true;
			}
			return false;
		}
		if (Input.GetMouseButton(1))
		{
			spawnedWhirlwind = GameObject.Instantiate(hooverParticle, particleSpawnPoint.position, Quaternion.identity, particleSpawnPoint.transform);
			hooverAudio.Play();
			Hoover();
			return true;
		}
		hooverAudio.Stop();
		return false;
	}

	private void EmptyHoover()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Vector3 direction = Vector3.Normalize(WorldMouse.GetWorldMouse() - transform.position);
			Debug.DrawLine(RayOrigin, RayOrigin + hooverRange * direction, Color.red, 0.5F);
			if (Physics.Raycast(RayOrigin, direction, out RaycastHit hit, emptyRange, emptyCast))
			{
				capacity.value = 0F;
			}
		}
	}

	private void Shoot()
	{
		fireCoolDown = 0;
		PlayerBullet bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
	}

	private void Hoover()
	{
		Vector3 direction = Vector3.Normalize(WorldMouse.GetWorldMouse() - transform.position);
		Debug.DrawLine(RayOrigin, RayOrigin + hooverRange * direction, Color.red, 0.5F);
		if (Physics.Raycast(RayOrigin, direction, out RaycastHit hit, hooverRange, hooverCast))
		{
			Ghost ghost = hit.collider.GetComponent<Ghost>();
			if (capacity <= 100F)
			{
				capacity.value += ghost.Die();
			}
		}
	}

	private void OnDrawGizmos()
	{
		// Gizmos.color = Color.blue;
		// Gizmos.DrawSphere(WorldMouse.Position, 0.5F);
		// Gizmos.DrawLine(WorldMouse.StartPositon, WorldMouse.Position);
		// Gizmos.color = Color.red;
		// Gizmos.DrawLine(transform.position, WorldMouse.Position);
	}
}

public enum DamageType
{
	Green,
	Purple
}
