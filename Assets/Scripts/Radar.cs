using UnityEngine;
public class Radar : MonoBehaviour
{
    [SerializeField] Transform _antenna;

    [SerializeField] float _speed = 220f;

    private Vector3 _rotationVectorAntenna;

    void Awake()
    {
        _rotationVectorAntenna = new Vector3(0, 0, _speed);
    }

    void Update()
    {
        RotateAntenna();
    }

    private void RotateAntenna()
    {
        _antenna.Rotate(_rotationVectorAntenna * Time.deltaTime);
    }
}