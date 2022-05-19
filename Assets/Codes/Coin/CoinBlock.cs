using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
	private Animator spriteAnim;
	private GameObject child;
	public int totalCoins;
	public Sprite disabled;
	public CoinCounter coinCounter;
	private AudioSource audioS;
	public AudioClip hit;

	// Use this for initialization
	void Start()
	{
		child = transform.GetChild(0).gameObject;
		spriteAnim = child.GetComponent<Animator>();
		audioS = GetComponent<AudioSource>();
		audioS.clip = hit;
	}
}