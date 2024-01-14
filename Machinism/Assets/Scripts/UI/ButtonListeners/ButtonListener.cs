using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonListener : MonoBehaviour
{
	protected Button button;


	//button reference got in awake because otherwise if you override Start you couldn't
	//use the reference before base.Start()
	protected virtual void Awake() => button = GetComponent<Button>();
	protected virtual void Start() => button.onClick.AddListener(NewListener);

	protected virtual void NewListener() { }
}
