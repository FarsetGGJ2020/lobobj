using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : BaseBullet
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private GameObject destroyedParticle;

	protected override void Awake()
	{
		base.Awake();
		target = Vector3.Scale(Player.Instance.transform.position, new Vector3(1, 0, 1)) + bulletType.heightOffset * Vector3.up;
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

	public void Fizzle(Vector3 pos)
	{
		GameObject particle = Instantiate(destroyedParticle, pos, Quaternion.identity);
	}
	private void OnDrawGizmos()
	{
		// Gizmos.color = Color.blue;
		// Gizmos.DrawSphere(target, 0.5F);
		// Gizmos.DrawLine(startPosition, startPosition + 20F * direction);
	}
}
