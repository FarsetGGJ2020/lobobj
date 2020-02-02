using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneText : MonoBehaviour
{
	[SerializeField] private Text text;

	private void Update()
	{
		text.text = "R E P A I R  Z O N E :  " + GameManager.Instance.CurrentArea.AreaName;
	}
}
