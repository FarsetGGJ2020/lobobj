using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneText : MonoBehaviour
{
	[SerializeField] private Text text;

	private void Update()
	{
		text.text = "Zone: " + GameManager.Instance.CurrentArea.AreaName;
	}
}
