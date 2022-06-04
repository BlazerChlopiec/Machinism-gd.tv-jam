using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
	Slider slider;

	[SerializeField] private Vector2 followOffset;


	private void Start() => slider = GetComponent<Slider>();

	private void Update()
	{
		slider.maxValue = SpaceshipHealth.maxHealth;
		slider.value = SpaceshipHealth.health;
	}
}
