using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; // Ссылка на интересующий объект

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero; // Добавляет ограничение на позицию камеры

    [Header("Set Dynamically")]
    public float camZ; // Желаемая координата Z камеры

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 destination;
        // Если нет интересующего объекта, вернуть P:[ 0, 0, 0 ]
        if(POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Получить позицию интересующего объекта
            destination = POI.transform.position;
            // Если интересующий объект - снаряд, убедиться, что он остановился
            if(POI.tag == "Projectile")
            {
                // Если он стоит на месте (то есть не двигается)
                if(POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    // Вернуть исходные настройки поля зрения камеры...
                    POI = null;
                    // ...в следующем кадре
                    return;
                }
            }
        }

        // Ограничить Х и Y минимальными значениями
        destination.x = Mathf.Max(minXY.x, destination.x); 
        destination.y = Mathf.Max(minXY.y, destination.y); // Эти функции не позволят камере выйти в отрицательные значения осей Х и Y
        // Определить точку между текущим местоположением камеры и destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Принудительно установить значение destination.z равным camZ, чтобы отодвинуть камеру подальше
        destination.z = camZ;
        // Поместить камеру в позицию destination
        transform.position = destination;
        // Изменить размер orthographicSize камеры, чтобы земля оставалась в поле зрения
        Camera.main.orthographicSize = destination.y + 10;
    }
}
