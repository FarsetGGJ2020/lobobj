using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostCountDiplsay : MonoBehaviour
{
	[SerializeField] private Text text;

	private void Update()
	{
		text.text = "R E M A I N I N G   G H O S T S: " + GameManager.Instance.CurrentArea.GhostCount;
	}
}
