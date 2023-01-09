using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_changer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Endingtext;
    [SerializeField] private GameObject CreditsAnimation;
    [SerializeField] private GameObject Flower;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private float fadeTime = 3;

    IEnumerator Credits()
    {
        float waitTime = 0;
        
        while (waitTime < 1)
        {
            Endingtext.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.clear, Color.white, waitTime));
            waitTime += Time.deltaTime / fadeTime;
            yield return null;
        } 
        waitTime = 0;
        
        yield return new WaitForSeconds(5);
        while (waitTime < 1)
        {
            Endingtext.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.white, Color.clear, waitTime));
            waitTime += Time.deltaTime / fadeTime;
            yield return null;
        } 
        CreditsAnimation.SetActive(true);
        yield return new WaitForSeconds(30);
        Flower.SetActive(true);
        Buttons.SetActive(true);
        CreditsAnimation.SetActive(false);
    }


    void Start()
    {
        StartCoroutine(Credits());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
