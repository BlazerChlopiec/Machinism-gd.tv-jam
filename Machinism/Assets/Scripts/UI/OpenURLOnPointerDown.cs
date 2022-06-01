using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class OpenURLOnPointerDown : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private string URL;


	public void OnPointerDown(PointerEventData eventData) => OpenLink(URL);

	public static void OpenLink(string url)
	{
#if !UNITY_EDITOR && UNITY_WEBGL
		openWindow(url);
#elif UNITY_2017_1_OR_NEWER
		OpenPCBuild(url);
#endif
	}

	private static void OpenPCBuild(string url) => Application.OpenURL(url);



	[DllImport("__Internal")] private static extern void openWindow(string url);
}
