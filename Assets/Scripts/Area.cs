using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Area : MonoBehaviour
{
	[SerializeField] private string areaName;
	public string AreaName => areaName;

	[SerializeField] private List<GhostSpawnData> spawnDatas;
	[SerializeField] private Transform[] targets;

	[SerializeField] private UnityEvent onAreaClear;
	public UnityEvent OnAreaClear => onAreaClear;

	[SerializeField] private GameObject[] doors;

	private int ghostCount = 0;
	public int GhostCount => ghostCount;

	public void Spawn()
	{
		foreach (GhostSpawnData data in spawnDatas)
		{
			foreach (Transform marker in data.spawnPoints)
			{
				Ghost newGhost = GameObject.Instantiate(data.prefab, marker.position, Quaternion.identity);
				newGhost.SetTargets(targets);
				newGhost.OnDeath.AddListener(OnGhostDeath);
				ghostCount++;
			}
		}
	}

	private void OnGhostDeath()
	{
		ghostCount--;
		if (ghostCount == 0)
		{
			if (doors.Length > 0)
			{
				foreach (GameObject door in doors)
				{
					door.SetActive(false);
				}
			}
			else
			{
				Debug.LogError("MAKE A DOOR!!!!");
			}
			onAreaClear.Invoke();
		}
	}

	[System.Serializable]
	public class GhostSpawnData
	{
		public Ghost prefab;
		public Transform[] spawnPoints;
	}
}
