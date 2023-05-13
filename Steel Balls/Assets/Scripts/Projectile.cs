using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }

    void Start()
    {
        StartCoroutine(Lifetime());
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
