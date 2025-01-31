using System;
using UnityEngine;

public interface IDestroyable 
{
  public event Action<Transform> OnDestroy;

   public void Destroy();
}