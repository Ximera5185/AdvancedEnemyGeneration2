using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraRotationRocket : MonoBehaviour
{
    public Transform target; // ����, �� ������� ������ �������� ������

    [SerializeField] public Vector3 offset = new Vector3(0, 10, -10); // �������� ������ ������������ ����

    void LateUpdate()
    {
        if (target != null)
        {
            // ������������� ������� ������
            transform.position = target.position + offset;

            // ������� ������, ������� ��������� �� ����
            Vector3 direction = target.position - transform.position;

            // ������������� ����������� ������ �� ����, �� ��� �������� �� ��� Z
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y, lookRotation.eulerAngles.z);
        }
    }
}
