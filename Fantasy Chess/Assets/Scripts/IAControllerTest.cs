using UnityEngine;

public class IAControllerTest : MonoBehaviour
{

    private static IAControllerTest instance;

    //private const int MonteCarloIterations = 100;

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

    // public static void MonteCarloAlphaBeta(BoardState boardState, int profundidad, out Vector2Int? bestPlay)
    // {
    //     Dictionary<Vector2Int, float> playScores = new Dictionary<Vector2Int, float>();
    //     bestPlay = null;
    //     float bestScore = float.MinValue;

    //     foreach (var move in GetValidMoves(boardState))
    //     {
    //         float totalScore = 0;
    //         for (int i = 0; i < MonteCarloIterations; i++)
    //         {
    //             BoardState simulatedBoard = boardState.ApplyPlay(move.x, move.y);
    //             totalScore += AlfaBeta(simulatedBoard, profundidad, float.MinValue, float.MaxValue);
    //         }
    //         float avgScore = totalScore / MonteCarloIterations;
    //         playScores[move] = avgScore;

    //         if (avgScore > bestScore)
    //         {
    //             bestScore = avgScore;
    //             bestPlay = move;
    //         }
    //     }
    // }

    // public static List<Vector2Int> GetValidMoves(BoardState boardState)
    // {
    //     List<Vector2Int> moves = new List<Vector2Int>();
    //     for (int j = 0; j < boardState.rows.Count; j++)
    //     {
    //         for (int i = 0; i < boardState.rows[j].columnCells.Count; i++)
    //         {
    //             if (boardState.IsValidPlay(i, j, boardState))
    //             {
    //                 moves.Add(new Vector2Int(i, j));
    //             }
    //         }
    //     }
    //     return moves;
    // }
}
