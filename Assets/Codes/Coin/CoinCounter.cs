using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
	public int coinCount;
	public Text coinText;
	private AudioSource audioS;
	public AudioClip coin;
	public AudioClip bigCoin;

	void Start()
	{
		audioS = GetComponent<AudioSource>();
	}

	public void AddCoin()
	{
		audioS.clip = coin;
		audioS.Play();
		coinCount += 1;
		if (coinCount > 99)
		{
			coinCount -= 100;
		}
		coinText.text = coinCount.ToString("00");
	}

}