using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public int playerHealth;
    public string playerName;
    [SerializeField] private ParticleSystem movementParticles;
    public DamageType damageType;
    public LayerMask enemyLayers;
    public float range;

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
         if (Input.GetMouseButtonDown(0))
         {
             Shoot();
         }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, range,enemyLayers))
        {
            Ghost enemy = hit.collider.GetComponent<Ghost>();
            enemy.Die();
        }
    }
}

public enum DamageType 
{
    Green,
    Purple
}
