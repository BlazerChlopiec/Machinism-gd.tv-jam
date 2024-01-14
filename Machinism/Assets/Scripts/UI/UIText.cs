using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
	public TextMeshProUGUI field { get; private set; }

	protected virtual void Start() => field = GetComponent<TextMeshProUGUI>();
}
