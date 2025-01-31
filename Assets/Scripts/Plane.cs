using UnityEngine;
using System;

public  class Plane : MonoBehaviour, IDamageable, IDestroyable
{
    public event Action<Transform> OnDestroy;

    [SerializeField] private int _health = 100;

    [SerializeField] private float _speed = 5.0f;

    public void TakeDamage(float amount)
    {
        _health -= (int) amount;

        if (_health <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        OnDestroy?.Invoke(transform);

        Destroy(gameObject);
    }
}