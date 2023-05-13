using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    //================================ Функции для работы с материалами ================================\\

    // Возвращает список всех материалов в данном игровом объекте и его дочерних объектах
    // Метод можно вызвать из любой части кода в проекте как Utils.GetAllMaterials()
    static public Material[] GetAllMaterials(GameObject go)
    {
        // Выполняет обход компонентов игрового объекта и всех его дочерних компонентов
        Renderer[] rends = go.GetComponentsInChildren<Renderer>();

        List<Material> mats = new List<Material>();

        foreach (Renderer rend in rends)
        {
            mats.Add(rend.material);
        }

        return (mats.ToArray()); // лист mats преобразуется в массив
    }
}