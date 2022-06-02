using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
	public static void LookAtMouse(this Transform transform, Camera customCamera = null)
	{
		var cam = customCamera == null ? Camera.main : customCamera;

		Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = (mouseWorldPosition - (Vector2)transform.position).normalized;
		transform.up = direction;
	}

	public static void LookAtMouseSmoothly(this Transform transform, float smoothT = 20, Camera customCamera = null)
	{
		var cam = customCamera == null ? Camera.main : customCamera;

		Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = (mouseWorldPosition - (Vector2)transform.position).normalized;
		transform.up = Vector3.Lerp(transform.up, direction, smoothT * Time.deltaTime);
	}

	public static void LookAtPosSmoothly(this Transform transform, Vector3 target, float smoothT = 20)
	{
		Vector2 direction = (target - transform.position).normalized;
		transform.up = Vector3.Lerp(transform.up, direction, smoothT * Time.deltaTime);
	}

	public static void LookAtPos(this Transform transform, Vector3 target)
	{
		Vector2 direction = (target - transform.position).normalized;
		transform.up = direction;
	}

	public static bool IsSeenByCamera(this Transform transform, Camera customCamera = null)
	{
		var cam = customCamera == null ? Camera.main : customCamera;
		var viewportSpace = cam.WorldToViewportPoint(transform.position);

		return viewportSpace.x > 0 && viewportSpace.x < 1 &&
			   viewportSpace.y > 0 && viewportSpace.y < 1;
	}

	public static T GetRandom<T>(this IList<T> list)
	{
		return list[UnityEngine.Random.Range(0, list.Count)];
	}
}
