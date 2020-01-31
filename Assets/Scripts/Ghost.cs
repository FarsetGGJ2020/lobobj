using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private GameObject deathParticleEffect;
    [SerializeField] private GameObject mainGhost; 
    private ParticleSystem currentActiveParticles;

    [ContextMenu("Die")]
    public void Die()
    {
        Destroy(mainGhost);
        GameObject deathParticles = Instantiate(deathParticleEffect, mainGhost.transform.position,Quaternion.identity);
    }
}
