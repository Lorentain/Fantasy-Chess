using System;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Ogro, Caballero, Mago, Interactable, None }

[Serializable]
public class BoardCell
{

    public BoardCell(BoardCell other)
    {
        character = other.character;
        player = other.player;
    }

    public BoardCell() {}

    public CharacterType character;

    public int player;
}

[Serializable]
public class BoardRow
{
    public List<BoardCell> columnCells;

    public BoardRow() {}

    public BoardRow(List<BoardCell> columnCells)
    {
        this.columnCells = new List<BoardCell>(columnCells);
    }
}

[Serializable]
public class BoardState
{
    public BoardState(BoardState other)
    {
        rows = new List<BoardRow>(other.rows);
        playerTurn = other.playerTurn;
        cellChooseP1 = other.cellChooseP1;
        cellChooseP2 = other.cellChooseP2;
    }

    public BoardState() {}

    public List<BoardRow> rows;

    public int playerTurn = 1;

    public Vector2Int cellChooseP1;

    public Vector2Int cellChooseP2;

    public string GetPlayerWin()
    {
        string res = "No winner";
        if (rows[8].columnCells[14].player == 1 || rows[8].columnCells[15].player == 1 || rows[8].columnCells[16].player == 1)
        {
            res = "Player 1 wins";
        }

        if (rows[1].columnCells[2].player == 2 || rows[1].columnCells[3].player == 2 || rows[1].columnCells[4].player == 2)
        {
            res = "Player 2 wins";
        }
        return res;
    }

    public int GetHeuristic()
    {
        Debug.Log("Heuristica");
        int res = 0;
        if (rows[8].columnCells[14].player == 1 || rows[8].columnCells[15].player == 1 || rows[8].columnCells[16].player == 1)
        {
            res = int.MaxValue;
        }

        if (rows[1].columnCells[2].player == 2 || rows[1].columnCells[3].player == 2 || rows[1].columnCells[4].player == 2)
        {
            res = int.MinValue;
        }
        return res;
    }

    // Función que comprueba si el personaje se puede mover a la nueva posición
    public void MovementCharacter(int x, int y) // x = 0 a 18, y = 0 a 10
    {
        if ((playerTurn == 3)
            && !(x == 2 && y == 1 || x == 3 && y == 1 || x == 4 && y == 1)
            && rows[y].columnCells[x].character == CharacterType.Interactable
            || x == 14 && y == 8 || x == 15 && y == 8 || x == 16 && y == 8
            )
        {
            MoveCharacter(x, y, cellChooseP1, 1);
            Debug.Log(GetPlayerWin());
            Debug.Log("Movimiento de personaje turno 3");
        }
        else if ((playerTurn == 4)
            && !(x == 14 && y == 8 || x == 15 && y == 8 || x == 16 && y == 8)
            && rows[y].columnCells[x].character == CharacterType.Interactable
            || x == 2 && y == 1 || x == 3 && y == 1 || x == 4 && y == 1
            )
        {
            MoveCharacter(x, y, cellChooseP2, 2);
            Debug.Log(GetPlayerWin());
            Debug.Log("Movimiento de personaje turno 4");
        }
        else
        {
            Debug.Log("Movimiento erroneo");
        }
    }

    // Función que mueve el personaje a la nueva posición
    private void MoveCharacter(int x, int y, Vector2Int cellChoose, int player)
    {
        if ((x == cellChoose.x + 1 && y == cellChoose.y) ||
            (x == cellChoose.x - 1 && y == cellChoose.y) ||
            (y == cellChoose.y + 1 && x == cellChoose.x) ||
            (y == cellChoose.y - 1 && x == cellChoose.x))
        {
            // Cambiar el personaje a la nueva posición
            rows[y].columnCells[x].character = GetCharacter(cellChoose.x, cellChoose.y);
            rows[y].columnCells[x].player = player;

            // Borrar el personaje de la posición anterior
            rows[cellChoose.y].columnCells[cellChoose.x].character = CharacterType.Interactable;
            rows[cellChoose.y].columnCells[cellChoose.x].player = 0;

            // Cambiar el turno
            if (player == 1)
            {
                playerTurn++;
            }
            else
            {
                playerTurn = 1;
            }
        }
    }

