using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float heightOffset;
	[SerializeField] private BulletType bulletType;

	private float life = 0F;
	private Vector3 startPosition;
	private Vector3 target;
	private Vector3 direction;
	private int bounceCount = 0;

	private void Awake()
	{
		transform.position = new Vector3(transform.position.x, heightOffset, transform.position.z);
		startPosition = transform.position;
		target = Vector3.Scale(Player.Instance.transform.position, new Vector3(1, 0, 1)) + heightOffset * Vector3.up;
		direction = Vector3.Normalize(target - transform.position);
		rb.velocity = bulletType.velocityScalor * direction;
		rb.WakeUp();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 14)
		{
			GameEvents.PlayerDamage(bulletType.damage);
		}
		Destroy(gameObject);
	}

	private void Update()
	{
		life += Time.deltaTime;

		if (life >= bulletType.lifeTime)
		{
			Destroy(gameObject);
		}
	}

	private void OnDrawGizmos()
	{
		// Gizmos.color = Color.blue;
		// Gizmos.DrawSphere(target, 0.5F);
		// Gizmos.DrawLine(startPosition, startPosition + 20F * direction);
	}
}
