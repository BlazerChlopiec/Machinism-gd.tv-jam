using UnityEngine;

public class Affectable : MonoBehaviour
{
	protected bool affected;

	public virtual void OnAffected() => affected = true;

	public virtual void OnTouch() { }
}
