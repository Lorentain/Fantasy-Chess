using System;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType {Ogro, Caballero, Mago, Interactable, None}

[Serializable]
public class BoardCell
{
    public CharacterType character;

    public int player;
}

[Serializable]
public class BoardRow {
    public List<BoardCell> columnCells;
}

[Serializable]
public class BoardState
{
    public List<BoardRow> rows;

    public int playerTurn = 1;

    public Vector2Int cellChooseP1;

    public Vector2Int cellChooseP2;
    
}
