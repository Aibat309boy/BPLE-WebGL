using UnityEngine;

public class DeviceDependency : MonoBehaviour
{
	[SerializeField]
	private DeviceInfo.DeviceFamily[] enabledOnDevices;

	private void Awake()
	{
		DeviceInfo.DeviceFamily activeDeviceFamily = DeviceInfo.ActiveDeviceFamily;
		for (int i = 0; i < enabledOnDevices.Length; i++)
		{
			if (enabledOnDevices[i] == activeDeviceFamily)
				return;
		}

		gameObject.SetActive(false);
		Destroy(gameObject);
	}
}