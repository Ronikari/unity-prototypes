using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleTree : MonoBehaviour
{
    //Шаблон для создания яблок
    public GameObject applePrefab;
    public GameObject bombPrefab;

    // Скорость движения яблони
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

    // Расстояние, на котором должно изменяться направление движения яблони
    public float leftAndRightEdge = 25f;

    // Вероятность случайного изменения направления движения
    public float chancetoChangeDirections=0.08f;

    // Частота создания экземпляров яблок
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

    // Текущий уровень в игре
    public Text levelUI;
    public int lvl;

    public static float chanceToSpawnBomb = 0.08f;

    void Start()
    {
        // Сбрасывать яблоки раз в секунду
        Invoke("DropApple", 2f); // Invoke() вызывает метод, указанный именем в первом аргументе, через число секунд, указанное во втором аргументе
    }

    void DropApple()
    {
        if(Basket.scoreNum >= 0 & Basket.scoreNum <= 30)
        {
            if(Random.value >= chanceToSpawnBomb)
            {
                GameObject apple = Instantiate<GameObject>(applePrefab); // Инициализация спавна яблок
                apple.transform.position = transform.position; // Положение спавна яблок равно положению яблони в определенный момент времени
                Invoke("DropApple", 1f); // DropApple() вновь ссылается на саму себя через 1 секунду, тем самым устанавливая кд сброса
            }
            else
            {
                GameObject bomb = Instantiate<GameObject>(bombPrefab); // Инициализация спавна бомб
                bomb.transform.position = transform.position; // Положение спавна бомб равно положению яблони в определенный момент времени
                Invoke("DropApple", 1f); // DropApple() вновь ссылается на саму себя через 1 секунду, тем самым устанавливая кд сброса
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
            // Простое перемещение
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;

            // Изменение направления
            if (pos.x < -leftAndRightEdge)
            {
                speed = Mathf.Abs(speed); // Начать движение вправо
            }
            else if (pos.x > leftAndRightEdge)
            {
                speed = -Mathf.Abs(speed); // Начать движение влево
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

    void FixedUpdate() // Функция вызывается фиксированно 50 раз в секунду
    {
        // Теперь случайная смена направления привязана ко времени, потому что выполняется в FixedUpdate()
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 0 & Basket.scoreNum <= 30)
        {
            speed *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 30 & Basket.scoreNum <= 80)
        {
            speed2 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 80 & Basket.scoreNum <= 130)
        {
            speed3 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 130 & Basket.scoreNum <= 200)
        {
            speed4 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 200 & Basket.scoreNum <= 300)
        {
            speed5 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 300 & Basket.scoreNum <= 700)
        {
            speed6 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 700 & Basket.scoreNum <= 1500)
        {
            speed7 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 1500 & Basket.scoreNum <= 2500)
        {
            speed8 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum >= 2500 & Basket.scoreNum <= 4500)
        {
            speed9 *= -1; // Направление движения меняется в другую сторону
        }
        if (Random.value < chancetoChangeDirections & Basket.scoreNum > 4500)
        {
            speed10 *= -1; // Направление движения меняется в другую сторону
        }
        GameObject scoreGO = GameObject.Find("Level");
        levelUI = scoreGO.GetComponent<Text>();
        levelUI.text = "Level: " + lvl;
    }
}
