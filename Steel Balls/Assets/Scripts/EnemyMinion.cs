using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinion : EnemyController
{
    protected override void Update()
    {
        speed = 2;
        base.Update();
    }
}
