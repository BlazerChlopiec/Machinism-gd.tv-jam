using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WobbleExtention : MonoBehaviour, IPointerUpHandler
{
	[SerializeField] private float wobbleSizeMultiplier = 1.5f;

	Button button;


	private void Start() => button = GetComponent<Button>();


	Vector2 prevScale;
	public void OnPointerUp(PointerEventData eventData)
	{
		if (!button.interactable) return;

		if (prevScale != Vector2.zero)
		{
			transform.localScale = prevScale;
			LeanTween.cancel(gameObject);
		}
		prevScale = transform.localScale;

		LeanTween.scale(gameObject, transform.localScale * wobbleSizeMultiplier, 1f)
			.setEasePunch()
			.setIgnoreTimeScale(true);
	}
}
