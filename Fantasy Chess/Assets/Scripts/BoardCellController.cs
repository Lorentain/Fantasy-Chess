using UnityEngine;

public class BoardCellController : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        BoardController.UpdateCell((int)transform.position.x,(int)transform.position.y);
    }
}
