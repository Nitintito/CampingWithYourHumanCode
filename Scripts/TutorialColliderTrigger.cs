using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialColliderTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tutorial.tutorialText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tutorial.tutorialText = false;
        }
    }
}
