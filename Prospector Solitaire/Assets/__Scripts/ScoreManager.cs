using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Перечисление со всеми возможными событиями начисления очков
public enum eScoreEvent
{
    draw,
    undo,
    mine,
    mineGold,
    gameWin,
    gameLoss
}

// ScoreManager управляет подсчетом очков
public class ScoreManager : MonoBehaviour
{
    static private ScoreManager S;

    static public int SCORE_FROM_PREV_ROUND = 0;
    static public int HIGH_SCORE = 0;
    static public int SCORE_BONUS = 0;
    static public int ROUND_LEVEL = 1;
    static public int MULTIPLIER = 1;
    static public int SCORE_DELTA = 0; // Переменная запоминает очки в последнем ходе, чтобы передать их в Prospector.Undo()

    [Header("Set Dynamically")]
    // Поля для хранения информации о заработанных очках
    public int chain = 0;
    public int scoreRun = 0;
    public int score = 0;

    void Awake()
    {
        if (S == null)
        {
            S = this; // подготовка скрытого объекта-одиночки
        }
        else
        {
            Debug.LogError("ERROR: ScoreManager.Awake(): S is already set!");
        }

        HIGH_SCORE = UpdateHighScore();
        // Добавить очки, заработанные в последнем раунде, которые должны быть >0,
        // если раунд завершился победой
        score += SCORE_FROM_PREV_ROUND + SCORE_BONUS;
        // И сбросить SCORE_FROM_PREV_ROUND
        SCORE_FROM_PREV_ROUND = 0;
        SCORE_BONUS = 0;
        SCORE_DELTA = 0;
    }

    static public int UpdateHighScore()
    {
        // Проверить рекорд в PlayerPrefs
        int highScore;
        if (PlayerPrefs.HasKey("ProspectorHighScore"))
        {
            return highScore = PlayerPrefs.GetInt("ProspectorHighScore");
            //Debug.Log($"Current High Score is: {HIGH_SCORE}");
        }
        else return 0;
    }

    static public void EVENT(eScoreEvent evt)
    {
        try
        {
            // try-catch не позволит ошибке аварийно завершить программу
            S.Event(evt);
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:EVENT() called while S=null.\n" + nre);
        }
    }

    void Event(eScoreEvent evt)
    {
        switch (evt)
        {
            // В случае победы, проигрыша и завершения хода выполняются одни и те же действия
            case eScoreEvent.draw: // Выбор свободной карты
            case eScoreEvent.gameWin: // Победа в раунде
            case eScoreEvent.gameLoss: // Проигрыш в раунде
                chain = 0; // сбросить цепочку подсчета очков
                score += scoreRun; // добавить scoreRun к общему числу очков
                scoreRun = 0; // сбросить scoreRun
                MULTIPLIER = 1;
                SCORE_DELTA = 0;
                break;

            case eScoreEvent.mine: // Удаление карты из основной раскладки
                chain++; // увеличить количество очков в цепочке
                scoreRun += chain * MULTIPLIER; // добавить очки за карту
                SCORE_DELTA = chain;
                break;

            case eScoreEvent.mineGold: // Удаление золотой карты из основной раскладки
                chain++;
                scoreRun *= (2 * MULTIPLIER); // удвоить заработанное количество очков
                SCORE_DELTA = scoreRun / 2;
                break;

            case eScoreEvent.undo: // Если произошла отмена последнего хода
                chain = 0;
                score += (scoreRun - SCORE_DELTA);
                scoreRun = 0;
                MULTIPLIER = 1;
                break;
        }

        // Эта вторая инструкция switch обрабатывает победу и проигрыш в раунде
        switch (evt)
        {
            case eScoreEvent.gameWin:
                // В случае победы перенести очки в следующий раунд
                // Статические поля НЕ сбрасываются методом SceneManager.LoadScene()
                SCORE_FROM_PREV_ROUND = score;
                print("You won this round! Round score: " + score);
                break;

            case eScoreEvent.gameLoss:
                // В случае проигрыша сравнить с рекордом
                if (HIGH_SCORE <= score)
                {
                    print("You got the high score! High score: " + score);
                    HIGH_SCORE = score;
                    PlayerPrefs.SetInt("ProspectorHighScore", score);
                }
                else
                {
                    print("Your final score for the game was: " + score);
                }
                break;

            default:
                print("score: " + score + " scoreRun:" + scoreRun + " chain:" + chain + " ScoreDelta: " + SCORE_DELTA);
                break;
        }
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("ProspectorHighScore", score);
    }

    static public int CHAIN { get { return S.chain; } }
    static public int SCORE { get { return S.score; } }
    static public int SCORE_RUN { get { return S.scoreRun / 2; } }
}