using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public enum Amount { one};
	public Amount type;
	public CoinCounter coinCounter;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (type == Amount.one)
			{
				coinCounter.AddCoin();
			}
			Destroy(this.gameObject);
		}
	}
}