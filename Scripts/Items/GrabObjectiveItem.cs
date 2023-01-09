using Cinemachine;
using TMPro;
using UnityEngine;

public class GrabObjectiveItem : MonoBehaviour
{
    private ObjectGrab _objectGrab;
    private GameObject _camera;
    private CinemachineFreeLook _cinemachineFreeLook;
    private HighLightCloest _highLightCloest;
    
    [HideInInspector]public GameObject objectiveItemMenu;
    [HideInInspector]public TextMeshProUGUI itemDescriptionText;

    public float distFromCam = 1.5f;
    public float rotateSpeed = 0.2f;

    private bool _rotate;

    private GameObject _tempObject;
    //private Shader _unlitShader;
    private Shader _litShader;


    private void Awake()
    {
        _objectGrab = GetComponent<ObjectGrab>();
        _camera = Camera.main.gameObject;
        _cinemachineFreeLook = _camera.transform.parent.GetComponentInChildren<CinemachineFreeLook>();
        _highLightCloest = GetComponent<HighLightCloest>();
        //_unlitShader = Shader.Find("Universal Render Pipeline/Unlit");
        _litShader = Shader.Find("Universal Render Pipeline/Lit");
    }

    private void Start()
    {
        ObjectGrab.pickup.AddListener(CheckIfObjectiveItem);
    }

    private void Update()
    {
        if (_rotate)
        {
            _tempObject.transform.RotateAround(_tempObject.transform.position, _camera.transform.up, rotateSpeed);
        }
    }

    void CheckIfObjectiveItem()
    {
        if (!_objectGrab.heldObject.CompareTag("ObjectiveItem")) return;

        ItemType item = _objectGrab.heldObject.GetComponent<ItemType>();

        if (item.itemHasBeenPickedUp) return; //Prevents item closeup & description window to show up more than once
        item.itemHasBeenPickedUp = true;
        
        objectiveItemMenu.SetActive(true);
        _cinemachineFreeLook.enabled = false;
        PauseMenu.canPauseGame = false;
        
        _tempObject = Instantiate(_objectGrab.heldObject, _camera.transform.position + (_camera.transform.forward * distFromCam), Quaternion.identity);
        
        _rotate = item.rotate;

        _tempObject.transform.LookAt(_camera.transform);

        Material newMaterial = new Material(_highLightCloest.originalMaterial);
        _tempObject.GetComponentInChildren<MeshRenderer>().material = newMaterial;
        //_tempObject.GetComponentInChildren<MeshRenderer>().material.shader = _unlitShader; // Does not work in build, only editor.
        
        itemDescriptionText.text = item.itemDescription;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    public void ReturnToGame()
    {
        _tempObject.GetComponentInChildren<MeshRenderer>().sharedMaterial.shader = _litShader;
        PauseMenu.canPauseGame = true;
        objectiveItemMenu.SetActive(false);
        _cinemachineFreeLook.enabled = true;
        Destroy(_tempObject);
        _rotate = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
