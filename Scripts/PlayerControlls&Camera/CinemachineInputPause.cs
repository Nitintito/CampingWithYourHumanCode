using UnityEngine;
using Cinemachine;

public class CinemachineInputPause : MonoBehaviour
{
    public static bool isLocked;
    void Start () 
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            if (!isLocked)
                return Input.GetAxis("Mouse X");
            else
                return 0;
        }
        
        if (axisName == "Mouse Y")
        {
            if (!isLocked)
                return Input.GetAxis("Mouse Y");
            else
                return 0;
        }
        return Input.GetAxis(axisName);
    }
}
