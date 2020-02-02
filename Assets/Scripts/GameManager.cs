using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private List<Area> areas;

	private int areaIndex = 0;
	public int AreaIndex => areaIndex;

	private Area currentArea;
	public Area CurrentArea => currentArea;

	void Awake()
	{
		GameEvents.GameEnd += OnGameEnd;
		Debug.Log("Wakey Wakey");
		SceneManager.LoadScene(2, LoadSceneMode.Additive);
		foreach (Area area in areas)
		{
			area.OnAreaClear.AddListener(OnAreaClear);
		}
		SetArea();
	}

	private void OnAreaClear()
	{
		areaIndex++;
		if (areaIndex >= areas.Count)
		{
			Debug.LogError("GAME OVER, ALL AREAS CLEARED");
			GameEvents.OnGameEnd();
			return;
		}
		SetArea();
	}

	void OnDestroy()
	{
		GameEvents.GameEnd -= OnGameEnd;
	}

	private void OnGameEnd()
	{
		SceneManager.LoadScene(4, LoadSceneMode.Single);
	}

	private void SetArea()
	{
		currentArea = areas[areaIndex];
		currentArea.Spawn();
	}
}
