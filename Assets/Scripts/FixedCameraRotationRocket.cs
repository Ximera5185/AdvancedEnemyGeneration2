using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraRotationRocket : MonoBehaviour
{
    public Transform target; // Цель, на которую должна смотреть камера

    [SerializeField] public Vector3 offset = new Vector3(0, 10, -10); // Смещение камеры относительно цели

    void LateUpdate()
    {
        if (target != null)
        {
            // Устанавливаем позицию камеры
            transform.position = target.position + offset;

            // Создаем вектор, который указывает на цель
            Vector3 direction = target.position - transform.position;

            // Устанавливаем направление камеры на цель, но без поворота по оси Z
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y, lookRotation.eulerAngles.z);
        }
    }
}
