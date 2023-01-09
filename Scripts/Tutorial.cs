using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using TMPro;

enum TutorialPart {GameInStartMenu, FindFireWood, FuelFire, Sniff, Dig, BurnObjectiveItem, GhostIntroduction, LastAdvice, TutorialBlank, TutorialDone};
public class Tutorial : MonoBehaviour
{
    private TutorialPart tutorialPart;
    TextMeshProUGUI text;
    private AI ai;

    private ObjectGrab objectGrab;
    private DetectFire detectFire;
    private PlayerMovement playerMovement;
    private Sniff sniff;
    
    bool burnt;

    [SerializeField] private GameObject ghost;
    [SerializeField] private CinemachineVirtualCamera tutorialCam;
    [SerializeField] private float timeWaitAfterStartMenu = 3f;
    [SerializeField] private FireMechanics startingFireBurnout;
    
    
    [SerializeField] private float timeUntilGhostGoesInAttackMode = 8f;
    [SerializeField] private float timeAfterGhostIsRepelled = 4f;
    [SerializeField] private float timeWaitLastTutorialText = 10f;
    [SerializeField] private GameObject firstDigZone;
    [SerializeField] private GameObject CampFireGroup;
    [SerializeField] private GameObject tutorialColliders;
    [SerializeField] private GameObject returnToCampText;
    [SerializeField] private GameObject playerHelpText;
    
    public static bool tutorialText = false;
    
    
    private FireMechanics[] _fires;

    [Header("Texts that appears in tutorial")][TextArea(1,10)] [SerializeField]
    private string findFireWood;
    [TextArea(1,10)][SerializeField] private string fuelFire, sniffText, yellowZoneText, dig, burnObjItem, ghostIntro, lastAdvice;

    private void Awake()
    {
        tutorialPart = TutorialPart.GameInStartMenu;
        text = GetComponentInChildren<TextMeshProUGUI>();
        objectGrab = FindObjectOfType<ObjectGrab>();
        detectFire = FindObjectOfType<DetectFire>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        sniff = FindObjectOfType<Sniff>();

        _fires = CampFireGroup.GetComponentsInChildren<FireMechanics>();
        Debug.Log("amount of fires = " + _fires.Length);

        ai = ghost.GetComponent<AI>();
        ghost.SetActive(false);
    }
    private void Start()
    {
        DetectFire.burnObjectiveItem.AddListener(ObjectivItemBurnt);
    }

    private void Update()
    {
        if (tutorialText)
        {
            returnToCampText.SetActive(true);
        }
        else returnToCampText.SetActive(false);

        if (PauseMenu.gameIsPaused) text.enabled = false;
        else text.enabled = true;
        
        switch (tutorialPart)
        {
            case TutorialPart.GameInStartMenu:
                InStartMenu();
                break;
            
            case TutorialPart.FindFireWood:
                FindFireWood();
                break;

            case TutorialPart.FuelFire:
                FuelFire();
                break;

            case TutorialPart.Sniff:
                Sniff();
                break;

            case TutorialPart.Dig:
                Dig();
                break;

            case TutorialPart.BurnObjectiveItem:
                BurnObjectiveItem();
                break;

            case TutorialPart.GhostIntroduction:
                GhostIntroduction();
                break;
            
            case TutorialPart.LastAdvice:
                LastAdvice();
                break;
            
            case TutorialPart.TutorialBlank:
                TutorialBlank();
                break;
            
            case TutorialPart.TutorialDone:
                TutorialDone();
                break;

            default:
                break;
        }
    }

    private void InStartMenu()
    {
        if (StartMenu.gameHasStarted && tutorialPart == TutorialPart.GameInStartMenu)
        {
            tutorialPart = TutorialPart.TutorialBlank;
            PlayerMovement.disableControls = true;
            StartCoroutine(WaitForCameraPanAtStart());
        }
    }

    IEnumerator WaitForCameraPanAtStart()
    {
        CinemachineInputPause.isLocked = true;
        startingFireBurnout.QuickBurnoutOnStart(timeWaitAfterStartMenu);
        yield return new WaitForSeconds(timeWaitAfterStartMenu);
        tutorialPart = TutorialPart.FindFireWood;
        PlayerMovement.disableControls = false;
        CinemachineInputPause.isLocked = false;
    }

