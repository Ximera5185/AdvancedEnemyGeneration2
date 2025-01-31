using UnityEngine;

public class BukRoket : MonoBehaviour
{
    private const float MinorAttackSpeed = 150f;
    private const float MinorAttackFlightDistanceWithoutVision = 1000f;
    private const float MinorBaseRotationSpeed = 0.5f;
    private const float MinorMaxRotationSpeed = 5f;
    private const float MinorActivationDistance = 200f;
    private const float MinorExplosionRadius = 5f;
    private const float MinorExplosionDamage = 50f;
    private const float MinorSpeedFantom = 1f;
    private const float MinorRotationSpeedDistanceLimit = 300f;

    private const float MiddleAttackSpeed = 150f;
    private const float MiddleAttackFlightDistanceWithoutVision = 1000f;
    private const float MiddleBaseRotationSpeed = 0.5f;
    private const float MiddleMaxRotationSpeed = 5f;
    private const float MiddleActivationDistance = 200f;
    private const float MiddleExplosionRadius = 5f;
    private const float MiddleExplosionDamage = 50f;
    private const float MiddleSpeedFantom = 1f;
    private const float MiddleRotationSpeedDistanceLimit = 300f;

    private const float HardAttackSpeed = 150f;
    private const float HardAttackFlightDistanceWithoutVision = 1000f;
    private const float HardBaseRotationSpeed = 0.5f;
    private const float HardMaxRotationSpeed = 5f;
    private const float HardActivationDistance = 200f;
    private const float HardExplosionRadius = 5f;
    private const float HardExplosionDamage = 50f;
    private const float HardSpeedFantom = 1f;
    private const float HardRotationSpeedDistanceLimit = 300f;

    [SerializeField] private ExplosionPartical _explosionPartical;

    [SerializeField] private FirePackPartical _firePackPartical;

    [SerializeField] private StartingSmokePartical _startingSmokePartical;

    [SerializeField] private SmokeTrail _smokeTrail;

    [SerializeField] private Transform _fantom;

    [Space(10)]
    [Header("DeveloperRegimes")]
    [SerializeField] private float _speed = 150f;

    [Tooltip("Расстояние полета без автонавидения")]
    [SerializeField] private float _flightDistanceWithoutVision = 100f;

    [Tooltip("Базовая скорость поворота")]
    [SerializeField] private float _baseRotationSpeed = 0.5f;

    [Tooltip("Максимальная скорость поворота")]
    [SerializeField] private float _maxRotationSpeed = 5f;

    [Tooltip("Расстояние, при котором ракета начинает смещаться всеми осями к цели для горантированного поражения")]
    [SerializeField] private float _activationDistance = 200f;

    [Tooltip("Радиус взрыва")]
    [SerializeField] private float _explosionRadius = 5f;

    [Tooltip("Урон от взрыва")]
    [SerializeField] private float _explosionDamage = 50f;

    [Tooltip("Скорость фантома")]
    [SerializeField] private float _speedFantom = 1f;

    [Tooltip("Расстояние до цели, при котором ракета использует максимальную скорость поворота")]
    [SerializeField] private float _rotationSpeedDistanceLimit = 300f;

    [Space(10)]
    [Header("MissileRegimes")]
    [SerializeField] private bool _isMinorAttack;
    [SerializeField] private bool _isMiddleAttack;
    [SerializeField] private bool _isHardAttack;

    private Transform _target;

    private Transform _transform;

    private float _distanceTraveled = 0f;

    private float _affset = 200;

    private float _maxDistanceFantom = 100f;

    private float _minDistanceFantom = 0f;

    private bool _isFirstFlight = true;

    private bool _isAttack = false;

    private bool _isDiverging = false;

    private Vector3 _divertDirection;

    private float _divertDuration = 2f;

    private void Start()
    {
        InitiationField();

        _transform = transform;
    }

    private void Update()
    {
        Move();

        RotateRocket(_isFirstFlight);
    }
    private void OnTriggerEnter(Collider other)
    {
        DestroyRocket();
    }

