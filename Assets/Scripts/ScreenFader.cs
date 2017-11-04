using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour 
{
    public Color solidColour = Color.white;
    public Color clearColour = new Color(1f, 1f, 1f, 0f);

    public float delay = 0.5f;
    public float timeToFade = 1f;
    public iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    private MaskableGraphic graphic;

    private void Awake()
    {
        graphic = GetComponent<MaskableGraphic>();
    }

    public void FadeOff()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", solidColour,
            "to", clearColour,
            "time", timeToFade,
            "delay", delay,
            "easetype", easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateColour"
        ));
    }

    public void FadeOn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", clearColour,
            "to", solidColour,
            "time", timeToFade,
            "delay", delay,
            "easetype", easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateColour"
        ));
    }


    private void UpdateColour(Color newColour)
    {
        graphic.color = newColour;
    }
}
