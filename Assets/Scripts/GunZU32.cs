using System.Collections;
using UnityEngine;

public class GunZU32 : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private float _fireRate = 10f;

    private Coroutine _shootCoroutine;

    public void AutoAttack()
    {
        StopAttack();

        _shootCoroutine = StartCoroutine(Shoot());
    }

    public void StopAttack()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);

            _shootCoroutine = null;
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            _spawner.SpawnBullet();

            yield return new WaitForSeconds(_fireRate);
        }
    }
}