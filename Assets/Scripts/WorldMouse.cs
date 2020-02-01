using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldMouse
{
	private static Plane plane = new Plane(Vector3.zero, Vector3.forward, Vector3.right);
	private static Ray ray;
	private static Vector3 startPosition;
	private static Vector3 position;

	public static Vector3 GetWorldMouse()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		startPosition = ray.origin;
		plane.Raycast(ray, out float distance);
		position = ray.origin + ray.direction * distance;
		// Debug.DrawLine(position, position + 10 * Vector3.up, Color.yellow, 2F);
		// Debug.DrawLine(ray.origin, ray.origin + distance * ray.direction, Color.magenta, 2F);
		return position;
	}
}
