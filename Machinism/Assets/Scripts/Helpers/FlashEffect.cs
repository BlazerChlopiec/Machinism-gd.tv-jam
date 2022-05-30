using System.Collections;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
	private float duration = .3f;

	[SerializeField] private Material flashMaterial;
	private Material startMaterial;

	SpriteRenderer sprite;


	void Start()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
		startMaterial = sprite.material;
	}

	public void Flash() => StartCoroutine(FlashRoutine());

	private IEnumerator FlashRoutine()
	{
		sprite.material = flashMaterial;
		yield return new WaitForSeconds(duration);
		sprite.material = startMaterial;
	}
}