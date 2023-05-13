using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameController gameController;
    public GameObject[] obstaclePrefabs;
    public Vector3 spawnPos = new Vector3(25, 0.01f, 0);
    private int _state;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if (PlayerController.S.gameOver == false)
        {
            StartCoroutine(SpawnObstacle());
        }
    }

    void Update()
    {
        _state = (int)gameController.state;
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            if (!PlayerController.S.gameOver && _state == 2)
            {
                int index = Random.Range(0, obstaclePrefabs.Length);
                float timeBetweenObstacleSpawns = Random.Range(1f, 2f) / MoveLeft.sprintMult;
                Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
                yield return new WaitForSeconds(timeBetweenObstacleSpawns);
            }
            else yield return null;
        }
    }
}
