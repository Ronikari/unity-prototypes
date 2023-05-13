using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum ePlayerState { idle, walk, run }

    public ePlayerState state = ePlayerState.idle;
    private float u = 0.0025f; // Step
    private Vector3 p0, p1; // Player's positions for the lerp

    void Update()
    {
        if (PlayerController.S.transform.position.x < -2f)
        {
            state = ePlayerState.walk;
            MoveLeft.speed = 0;
            p0 = PlayerController.S.transform.position;
            p1 = Vector3.zero;
            PlayerController.S.transform.position = Vector3.Lerp(p0, p1, u);
        }
        else
        {
            state = ePlayerState.run;
            MoveLeft.speed = 12;
        }
    }
}
