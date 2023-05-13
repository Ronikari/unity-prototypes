using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    void Start()
    {
        UnityEngine.Cursor.visible = false;

        basketList = new List<GameObject>();
        for(int i=0; i<numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleDestroyed()
    {
        // Удалить все упавшие яблоки
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple"); // Находятся все существующие объекты с тегом Apple и вносятся в массив tAppleArray
        // Метод FindGameObjectsWithTag() замедляет работу программы, потому может применяться лишь в определенных случаях
        foreach(GameObject tGO in tAppleArray)
        {
            Destroy(tGO); // Цикл foreach обходит весь массив и удаляет его составляющие
        }

        GameObject[] tBombArray = GameObject.FindGameObjectsWithTag("Bomb"); // Находятся все существующие объекты с тегом Bomb и вносятся в массив tBombArray
        // Метод FindGameObjectsWithTag() замедляет работу программы, потому может применяться лишь в определенных случаях
        foreach (GameObject tGO in tBombArray)
        {
            Destroy(tGO); // Цикл foreach обходит весь массив и удаляет его составляющие
        }

        // Удалить одну корзину
        // Получить индекс последней корзины в basketList
        int basketIndex = basketList.Count - 1;
        // Получить ссылку на это игровой объект Basket
        GameObject tBasketGO = basketList[basketIndex];
        // Исключить корзину из списка и удалить сам игровой объект
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        // Если корзин не осталось, перезапустить игру
        if(basketList.Count==0)
        {
            SceneManager.LoadScene("_Scene_EndGame");
        }
    }

    public void BombDestroyed()
    {
        // Удалить все упавшие бомбы
        GameObject[] tBombArray = GameObject.FindGameObjectsWithTag("Bomb"); // Находятся все существующие объекты с тегом Bomb и вносятся в массив tBombArray
        // Метод FindGameObjectsWithTag() замедляет работу программы, потому может применяться лишь в определенных случаях
        foreach (GameObject tGO in tBombArray)
        {
            Destroy(tGO); // Цикл foreach обходит весь массив и удаляет его составляющие
        }

        // Удалить все корзины
        for(int i=0; i < basketList.Count; i++)
        {
            GameObject tBasketGO = basketList[i];
            // Исключить корзину из списка и удалить сам игровой объект
            basketList.RemoveAt(i);
            Destroy(tBasketGO);
        }
    }
}
