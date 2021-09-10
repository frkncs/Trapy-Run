using UnityEngine;

public class MoveScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] bool useAutoSpeed = false;
    [SerializeField] float autoSpeedControlDistance = 11;
    [SerializeField] bool useGlobalDirections = false;
    [SerializeField] bool moveForward = false;
    [SerializeField] bool moveHorizontal = false;
    [SerializeField] bool canTurn = false;
    [SerializeField] bool turnClockwise = false;
    [SerializeField] string turnCoordinate = "x";

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float turnSpeed = 0;

    GameObject player;

    Vector3 dir = Vector3.right;
    Vector3 rot = Vector3.zero;

    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        switch (turnCoordinate.ToLower().Trim())
        {
            case "x":
                rot = new Vector3((turnClockwise ? 1 : -1) * 360, 0, 0);
                break;
            case "y":
                rot = new Vector3(0, (turnClockwise ? 1 : -1) * 360, 0);
                break;
            case "z":
                rot = new Vector3(0, 0, (turnClockwise ? 1 : -1) * 360);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (!PlayerController.isGameStart) return;

        if (moveForward)
            MoveForward();
        else if (moveHorizontal)
            MoveHorizontal();

        if (canTurn)
            Turn();
    }

    void MoveForward()
    {
        if (useAutoSpeed)
        {
            if (player != null)
            {
                Vector3 playerPos = player.transform.position;

                float dist = playerPos.z - transform.position.z;

                if (dist >= autoSpeedControlDistance) moveSpeed = dist;
            }
        }

        if (!useGlobalDirections)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    void MoveHorizontal()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 5, LayerMask.GetMask("Ground")))
            dir *= -1;

        transform.Translate(dir * Time.deltaTime * moveSpeed);
    }

    void Turn()
    {
        transform.Rotate(rot * Time.deltaTime * turnSpeed);
    }
}
