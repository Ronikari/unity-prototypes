using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController S; // Singleton

    private SpawnManager spawnManager;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 5f;
    private float powerUpStrength = 15.0f;
    public bool hasPowerUpBounce;
    public bool hasPowerUpProjectile;
    public bool hasPowerUpJump;
    public GameObject powerUpIndicator;
    public GameObject projectilePrefab;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("PlayerController.Awake() - Attempted to assign second PlayerController.S!");
        }
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.52f, 0);

        // Активировать возможность осуществления прыжка, если соответствующий бонус взят
        if (Input.GetKeyDown(KeyCode.Space) && hasPowerUpJump) StartCoroutine(PowerUpJump());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUP")) // если игрок подбирает бонус
        {
            int powerUpType = (int)other.gameObject.GetComponent<PowerUp>().powerUpType; // тип бонуса
            switch (powerUpType)
            {
                case 0: // бонус "Толчок"
                    hasPowerUpBounce = true;
                    powerUpIndicator.gameObject.SetActive(true);
                    StartCoroutine(PowerUpCountdown());
                    Debug.Log("It's Bounce Power Up!");
                    break;
                case 1: // бонус "Ракеты"
                    hasPowerUpProjectile = true;
                    StartCoroutine(PowerUpProjectile());
                    Debug.Log("It's Projectile Power Up!");
                    break;
                case 2: // бонус "Прыжок"
                    hasPowerUpJump = true;
                    powerUpIndicator.gameObject.SetActive(true);
                    StartCoroutine(PowerUpCountdown());
                    Debug.Log("It's Jump Power Up!");
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void PowerUpBounce(Collision coll)
    {
        Rigidbody enemyRigidbody = coll.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = coll.gameObject.transform.position - transform.position;

        enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        //Debug.Log("Collided with: " + coll.gameObject.name + " with powerup set to " + hasPowerUP);
    }

    IEnumerator PowerUpProjectile()
    {
        // Активировать индикатор бонуса
        powerUpIndicator.gameObject.SetActive(true);
        // Найти всех противников на поле
        var enemiesOnScene = FindObjectsOfType<EnemyController>();
        int numberOfShots = 5;
        for (int j = 0; j < numberOfShots; j++)
        {
            for (int i = 0; i < enemiesOnScene.Length; i++)
            {
                // Найти противника
                if (enemiesOnScene[i].transform.position.y < 0) continue;
                Vector3 findEnemy = enemiesOnScene[i].transform.position - S.transform.position;
                // Задать точку спавна
                Vector3 spawnPos = S.transform.position + findEnemy.normalized;
                // Задать угол снаряда и начальный угол при спавне
                float projectileAngle = Mathf.Atan2(findEnemy.x, findEnemy.z) * Mathf.Rad2Deg;
                Quaternion spawnRotation = Quaternion.Euler(90, projectileAngle, 0);
                // Создать массив снарядов
                GameObject[] projectiles = new GameObject[enemiesOnScene.Length];
                projectiles[i] = Instantiate(projectilePrefab, spawnPos, spawnRotation);
                projectiles[i].gameObject.GetComponent<Rigidbody>().AddForce(findEnemy.normalized * 50f, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(0.2f);
        }
        // После окончания циклов деактивировать бонус и индикатор
        powerUpIndicator.gameObject.SetActive(false);
        hasPowerUpProjectile = false;
    }

    IEnumerator PowerUpJump()
    {
        // Подбросить игрока в воздух
        playerRb.AddForce(Vector3.up * 60f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.35f);
        // Приземлиться
        playerRb.AddForce(Vector3.down * 120f, ForceMode.Impulse);
        // WaitUntil() осуществляет следующие действия только тогда, когда игрок снизится до указанной высоты
        // внутренний синтаксис: () => "условие"
        yield return new WaitUntil(() => S.transform.position.y <= 0.1f);

        // Осуществить ударную волну
        float smashForce = 25f; // ударная сила
        // Найти всех противников на поле
        var enemiesOnScene = FindObjectsOfType<EnemyController>();
        foreach(var enemy in enemiesOnScene)
        {
            // Найти расстояние между игроком и врагом
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, S.transform.position);
            // Получить единичный вектор между игроком и врагом для определения направления движения врага от игрока
            Vector3 enemyMoveDirection = (enemy.transform.position - S.transform.position).normalized;
            // Реализовать ударную волну с учетом расстояния между игроком и врагом;
            // чем враг ближе, тем сильнее удар
            enemy.gameObject.GetComponent<Rigidbody>().AddForce(enemyMoveDirection * (smashForce - distanceToEnemy), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Enemy") && hasPowerUpBounce)
        {
            PowerUpBounce(coll);
        }
        if (coll.gameObject.CompareTag("EnemyProjectile")) Destroy(coll.gameObject);
    }

    IEnumerator PowerUpCountdown()
    {
        if(hasPowerUpBounce)
        {
            yield return new WaitForSeconds(7);
            hasPowerUpBounce = false;
            powerUpIndicator.gameObject.SetActive(false);
        }
        if (hasPowerUpJump)
        {
            yield return new WaitForSeconds(3);
            hasPowerUpJump = false;
            powerUpIndicator.gameObject.SetActive(false);
        }
    }
}
