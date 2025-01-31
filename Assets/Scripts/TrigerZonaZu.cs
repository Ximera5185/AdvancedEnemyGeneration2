using System;
using System.Collections.Generic;
using UnityEngine;

public class TrigerZonaZu : MonoBehaviour
{
    public event Action<Transform> OnTriggerEntered;

    public event Action<Transform> OnTriggerExited;

    [SerializeField] private List<Transform> _targets = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.transform == null) return;

        if (other.CompareTag("Su"))
        {
            Transform targetTransform = other.transform;

            _targets.Add(targetTransform);

            var destroyableEnemy = other.GetComponent<IDestroyable>();

            if (destroyableEnemy != null)
            {
                destroyableEnemy.OnDestroy += HandleTargetDestroyed;
            }
            else
            {
                Debug.LogWarning($"Объект {targetTransform.name} не реализует интерфейс IDestroyable.");
            }

            OnTriggerEntered?.Invoke(targetTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null || other.transform == null) return;

        if (_targets.Contains(other.transform))
        {
            var destroyableEnemy = other.GetComponent<IDestroyable>();

            if (destroyableEnemy != null)
            {
                destroyableEnemy.OnDestroy -= HandleTargetDestroyed;
            }

            _targets.Remove(other.transform);

            OnTriggerExited?.Invoke(other.transform);
        }
    }

    private void HandleTargetDestroyed(Transform transform)
    {
        if (_targets.Contains(transform))
        {
            _targets.Remove(transform);
        }
    }

    public bool TryGetTarget(out Transform target)
    {
        target = null;

        if (_targets.Count == 0)
        {
            return false;
        }

        target = _targets [0];

        return true;
    }
}