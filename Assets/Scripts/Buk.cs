using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buk : MonoBehaviour
{
    [SerializeField] private KillZone _killZone;

    [SerializeField] private List<BukRoket> _rockets;

    [SerializeField] private List<Transform> _targets = new List<Transform>();

    [SerializeField] private Transform _rocketPlatform;

    [SerializeField] private Transform _radar;

    [SerializeField] private float _speed;

    [SerializeField] private float _launchDelay = 2f;

    private bool _isAttacking = false;

    private void OnEnable()
    {
        _killZone.OnTriggerEntered += HandleTriggerEntered;
        _killZone.OnTriggerExited += HandleTriggerExited;
    }

    void Update()
    {
        if (_rockets.Count > 0)
        {
            SearchAirTargets();
        }
        else
        {
            DeactivateDefenseSystem();
        }

    }

    private void OnDisable()
    {
        _killZone.OnTriggerEntered -= HandleTriggerEntered;
        _killZone.OnTriggerExited -= HandleTriggerExited;
    }

    private void HandleTriggerEntered(Transform other)
    {
        if (other.CompareTag("Su") && _targets.Count == 0)
        {
            _targets.Add(other.transform);
        }
    }

    private void HandleTriggerExited(Transform other)
    {
        if (_targets.Contains(other.transform) && !_isAttacking)
        {
            _targets.Remove(other.transform);
        }
    }

    private void DeactivateDefenseSystem()
    {
        ReturnToStartPosition(_radar, Quaternion.identity);

        ReturnToStartPosition(_rocketPlatform, Quaternion.Euler(0, _rocketPlatform.rotation.eulerAngles.y, _rocketPlatform.rotation.eulerAngles.z));
    }

    private void ActivateRadar()
    {
        if (_targets [0] != null)
        {
            Vector3 direction = (_targets [0].position - _radar.position).normalized;

            direction.y = 0;

            direction.Normalize();

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            _radar.rotation = Quaternion.Slerp(_radar.rotation, lookRotation, Time.deltaTime * _speed);
        }

    }

    private bool ActivateRokets()
    {
        float minAttackAngle = 1f;

        Quaternion endRocketPlatformRotation = Quaternion.Euler(45, _rocketPlatform.rotation.eulerAngles.y, _rocketPlatform.rotation.eulerAngles.z);

        _rocketPlatform.rotation = Quaternion.Slerp(_rocketPlatform.rotation, endRocketPlatformRotation, Time.deltaTime * _speed);

        float angleDifference = Quaternion.Angle(_rocketPlatform.rotation, endRocketPlatformRotation);

        if (angleDifference < minAttackAngle)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
    private void ReturnToStartPosition(Transform target, Quaternion startRotation)
    {
        target.rotation = Quaternion.Slerp(target.rotation, startRotation, Time.deltaTime * _speed);
    }

    private void SearchAirTargets()
    {
        if (_targets.Count > 0)
        {
            ActivateRadar();

            if (ActivateRokets() && !_isAttacking)
            {
                StartCoroutine(AttackWithDelay());
            }
        }
        else
        {
            DeactivateDefenseSystem();
        }
    }
    private IEnumerator AttackWithDelay()
    {
        _isAttacking = true;

        if (_rockets.Count > 0 && _targets.Count > 0)
        {
            BukRoket roket = _rockets [0];

            Transform target = _targets [0];

            roket.SetTarget(target);

            roket.transform.SetParent(null);

            while (roket != null && target != null)
            {
                yield return null;
            }

            if (target == null || _rockets [0] == null)
            {
                _rockets.RemoveAt(0);

                _targets.RemoveAt(0);
            }
        }

        yield return new WaitForSeconds(_launchDelay);

        _isAttacking = false;
    }
}