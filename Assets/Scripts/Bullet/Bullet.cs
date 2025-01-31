using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<Bullet> Disabled;

    [SerializeField] private float _speed = 50;

    [SerializeField] private float _lifeTime = 5f;

    [SerializeField] private float _damage = 10f;

    private float _minLifeTime = 0;

    private float _maxLifeTime = 5f;


    private void Update()
    {
        MoveBullet();

        WasteTime();
    }


    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }
   
        DeactivateBullet();
    }

    private void OnDisable()
    {
        Disabled?.Invoke(this);
    }

    private void MoveBullet()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void WasteTime()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= _minLifeTime)
        {
            DeactivateBullet();
        }
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);

        _lifeTime = _maxLifeTime;
    }
}