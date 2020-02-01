﻿using System.Collections;
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
	[SerializeField] private Transform robotModel;
	public AnimationCurve bobbingCurve;

	private float fireCoolDown = 0F;

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
		fireCoolDown += Time.deltaTime;
		if (Input.GetMouseButton(0) && fireCoolDown >= fireRate)
		{
			Shoot();
		}

  		 robotModel.position = new Vector3(robotModel.position.x, bobbingCurve.Evaluate((Time.time % bobbingCurve.length))/6, robotModel.position.z);

	}

	public void Shoot()
	{
		fireCoolDown = 0;
		PlayerBullet bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
	}
}

public enum DamageType
{
	Green,
	Purple
}
