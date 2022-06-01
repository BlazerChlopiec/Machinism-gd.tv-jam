using UnityEngine;
using UnityEngine.EventSystems;

public class BreaksButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public bool pressed { get; private set; }


	public void OnPointerDown(PointerEventData eventData) => pressed = true;

	public void OnPointerUp(PointerEventData eventData) => pressed = false;
}
