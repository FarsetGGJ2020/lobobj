using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public int playerHealth;
    public string playerName;
    [SerializeField] private ParticleSystem movementParticles;

    public void StartMoving()
    {
        movementParticles.Play();   
    }

    public void StopMoving()
    {
        movementParticles.Stop();   
    }
}
