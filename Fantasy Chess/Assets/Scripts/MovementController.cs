using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gameObject.transform.position.x < 8.5)
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gameObject.transform.position.x > -8.5)
            {
                gameObject.transform.position += new Vector3(-1, 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gameObject.transform.position.y < 4.5)
            {
                gameObject.transform.position += new Vector3(0, 1, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gameObject.transform.position.y > -4.5)
            {
                gameObject.transform.position += new Vector3(0, -1, 0);
            }
        }
    }
}
