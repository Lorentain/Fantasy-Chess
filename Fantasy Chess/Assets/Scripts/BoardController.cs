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

        boardState.rows[8].columnCells[14].character = CharacterType.None;
        boardState.rows[8].columnCells[15].character = CharacterType.None;
        boardState.rows[8].columnCells[16].character = CharacterType.None;
        boardState.rows[8].columnCells[14].player = 2;
        boardState.rows[8].columnCells[15].player = 2;
        boardState.rows[8].columnCells[16].player = 2;
        BoardRepresentation(boardState);
    }

    void Update()
    {

    }

    private void BoardRepresentation(BoardState boardToRepresent)
    {
        ClearBoard();
        for (int i = 0; i < boardToRepresent.rows.Count; i++)
        {
            for (int j = 0; j < boardToRepresent.rows[i].columnCells.Count; j++)
            {
                switch (GetCharacter(j, i))
                {
                    case CharacterType.Caballero:
                        {
                            if (boardToRepresent.rows[i].columnCells[j].player == 1)
                            {
                                Instantiate(characterKnightP1, new Vector3(j, -i, 0), Quaternion.identity, transform);
                            }
                            else if (boardToRepresent.rows[i].columnCells[j].player == 2)
                            {
                                Instantiate(characterKnightP2, new Vector3(j, -i, 0), Quaternion.identity, transform);
                            }
                            break;
                        }
                    case CharacterType.Mago:
                        {
                            if (boardToRepresent.rows[i].columnCells[j].player == 1)
                            {
                                Instantiate(characterMageP1, new Vector3(j, -i, 0), Quaternion.identity, transform);
                            }
                            else if (boardToRepresent.rows[i].columnCells[j].player == 2)
                            {
                                Instantiate(characterMageP2, new Vector3(j, -i, 0), Quaternion.identity, transform);
                            }
                            break;
                        }
                    case CharacterType.Ogro:
                        {
                            if (boardToRepresent.rows[i].columnCells[j].player == 1)
                            {
                                Instantiate(characterOgreP1, new Vector3(j, -i, 0), Quaternion.identity, transform);
                            }
                            else if (boardToRepresent.rows[i].columnCells[j].player == 2)
                            {
                                Instantiate(characterOgreP2, new Vector3(j, -i, 0), Quaternion.identity, transform);
                            }
                            break;
                        }
                    case CharacterType.Interactable:
                        {
                            Instantiate(characterInteractable, new Vector3(j, -i, 0), Quaternion.identity, transform);
                            break;
                        }
                }
            }
        }
    }

    public static void UpdateCell(int x, int y)
    {
        bool validPlay = false;

        if (instance.boardState.playerTurn == 3)
        {
            switch (instance.boardState.cellChooseP1.x, instance.boardState.cellChooseP1.y)
            {
                case (2, 1):
                    {
                        instance.boardState.rows[y].columnCells[x].character = CharacterType.Mago;
                        break;
                    }
                case (3, 1):
                    {
                        instance.boardState.rows[y].columnCells[x].character = CharacterType.Ogro;
                        break;
                    }
                case (4, 1):
                    {
                        instance.boardState.rows[y].columnCells[x].character = CharacterType.Caballero;
                        break;
                    }
            }
            validPlay = true;
        }
        else if (instance.boardState.playerTurn == 4)
        {
            switch (instance.boardState.cellChooseP2.x, instance.boardState.cellChooseP2.y)
            {
                case (16, 8):
                    {
                        instance.boardState.rows[y].columnCells[x].character = CharacterType.Mago;
                        break;
                    }
                case (15, 8):
                    {
                        instance.boardState.rows[y].columnCells[x].character = CharacterType.Ogro;
                        break;
                    }
                case (14, 8):
                    {
                        instance.boardState.rows[y].columnCells[x].character = CharacterType.Caballero;
                        break;
                    }
            }
            validPlay = true;
        }

        if (validPlay == true)
        {
            switch (instance.boardState.playerTurn)
            {
                case 1:
                case 3:
                    {
                        instance.boardState.rows[y].columnCells[x].player = 1;
                        if (instance.boardState.playerTurn == 4)
                        {
                            instance.boardState.playerTurn = 1;
                        }
                        else
                        {
                            instance.boardState.playerTurn++;
                        }
                        break;
                    }
                case 4:
                case 2:
                    {
                        instance.boardState.rows[y].columnCells[x].player = 2;
                        if (instance.boardState.playerTurn == 4)
                        {
                            instance.boardState.playerTurn = 1;
                        }
                        else
                        {
                            instance.boardState.playerTurn++;
                        }
                        break;
                    }
            }

            // Borro todo el tablero
            instance.ClearBoard();

            // Llama función para representar el tablero
            instance.BoardRepresentation(instance.boardState);
            Debug.Log("Tablero modificado");
        }
    }

    // Función que comprueba si el personaje se puede mover a la nueva posición
    public static void MovementCharacter(int x, int y) // x = 0 a 18, y = 0 a 10
    {
        if (instance.boardState.playerTurn == 3 && !(x == 2 && y == 1 || x == 3 && y == 1 || x == 4 && y == 1) && instance.boardState.rows[y].columnCells[x].character == CharacterType.Interactable)
        {
            MoveCharacter(x, y, instance.boardState.cellChooseP1, 1);
            Debug.Log("Movimiento de personaje turno 3");
        }
        else if (instance.boardState.playerTurn == 4 && !(x == 14 && y == 8 || x == 15 && y == 8 || x == 16 && y == 8) && instance.boardState.rows[y].columnCells[x].character == CharacterType.Interactable)
        {
            MoveCharacter(x, y, instance.boardState.cellChooseP2, 2);
            Debug.Log("Movimiento de personaje turno 4");
        }
        else
        {
            Debug.Log("Movimiento erroneo");
        }
        instance.BoardRepresentation(instance.boardState);
    }

    // Función que mueve el personaje a la nueva posición
    private static void MoveCharacter(int x, int y, Vector2Int cellChoose, int player)
    {
        if ((x == cellChoose.x + 1 && y == cellChoose.y) ||
            (x == cellChoose.x - 1 && y == cellChoose.y) ||
            (y == cellChoose.y + 1 && x == cellChoose.x) ||
            (y == cellChoose.y - 1 && x == cellChoose.x))
        {
            // Cambiar el personaje a la nueva posición
            instance.boardState.rows[y].columnCells[x].character = GetCharacter(cellChoose.x, cellChoose.y);
            instance.boardState.rows[y].columnCells[x].player = player;

            // Borrar el personaje de la posición anterior
            instance.boardState.rows[cellChoose.y].columnCells[cellChoose.x].character = CharacterType.Interactable;
            instance.boardState.rows[cellChoose.y].columnCells[cellChoose.x].player = 0;

            // Cambiar el turno
            if (player == 1)
            {
                instance.boardState.playerTurn++;
            }
            else
            {
                instance.boardState.playerTurn = 1;
            }
        }
    }

    // public static void AvailablePositionsToMove() {

    // }

    public static void ChooseCellPlayer(int x, int y)
    {
        if (instance.boardState.playerTurn == 1)
        {
            if (instance.boardState.rows[y].columnCells[x].character != CharacterType.Interactable && instance.boardState.rows[y].columnCells[x].player != 2)
            {
                instance.boardState.cellChooseP1 = new Vector2Int(x, y);
                instance.boardState.playerTurn++;
            }
            else
            {
                Debug.Log("Jugador 1 jugada erronea");
            }
        }
        else if (instance.boardState.playerTurn == 2)
        {
            if (instance.boardState.rows[y].columnCells[x].character != CharacterType.Interactable && instance.boardState.rows[y].columnCells[x].player != 1)
            {
                instance.boardState.cellChooseP2 = new Vector2Int(x, y);
                instance.boardState.playerTurn++;
            }
            else
            {
                Debug.Log("Jugador 2 jugada erronea");
            }
        }
    }

    // Función que comprueba si estás seleccionando para instanciar personaje o mover personaje
    public static void InstanceOrMove(int x, int y)
    {
        Debug.Log("Entro en instanceOrMove" + x + "," + y);

        if (instance.boardState.playerTurn == 3)
        {
            if (IsInstance(instance.boardState.cellChooseP1.x, instance.boardState.cellChooseP1.y) == true && (x == 2 && y == 2 || x == 3 && y == 2 || x == 4 && y == 2))
            {
                Debug.Log("Entra turno 3");
                UpdateCell(x, y);
            }
            else
            {
                Debug.Log("Jugador 1 jugada erronea de instancia de instancia");
            }

            if (IsCharacter(instance.boardState.cellChooseP1.x, instance.boardState.cellChooseP1.y) == true)
            {
                MovementCharacter(x, y);
                Debug.Log("Función de movimiento de personaje turno 3");
            }
            else
            {
                Debug.Log("Jugador 1 jugada erronea de instancia de movimiento");
            }
        }

        else if (instance.boardState.playerTurn == 4)
        {
            if (IsInstance(instance.boardState.cellChooseP2.x, instance.boardState.cellChooseP2.y) == true && (x == 14 && y == 7 || x == 15 && y == 7 || x == 16 && y == 7))
            {
                Debug.Log("Entra turno 4");
                UpdateCell(x, y);
            }
            else
            {
                Debug.Log("Jugador 2 jugada erronea de instancia");
            }

            if (IsCharacter(instance.boardState.cellChooseP2.x, instance.boardState.cellChooseP2.y) == true)
            {
                MovementCharacter(x, y);
                Debug.Log("Función de movimiento de personaje turno 4");
            }
            else
            {
                Debug.Log("Jugador 2 jugada erronea de movimiento");
            }
        }

        // if (instance.boardState.playerTurn == 3 && IsInstance(instance.boardState.cellChooseP1.x, instance.boardState.cellChooseP1.y) == true)
        // {
        //     Debug.Log("Entra turno 3");
        //     UpdateCell(x, y);
        // }
        // else if (instance.boardState.playerTurn == 4 && IsInstance(instance.boardState.cellChooseP2.x, instance.boardState.cellChooseP2.y) == true)
        // {
        //     Debug.Log("Entra turno 4");
        //     UpdateCell(x, y);
        // }

        // if (instance.boardState.playerTurn == 3 && IsCharacter(instance.boardState.cellChooseP1.x, instance.boardState.cellChooseP1.y) == true)
        // {
        //     // Llamar función de movimiento del personaje
        //     Debug.Log("Función de movimiento de personaje turno 3");
        // }
        // else if (instance.boardState.playerTurn == 4 && IsCharacter(instance.boardState.cellChooseP2.x, instance.boardState.cellChooseP2.y) == true)
        // {
        //     // Llamar función de movimiento del personaje
        //     Debug.Log("Función de movimiento de personaje turno 4");
        // }
        //return aux;
    }

    // Función que prueba si la casilla es de instanciar
    public static bool IsInstance(int x, int y)
    {
        Debug.Log($"{x},{y}");
        bool aux = false;
        Debug.Log(GetCharacter(x, y) + "," + instance.boardState.rows[y].columnCells[x].player);
        if (GetCharacter(x, y) == CharacterType.None && instance.boardState.rows[y].columnCells[x].player != 0)
        {
            aux = true;
        }
        return aux;
    }

    public static bool IsCharacter(int x, int y)
    {
        bool aux = false;

        if (GetCharacter(x, y) != CharacterType.Interactable && GetCharacter(x, y) != CharacterType.None)
        {
            aux = true;
        }

        return aux;
    }

    private void ClearBoard()
    {
        foreach (Transform transform in instance.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    public static int GetPlayerTurn()
    {
        return instance.boardState.playerTurn;
    }

    public static CharacterType GetCharacter(int x, int y)
    {
        CharacterType res = CharacterType.None;
        if (instance.boardState.rows[y].columnCells[x].character != CharacterType.None)
        {
            res = instance.boardState.rows[y].columnCells[x].character;
        }
        return res;
    }

    // public static string GetPlayerWin() {
    //     if(instance.boardState.rows[2].columnCells[1].character != CharacterType.None || instance.boardState.rows[3].columnCells[1].character != CharacterType.Interactable) {
    //         return "Player 1 wins";
    //     }

    //     if(instance.boardState.rows[7].columnCells[16].character != CharacterType.None || instance.boardState.rows[8].columnCells[16].character != CharacterType.Interactable) {
    //         return "Player 2 wins";
    //     }
    // }
}
