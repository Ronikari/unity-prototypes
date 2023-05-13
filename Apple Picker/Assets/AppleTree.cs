using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleTree : MonoBehaviour
{
    //������ ��� �������� �����
    public GameObject applePrefab;
    public GameObject bombPrefab;

    // �������� �������� ������
    public float speed = 10f;
    public float speed2 = 13f;
    public float speed3 = 16f;
    public float speed4 = 19f;
    public float speed5 = 22f;
    public float speed6 = 25f;
    public float speed7 = 28f;
    public float speed8 = 31f;
    public float speed9 = 34f;
    public float speed10 = 37f;

    // ����������, �� ������� ������ ���������� ����������� �������� ������
    public float leftAndRightEdge = 25f;

    // ����������� ���������� ��������� ����������� ��������
    public float chancetoChangeDirections=0.08f;

    // ������� �������� ����������� �����
    public float secondsBetweenAppleDrops = 1.0f;
    public float secondsBetweenAppleDrops2 = 0.9f;
    public float secondsBetweenAppleDrops3 = 0.8f;
    public float secondsBetweenAppleDrops4 = 0.7f;
    public float secondsBetweenAppleDrops5 = 0.6f;
    public float secondsBetweenAppleDrops6 = 0.5f;
    public float secondsBetweenAppleDrops7 = 0.4f;
    public float secondsBetweenAppleDrops8 = 0.3f;
    public float secondsBetweenAppleDrops9 = 0.2f;
    public float secondsBetweenAppleDrops10 = 0.1f;

    // ������� ������� � ����
    public Text levelUI;
    public int lvl;

    public static float chanceToSpawnBomb = 0.08f;

    void Start()
    {
        // ���������� ������ ��� � �������
        Invoke("DropApple", 2f); // Invoke() �������� �����, ��������� ������ � ������ ���������, ����� ����� ������, ��������� �� ������ ���������
    }

    void DropApple()
    {
        if(Basket.scoreNum >= 0 & Basket.scoreNum <= 30)
        {
            if(Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab); // ������������� ������ �����
                apple.transform.position = transform.position; // ��������� ������ ����� ����� ��������� ������ � ������������ ������ �������
                Invoke("DropApple", 1f); // DropApple() ����� ��������� �� ���� ���� ����� 1 �������, ��� ����� ������������ �� ������
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab); // ������������� ������ ����
                bomb.transform.position = transform.position; // ��������� ������ ���� ����� ��������� ������ � ������������ ������ �������
                Invoke("DropApple", 1f); // DropApple() ����� ��������� �� ���� ���� ����� 1 �������, ��� ����� ������������ �� ������
            }
        }
        if (Basket.scoreNum > 30 & Basket.scoreNum <= 80)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops2);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops2);
            }
        }
        if (Basket.scoreNum > 80 & Basket.scoreNum <= 130)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops3);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops3);
            }
        }
        if (Basket.scoreNum > 130 & Basket.scoreNum <= 200)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops4);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops4);
            }
        }
        if (Basket.scoreNum > 200 & Basket.scoreNum <= 300)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops5);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops5);
            }
        }
        if (Basket.scoreNum > 300 & Basket.scoreNum <= 700)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops6);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops6);
            }
        }
        if (Basket.scoreNum > 700 & Basket.scoreNum <= 1500)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops7);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops7);
            }
        }
        if (Basket.scoreNum > 1500 & Basket.scoreNum <= 2500)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops8);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops8);
            }
        }
        if (Basket.scoreNum > 2500 & Basket.scoreNum <= 4500)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops9);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops9);
            }
        }
        if (Basket.scoreNum > 4500)
        {
            if (Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab);
                apple.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops10);
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab);
                bomb.transform.position = transform.position;
                Invoke("DropApple", secondsBetweenAppleDrops10);
            }
        }
    }

    void Update()
    {
        if (Basket.scoreNum >= 0 & Basket.scoreNum <= 30)
        {
            // ������� �����������
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;

            // ��������� �����������
            if (pos.x < -leftAndRightEdge)
            {
                speed = Mathf.Abs(speed); // ������ �������� ������
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed = -Mathf.Abs(speed); // ������ �������� �����
            }
            lvl = 1;
        }
        if (Basket.scoreNum > 30 & Basket.scoreNum <= 80)
        {
            Vector3 pos = transform.position;
            pos.x += speed2 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed2 = Mathf.Abs(speed2);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed2 = -Mathf.Abs(speed2);
            }
            lvl = 2;
        }
        if (Basket.scoreNum > 80 & Basket.scoreNum <= 130)
        {
            Vector3 pos = transform.position;
            pos.x += speed3 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed3 = Mathf.Abs(speed3);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed3 = -Mathf.Abs(speed3);
            }
            lvl = 3;
        }
        if (Basket.scoreNum > 130 & Basket.scoreNum <= 200)
        {
            Vector3 pos = transform.position;
            pos.x += speed4 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed4 = Mathf.Abs(speed4);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed4 = -Mathf.Abs(speed4);
            }
            lvl = 4;
        }
        if (Basket.scoreNum > 200 & Basket.scoreNum <= 300)
        {
            Vector3 pos = transform.position;
            pos.x += speed5 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed5 = Mathf.Abs(speed5);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed5 = -Mathf.Abs(speed5);
            }
            lvl = 5;
        }
        if (Basket.scoreNum > 300 & Basket.scoreNum <= 700)
        {
            Vector3 pos = transform.position;
            pos.x += speed6 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed6 = Mathf.Abs(speed6);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed6 = -Mathf.Abs(speed6);
            }
            lvl = 6;
        }
        if (Basket.scoreNum > 700 & Basket.scoreNum <= 1500)
        {
            Vector3 pos = transform.position;
            pos.x += speed7 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed7 = Mathf.Abs(speed7);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed7 = -Mathf.Abs(speed7);
            }
            lvl = 7;
        }
        if (Basket.scoreNum > 1500 & Basket.scoreNum <= 2500)
        {
            Vector3 pos = transform.position;
            pos.x += speed8 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed8 = Mathf.Abs(speed8);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed8 = -Mathf.Abs(speed8);
            }
            lvl = 8;
        }
        if (Basket.scoreNum > 2500 & Basket.scoreNum <= 4500)
        {
            Vector3 pos = transform.position;
            pos.x += speed9 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed9 = Mathf.Abs(speed9);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed9 = -Mathf.Abs(speed9);
            }
            lvl = 9;
        }
        if (Basket.scoreNum > 4500)
        {
            Vector3 pos = transform.position;
            pos.x += speed10 * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
            {
                speed10 = Mathf.Abs(speed10);
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed10 = -Mathf.Abs(speed10);
            }
            lvl = 10;
        }
    }

    void FixedUpdate() // ������� ���������� ������������ 50 ��� � �������
    {
        // ������ ��������� ����� ����������� ��������� �� �������, ������ ��� ����������� � FixedUpdate()
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 0 & Basket.scoreNum <= 30)
        {
            speed *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 30 & Basket.scoreNum <= 80)
        {
            speed2 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 80 & Basket.scoreNum <= 130)
        {
            speed3 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 130 & Basket.scoreNum <= 200)
        {
            speed4 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 200 & Basket.scoreNum <= 300)
        {
            speed5 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 300 & Basket.scoreNum <= 700)
        {
            speed6 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 700 & Basket.scoreNum <= 1500)
        {
            speed7 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 1500 & Basket.scoreNum <= 2500)
        {
            speed8 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 2500 & Basket.scoreNum <= 4500)
        {
            speed9 *= -1; // ����������� �������� �������� � ������ �������
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum > 4500)
        {
            speed10 *= -1; // ����������� �������� �������� � ������ �������
        }
        GameObject scoreGO = GameObject.Find("Level");
        levelUI = scoreGO.GetComponent<Text>();
        levelUI.text = "Level: " + lvl;
    }
}
