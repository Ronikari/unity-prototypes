using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; // Одиночка

    [Header("Set in Inspector")]
    // Поля, управляющие движением корабля
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;

    [Header("Set Dynamically")]
    [SerializeField] // позволяет отобразить скрытое поле в инспекторе
    private float _shieldLevel = 1;

    // Эта переменная хранит ссылку на последний столкнувшийся игровой объект
    private GameObject lastTriggerGo = null;

    // Объявление нового делегата типа WeaponFireDelegate
    public delegate void WeaponFireDelegate(); // функция-делегат не отображается в инспекторе
    // Создать поле WeaponFireDelegate с именем fireDelegate
    public WeaponFireDelegate fireDelegate;

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            // Если уровень поля упал до нуля или ниже
            if (value < 0)
            {
                Destroy(this.gameObject);
                // Сообщить объекту Main.S о необходимости перезапустить игру
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

    void Start()
    {
        if (S == null)
        {
            S = this; // Сохранить ссылку на одиночку
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
        // Очистить массив weapons и начать игру с 1 бластером
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
    }

    void Update()
    {
        // Извлечь информацию из класса Input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Изменить transform.position, опираясь на информацию по осям
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Повернуть корабль, чтобы придать ощущение динамизма
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Произвести выстрел из всех видов оружия вызовом fireDelegate
        // Сначала проверить нажатие клавиши: Axis("Jump");
        // Затем убедиться, что значение fireDelegate не равно null, чтобы избежать ошибки
        // Input.GetAxis("Jump") вернет 1, если нажата клавиша, отвечающая за прыжок
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        Projectile proj = projGO.GetComponent<Projectile>();
        proj.type = WeaponType.blaster;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        // Гарантировать невозможность повторного столкновения с тем же объектом
        if (go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if (go.tag == "Enemy") // Если защитное поле столкнулось с вражеским кораблем
        {
            shieldLevel--; // Уменьшить уровень защиты на 1...
            Destroy(go); // ...и уничтожить врага
        }
        else if (go.tag == "PowerUp")
        {
            // Если защитное поле столкнулось с бонусом
            AbsorbPowerUp(go);
        }
        else if (go.tag == "ProjectileEnemy")
        {
            // Если защитное поле столкнулось с вражеским снарядом
            shieldLevel--; // Уменьшить уровень защиты на 1...
            Destroy(go); // ...и уничтожить снаряд 
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield: // если S, то уровень щита увеличивается на 1
                shieldLevel++;
                break;

            default:
                // Если тип оружия бонуса совпадает с имеющимся на корабле, то находится пустой
                // слот под оружие, если все слоты заняты - ничего не происходит
                if (pu.type == weapons[0].type) // если оружие того же типа
                {
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null)
                    {
                        // Установить в pu.type
                        w.SetType(pu.type);
                    }
                }
                else
                {
                    // Если оружие другого типа
                    // Если тип оружия бонуса отличается от текущего типа на корабле, то очищаются
                    // все слоты, в 1 слот записывается тип оружия из полученного бонуса
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
        pu.AbsorbedBy(this.gameObject);
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}