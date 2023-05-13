using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static public int score = 1000;

    void Awake() // Вызывается при создании экземпляра класса HighScore ( перед Start() )
    {
        // Если значение HighScore уже существует в PlayerPrefs, прочитать его
        if(PlayerPrefs.HasKey("HighScore")) // PlayerPrefs - словарь-хранилище информации из сценариев проекта
        {
            score = PlayerPrefs.GetInt("HighScore"); // Здесь HighScore - ключ, по которому осуществляется проверка наличия HighScore в словаре и возвращает его в переменную при наличии
        }
        // Сохранить высшее достижение HighScore в хранилище
        PlayerPrefs.SetInt("HighScore", score);
    }

    void Update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = "High Score: " + score;
        // Обновить HighScore в PlayerPrefs, если необходимо
        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
