using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldMouse
{
	private static Plane plane = new Plane(Vector3.zero, Vector3.forward, Vector3.right);
	private static Ray ray;
	private static Vector3 position;

	public static Vector3 GetWorldMouse()
	{
		position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		plane.Raycast(ray, out float distance);
		position = position + ray.direction.normalized * distance;
		return position;
	}
}
