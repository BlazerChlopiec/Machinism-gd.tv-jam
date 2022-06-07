using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T instance;

	protected virtual void Awake()
	{
		if (FindObjectsOfType<T>().Length == 1)
		{
			instance = gameObject.GetComponent<T>();
		}
		else
		{
			DestroyImmediate(gameObject);
		}

		DontDestroyOnLoad(instance.gameObject);
	}
}
