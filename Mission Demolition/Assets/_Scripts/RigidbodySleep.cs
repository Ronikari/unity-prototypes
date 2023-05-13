using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodySleep : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.Sleep(); // Если на стены замка не воздействуют другие объекты, то они не двигаются, замок не разваливается
    }
}
