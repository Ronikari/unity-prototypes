using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    static public Text scoreGT;
    static public int scoreNum;

    void Start()
    {
        // Получить ссылку на игровой объект ScoreCounter
        GameObject scoreGO = GameObject.Find("ScoreCounter"); // GameObject.Find() отыскивает в СЦЕНЕ объект с именем GameObject и возвращает ссылку на него
        // Получить компонент Text этого игрового объекта
        scoreGT = scoreGO.GetComponent<Text>();
        // Установить начальное число очков равным 0
        scoreGT.text = "0";
    }

    void Update()
    {
        // Получить текущие координаты указателя мыши на экране из Input
        Vector3 mousePos2D = Input.mousePosition; // Координаты положения курсора мыши на экране

        // Координата Z камеры определяет, как далеко в трехмерном пространстве находится указатель мыши
        mousePos2D.z = -Camera.main.transform.position.z; // Т.к. Z камеры равен -10, то Z mousePos2D = 10

        // Преобразовать точку на двумерной плоскости экрана в трехмерные координаты игры
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // Действие происходит в трехмерном пространстве, потому корзина находится в координате Z=0 

        // Переместить корзину вдоль оси Х в координату Х указателя мыши
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll) // Метод вызывается всякий раз, когда другой объект сталкивается с исходным
    {
        // Отыскать яблоко, попавшее в эту корзину
        GameObject collidedWith = coll.gameObject; // Переменной collidedWith присваивается ссылка на объект, столкнувшийся с корзиной
        if(collidedWith.tag=="Apple") // С помощью tag проверяется, является ли объект столкновения экземпляром Apple
        {
            Destroy(collidedWith);
            scoreNum += 5;
        }
        if (collidedWith.tag == "Bomb") // С помощью tag проверяется, является ли объект столкновения экземпляром Apple
        {
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>(); // Обращение к C# скрипту ApplePicker, привязанного к Main Camera
            // Вызвать общедоступный метод AppleDestroyed() из apScript
            apScript.BombDestroyed();
            SceneManager.LoadScene("_Scene_EndGame");
        }

        // Преобразовать текст в scoreGT в целое число
        int score = int.Parse(scoreGT.text); // string to integer32
        // Добавить очки за пойманное яблоко
        score += 5;
        //AppleTree.lvl = score;
        // Преобразовать число очков в строку и вывести ее на экран
        scoreGT.text = score.ToString(); // integer32 to string

        // Запомнить высшее достижение
        if(score > HighScore.score)
        {
            HighScore.score = score;
        }
    }
}
