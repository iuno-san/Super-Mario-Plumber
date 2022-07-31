using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class SpikeHead : Enemy_damage
{
   [Header("SpikeHead Attributes")]
   [SerializeField] private float speed;
   [SerializeField] private float range;
   [SerializeField] private float ckeckDelay;
   [SerializeField] private LayerMask playerLayer;
   private float checkTimer;
   private Vector3 destination;
   private Vector2 vector2;
   
   private bool attack;
   
   private Vector3[] directions = new Vector3[4];

   private void OnEnable()
   {
      Stop();
   }

   private void Update()
   {
      if (attack)
      {
         transform.Translate(destination * (Time.deltaTime * speed));
      }
      else
      {
         checkTimer += Time.deltaTime;
         if(checkTimer > ckeckDelay)
            CheckForPlayer();
      }
   }

   private void CheckForPlayer()
   {
      CalculateDirections();
      
      //Check if spiked sees player in all 4 directions
      for (int i = 0; i < directions.Length; i++)
      {
         Debug.DrawRay(transform.position, directions[i], Color.red);
         RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

         if (hit.collider != null && !attack)
         {
            attack = true;
            destination = directions[i];
            checkTimer = 0f;
         }
      }
      
   }

   private void CalculateDirections()
   {
      directions[0] = transform.right * range;
      directions[1] = -transform.right * range;
      directions[2] = transform.up * range;
      directions[3] = -transform.up * range;
   }

   private void Stop()
   {
      destination = transform.position;
      attack = false;
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      base.OnTriggerEnter2D(col);
      Stop();
   }
}