    private void SetAttackParameters(float speed, float flightDistanceWithoutVision, float baseRotationSpeed, float maxRotationSpeed, float activationDistance, float explosionRadius, float explosionDamage, float speedFantom, float rotationSpeedDistanceLimit)
    {
        _speed = speed;
        _flightDistanceWithoutVision = flightDistanceWithoutVision;
        _baseRotationSpeed = baseRotationSpeed;
        _maxRotationSpeed = maxRotationSpeed;
        _activationDistance = activationDistance;
        _explosionRadius = explosionRadius;
        _explosionDamage = explosionDamage;
        _speedFantom = speedFantom;
        _rotationSpeedDistanceLimit = rotationSpeedDistanceLimit;
    }

    private void InitiationField()
    {
        if (_isMinorAttack)
        {
            _isMiddleAttack = false;
            _isHardAttack = false;

            SetAttackParameters(MinorAttackSpeed, MinorAttackFlightDistanceWithoutVision, MinorBaseRotationSpeed, MinorMaxRotationSpeed, MinorActivationDistance, MinorExplosionRadius, MinorExplosionDamage, MinorSpeedFantom, MinorRotationSpeedDistanceLimit);
        }
        else if (_isMiddleAttack)
        {
            _isMinorAttack = false;
            _isHardAttack = false;

            SetAttackParameters(MiddleAttackSpeed, MiddleAttackFlightDistanceWithoutVision, MiddleBaseRotationSpeed, MiddleMaxRotationSpeed, MiddleActivationDistance, MiddleExplosionRadius, MiddleExplosionDamage, MiddleSpeedFantom, MiddleRotationSpeedDistanceLimit);
        }
        else if (_isHardAttack)
        {
            _isMinorAttack = false;
            _isMiddleAttack = false;

            SetAttackParameters(HardAttackSpeed, HardAttackFlightDistanceWithoutVision, HardBaseRotationSpeed, HardMaxRotationSpeed, HardActivationDistance, HardExplosionRadius, HardExplosionDamage, HardSpeedFantom, HardRotationSpeedDistanceLimit);
        }
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void RotateRocket(bool firstFlight)
    {
        float rotationSpeed = 400f;

        if (firstFlight == false)
        {
            _transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }
    }
    private void Move()
    {
        float deltaTime = Time.deltaTime;

        if (_isFirstFlight && _target != null)
        {
            _isAttack = true;

            _firePackPartical.Activate();

            _startingSmokePartical.DisconnectObject();

            float step = _speed * deltaTime;

            _transform.position += _transform.forward * step;

            _distanceTraveled += step;

            if (_distanceTraveled >= _flightDistanceWithoutVision)
            {
                _isFirstFlight = false;

                _distanceTraveled = 0f;
            }
        }
        else
        {
            if (_target != null && _isAttack == true)
            {
                float distanceToTarget = Vector3.Distance(_target.position, _transform.position);

                float offset = Mathf.Clamp(distanceToTarget - _maxDistanceFantom, _minDistanceFantom, _maxDistanceFantom);

                _affset = Mathf.Lerp(_affset, offset, _speedFantom * deltaTime);

                Vector3 targetPosition = _target.position - _target.forward * _affset;

                _fantom.position = targetPosition;

                Vector3 direction = (_fantom.position - _transform.position).normalized;

                float rotationSpeed = Mathf.Lerp(_baseRotationSpeed, _maxRotationSpeed, Mathf.Clamp01((_rotationSpeedDistanceLimit - distanceToTarget) / _rotationSpeedDistanceLimit));

                Quaternion lookRotation = Quaternion.LookRotation(direction);

                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, deltaTime * rotationSpeed);

                if (distanceToTarget > _activationDistance)
                {
                    _transform.position += _transform.forward * _speed * deltaTime;
                }
                else
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, _fantom.position, _speed * deltaTime);
                }
            }
            if (_target == null && _isAttack == true && _isDiverging == false)
            {
                StartDiverging();
            }
            if (_isDiverging)
            {
                _transform.position += _divertDirection * _speed * deltaTime;

                _divertDuration -= deltaTime;

                if (_divertDuration <= 0)
                {
                    DestroyRocket();
                }
            }
        }
    }

    private void StartDiverging()
    {
        _isDiverging = true;

        _divertDirection = Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)) * _transform.forward;

        _divertDuration = 2f;
    }

    private void DestroyRocket()
    {
        Explode();

        _smokeTrail.DestroyWith();

        Destroy(gameObject);
    }

    private void Explode()
    {
        _explosionPartical.Activate();

        _explosionPartical.DestroyWith();

        Collider [] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_explosionDamage);
            }
        }
    }
}