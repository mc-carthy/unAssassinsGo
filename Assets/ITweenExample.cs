using UnityEngine;

public class ITweenExample : MonoBehaviour 
{

    private void Start()
    {
        iTween.RotateTo(gameObject, iTween.Hash(
            "y", 90f,
            "delay", 1f,
            "time", 2f,
            "easetype", iTween.EaseType.easeInBounce
        ));
    }

}
