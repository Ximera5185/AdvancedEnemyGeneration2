using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private BulletPool _bullets;

    [SerializeField] private Transform _firePointLeft;

    [SerializeField] private Transform _firePointRight;

    private Bullet _bullet;

    private bool _isLeftFirePointActive = true;

    public void SpawnBullet()
    {
        _bullet = _bullets.GetBullet();

        _bullet.Disabled += HandleBulletDestroyed;

        if (_isLeftFirePointActive)
        {
            _bullet.transform.position = _firePointLeft.position;
            _bullet.transform.rotation = _firePointLeft.rotation;
        }
        else
        {
            _bullet.transform.position = _firePointRight.position;
            _bullet.transform.rotation = _firePointRight.rotation;
        }

        _isLeftFirePointActive = !_isLeftFirePointActive;
    }

    private void HandleBulletDestroyed(Bullet bullet)
    {
        bullet.Disabled -= HandleBulletDestroyed;

        _bullets.Release(bullet);
    }
}