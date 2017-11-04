using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public Vector3 destination;
    public bool isMoving = false;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    public float moveSpeed = 1.5f;
    public float iTweenDelay = 0f;

    private Board board;

    private void Awake()
    {
        board = GameObject.FindObjectOfType<Board>().GetComponent<Board>();
    }

    private void Start()
    {
        UpdateBoard();
    }

    public void MoveLeft()
    {
        Vector3 newPos = transform.position + new Vector3(-Board.spacing, 0f, 0f);
        Move(newPos, 0);
    }

    public void MoveRight()
    {
        Vector3 newPos = transform.position + new Vector3(Board.spacing, 0f, 0f);
        Move(newPos, 0);
    }

    public void MoveForward()
    {
        Vector3 newPos = transform.position + new Vector3(0f, 0f, Board.spacing);
        Move(newPos, 0);
    }

    public void MoveBackward()
    {
        Vector3 newPos = transform.position + new Vector3(0f, 0f, -Board.spacing);
        Move(newPos, 0);
    }

    public void Move(Vector3 pos, float delay = 0.25f)
    {
        if (board != null)
        {
            Node targetNode = board.FindNodeAt(pos);
            if (targetNode != null && board.PlayerNode.LinkedNodes.Contains(targetNode))
            {
                StartCoroutine(MoveRoutine(pos, delay));
            }
        }
    }

    private IEnumerator MoveRoutine(Vector3 pos, float delay = 0.25f)
    {
        isMoving = true;
        destination = pos;

        yield return new WaitForSeconds(delay);

        iTween.MoveTo(gameObject, iTween.Hash(
            "x", pos.x,
            "y", pos.y,
            "z", pos.z,
            "delay", iTweenDelay,
            "easetype", easeType,
            "speed", moveSpeed
        ));

        while(Vector3.Distance(pos, transform.position) > 0.01f)
        {
            yield return null;
        }

        iTween.Stop(gameObject);
        transform.position = pos;
        isMoving = false;
        UpdateBoard();
    }

    private void UpdateBoard()
    {
        if (board != null)
        {
            board.UpdatePlayerNode();
        }
    }
}
