using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
	[SerializeField]
	private ScriptableFloat health;

	[SerializeField]
	private RectTransform border;

	[SerializeField]
	private RectTransform bar;

	void Update()
	{
		float width = border.rect.width;
		float inset = (width / 100) * (100 - health);
		float size = width - inset;
		// bar.rect.Set(0, 0, width, bar.rect.height);
		bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, inset, size);
		// bar.sizeDelta = new Vector2 (width, bar.sizeDelta.y);
	}
}
