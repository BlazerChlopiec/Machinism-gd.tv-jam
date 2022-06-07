using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class OpenURLExtention : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private string URL;


	public void OnPointerDown(PointerEventData eventData) => OpenLink(URL);

	public static void OpenLink(string url)
	{
#if !UNITY_EDITOR && UNITY_WEBGL // WEBGL NEEDS A SPECIAL WAY OF OPENING LINKS
		openWindow(url);
#elif UNITY_2017_1_OR_NEWER // EVERY OTHER SCENARIO JUST OPENS NORMALLY
		OpenStandardLink(url);
#endif
	}

	private static void OpenStandardLink(string url) => Application.OpenURL(url);



	[DllImport("__Internal")] private static extern void openWindow(string url);
}
