using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFPSController : MonoBehaviour
{
	[Header("0 - Unlimited")]
	[Range(0, 360)] public float targetFps = 144;
	[SerializeField] private bool turnOffVSync = true;

	float oldTargetFps;


	private void Awake()
	{
		if (turnOffVSync)
			QualitySettings.vSyncCount = 0;
	}

	private void Update()
	{
		if (targetFps != oldTargetFps) OnChange(); // this is created to not lag because of constant Application. use
		oldTargetFps = targetFps;
		OnChange();
	}

	private void OnChange() => Application.targetFrameRate = (int)targetFps;
}