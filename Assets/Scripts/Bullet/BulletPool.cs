using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private const int MinSize = 20;
    private const int MaxSize = 200;

    [SerializeField] private Bullet _bulletPrefab;

    private Stack<Bullet> _bullets;

    private void Awake()
    {
        Inicialaze(MinSize);
    }

    public Bullet GetBullet()
    {
        Bullet bullet;

        if (_bullets.Count > 0)
        {
            bullet = _bullets.Pop();
        }
        else
        {
            bullet = Create();
        }

        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);

        }

        return bullet;
    }

    public void Release(Bullet bullet)
    {
        if (_bullets.Count < MaxSize)
        {
            _bullets.Push(bullet);

            bullet.gameObject.SetActive(false);
        }
        else
        {
            Destroy(bullet.gameObject);
        }
    }

    private void Inicialaze(int minSize)
    {
        _bullets = new Stack<Bullet>(minSize);

        for (int i = 0; i < minSize; i++)
        {
            Bullet bullet = Create();

            bullet.gameObject.SetActive(false);

            _bullets.Push(bullet);
        }
    }

    private Bullet Create()
    {
        Bullet bullet = Instantiate(_bulletPrefab);

        return bullet;
    }
}