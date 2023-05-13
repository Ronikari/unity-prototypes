using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyController
{
    public enum eBossPower
    {
        none,
        bounce,
        projectile,
        spawnMinions
    }

    public eBossPower bossPowerType = eBossPower.none;
    private SpawnManager spawnManager;
    public GameObject projectilePrefab;
    public GameObject minionPrefab;
    public float bounceForce;

    protected override void Start()
    {
        base.Start();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        bossPowerType = (eBossPower)Random.Range(1, 4);
        // Если босс создает ракеты
        if (bossPowerType == eBossPower.projectile) StartCoroutine(ProjectileBossPower());
        // Если босс создает прислужников
        if (bossPowerType == eBossPower.spawnMinions) StartCoroutine(SpawnMinionsPower());
    }

    protected override void Update()
    {
        speed = 1;
        base.Update();
    }

    protected override void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);
        if (coll.gameObject.CompareTag("Player") && bossPowerType == eBossPower.bounce)
        {
            Rigidbody playerRb = coll.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = coll.gameObject.transform.position - transform.position;
            playerRb.AddForce(awayFromEnemy * bounceForce, ForceMode.Impulse);
        }
    }

    IEnumerator ProjectileBossPower()
    {
        while (PlayerController.S.transform.position.y >= 0)
        {
            // Найти игрока
            Vector3 findPlayer = PlayerController.S.transform.position - transform.position;
            // Задать точку спавна
            Vector3 spawnPos = transform.position + findPlayer.normalized;
            // Задать угол снаряда и начальный угол при спавне
            float projectileAngle = Mathf.Atan2(findPlayer.x, findPlayer.z) * Mathf.Rad2Deg;
            Quaternion spawnRotation = Quaternion.Euler(90, projectileAngle, 0);
            // Создать снаряд, летящий в игрока
            GameObject projectile = Instantiate(projectilePrefab, spawnPos, spawnRotation);
            projectile.GetComponent<Rigidbody>().AddForce(findPlayer.normalized * 32f, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpawnMinionsPower()
    {
        while (transform.position.y >= 0)
        {
            yield return new WaitForSeconds(4);
            int minionsCount = 2;
            for(int i = 0; i < minionsCount; i++)
            {
                Instantiate(minionPrefab, spawnManager.GenerateSpawnPosition(), minionPrefab.transform.rotation);
            }
        }
    }
}
