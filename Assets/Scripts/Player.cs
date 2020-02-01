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
	[SerializeField] private PlayerBullet bulletPrefab;
	[SerializeField] private float fireRate = 2F;
	[SerializeField] private LayerMask hooverCast;
	[SerializeField] private float hooverRange = 2F;
	[SerializeField] private Transform robotModel;
	public AnimationCurve bobbingCurve;
	[SerializeField] private AudioSource hooverAudio;

	private float fireCoolDown = 0F;

	private Vector3 RayOrigin => transform.position + Vector3.up;

	private void Awake()
	{
		health.value = startingHealth;
		GameEvents.PlayerDamage += OnDamage;
	}

	private void OnDestroy()
	{
		GameEvents.PlayerDamage -= OnDamage;
	}

	private void OnDamage(float damage)
	{
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

  		 robotModel.position = new Vector3(robotModel.position.x, bobbingCurve.Evaluate((Time.time % bobbingCurve.length))/6, robotModel.position.z);

	}

	private bool SecondaryFire()
	{
		if (Input.GetMouseButton(1))
		{
			hooverAudio.Play();
			Hoover();
			return true;
		}
		hooverAudio.Stop();
		return false;
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
			ghost.Die();
		}
	}
}

public enum DamageType
{
	Green,
	Purple
}
