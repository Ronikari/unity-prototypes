using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Перечисление, определяющее тип переменной, которая может принимать несколько предопределенных значений
public enum eCardState
{
    drawpile,
    tableau,
    target,
    discard
}

public class CardProspector : Card
{
    [Header("Set Dynamically: CardProspector")]
    // Так как используется перечисление eCardState
    public eCardState state = eCardState.drawpile;
    // hiddenby - список других карт, не позволяющих перевернуть эту лицом вверх
    public List<CardProspector> hiddenBy = new List<CardProspector>();
    // layoutID определяет для этой карты ряд в раскладке
    public int layoutID;
    // Класс SlotDef хранит информацию из элемента <slot> в LayoutXML
    public SlotDef slotDef;

    // Определяет реакцию карт на щелчок мыши
    public override void OnMouseUpAsButton()
    {
        // Вызвать метод OnClicked объекта-одиночки Prospector
        Prospector.S.CardClicked(this);
        // а также версию этого метода в базовом классе (Card.cs)
        base.OnMouseUpAsButton();
    }
}