    private void FindFireWood()
    { 
        text.text = findFireWood;
        //Approaching firewood for the first time causes the text “Press (PICKUP) to pick up items”.
        if (objectGrab.heldObject != null && tutorialPart == TutorialPart.FindFireWood)
        {
            tutorialPart = TutorialPart.FuelFire;
        }
    }

    private void FuelFire()
    {
        text.text = fuelFire;
        if (detectFire.firstTimeRefuling && tutorialPart == TutorialPart.FuelFire)
        {
            tutorialPart = TutorialPart.Sniff;
        }
        //Bringing the firewood back to camp causes the text “Press (PICKUP) again to drop the held item.”
        //Dropping the firewood in the near vicinity of the fire causes it to disappear and the fire to grow bigger.
    }

    private void Sniff()
    {
        firstDigZone.SetActive(true);
        if (sniff.sniffableObject != null)
        {
            if (sniff.sniffableObject._curRangeState == ItemDigZone.RangeStates.Red && tutorialPart == TutorialPart.Sniff)
            {
                text.text = sniffText;
            }
            
            if (sniff.sniffableObject._curRangeState == ItemDigZone.RangeStates.Yellow && tutorialPart == TutorialPart.Sniff)
            {
                text.text = yellowZoneText;
            }

            if (sniff.sniffableObject._curRangeState == ItemDigZone.RangeStates.Green && tutorialPart == TutorialPart.Sniff)
            {
                tutorialPart = TutorialPart.Dig;
            }
        }
    }
    private void Dig()
    {
        /*Image of sniff UI in red) Text: “Somethings nearby”
        (Image of sniff UI in yellow) Text: “It’s close”
        (Image of sniff UI in green) Text: “You’ve found something! Press (DIG) to dig it up.”
        Digging causes an (OBJECTIVE ITEM) to appear. (PICKUP) Picks up item, text after inspection:*/ 
         
        text.text = dig;

        if (playerMovement.IsDigging() && tutorialPart == TutorialPart.Dig)
        {
            tutorialPart = TutorialPart.BurnObjectiveItem;
        }
    }

    private void BurnObjectiveItem()
    {
        /*
         * When taken near a fire a text prompt appears:
            “Press (PICKUP) to throw in fire”*/

        text.text = burnObjItem;
        if (burnt && tutorialPart == TutorialPart.BurnObjectiveItem)
        {
            tutorialPart = TutorialPart.GhostIntroduction;
        }
    }

    private void GhostIntroduction()
    {
        text.text = ghostIntro;
        tutorialText = false;
        tutorialColliders.SetActive(false);
         
        if (tutorialPart == TutorialPart.GhostIntroduction)
        {
            // ghost spawn pan camera to ghost

            for (int i = 0; i < _fires.Length; i++)
            {
                _fires[i].FireUp(_fires[i].Id); // FUEL ALL FIRES DONT WORK
            }
            
            ghost.SetActive(true);
            CinemachineInputPause.isLocked = true;
            PlayerMovement.disableControls = true;

            tutorialPart = TutorialPart.TutorialBlank;
            StartCoroutine(ActivateGhost());
        }
    }
    IEnumerator ActivateGhost()
    {
        tutorialCam.Priority = 100;
        yield return new WaitForSeconds(timeUntilGhostGoesInAttackMode);
        ai.ifCalledByTutorial = true;
        yield return new WaitForSeconds(0.1f);
        ai.ifCalledByTutorial = false;

        yield return new WaitUntil(CheckState);
        yield return new WaitForSeconds(timeAfterGhostIsRepelled);
        
        tutorialCam.Priority = -10;
        CinemachineInputPause.isLocked = false;
        PlayerMovement.disableControls = false;
        tutorialPart = TutorialPart.LastAdvice;

    }

    private void LastAdvice()
    {
        text.text = lastAdvice;
        timeWaitLastTutorialText -= Time.deltaTime;

        if (timeWaitLastTutorialText < 0)
        {
            playerHelpText.SetActive(false);
            tutorialPart = TutorialPart.TutorialDone;
        }
    }
    
    private void TutorialBlank()
    {
        text.text = "";
    }
    
    private void TutorialDone()
    {
        text.text = "";
    }


    bool CheckState()
    {
        return string.Equals(ai.GetCurrentState(), "goback");
    }

    void ObjectivItemBurnt()
    {
        burnt = true;
    }
}
