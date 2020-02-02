using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostCountDiplsay : MonoBehaviour
{
	[SerializeField] private Text text;

	private void Update()
	{
		text.text = "Remaining Ghosts: " + GameManager.Instance.CurrentArea.GhostCount;
	}
}
