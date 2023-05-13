using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Перечисление со всеми возможными состояниями FloatingScore
public enum eFSState
{
    idle,
    pre,
    active,
    post
}

// FloatingScore может перемещаться на экране по траектории, которая определяется
// кривой Безье
public class FloatingScore : MonoBehaviour
{
    [Header("Set Dynamically")]
    public eFSState state = eFSState.idle;

    [SerializeField]
    protected int _score = 0;
    public string scoreString;

    // Свойство score устанавливает два поля, _score и scoreString
    public int score
    {
        get { return (_score); }
        set
        {
            _score = value;
            scoreString = _score.ToString("N0"); // аргумент "N0" требует добавить
                                                 // точки в число
            GetComponent<Text>().text = scoreString;
        }
    }

    public List<Vector2> bezierPts; // Точки, определяющие кривую Безье
    public List<float> fontSizes; // Точки кривой Безье для масштабирования шрифта
    public float timeStart = -1f;
    public float timeDuration = 1f;
    public string easingCurve = Easing.InOut; // Функция сглаживания из Utils.cs

    // Игровой объект, для которого будет вызван метод SendMessage, когда этот
    // экземпляр FloatingScore закончит движение
    public GameObject reportFinishTo = null;

    private RectTransform rectTrans;
    private Text txt;

    // Настроить FloatingScore и параметры движения
    // Обратить внимание, что для параметров eTimeS и eTimeD определены значения по умолчанию
    public void Init(List<Vector2> ePts, float eTimeS = 0, float eTimeD = 1)
    {
        rectTrans = GetComponent<RectTransform>();
        rectTrans.anchoredPosition = Vector2.zero;

        txt = GetComponent<Text>();

        bezierPts = new List<Vector2>(ePts);

        if(ePts.Count == 1)
        {
            // Если задана только одна точка - переместиться в нее
            transform.position = ePts[0];
            return;
        }

        // Если eTimeS имеет значение по умолчанию, запустить отсчет от текущего времени
        if (eTimeS == 0) eTimeS = Time.time;
        timeStart = eTimeS;
        timeDuration = eTimeD;

        state = eFSState.pre; // установить состояние pre - готовность начать движение
    }

    public void FSCallBack(FloatingScore fs)
    {
        // Когда SendMessage вызовет эту функцию, она должна добавить очки из вызвавшего
        // экземпляра FloatingScore
        score += fs.score;
    }

    private void Update()
    {
        // Если объект никуда не перемещается, просто выйти
        if (state == eFSState.idle) return;

        // Вычислить u на основе текущего времени и продолжительности движения
        // u изменяется от 0 до 1 (обычно)
        float u = (Time.time - timeStart) / timeDuration;
        // Использовать класс Easing из Utils для корректировки значения u
        float uC = Easing.Ease(u, easingCurve);
        if(u < 0)
        {
            // Если u < 0, объект не должен двигаться
            state = eFSState.pre;
            txt.enabled = false; // изначально скрыть число
        }
        else
        {
            if(u >= 1)
            {
                // Если u >= 1, выполняется движение
                // Установить uC = 1, чтобы не выйти за крайнюю точку
                state = eFSState.post;
                if(reportFinishTo != null)
                {
                    // Если игровой объект указан
                    // Использовать SendMessage для вызова метода FSCallBack и передачи
                    // ему текущего экземпляра в параметре
                    reportFinishTo.SendMessage("FSCallBack", this);
                    // После отправки сообщения уничтожить gameObject
                    Destroy(gameObject);
                }
                else
                {
                    // Если игровой объект не указан
                    // Не уничтожать текущий экземпляр, оставить его в покое
                    state = eFSState.idle;
                }
            }
            else
            {
                // Если 0 <= u < 1, то текущий экземпляр активен и движется
                state = eFSState.active;
                txt.enabled = true; // показать число очков
            }

            // Использовать кривую Безье для перемещения к заданной точке
            Vector2 pos = Utils.Bezier(uC, bezierPts);
            // Опорные точки RectTransform можно использовать для позиционирования объектов
            // пользовательского интерфейса относительно общего размера экрана
            rectTrans.anchorMin = rectTrans.anchorMax = pos;
            if(fontSizes != null && fontSizes.Count > 0)
            {
                // Если список fontSizes содержит значения
                // Скорректировать fontSize этого объекта GUIText
                int size = Mathf.RoundToInt(Utils.Bezier(uC, fontSizes));
                GetComponent<Text>().fontSize = size;
            }
        }
    }
}
