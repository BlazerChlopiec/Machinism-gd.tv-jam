using UnityEngine;

public class Affectable : MonoBehaviour
{
	public bool affected;

	public virtual void OnAffected()
	{
		affected = true;
	}

	public virtual void OnTouch() { }
}
