using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public event Action<Transform> OnTriggerEntered;

    public event Action<Transform> OnTriggerExited;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExited?.Invoke(other.transform);
    }
}