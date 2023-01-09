using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineFovChange : MonoBehaviour
{
    CinemachineFreeLook freeLookCamera;

    private void Awake()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (freeLookCamera.m_Lens.FieldOfView < 90)
                freeLookCamera.m_Lens.FieldOfView += 5;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (freeLookCamera.m_Lens.FieldOfView > 60)
            freeLookCamera.m_Lens.FieldOfView -= 5;
        }
    }
}
