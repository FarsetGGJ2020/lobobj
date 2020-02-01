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
    public float damageWidth;

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
        Vector3 endpoint = transform.position + transform.forward*range;
        Debug.DrawLine(transform.position, endpoint, Color.white, 1000f);
        RaycastHit hit;
        if(Physics.CapsuleCast(transform.position,endpoint,damageWidth,transform.forward, out hit,5,enemyLayers))
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
