using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectible : MonoBehaviour
{
   // [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SoundManager.Instance.PlaySound(pickupSound);
            gameObject.SetActive(false);
        }
    }
}
