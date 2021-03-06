﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BaseBullet
{
	[SerializeField] private Rigidbody rb;

	protected override void Awake()
	{
		base.Awake();
		target = WorldMouse.GetWorldMouse();
		transform.LookAt(target);
		target.y = bulletType.heightOffset;
		direction = Vector3.Normalize(target - OffsetPosition);
		rb.velocity = bulletType.velocityScalor * direction;
		rb.WakeUp();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 9)
		{
			Ghost ghost = collision.gameObject.GetComponent<Ghost>();
			if (ghost)
			{
				ghost.Damage(bulletType.damage);
			}
		}
		// Destroy bullets
		if(collision.gameObject.layer == 15)
		{
			GhostBullet bullet = collision.gameObject.GetComponent<GhostBullet>();
			bullet.Fizzle(collision.contacts[0].point);
		}
		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(OffsetOriginalPosition, OffsetOriginalPosition + 10 * direction);
	}
}
