using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorConroller : MonoBehaviour
{
   public Texture2D cursor;

   private void Awake()
   {
      ChangeCursor(cursor);
   }

   private void ChangeCursor(Texture2D cursortype)
   {
      Vector2 hotspot = new Vector2(cursortype.width / 2, cursortype.height / 2);
      
   }
}
