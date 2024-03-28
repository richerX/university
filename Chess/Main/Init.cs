using UnityEngine;
using static Constants;
using static Field;
using static Cell;
using static PossibleMoves;


public class Init : MonoBehaviour
{
    private void Awake()
    {
        CreateUI();
        CreateBoardUI();
        FillInMoves();
    }

    private void Start()
    {
        int[] startRows = new int[4] { 7, 6, 1, 0 };
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < initialRows[i].Count; j++)
            {
                Piece currentPiece = initialRows[i][j];
                GameObject gameObject = GameObject.Find(currentPiece.ToString());
                gameObject.transform.position = new Vector3(initialCorner + delta * j, initialCorner + delta * startRows[i], 0);
                Centralize(gameObject.transform);
                AddPieceToField(currentPiece, GetCell(gameObject.transform));
            }
        }
    }

    private void Update()
    {
        
    }
}
