using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Set Dynamically")]
    public string suit; // Масть карты (C, D, H, S)
    public int rank; // Достоинство карты (1-14)
    public Color color = Color.black; // Цвет значков
    public string colS = "Black"; // или "Red". Имя цвета
    // Этот список хранит все игровые объекты Decorator
    public List<GameObject> decoGOs = new List<GameObject>();
    // Этот список хранит все игровые объекты Pip
    public List<GameObject> pipGOs = new List<GameObject>();

    public GameObject back; // Игровой объект рубашки карты

    public CardDefinition def; // Извлекается из DeckXML.xml

    // Список компонентов SpriteRenderer этого и вложенных в него игровых объектов
    public SpriteRenderer[] spriteRenderers;

    // Реализация золотых карт
    public bool isGold;

    void Start()
    {
        SetSortOrder(0); // обеспечит правильную сортировку карт
    }

    // Если spriteRenderers не определен, эта функция определит его
    public void PopulateSpriteRenderers()
    {
        // Если spriteRenderers содержит null или пустой список
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            // Получить компоненты SpriteRenderer этого игрового объекта и вложенных
            // в него игровых объектов
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    // Инициализирует поле sortingLayerName во всех компонентах SpriteRenderer
    public void SetSortingLayerName(string tSLN)
    {
        PopulateSpriteRenderers();

        foreach (SpriteRenderer tSR in spriteRenderers)
        {
            tSR.sortingLayerName = tSLN;
        }
    }

    // Инициализирует поле sortingOrder всех компонентов SpriteRenderer
    public void SetSortOrder(int sOrd)
    {
        PopulateSpriteRenderers();

        // Выполнить обход всех элементов в списке spriteRenderers
        foreach (SpriteRenderer tSR in spriteRenderers)
        {
            if (tSR.gameObject == this.gameObject)
            {
                // Если компонент принадлежит текущему игровому объекту, это фон
                tSR.sortingOrder = sOrd; // установить порядковый номер для
                                         // сортировки в sOrd
                continue; // и перейти к следующей итерации цикла
            }

            // Каждый дочерний игровой объект имеет имя
            // Установить порядковый номер для сортировки, в зависимости от имени
            switch (tSR.gameObject.name)
            {
                case "back": // если имя "back"
                    // Установить наибольший порядковый номер для отображения
                    // поверх других спрайтов
                    tSR.sortingOrder = sOrd + 2;
                    break;

                case "face": // если имя "face"
                default: // или же другое
                    // Установить промежуточный порядковый номер для отображения поверх фона
                    tSR.sortingOrder = sOrd + 1;
                    break;
            }
        }
    }

    public bool faceUp
    {
        get { return (!back.activeSelf); }
        set { back.SetActive(!value); }
    }

    // Виртуальные методы могут переопределяться в подклассах определением методов
    // с теми же именами
    public virtual void OnMouseUpAsButton()
    {
        print(name); // по щелчку эта строка выведет имя карты
    }
}

[System.Serializable] // Сериализуемый класс доступен для правки в инспекторе
public class Decorator
{
    // Этот класс хранит информацию из DeckXML о каждом значке на карте
    public string type; // Значок, определяющий достоинство карты, имеет type = "pip"
    public Vector3 loc; // Местоположение спрайта на карте
    public bool flip = false; // Признак переворота спрайта по вертикали
    public float scale = 1f; // масштаб спрайта
}

[System.Serializable]
public class CardDefinition
{
    // Этот класс хранит информацию о достоинстве карты
    public string face; // Спрайт, изображающий лицевую сторону карты
    public int rank; // Достоинство карты
    public List<Decorator> pips = new List<Decorator>(); // Значки на карте (черви, пики)
}