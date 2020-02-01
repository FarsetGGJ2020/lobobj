using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectiles/Bullet Type", fileName = "New BulletType")]
public class BulletType : ScriptableObject
{
	public float velocityScalor;
	public float lifeTime;
	public float damage;
	public int bounces = 1;
}
