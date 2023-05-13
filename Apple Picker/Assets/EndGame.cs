using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Text scoreFinal; // Записывает конечный результат
    public Text newRecord;

    void Start()
    {
        UnityEngine.Cursor.visible = true;
    }

    void Update()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreFinal = scoreGO.GetComponent<Text>();
        scoreFinal.text = "Your Score: "+Basket.scoreGT.text;

        GameObject scoreGO1 = GameObject.Find("NewRecord");
        newRecord = scoreGO1.GetComponent<Text>();
        int endScore = int.Parse(Basket.scoreGT.text);
        if(HighScore.score > endScore)
        {
            newRecord.text = "";
        }
        else
        {
            newRecord.text = "New Record!";
        }
    }

    public void OnClick()
    {
        Basket.scoreNum = 0;
        SceneManager.LoadScene("_Scene_0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