    public void ChooseCellPlayer(int x, int y)
    {
        if (playerTurn == 1)
        {
            if (rows[y].columnCells[x].character != CharacterType.Interactable && rows[y].columnCells[x].player == 1)
            {
                cellChooseP1 = new Vector2Int(x, y);
                playerTurn++;
                IAController.IA(this);
            }
            else
            {
                Debug.Log("Jugador 1 jugada erronea");
            }
        }
        else if (playerTurn == 2)
        {
            if (rows[y].columnCells[x].character != CharacterType.Interactable && rows[y].columnCells[x].player == 2)
            {
                cellChooseP2 = new Vector2Int(x, y);
                playerTurn++;
            }
            else
            {
                Debug.Log("Jugador 2 jugada erronea");
            }
        }
    }

    public void UpdateCell(int x, int y)
    {
        bool validPlay = false;

        if (playerTurn == 3)
        {
            switch (cellChooseP1.x, cellChooseP1.y)
            {
                case (2, 1):
                    {
                        rows[y].columnCells[x].character = CharacterType.Mago;
                        break;
                    }
                case (3, 1):
                    {
                        rows[y].columnCells[x].character = CharacterType.Ogro;
                        break;
                    }
                case (4, 1):
                    {
                        rows[y].columnCells[x].character = CharacterType.Caballero;
                        break;
                    }
            }
            validPlay = true;
        }
        else if (playerTurn == 4)
        {
            switch (cellChooseP2.x, cellChooseP2.y)
            {
                case (16, 8):
                    {
                        rows[y].columnCells[x].character = CharacterType.Mago;
                        break;
                    }
                case (15, 8):
                    {
                        rows[y].columnCells[x].character = CharacterType.Ogro;
                        break;
                    }
                case (14, 8):
                    {
                        rows[y].columnCells[x].character = CharacterType.Caballero;
                        break;
                    }
            }
            validPlay = true;
        }

        if (validPlay == true)
        {
            switch (playerTurn)
            {
                case 1:
                case 3:
                    {
                        rows[y].columnCells[x].player = 1;
                        if (playerTurn == 4)
                        {
                            playerTurn = 1;
                        }
                        else
                        {
                            playerTurn++;
                        }
                        break;
                    }
                case 4:
                case 2:
                    {
                        rows[y].columnCells[x].player = 2;
                        if (playerTurn == 4)
                        {
                            playerTurn = 1;
                        }
                        else
                        {
                            playerTurn++;
                        }
                        break;
                    }
            }
        }
    }

    // Función que comprueba si estás seleccionando para instanciar personaje o mover personaje
    public void InstanceOrMove(int x, int y)
    {
        Debug.Log("Entro en instanceOrMove" + x + "," + y);

        if (playerTurn == 3)
        {
            bool aux = false;
            if (IsInstance(cellChooseP1.x, cellChooseP1.y) == true && (x == 2 && y == 2 || x == 3 && y == 2 || x == 4 && y == 2))
            {
                Debug.Log("Entra turno 3");
                UpdateCell(x, y);
                aux = true;
            }
            else
            {
                Debug.Log("Jugador 1 jugada erronea de instancia de instancia");
            }

            if (IsCharacter(cellChooseP1.x, cellChooseP1.y) == true)
            {
                Debug.Log("Función de movimiento de personaje turno 3");
                MovementCharacter(x, y);
                aux = true;
            }
            else
            {
                Debug.Log("Jugador 1 jugada erronea de instancia de movimiento");
            }
            if (aux == true)
            {
                IAController.IA(this);
            }
        }

        else if (playerTurn == 4)
        {
            if (IsInstance(cellChooseP2.x, cellChooseP2.y) == true && (x == 14 && y == 7 || x == 15 && y == 7 || x == 16 && y == 7))
            {
                Debug.Log("Entra turno 4");
                UpdateCell(x, y);
            }
            else
            {
                Debug.Log("Jugador 2 jugada erronea de instancia");
            }

            if (IsCharacter(cellChooseP2.x, cellChooseP2.y) == true)
            {
                MovementCharacter(x, y);
                Debug.Log("Función de movimiento de personaje turno 4");
            }
            else
            {
                Debug.Log("Jugador 2 jugada erronea de movimiento");
            }
        }
    }

