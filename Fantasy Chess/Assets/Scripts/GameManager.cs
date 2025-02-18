using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    [SerializeField] private int turn;

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
        turn = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (turn == 4)
            {
                turn = 1;
            }
            else
            {
                turn++;
            }
        }
    }
}
