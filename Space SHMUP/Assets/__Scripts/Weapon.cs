using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Это перечисление всех возможных типов оружия.
/// Также включает тип "shield", чтобы дать возможность совершенствовать защиту.
/// Аббревиатурой [HP] ниже отмечены элементы, не реализованные в основном прототипе.
/// </summary>
public enum WeaponType
{
    none, // По умолчанию / нет оружия
    blaster, // Простой бластер
    spread, // Веерная пушка, стреляющая несколькими снарядами
    turret, // Поворотная турель
    rockets, // Пока не реализовано
    enemy_blaster, // Вражеский бластер
    shield // Увеличивает shieldLevel
}

/// <summary>
/// Класс WeaponDefinition позволяет настраивать свойства конкретного вида оружия в инспекторе.
/// Для этого класс Main будет хранить массив элементов типа WeaponDefinition.
/// </summary>
[System.Serializable] // Делает класс редактируемым в инспекторе
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter; // Буква на кубике, изображающем бонус
    public Color color = Color.white; // Цвет ствола оружия и кубика бонуса
    public GameObject projectilePrefab; // Шаблон снарядов
    public Color projectileColor = Color.white;
    public float damageOnHit = 0; // Разрушительная мощность
    public float continiousDamage = 0; // Степень разрушения в секунду (для Laser)
    public float delayBetweenShots = 0;
    public float velocity = 20; // Скорость полета снарядов
}

public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")]
    [SerializeField]
    private WeaponType _type = WeaponType.none;
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShotTime; // Время последнего выстрела
    private Renderer collarRend;

    void Start()
    {
        collar = transform.Find("Collar").gameObject;
        collarRend = collar.GetComponent<Renderer>();

        // Вызвать SetType(), чтобы заменить тип оружия по умолчанию
        // WeaponType.none
        SetType(_type); // Метод осуществляет выбор оружия
        // Динамически создать точку привязки для всех снарядов
        if (PROJECTILE_ANCHOR == null) // родительский компонент всех снарядов
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }
        // Найти fireDelegate в корневом игровом объекте
        GameObject rootGO = transform.root.gameObject; // строка находит корневой игровой объект, к которому подключается оружие
        if (rootGO.GetComponent<Hero>() != null)
        {
            rootGO.GetComponent<Hero>().fireDelegate += Fire;
        }
    }

    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    public void SetType(WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false); // деактивирует объект до повторного вызова функции
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = Main.GetWeaponDefinition(_type);
        collarRend.material.color = def.color;
        lastShotTime = 0; // сразу после установки _type можно выстрелить
    }

    public void Fire()
    {
        // Если this.gameObject неактивен, выйти
        if (!gameObject.activeInHierarchy) return; // если выбран WeaponType.none
        // Если между выстрелами прошло недостаточно много времени, выйти
        if (Time.time - lastShotTime < def.delayBetweenShots) // в расчет берется значение, указанное для каждого типа оружия
        {
            return;
        }
        Projectile p;
        // Изначально снаряд движется вверх, но если оружие направлено вниз (к примеру, если таковые имеются у вражеских
        // кораблей, то направление вектора скорости vel изменяется на противоположное
        Vector3 vel = Vector3.up * def.velocity;
        if (transform.up.y < 0)
        {
            vel.y = -vel.y;
        }
        // Инструкция switch реализовывает все варианты оружия в прототипе
        // Добавить сюда остальные типы оружия
        switch (type)
        {
            case WeaponType.blaster:
                p = MakeProjectile();
                p.rigid.velocity = vel;
                break;

            case WeaponType.spread:
                p = MakeProjectile(); // снаряд, летящий прямо
                p.rigid.velocity = vel;
                p = MakeProjectile(); // первый снаряд, летящий влево
                // Ось Z в проекте направлена к нам, в данном случае положительное вращение осуществляется
                // по часовой стрелке
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                p = MakeProjectile(); // второй снаряд, летящий влево
                p.transform.rotation = Quaternion.AngleAxis(-20, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                p = MakeProjectile(); // первый снаряд, летящий вправо
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                p = MakeProjectile(); // второй снаряд, летящий вправо
                p.transform.rotation = Quaternion.AngleAxis(20, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                break;

            case WeaponType.turret:
                p = MakeProjectile();
                float turretWaveWidth = 20f;
                float turretWaveFrequency = 4f;
                vel.x = turretWaveWidth * Mathf.Cos(Time.time * turretWaveFrequency);
                p.rigid.velocity = vel;
                break;
        }
    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(def.projectilePrefab);
        if (transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");
            go.transform.position = collar.transform.position; // вылет точно из дула пушки
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
            go.transform.position = Enemy.enemyPos;
        }
        go.transform.SetParent(PROJECTILE_ANCHOR, true); // true, ибо дочерний объект должен сохранить мировые координаты
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShotTime = Time.time;
        return (p);
    }
}