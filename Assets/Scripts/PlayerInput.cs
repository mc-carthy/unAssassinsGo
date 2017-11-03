using UnityEngine;

public class PlayerInput : MonoBehaviour 
{
    private float h;
    public float H { get { return h; }}

    private float v;
    public float V { get { return v; }}

    private bool inputEnabled = false;
    public bool InputEnabled 
    { 
        get { return inputEnabled; } 
        set { inputEnabled = value; }
    }

    public void GetKeyInput()
    {
        if (inputEnabled)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
    }
}
