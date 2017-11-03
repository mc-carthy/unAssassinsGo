using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public Vector3 destination;
    public bool isMoving = false;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    public float moveSpeed = 1.5f;
    public float iTweenDelay = 0f;

    private void Start()
    {
        Move(new Vector3(2f, 0f, 0f), 1f);
        Move(new Vector3(2f, 0f, 2f), 3f);
        Move(new Vector3(4f, 0f, 2f), 5f);
        Move(new Vector3(4f, 0f, 4f), 7f);
    }

    public void Move(Vector3 pos, float delay = 0.25f)
    {
        StartCoroutine(MoveRoutine(pos, delay));
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
    }
}
