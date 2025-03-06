using System.Collections;
using UnityEngine;

public class Dot : MonoBehaviour
{
	public float dotInterval;
	public int healthChangeAmount;

	private PlayerHealth _healthComponent;
	private WaitForSeconds _waitInterval;


	private void Start()
	{
		this._healthComponent = GetComponent<PlayerHealth>();
		this._waitInterval = new WaitForSeconds(this.dotInterval);
		StartCoroutine(CoroutineChangeHealthBy(healthChangeAmount));
	}

	private IEnumerator CoroutineChangeHealthBy(int amount)
	{
		while (true)
		{
			if (_healthComponent.currentHealth <= 0)
			{
				yield break;
			}

			this._healthComponent.ChangeHealth(amount);
			yield return _waitInterval;
		}
	}
}
