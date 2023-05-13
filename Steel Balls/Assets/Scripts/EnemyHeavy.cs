using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavy : EnemyController
{
    public float extraForce;

    protected override void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);
        if (coll.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = coll.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = coll.gameObject.transform.position - transform.position;
            playerRb.AddForce(awayFromEnemy * extraForce, ForceMode.Impulse);
        }
    }
}
