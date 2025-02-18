using System;
using System.Collections.Generic;

public enum CharacterType {Ogro, Caballero, Mago, None}

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

    public int playerTurn;

    public CharacterType nextCharacter;

    
}
