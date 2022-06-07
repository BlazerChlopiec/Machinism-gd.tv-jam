using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
	public static Quaternion AxesToZRot(Vector2 axes)
	{
		return Quaternion.LookRotation(Vector3.forward, axes);
	}

	public static Quaternion AxesToZRotSmoothed(Quaternion currentRot, Vector2 axes, float smoothT = 20)
	{
		var targetRotation = Quaternion.LookRotation(Vector3.forward, axes);
		return Quaternion.Slerp(currentRot, targetRotation, smoothT * Time.deltaTime);
	}

	public static Component GetComponentByName(GameObject typeHolder, string typeName) => typeHolder.GetComponent(typeName);
}
