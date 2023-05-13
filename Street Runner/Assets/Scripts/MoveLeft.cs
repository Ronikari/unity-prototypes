using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public static float speed = 12;
    private float leftBound = -15;

    static public float sprintMult = 1;

    void LateUpdate()
    {
        if (PlayerController.S.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * sprintMult * Time.deltaTime);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
