using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	[SerializeField] private float timeToDestroy = 1;

	private void Start() => Destroy(gameObject, timeToDestroy);
}
