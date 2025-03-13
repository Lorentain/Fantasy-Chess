using UnityEngine;

public class BoardCellController : MonoBehaviour
{
    //[SerializeField] private CharacterType characterType;

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        if (BoardController.GetPlayerTurn() == 1 || BoardController.GetPlayerTurn() == 2)
        {
            BoardController.GetChooseCellPlayer((int)transform.position.x, -(int)transform.position.y);
        }
        else if (BoardController.GetPlayerTurn() == 3 || BoardController.GetPlayerTurn() == 4)
        {
            BoardController.GetInstanceOrMove((int)transform.position.x, -(int)transform.position.y);
            BoardController.BoardRepresentation();
        }
    }
}
