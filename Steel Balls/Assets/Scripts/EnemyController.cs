using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody enemyRb;
    protected float speed = 2;

    protected virtual void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        if (transform.position.y >= 0)
        {
            Vector3 lookDirection = (PlayerController.S.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }

        if (transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Projectile"))
        {
            Destroy(coll.gameObject);
        }
    }
}
