using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Part - еще один сериализуемый класс подобно WeaponDefinition, предназначенный для
/// хранения данных
/// </summary>
[System.Serializable]
public class Part
{
    // Значения этих трех полей должны определяться в инспекторе
    public string name; // Имя этой части
    public float health; // Степень стойкости этой части
    public string[] protectedBy; // Другие части, защищающие эту

    // Эти два поля инициализируются автоматически в Start().
    // Кэширование, как здесь, ускоряет получение необходимых данных
    [HideInInspector] // Не позволяет следующему полю появиться в инспекторе
    public GameObject go; // Игровой объект этой части
    [HideInInspector]
    public Material mat; // Материал для отображения повреждений
}

/// <summary>
/// Enemy_4 создается за верхней границей, выбирает случайную точку на экране и перемещается к ней.
/// Добравшись до места, выбирает другую случайную точку и продолжает двигаться, пока игрок не
/// уничтожит его.
/// </summary>

public class Enemy_4 : Enemy
{
    [Header("Set in Inspector: Enemy_4")]
    public Part[] parts; // Массив частей, составляющих корабль

    private Vector3 p0, p1; // Две точки для интерполяции
    private float timeStart; // Время создания этого корабля
    private float duration = 4; // Продолжительность перемещения

    void Start()
    {
        // Начальная позиция уже выбрана в Main.SpawnEnemy(), поэтому запишем ее как начальные
        // значения в р0 и р1
        p0 = p1 = pos;
        InitMovement();

        // Записать в кэш игровой объект и материал каждой части в parts
        Transform t;
        foreach (Part prt in parts)
        {
            t = transform.Find(prt.name);
            if (t != null)
            {
                prt.go = t.gameObject;
                prt.mat = prt.go.GetComponent<Renderer>().material;
            }
        }
    }

    void InitMovement()
    {
        p0 = p1; // Переписать р1 в р0 (действие осуществляется справа-налево, не забыть)
        // Выбрать новую точку р1 на экране
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        // Сбросить время
        timeStart = Time.time;
    }

    public override void Move()
    {
        // Этот метод переопределяет Enemy.Move() и реализует линейную интерполяцию
        float u = (Time.time - timeStart) / duration;

        if (u >= 1)
        {
            InitMovement(); // выбирается новая точка для перемещения
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2); // применить плавное замедление
        pos = (1 - u) * p0 + u * p1; // простая линейная интерполяция
    }

    // Эти две функции выполняют поиск части в массиве parts по имени и по ссылке на
    // игровой объект
    Part FindPart(string n)
    {
        foreach (Part prt in parts)
        {
            if (prt.name == n)
            {
                return (prt);
            }
        }
        return (null);
    }

    Part FindPart(GameObject go)
    {
        foreach (Part prt in parts)
        {
            if (prt.go == go)
            {
                return (prt);
            }
        }
        return (null);
    }

    // Эти функции возвращают true, если данная часть уничтожена
    bool Destroyed(GameObject go)
    {
        return (Destroyed(FindPart(go)));
    }

    bool Destroyed(string n)
    {
        return (Destroyed(FindPart(n)));
    }

    bool Destroyed(Part prt)
    {
        if (prt == null) // если ссылка на часть не была передана
        {
            return (true); // вернуть true (да, была уничтожена)
        }
        // Вернуть результат сравнения: prt.health <= 0 
        // Если prt.health <= 0, вернуть true (да, была уничтожена)
        return (prt.health <= 0);
    }

    // Окрашивает в красный только одну часть, а не весь корабль
    void ShowLocalizedDamage(Material m)
    {
        m.color = Color.red;
        damageDoneTime = Time.time + showDamageDuration;
        showingDamage = true;
    }

    // Переопределяет метод OnCollisionEnter из сценария Enemy.cs
    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject;
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
                // Если корабль за границами экрана, не повреждать его
                if (!bndCheck.isOnScreen)
                {
                    Destroy(other);
                    break;
                }

                // Поразить вражеский корабль
                GameObject goHit = coll.contacts[0].thisCollider.gameObject;
                // Эта функция пытается найти игровой объект, в который попал снаряд. Описатель
                // столкновения coll типа Collision включает поле contacts[] - массив точек контакта
                // ContactPoint. Посколько речь в данном случае идет о столкновении, гарантированно
                // будет иметься хотя бы одна точка контакта ContactPoint (то есть contacts[0]).
                // Каждая точка ContactPoint имеет поле с именем thisCollider, представляющее
                // коллайдер части корабля Enemy_4, которая была поражена.
                Part prtHit = FindPart(goHit);
                if (prtHit == null) // Если prtHit не был найден...
                {
                    goHit = coll.contacts[0].otherCollider.gameObject;
                    prtHit = FindPart(goHit);
                }
                // Проверить, защищена ли эта часть корабля
                // Если эта часть корабля все еще защищена другой частью, не разрушенной до конца,
                // применить разрушение к защищающей части
                if (prtHit.protectedBy != null)
                {
                    foreach (string s in prtHit.protectedBy)
                    {
                        // Если хотя бы одна из защищающих частей еще не разрушена...
                        if (!Destroyed(s))
                        {
                            // ...не наносить повреждений этой части
                            Destroy(other); // уничтожить снаряд ProjectileHero
                            return; // выйти, не повреждая Enemy_4
                        }
                    }
                }

                // Эта часть не защищена, нанести ей повреждение
                // Получить разрушающую силу из Projectile.type и Main.WEAP_DICT
                prtHit.health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                // Показать эффект попадания в часть
                ShowLocalizedDamage(prtHit.mat);
                // Если степень стойкости части достигла 0, она деактивируется, благодаря чему перестает
                // отображаться на экране и взаимодействовать с другими игровыми объектами
                if (prtHit.health <= 0)
                {
                    // Вместо разрушения всего корабля деактивировать уничтоженную часть
                    prtHit.go.SetActive(false);
                }
                // Проверить, был ли корабль полностью разрушен
                bool allDestroyed = true; // предположить, что разрушен
                foreach (Part prt in parts)
                {
                    if (!Destroyed(prt)) // если какая-то часть еще существует...
                    {
                        allDestroyed = false; // ...записать false в allDestroyed...
                        break; // ...и прервать цикл foreach
                    }
                }
                if (allDestroyed) // если корабль разрушен полностью...
                {
                    // ...уведомить объект-одиночку Main, что этот корабль разрушен
                    Main.S.ShipDestroyed(this);
                    // Уничтожить этот объект Enemy
                    Destroy(this.gameObject);
                }
                Destroy(other); // уничтожить снаряд ProjectileHero
                break;
        }
    }
}