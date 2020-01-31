using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public delegate void GameEvent();
    public delegate void ScoreEvent(int points);

    public static GameEvent GameStart;
    public static void OnGameStart()
    {
        if (GameStart != null)
        {
            GameStart();
        }
    }

    public static GameEvent GameEnd;
    public static void OnGameEnd()
    {
        if (GameEnd != null)
        {
            GameEnd();
        }
    }

    public static ScoreEvent ScorePoints;
    public static void OnScorePoints(int i)
    {
        if (ScorePoints != null)
        {
            ScorePoints(i);
        }
    }

    // public static ScoreEvent ScorePoints;
    // public static void OnScorePoints(int i)
    // {
    //     if (ScorePoints != null)
    //     {
    //         ScorePoints(i);
    //     }
    // }
}
