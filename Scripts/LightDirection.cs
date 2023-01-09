using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightDirection : MonoBehaviour
{      
    void Update()
    {
        Shader.SetGlobalVector("_SunDirection", transform.forward);
        Shader.SetGlobalVector("_SunMoonColor", new Color(0.1f, 0f, 0.2f));
    }
}
