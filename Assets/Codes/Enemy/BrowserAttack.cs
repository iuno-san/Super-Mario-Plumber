using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject FireBall;
    private float Timer;

    public void ShootFireball()
    {
        Instantiate(
            FireBall,
            firePoint.transform.position,
            firePoint.transform.rotation
        );
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= attackCoolDown)
        {
            Timer = 0;
            ShootFireball();
        }
    }
}