    // Función que prueba si la casilla es de instanciar
    public bool IsInstance(int x, int y)
    {
        Debug.Log($"{x},{y}");
        bool aux = false;
        Debug.Log(GetCharacter(x, y) + "," + rows[y].columnCells[x].player);
        if (GetCharacter(x, y) == CharacterType.None && rows[y].columnCells[x].player != 0)
        {
            aux = true;
        }
        return aux;
    }

    public bool IsCharacter(int x, int y)
    {
        bool aux = false;

        if (GetCharacter(x, y) != CharacterType.Interactable && GetCharacter(x, y) != CharacterType.None)
        {
            aux = true;
        }

        return aux;
    }

    public CharacterType GetCharacter(int x, int y)
    {
        CharacterType res = CharacterType.None;
        if (rows[y].columnCells[x].character != CharacterType.None)
        {
            res = rows[y].columnCells[x].character;
        }
        return res;
    }

    public bool IsValidPlay(int x, int y, BoardState boardState)
    {
        bool res = false;

        switch (boardState.playerTurn)
        {
            case 1:
                {
                    if (boardState.rows[y].columnCells[x].character != CharacterType.Interactable && boardState.rows[y].columnCells[x].player == 1)
                    {
                        Debug.Log($"Caso 1, {x},{y}");
                        res = true;
                    }
                    break;
                }
            case 2:
                {
                    if (boardState.rows[y].columnCells[x].character != CharacterType.Interactable && boardState.rows[y].columnCells[x].player == 2)
                    {
                        Debug.Log("Caso 2, " + x + "," + y);
                        res = true;
                    }
                    break;
                }
            case 3:
                {
                    if (IsInstance(cellChooseP1.x, cellChooseP1.y) == true && (x == 2 && y == 2 || x == 3 && y == 2 || x == 4 && y == 2))
                    {
                        Debug.Log("Caso 3, " + x + "," + y);
                        res = true;
                    }
                    else if (IsCharacter(cellChooseP1.x, cellChooseP1.y) == true)
                    {
                        if (!(x == 2 && y == 1 || x == 3 && y == 1 || x == 4 && y == 1)
                            && rows[y].columnCells[x].character == CharacterType.Interactable
                            || x == 14 && y == 8 || x == 15 && y == 8 || x == 16 && y == 8)
                        {
                            Debug.Log("Caso 3, " + x + "," + y);
                            res = true;
                        }
                    }
                    break;
                }
            case 4:
                {
                    if(IsInstance(cellChooseP2.x, cellChooseP2.y) == true && (x == 14 && y == 7 || x == 15 && y == 7 || x == 16 && y == 7))
                    {
                        Debug.Log("Caso 4, " + x + "," + y);
                        res = true;
                    }
                    else if (IsCharacter(cellChooseP2.x, cellChooseP2.y) == true)
                    {
                        if (!(x == 14 && y == 8 || x == 15 && y == 8 || x == 16 && y == 8)
                            && rows[y].columnCells[x].character == CharacterType.Interactable
                            || x == 2 && y == 1 || x == 3 && y == 1 || x == 4 && y == 1)
                        {
                            Debug.Log("Caso 4, " + x + "," + y);
                            res = true;
                        }
                    }
                    break;
                }
        }

        return res;
    }

    public BoardState ApplyPlay(int x, int y)
    {
        BoardState res = new BoardState(this);
        if (playerTurn == 1 || playerTurn == 2)
        {
            ChooseCellPlayer(x, y);
        }
        else if (playerTurn == 3 || playerTurn == 4)
        {
            InstanceOrMove(x, y);
        }
        return res;
    }
}
