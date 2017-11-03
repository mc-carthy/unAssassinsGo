using UnityEngine;

public class Link : MonoBehaviour 
{
    public float boarderWidth = 0.02f;
    public float lineThickness = 0.5f;
    public float scaleTime = 0.25f;
    public float delay = 0.1f;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    public void DrawLink(Vector3 start, Vector3 end)
    {
        transform.localScale = new Vector3(lineThickness, 1f, 0);
        Vector3 dir = end - start;
        float zScale = dir.magnitude - (boarderWidth * 2);

        Vector3 newScale = new Vector3(lineThickness, 1f, zScale);
        transform.rotation = Quaternion.LookRotation(dir);
        transform.position = start + (transform.forward * boarderWidth);

        iTween.ScaleTo(gameObject, iTween.Hash(
            "time", scaleTime,
            "scale", newScale,
            "easetype", easeType,
            "delay", delay
        ));
    }
}
