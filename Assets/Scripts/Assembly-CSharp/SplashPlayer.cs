using System.Collections;
using UnityEngine;

public class SplashPlayer : MonoBehaviour
{
	[SerializeField]
	private GameObject singletonSpawnerPrefab;

	private IEnumerator Start()
	{
		if (INUnity.Enabled)
		{
			Debug.Log($"INFileSystem root: {INFileSystem.Root}");
			INUnity.InitializeRoot();
			INInitializer initializer = Object.Instantiate(Resources.Load<GameObject>("Innovation/INInitializer")).GetComponent<INInitializer>();
			while (!initializer.Initialized)
			{
				yield return null;
			}
			Object.Instantiate(singletonSpawnerPrefab);
		}
		else
		{
			Object.Instantiate(singletonSpawnerPrefab);
			while (!SingletonSpawner.SpawnDone)
			{
				yield return null;
			}
			StartSplash();
		}
	}

	private void StartSplash()
	{
		string arg;
		if (!Singleton<BuildCustomizationLoader>.Instance.IsChina)
		{
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				arg = Application.isMobilePlatform ? "iphone" : "pc-osx";
			}
			else
			{
				arg = ((!Singleton<BuildCustomizationLoader>.Instance.IsHDVersion)
					? ((DeviceInfo.ActiveDeviceFamily == DeviceInfo.DeviceFamily.Pc || DeviceInfo.ActiveDeviceFamily == DeviceInfo.DeviceFamily.Osx)
						? "pc-osx"
						: "iphone")
					: ((DeviceInfo.ActiveDeviceFamily == DeviceInfo.DeviceFamily.Pc || DeviceInfo.ActiveDeviceFamily == DeviceInfo.DeviceFamily.Osx)
						? "pc-osx"
						: "ipad"));
			}
		}
		else
		{
			string currentLocale = Singleton<Localizer>.Instance.CurrentLocale;
			if (Singleton<BuildCustomizationLoader>.Instance.CustomerID == "chinatelecom" || Singleton<BuildCustomizationLoader>.Instance.CustomerID == "chinamobile")
			{
				if (currentLocale == "zh-CN")
				{
					MonoBehaviour.print("SplashSequence_China_CN");
				}
				else
				{
					MonoBehaviour.print("SplashSequence_China");
				}
			}
			if (currentLocale == "zh-CN")
			{
				MonoBehaviour.print("SplashSequence_Talkweb_CN");
				arg = "talkweb_cn";
			}
			else
			{
				MonoBehaviour.print("SplashSequence_PC-OSX ");
				arg = "pc-osx";
			}
		}
		string resourcePath = $"splashes/sequences/splashsequence_{arg}";
		var splashSequence = Resources.Load<SplashScreenSequence>(resourcePath);
		if (splashSequence == null)
		{
			Debug.LogWarning($"Failed to load splash resource: {resourcePath}");
			resourcePath = "splashes/sequences/splashsequence_pc-osx";
			splashSequence = Resources.Load<SplashScreenSequence>(resourcePath);
		}
		if (splashSequence == null)
		{
			Debug.LogError("No splash sequence found in Resources/splashes/sequences");
			return;
		}
		Object.Instantiate(splashSequence);
	}
}
