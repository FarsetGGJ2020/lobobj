using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject SpawnItem;
    void Awake() 
    {
        GameEvents.GameStart += GameStart;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnDestroy() 
    {
        GameEvents.GameStart -= GameStart;
    }

    private void GameStart() 
    {
        Instantiate(SpawnItem, transform.position, Quaternion.identity);
    }
}
