using UnityEngine;
public class ZU32 : MonoBehaviour
{
    private const float ZERO_THRESHOLD = 0.0f;

    [SerializeField] TrigerZonaZu _radar;

    [SerializeField] GunZU32 _gunZU32;

    [SerializeField] Transform _body;

    [SerializeField] Transform _gun;

    [SerializeField] float _speed;

    private Transform _target;

    private bool _isAttacking = false;

    private Quaternion _startBodyRotation;

    private Quaternion _startGunRotation;

    private void OnEnable()
    {
        _radar.OnTriggerEntered += HandleTriggerEntered;
        _radar.OnTriggerExited += HandleTriggerExited;
    }

    private void Start()
    {
        _startBodyRotation = _body.localRotation;
        _startGunRotation = _gun.localRotation;
    }

    private void Update()
    {
        SearchAirTargets();
    }

    private void OnDisable()
    {
        _radar.OnTriggerEntered -= HandleTriggerEntered;
        _radar.OnTriggerExited -= HandleTriggerExited;
    }

    private void Attack()
    {
        _gunZU32.AutoAttack();
    }

    private float ClampAngle(float value, float min, float max)
    {
        bool isBetweenMinAndMax;

        bool isCloserToMin;

        float delta1 = Mathf.DeltaAngle(value, min);
        float delta2 = Mathf.DeltaAngle(value, max);

        isBetweenMinAndMax = (delta1 < ZERO_THRESHOLD && delta2 > ZERO_THRESHOLD);

        if (isBetweenMinAndMax)
        {
            return value;
        }

        isCloserToMin = (Mathf.Abs(delta1) < Mathf.Abs(delta2));

        if (isCloserToMin)
        {
            return min;
        }
        else
        {
            return max;
        }
    }

    private void ActivateBody()
    {
        float currentParentAngleY = transform.eulerAngles.y;

        float minYAngle = currentParentAngleY - 90f;
        float maxYAngle = currentParentAngleY + 90f;

        Vector3 direction = _target.position - _body.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        float currentXRotation = _body.eulerAngles.x;
        float currentZRotation = _body.eulerAngles.z;

        float yRotation = targetRotation.eulerAngles.y;

        yRotation = ClampAngle(yRotation, minYAngle, maxYAngle);

        Quaternion newRotation = Quaternion.Euler(currentXRotation, yRotation, currentZRotation);

        _body.rotation = Quaternion.Slerp(_body.rotation, newRotation, Time.deltaTime * _speed);
    }

    private void ActivateGun()
    {
        float currentParentAngleX = transform.eulerAngles.x;

        float minXAngle = currentParentAngleX - 70f;
        float maxXAngle = currentParentAngleX + 10f;

        Vector3 directionGun = _target.position - _gun.position;

        Quaternion targetRotationGun = Quaternion.LookRotation(directionGun);

        float currentYRotationGun = _gun.rotation.eulerAngles.y;
        float currentZRotationGun = _gun.rotation.eulerAngles.z;

        float xRotation = targetRotationGun.eulerAngles.x;

        xRotation = ClampAngle(xRotation, minXAngle, maxXAngle);

        Quaternion newRotationGun = Quaternion.Euler(xRotation, currentYRotationGun, currentZRotationGun);

        _gun.rotation = Quaternion.Slerp(_gun.rotation, newRotationGun, Time.deltaTime * _speed);
    }

    private void SearchAirTargets()
    {
        if (_target != null)
        {
            ActivateBody();

            ActivateGun();

            if (!_isAttacking)
            {
                _isAttacking = true;

                Attack();
            }
        }
        else
        {
            _isAttacking = false;

            _gunZU32.StopAttack();

            OnTargetLif();
        }
    }

    private void DeactivateDefenseSystem()
    {
        ReturnToStartPosition(_body, _startBodyRotation);

        ReturnToStartPosition(_gun, _startGunRotation);
    }

    private void ReturnToStartPosition(Transform target, Quaternion startRotation)
    {
        target.localRotation = Quaternion.Slerp(target.localRotation, startRotation, Time.deltaTime * _speed);
    }

    private void HandleTriggerEntered(Transform newTarget)
    {
        if (_target = null)
        {
            _target = newTarget;
        }
    }

    private void HandleTriggerExited(Transform other)
    {
        if (_target == other)
        {
            OnTargetLif();
        }
    }

    private void OnTargetLif()
    {
        if (_radar.TryGetTarget(out Transform newTransform))
        {
            _target = newTransform;
        }
        else
        {
            _target = null;

            DeactivateDefenseSystem();
        }
    }
}