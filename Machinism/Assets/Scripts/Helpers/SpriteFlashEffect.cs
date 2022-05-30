using System.Collections;
using UnityEngine;

public class SpriteFlashEffect : MonoBehaviour
{
	[SerializeField] private float duration = .06f;

	Color startColor;
	public Color flashColor = Color.white;

	SpriteRenderer sprite;


	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		startColor = sprite.color;
	}

	public void Flash() => StartCoroutine(FlashRoutine());

	private IEnumerator FlashRoutine()
	{
		sprite.color = flashColor;
		yield return new WaitForSeconds(duration);
		sprite.color = startColor;
	}

	private void OnDisable() => sprite.color = startColor;
}