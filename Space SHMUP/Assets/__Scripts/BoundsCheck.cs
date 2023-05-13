using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Предотвращает выход игрового объекта за границы экрана.
///  Важно: работает ТОЛЬКО с ортографической проекцией Main Camera в [ 0, 0, 0 ].
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true; // определяет режим работы сценария
    [Header("Set Dynamically")]
    public bool isOnScreen = true; // false, если объект вышел за границы экрана, если вошел - true
    public float camWidth;
    public float camHeight;
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;
        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            isOnScreen = false;
            offRight = true;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
            offLeft = true;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            isOnScreen = false;
            offUp = true;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            isOnScreen = false;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown); // если одна из переменных возвращает true, то isOnScreen = false
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false; // если keepOnScreen = true
        }
    }

    // Рисует границы в панели Scene (Сцена) с помощью OnDrawGizmos()
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}