using UnityEngine;

public class Spinner : MonoBehaviour 
{
    public float rotSpeed = 20f;

    private void Start()
    {
        iTween.RotateBy(gameObject, iTween.Hash(
            "y", 360f,
            "looptype", iTween.LoopType.loop,
            "speed", rotSpeed,
            "easetype", iTween.EaseType.linear
        ));
    }
}
