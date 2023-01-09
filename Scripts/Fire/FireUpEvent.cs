using System;
using UnityEngine;

public class FireUpEvent : MonoBehaviour
{
    public event Action onFireUp;
    public void FireUp()
    {
        if (onFireUp != null)
        {
            onFireUp();
        }
    }
}
