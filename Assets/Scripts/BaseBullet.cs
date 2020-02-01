using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseBullet : MonoBehaviour
{
	[SerializeField] protected BulletType bulletType;
	[SerializeField] protected AudioSource audioSource;

	protected float life = 0F;
	protected Vector3 startPosition;
	protected Vector3 target;
	protected Vector3 direction;
	protected int bounceCount = 0;

	protected Vector3 OffsetPosition => new Vector3(transform.position.x, bulletType.heightOffset, transform.position.z);
	protected Vector3 OffsetOriginalPosition => new Vector3(startPosition.x, bulletType.heightOffset, startPosition.z);

	protected virtual void Awake()
	{
		transform.position = new Vector3(transform.position.x, bulletType.heightOffset, transform.position.z);
		startPosition = transform.position;
		audioSource.Play();
	}

	protected void Update()
	{
		life += Time.deltaTime;

		if (life >= bulletType.lifeTime)
		{
			Destroy(gameObject);
		}
	}
}