using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BaseBullet
{
	[SerializeField] private Rigidbody rb;

	protected override void Awake()
	{
		base.Awake();
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Plane plane = new Plane(Vector3.zero, Vector3.forward, Vector3.right);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		plane.Raycast(ray, out float distance);
		target = target + ray.direction.normalized * distance;
		target = Vector3.Scale(target, new Vector3(1, 0, 1)) + bulletType.heightOffset * Vector3.up;
		direction = Vector3.Normalize(target - transform.position);
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
		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		// Gizmos.color = Color.blue;
		// Gizmos.DrawSphere(target, 0.5F);
		// Gizmos.DrawLine(startPosition, startPosition + 20F * direction);
	}
}
