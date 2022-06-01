using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCanvas : MonoBehaviour
{
	public GameObject UIContainer;

	public List<DynamicCanvasMotion> motions;


	protected void Start() => UIContainer.SetActive(false);

	public virtual void Open()
	{
		UIContainer.SetActive(true);

		foreach (var item in motions)
		{
			item.previousCanvas = item.targetObject.parent;
			item.targetObject.SetParent(item.newCanvas);
		}
	}

	public virtual void Close()
	{
		UIContainer.SetActive(false);

		foreach (var item in motions)
		{
			item.targetObject.SetParent(item.previousCanvas);
		}
	}

	public void Toggle()
	{
		if (UIContainer.activeInHierarchy) Close();
		else Open();
	}
}

[Serializable]
public class DynamicCanvasMotion
{
	[HideInInspector] public Transform previousCanvas;
	public Transform targetObject;
	public Transform newCanvas;
}
