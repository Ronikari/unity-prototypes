using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < Apple.bottomY)
        {
            Destroy(this.gameObject); // Во всех сценариях ссылка this указывает на текущий экземпляр класса C#
        }
    }
}
