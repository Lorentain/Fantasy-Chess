using System.Collections.Generic;
using UnityEngine;

public class IAControllerTest : MonoBehaviour
{

    private static IAControllerTest instance;

    [SerializeField] private int MonteCarloIterations = 1;

    void Awake()
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

    public static void IA(BoardState boardState)
    {
        Debug.Log("Llamando a IA");
        AlfaBeta(boardState, 3, float.MinValue, float.MaxValue, out Vector2Int? bestPlay);
        if (BoardController.GetPlayerTurn() == 2)
        {
            boardState.ChooseCellPlayer(bestPlay.Value.x, bestPlay.Value.y);
        }

        if (BoardController.GetPlayerTurn() == 4)
        {
            boardState.InstanceOrMove(bestPlay.Value.x, bestPlay.Value.y);
        }
    }

    public static float AlfaBeta(BoardState boardState, int profundidad, float alpha, float beta)
    {
        if (boardState.GetPlayerWin() != "No winner" || profundidad == 0)
        {
            return boardState.GetHeuristic();
        }

        if (boardState.playerTurn == 2 || boardState.playerTurn == 4)
        {

            for (int j = 0; j < boardState.rows.Count; j++)
            {
                for (int i = 0; i < boardState.rows[j].columnCells.Count; i++)
                {
                    if (boardState.IsValidPlay(i, j, boardState))
                    {
                        Debug.Log("IsValid");
                        alpha = Mathf.Max(alpha, AlfaBeta(boardState.ApplyPlay(i, j), profundidad - 1, alpha, beta));
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return alpha;
        }
        else
        {
            for (int j = 0; j < boardState.rows.Count; j++)
            {
                for (int i = 0; i < boardState.rows[j].columnCells.Count; i++)
                {
                    if (boardState.IsValidPlay(i, j, boardState))
                    {
                        beta = Mathf.Min(beta, AlfaBeta(boardState.ApplyPlay(i, j), profundidad - 1, alpha, beta));
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return beta;
        }
    }

    public static float AlfaBeta(BoardState boardState, int profundidad, float alpha, float beta, out Vector2Int? bestPlay)
    {
        bestPlay = null;

        if (boardState.GetPlayerWin() != "No winner" || profundidad == 0)
        {
            return boardState.GetHeuristic();
        }

        if (boardState.playerTurn == 2 || boardState.playerTurn == 4)
        {
            for (int j = 0; j < boardState.rows.Count; j++)
            {
                for (int i = 0; i < boardState.rows[j].columnCells.Count; i++)
                {
                    if (boardState.IsValidPlay(i, j, boardState))
                    {
                        Debug.Log("IsValid");
                        float alphaAux = alpha;
                        alpha = Mathf.Max(alpha, AlfaBeta(boardState.ApplyPlay(i, j), profundidad - 1, alpha, beta));

                        if (alphaAux != alpha)
                        {
                            bestPlay = new Vector2Int(i, j);
                        }

                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return alpha;
        }
        else
        {
            for (int j = 0; j < boardState.rows.Count; j++)
            {
                for (int i = 0; i < boardState.rows[j].columnCells.Count; i++)
                {
                    if (boardState.IsValidPlay(i, j, boardState))
                    {
                        float betaAux = beta;
                        beta = Mathf.Min(beta, AlfaBeta(boardState.ApplyPlay(i, j), profundidad - 1, alpha, beta));

                        if (betaAux != beta)
                        {
                            bestPlay = new Vector2Int(i, j);
                        }

                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return beta;
        }
    }

    // Función de Monte Carlo 
    public static void MonteCarlo(BoardState boardState, int profundidad, out Vector2Int? bestPlay)
    {
        bestPlay = null;
        float bestScore = float.MinValue;

        foreach (var move in GetValidMoves(boardState))
        {
            float totalScore = 0;
            for (int i = 0; i < instance.MonteCarloIterations; i++)
            {
                BoardState simulatedBoard = boardState.ApplyPlay(move.x, move.y);
                if (SimulatedPlay(simulatedBoard, profundidad) == false)
                {
                    totalScore++;
                }
            }
            float avgScore = totalScore / instance.MonteCarloIterations;

            if (avgScore > bestScore)
            {
                bestScore = avgScore;
                bestPlay = move;
            }
        }
    }

    // Función que simula un tablero de juego
    // True = Player 1, False = Player 2 y null = Empate
    public static bool? SimulatedPlay(BoardState boardState, int limitSimulations)
    {
        bool? winner = null;
        List<Vector2Int> moves = GetValidMoves(boardState);

        if (moves.Count > 0 && limitSimulations > 0)
        {
            int randomIndex = Random.Range(0, moves.Count);
            Vector2Int randomMove = moves[randomIndex];
            BoardState simulatedBoard = boardState.ApplyPlay(randomMove.x, randomMove.y);
            switch (simulatedBoard.GetPlayerWin())
            {
                case "Player 1":
                    {
                        winner = true;
                        break;
                    }
                case "Player 2":
                    {
                        winner = false;
                        break;
                    }

                default:
                    {
                        winner = SimulatedPlay(simulatedBoard, limitSimulations - 1);
                        break;
                    }

            }
        }

        return winner;
    }

    // Función que almacena en una lista los movimientos válidos
    public static List<Vector2Int> GetValidMoves(BoardState boardState)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        for (int j = 0; j < boardState.rows.Count; j++)
        {
            for (int i = 0; i < boardState.rows[j].columnCells.Count; i++)
            {
                if (boardState.IsValidPlay(i, j, boardState))
                {
                    moves.Add(new Vector2Int(i, j));
                }
            }
        }
        return moves;
    }
}
