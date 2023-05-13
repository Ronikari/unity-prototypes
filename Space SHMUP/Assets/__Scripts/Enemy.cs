using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; // Скорость в м/с
    public float fireRate = 0.3f; // Секунд между выстрелами (не используется)
    public float health = 10;
    public int score = 100; // Очки за уничтожение этого корабля
    public float showDamageDuration = 0.1f; // Длительность эффекта попадания в секундах
    public float powerUpDropChance = 1f; // Вероятность сбросить бонус
    public GameObject projectilePrefab;
    public float projectileSpeed = 30;
    public float delayBetweenShots = 1f;

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials; // Все материалы игрового объекта и его потомков
    public bool showingDamage = false;
    public float damageDoneTime; // Время прекращения отображения эффекта
    public bool notifiedOfDestruction = false;
    public float timer;
    private Color projectileColor;

    protected BoundsCheck bndCheck;

    public delegate void WeaponFireDelegate(); // функция-делегат не отображается в инспекторе
    // Создать поле WeaponFireDelegate с именем fireDelegate
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        // Получает ссылку на компонент сценария BoundsCheck 
        // Если компонента нет - получает значение null
        bndCheck = GetComponent<BoundsCheck>();
        // Получить материалы и цвет этого игрового объекта и его потомков
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    static public Vector3 enemyPos;

    void Update()
    {
        Move();
        //EnemyFire();

        // Если отображается эффект попадания и прошло достаточно времени
        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }

        if (bndCheck != null && bndCheck.offDown)
        {
            // Корабль за нижней границей, поэтому его нужно уничтожить
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        EnemyFire();
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = enemyPos = tempPos;
    }

    public virtual void EnemyFire()
    {
        timer += Time.deltaTime;
        if (timer >= delayBetweenShots)
        {
            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = transform.position;
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

            Projectile proj = projGO.GetComponent<Projectile>();
            proj.type = WeaponType.enemy_blaster;
            delayBetweenShots = Main.GetWeaponDefinition(proj.type).delayBetweenShots;
            float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
            rigidB.velocity = Vector3.down * tSpeed;
            timer = 0;
            return;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        //  Коллайдер объейта Shield дочернего по отношению к _Hero, является триггером, а
        //  столкновение с триггерами не приводит к вызову OnCollisionEnter()

        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();

                // Если вражеский корабль за границами экрана, не наносить ему повреждений
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO); // уничтожается только попавший снаряд
                    break;
                }

                // Поразить вражеский корабль
                ShowDamage();
                // Получить разрушающую силу из WEAP_DICT в классе Main
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                {
                    // Сообщить объекту-одиночке Main об уничтожении
                    if (!notifiedOfDestruction) // если true
                    {
                        Main.S.ShipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    // Уничтожить этот вражеский корабль
                    Destroy(this.gameObject);
                }
                Destroy(otherGO); // уничтожается попавший снаряд
                break;

            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
    }

    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}