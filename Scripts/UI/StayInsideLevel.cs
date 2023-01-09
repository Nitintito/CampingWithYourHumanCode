using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInsideLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UImanager.returnToLevelTextActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UImanager.returnToLevelTextActive = false;
        }
    }
}
