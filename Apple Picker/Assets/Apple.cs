using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    void Update()
    {
        if(transform.position.y < bottomY)
        {
            Destroy(this.gameObject); // Во всех сценариях ссылка this указывает на текущий экземпляр класса C#

            // Получить ссылку на компонент ApplePicker главной камеры Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>(); // Обращение к C# скрипту ApplePicker, привязанного к Main Camera
            // Вызвать общедоступный метод AppleDestroyed() из apScript
            apScript.AppleDestroyed();
        }
    }
}
