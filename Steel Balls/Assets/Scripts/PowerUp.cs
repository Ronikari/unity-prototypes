using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum ePowerUps
    {
        bounce,
        projectile,
        jump
    }

    public ePowerUps powerUpType;
    private float rotationSpeed = 0.3f;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
