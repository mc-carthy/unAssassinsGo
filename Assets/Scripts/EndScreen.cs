using UnityEngine;
using UnityEngine.PostProcessing;

public class EndScreen : MonoBehaviour 
{
    public PostProcessingProfile defaultProfile;
    public PostProcessingProfile blurProfile;
    public PostProcessingBehaviour cameraPostProcess;

    public void EnableCameraBlur(bool state)
    {
        if (cameraPostProcess != null && defaultProfile != null && blurProfile != null)
        {
            cameraPostProcess.profile = state ? blurProfile : defaultProfile;
        }
    }

}
