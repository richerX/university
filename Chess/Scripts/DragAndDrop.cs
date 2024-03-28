using System.Collections.Generic;
using UnityEngine;
using static Cell;
using static Game;
using static Highlight;
using static Validator;
using static Constants;


public class DragAndDrop : MonoBehaviour
{
    bool moving;
    Cell startCell;

    // Нажал клавишу
    private void OnMouseDown()
    {
        if (ValidPiecePickUp())
        {
            moving = true;
            startCell = GetCell(transform);
            HighlightUpdate(transform);
        }
    }

    // Отпустил клавишу
    private void OnMouseUp()
    {
        if (ValidPiecePickUp())
        {
            moving = false;
            Centralize(transform);
            var move = new Move(transform, startCell, GetCell(transform));
            move.RunMove();
            HighlightClear();
        }
    }

    private void Update()
    {
        if (moving)
        {
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(newPosition);
        }
    }

    private bool ValidPiecePickUp()
    {
        return gameIsOn && !piecesOnParking.Contains(transform.name);
    }
}
