using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour
{
    private Sniff _sniff;
    [SerializeField] private Image sniffColor;
    [SerializeField] private Image rightMouseImage;
    [SerializeField] private Image leftMouseImage;

    private DetectFire _detectFire;
    [SerializeField] private GameObject burnButtonImage;

    [SerializeField] private ParticleSystem sniffUIParticleSystem;
    private SniffUIeffect sniffUIeffect;

    private GrabObjectiveItem _grabObjectiveItem;
    [SerializeField] private GameObject objectiveItemMenu;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    [SerializeField] private GameObject ingameUi;
    [SerializeField] private GameObject returnToLevelText;
    public static bool returnToLevelTextActive;

    private void Awake()
    {
        _sniff = Camera.main.transform.parent.GetComponentInChildren<Sniff>();
        _sniff.sniffColor = sniffColor;
        _sniff.leftMouseImage = leftMouseImage;
        _sniff.rightMouseImage = rightMouseImage;
        _sniff.particleSystem = sniffUIParticleSystem;

        _detectFire = Camera.main.transform.parent.GetComponentInChildren<DetectFire>();
        _detectFire.burnButtonImage = burnButtonImage;
        
        sniffUIeffect = GetComponentInChildren<SniffUIeffect>();
        sniffUIeffect.sniff = _sniff;

        _grabObjectiveItem = Camera.main.transform.parent.GetComponentInChildren<GrabObjectiveItem>();
        _grabObjectiveItem.objectiveItemMenu = objectiveItemMenu;
        _grabObjectiveItem.itemDescriptionText = itemDescriptionText;
    }

    private void Update()
    {
        if (returnToLevelTextActive)
        {
            returnToLevelText.SetActive(true);
        }
        else
        {
            returnToLevelText.SetActive(false);
        }
    }

    private void Start()
    {
        ingameUi.SetActive(false);
    }

    public void ReturnToGame()
    {
        _grabObjectiveItem.ReturnToGame();
    }
}
