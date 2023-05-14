using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Класс ScoreBoard управляет отображением очков игрока
public class ScoreBoard : MonoBehaviour
{
    static public ScoreBoard S;

    [Header("Set in Inspector")]
    public GameObject prefabFloatingScore;

    [Header("Set Dynamically")]
    [SerializeField] private int _score = 0;
    [SerializeField] private string _scoreString;

    private Transform canvasTrans;

    // Cвойство score также устанавливает scoreString
    public int score
    {
        get { return (_score); }
        set
        {
            _score = value;
            scoreString = _score.ToString("N0");
        }
    }

    // Свойство scoreString также устанавливает Text.text
    public string scoreString
    {
        get { return (_scoreString); }
        set
        {
            _scoreString = value;
            GetComponent<Text>().text = _scoreString;
        }
    }

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("ERROR: Scoreboard.Awake(): S is already set!");
        }
        canvasTrans = transform.parent;
    }

    // Когда вызывается методом SendMessage, прибавляет fs.score к this.score
    public void FSCallBack(FloatingScore fs)
    {
        if (Prospector.wasUndoFs)
        {
            score += (fs.score - ScoreManager.SCORE_DELTA);
            Prospector.wasUndoFs = false;
        }
        else
        {
            score += fs.score;
        }

        print("Float result: " + score + " fs.score: " + fs.score);
    }

    // Создает и инициализирует новый игровой объект FloatingScore
    // Возвращает указатель на созданный экземпляр FloatingScore, чтобы вызывающая функция
    // могла выполнить с ним дополнительные операции (например, определять список
    // fontSizes и т.д.)
    public FloatingScore CreateFloatingScore(int amt, List<Vector2> pts)
    {
        GameObject go = Instantiate<GameObject>(prefabFloatingScore);
        go.transform.SetParent(canvasTrans);
        FloatingScore fs = go.GetComponent<FloatingScore>();
        fs.score = amt;
        fs.reportFinishTo = this.gameObject; // настроить обратный вызов
        fs.Init(pts);
        return (fs);
    }
}