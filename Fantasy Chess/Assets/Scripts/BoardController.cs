using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private static BoardController instance;

    // Prefabs de los personajes
    [SerializeField] private BoardState boardState;

    [SerializeField] private int heightMap;

    [SerializeField] private int widthMap;

    [SerializeField] private GameObject characterMageP1;

    [SerializeField] private GameObject characterOgreP1; // Barbarian reference

    [SerializeField] private GameObject characterKnightP1;

    [SerializeField] private GameObject characterMageP2;

    [SerializeField] private GameObject characterOgreP2;

    [SerializeField] private GameObject characterKnightP2; // Archer reference

    [SerializeField] private GameObject characterInteractable;

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
        boardState = new BoardState { rows = new List<BoardRow>() };
        for (int i = 0; i < heightMap; i++)
        {
            boardState.rows.Add(new BoardRow { columnCells = new List<BoardCell>() });
            for (int j = 0; j < widthMap; j++)
            {
                boardState.rows[i].columnCells.Add(new BoardCell());
                boardState.rows[i].columnCells[j].character = CharacterType.Interactable;
                boardState.rows[i].columnCells[j].player = 0;
            }
        }
        boardState.rows[0].columnCells[2].character = CharacterType.None;
        boardState.rows[0].columnCells[3].character = CharacterType.None;
        boardState.rows[0].columnCells[4].character = CharacterType.None;

        boardState.rows[1].columnCells[2].player = 1;
        boardState.rows[1].columnCells[3].player = 1;
        boardState.rows[1].columnCells[4].player = 1;

        // Prueba
        // boardState.rows[1].columnCells[5].player = 2;
        // boardState.rows[1].columnCells[5].character = CharacterType.Ogro;

        boardState.rows[1].columnCells[2].character = CharacterType.None;
        boardState.rows[1].columnCells[3].character = CharacterType.None;
        boardState.rows[1].columnCells[4].character = CharacterType.None;

        boardState.rows[5].columnCells[4].character = CharacterType.None;
        boardState.rows[5].columnCells[5].character = CharacterType.None;
        boardState.rows[5].columnCells[6].character = CharacterType.None;

        boardState.rows[6].columnCells[5].character = CharacterType.None;

        boardState.rows[2].columnCells[12].character = CharacterType.None;
        boardState.rows[2].columnCells[13].character = CharacterType.None;
        boardState.rows[2].columnCells[14].character = CharacterType.None;

        boardState.rows[3].columnCells[13].character = CharacterType.None;

        boardState.rows[9].columnCells[14].character = CharacterType.None;
        boardState.rows[9].columnCells[15].character = CharacterType.None;
        boardState.rows[9].columnCells[16].character = CharacterType.None;

        // Prueba
        // boardState.rows[8].columnCells[13].character = CharacterType.Mago;
        // boardState.rows[8].columnCells[13].player = 1;

        boardState.rows[8].columnCells[14].character = CharacterType.None;
        boardState.rows[8].columnCells[15].character = CharacterType.None;
        boardState.rows[8].columnCells[16].character = CharacterType.None;
        boardState.rows[8].columnCells[14].player = 2;
        boardState.rows[8].columnCells[15].player = 2;
        boardState.rows[8].columnCells[16].player = 2;
        BoardRepresentation();
    }

    void Update()
    {

    }

    public static void BoardRepresentation()
    {
        instance.ClearBoard();
        for (int i = 0; i < instance.boardState.rows.Count; i++)
        {
            for (int j = 0; j < instance.boardState.rows[i].columnCells.Count; j++)
            {
                switch (instance.boardState.GetCharacter(j, i))
                {
                    case CharacterType.Caballero:
                        {
                            if (instance.boardState.rows[i].columnCells[j].player == 1)
                            {
                                Instantiate(instance.characterKnightP1, new Vector3(j, -i, 0), Quaternion.identity, instance.transform);
                            }
                            else if (instance.boardState.rows[i].columnCells[j].player == 2)
                            {
                                Instantiate(instance.characterKnightP2, new Vector3(j, -i, 0), Quaternion.identity, instance.transform);
                            }
                            break;
                        }
                    case CharacterType.Mago:
                        {
                            if (instance.boardState.rows[i].columnCells[j].player == 1)
                            {
                                Instantiate(instance.characterMageP1, new Vector3(j, -i, 0), Quaternion.identity, instance.transform);
                            }
                            else if (instance.boardState.rows[i].columnCells[j].player == 2)
                            {
                                Instantiate(instance.characterMageP2, new Vector3(j, -i, 0), Quaternion.identity, instance.transform);
                            }
                            break;
                        }
                    case CharacterType.Ogro:
                        {
                            if (instance.boardState.rows[i].columnCells[j].player == 1)
                            {
                                Instantiate(instance.characterOgreP1, new Vector3(j, -i, 0), Quaternion.identity, instance.transform);
                            }
                            else if (instance.boardState.rows[i].columnCells[j].player == 2)
                            {
                                Instantiate(instance.characterOgreP2, new Vector3(j, -i, 0), Quaternion.identity, instance.transform);
                            }
                            break;
                        }
                    case CharacterType.Interactable:
                        {
                            Instantiate(instance.characterInteractable, new Vector3(j, -i, 0), Quaternion.identity, instance.transform);
                            break;
                        }
                }
            }
        }
    }

    // public static void AvailablePositionsToMove() {

    // }

    private void ClearBoard()
    {
        foreach (Transform transform in instance.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    public static void GetInstanceOrMove(int x, int y)
    {
        instance.boardState.InstanceOrMove(x, y);
    }

    public static void GetChooseCellPlayer(int x, int y)
    {
        instance.boardState.ChooseCellPlayer(x, y);
    }

    public static int GetPlayerTurn()
    {
        return instance.boardState.playerTurn;
    }
}
