using System;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private static BoardController instance;

    [SerializeField] private BoardState boardState;

    [SerializeField] private GameObject characterMage;

    [SerializeField] private GameObject characterOgre;

    [SerializeField] private GameObject characterKnight;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        BoardRepresentation(boardState);
    }

    void Update()
    {

    }

    private void BoardRepresentation(BoardState boardToRepresent)
    {
        for (int i = 0; i < boardToRepresent.rows.Count; i++)
        {
            for (int j = 0; j < boardToRepresent.rows[i].columnCells.Count; j++)
            {
                switch (boardToRepresent.rows[i].columnCells[j].character)
                {
                    case CharacterType.Caballero:
                        {
                            Instantiate(characterKnight, new Vector3(i, -j, 0), Quaternion.identity);
                            break;
                        }
                    case CharacterType.Mago:
                        {
                            Instantiate(characterMage, new Vector3(i, -j, 0), Quaternion.identity);
                            break;
                        }
                    case CharacterType.Ogro:
                        {
                            Instantiate(characterOgre, new Vector3(i, -j, 0), Quaternion.identity);
                            break;
                        }
                }
            }
        }
    }

    public static void UpdateCell(int x, int y)
    {
        instance.boardState.rows[x].columnCells[y].character = CharacterType.Ogro;
        switch (instance.boardState.playerTurn)
        {
            case 1:
            case 3:
                {
                    instance.boardState.rows[x].columnCells[y].player = 1;
                    instance.boardState.playerTurn++;
                    break;
                }
            case 4:
            case 2:
                {
                    instance.boardState.rows[x].columnCells[y].player = 2;
                    instance.boardState.playerTurn++;
                    break;
                }
        }
        Debug.Log("Tablero modificado");
    }
}
