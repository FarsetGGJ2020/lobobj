using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    void Awake()
    {
        GameEvents.GameEnd += OnGameEnd;
        Debug.Log("Wakey Wakey");
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    void OnDestroy() 
    {
        GameEvents.GameEnd -= OnGameEnd;
    }

    private void OnGameEnd()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
