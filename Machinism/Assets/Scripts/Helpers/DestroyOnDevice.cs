using UnityEngine;

public class DestroyOnDevice : MonoBehaviour
{
	[SerializeField] private DeviceType allowedDevice;

	private void Start()
	{
		if (SystemInfo.deviceType != allowedDevice) Destroy(gameObject);
	}
}
