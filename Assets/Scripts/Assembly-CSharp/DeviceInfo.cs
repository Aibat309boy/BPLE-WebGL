using UnityEngine;

public static class DeviceInfo
{
	public enum DeviceFamily
	{
		Ios,
		Android,
		Pc,
		Osx,
		BB10,
		WP8
	}

	public static DeviceFamily ActiveDeviceFamily
	{
		get
		{
			if (Application.platform == RuntimePlatform.WebGLPlayer)
				return (Application.isMobilePlatform || Input.touchSupported) ? DeviceFamily.Android : DeviceFamily.Pc;

			if (Application.platform == RuntimePlatform.Android)
				return DeviceFamily.Android;

			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
				return DeviceFamily.Osx;

			return DeviceFamily.Pc;
		}
	}
}