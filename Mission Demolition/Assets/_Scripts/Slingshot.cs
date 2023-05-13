using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f; // Скорость снаряда

    // Поля, устанавливаемые динамически
    [Header("Set dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    void Awake() // Скрипт необходим для обхода ограничения Unity, не позволяющего программно воздействовать на компонент Halo
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject; // Создает объект для обхода ограничения
        launchPoint.SetActive(false); // По умолчанию - неактивен
        launchPos = launchPointTrans.position;
    }
    private void OnMouseEnter()
    {
        launchPoint.SetActive(true); // Активен только при наведении
        //print("Slingshot:OnMouseEnter()");
    }
    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
        //print("Slingshot:OnMouseExit()");
    }

    private void OnMouseDown()
    {
        // Игрок нажал кнопку мыши, когда указатель находился над рогаткой
        aimingMode = true; // Активируется в одном кадре, следующем сразу за нажатием кнопки мыши над объектом коллайдера рогатки
        // Создать снаряд
        projectile = Instantiate(prefabProjectile) as GameObject; // Создает ОДИН экземпляр из префаба
        // Поместить в точку launchPoint
        projectile.transform.position = launchPos; // Помещает в точку launchPos
        // Сделать его кинематическим
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true; // На кинематическое тело не действуют динамические силы, но все равно оно является физическим телом
    }

    void Update()
    {
        // Если рогатка не в режиме прицеливания, не выполнять этот код
        if (!aimingMode) return;

        // Получить текущие экранные координаты указателя мыши
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Найти разность координат между launchPos и mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;
        // Ограничить mouseDelta радиусом коллайдера объекта Slingshot
        float maxMagnitude = this.GetComponent<SphereCollider>().radius; // Радиус допустимого коллайдера, в котором существует создаваемый снаряд
        if(mouseDelta.magnitude > maxMagnitude) // Если дельта превышает радиус коллайдера...
        {
            mouseDelta.Normalize(); // ... привести вектор дельты к единичной длине...
            mouseDelta *= maxMagnitude; // ... и умножить на радиус коллайдера (максимальная дельта)
        }

        // Передвинуть снаряд в новую позицию
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if(Input.GetMouseButtonUp(0))
        {
            // Кнопка мыши отпущена
            aimingMode = false;
            projectileRigidbody.isKinematic = false; // На снаряд начинают действовать законы физики
            projectileRigidbody.velocity = -mouseDelta * velocityMult; // Минус необходим потому, что конечные координаты Х и Y меньше, чем у launchPos
            FollowCam.POI = projectile;
            projectile = null; // Эта строка освобождает поле projectile для записи в него следующего префаба
            MissionDemolition.ShotsFired();
            ProjectileLine.S.poi = projectile;
        }
    }
}
